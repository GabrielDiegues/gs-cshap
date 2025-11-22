using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUpPlus.Data;
using SkillUpPlus.Dtos;
using SkillUpPlus.Models;
using SkillUpPlus.Utils;

namespace SkillUpPlus.Controllers
{  
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppUserController(SkillUpPlusDbContext context) : ControllerBase
    {
        private readonly SkillUpPlusDbContext _context = context;

        // Controllers

        // Get requests
        [Authorize]
        [HttpGet("{appUserId}")]
        public async Task<ActionResult<AppUserDto>> GetAppUser(string appUserId)
        {
            // Validate ID
            if (!int.TryParse(appUserId, out int id))
                return BadRequest("Invalid user ID.");

            // Query only what is necessary for the DTO
            var userDto = await _context.AppUsers
                .Where(u => u.Id == id)
                .Select(u => new AppUserDto(
                    u.Id,
                    u.Name,
                    u.Email,
                    u.ProfilePhotoUrl
                ))
                .FirstOrDefaultAsync();

            if (userDto == null)
                return NotFound("User not found.");

            return Ok(userDto);
        }



        // Post requests
        [HttpPost]
        public async Task<ActionResult<AppUser>> CreateAppUser(AppUser newUser)
        {
            if (newUser == null)
                return BadRequest("Invalid user data.");

            var emailExists = await _context.AppUsers
                .AnyAsync(u => u.Email == newUser.Email);

            if (emailExists)
                return Conflict("Email is already registered.");

            newUser.Password = PasswordUtils.HashPassword(newUser.Password);

            _context.AppUsers.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppUser), new { appUserId = newUser.Id }, newUser);
        }
    }
}
