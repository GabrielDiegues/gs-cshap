using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUpPlus.Data;
using SkillUpPlus.Dtos;
using SkillUpPlus.Services;
using SkillUpPlus.Utils;

namespace SkillUpPlus.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiUserController(SkillUpPlusDbContext context, JwtService jwtService) : ControllerBase
    {
        private readonly SkillUpPlusDbContext _context = context;
        private readonly JwtService _jwtService = jwtService;

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var user = await _context.ApiUsers
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !PasswordUtils.VerifyPassword(dto.Password, user.Password))
                return Unauthorized("Invalid credentials.");


            string token = _jwtService.GenerateToken(user.Id, user.Email, user.Name);

            return Ok(new { token });
        }

    }
}
