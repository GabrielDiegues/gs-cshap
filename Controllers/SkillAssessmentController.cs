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
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SkillAssessmentController(SkillUpPlusDbContext context) : ControllerBase
    {
        private readonly SkillUpPlusDbContext _context = context;

        // Controllers

        // Get controllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillAssessmentItemDto>>> GetSkillAssessmentItems(string appUserId)
        {
            if (!int.TryParse(appUserId, out int userId))
                return BadRequest("Invalid AppUserId.");

            var assessment = await _context.SkillAssessments
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.AppUserId == userId);

            if (assessment == null)
                return NotFound("No skill assessment found for this user.");

            var itemsDto = assessment.Items.Select(item => new SkillAssessmentItemDto(
                item.Id,
                item.Name,
                item.SkillCategory.ToString(),
                item.Rating
            ));

            return Ok(itemsDto);
        }


        // Post controllers
        [HttpPost]
        public async Task<ActionResult<SkillAssessmentItemDto>> CreateSkillAssessmentItem(
            SkillAssessmentItemCreateDto dto)
        {
            var assessment = await _context.SkillAssessments
                .FirstOrDefaultAsync(a => a.Id == dto.SkillAssessmentId);

            if (assessment == null)
                return NotFound("SkillAssessment not found.");

            if (!Enum.TryParse<SkillCategories>(dto.SkillCategory, true, out var category))
                return BadRequest("Invalid SkillCategory.");

            var newItem = new SkillAssessmentItem
            {
                Name = dto.Name,
                SkillCategory = category,
                Rating = dto.Rating,
                SkillAssessmentId = dto.SkillAssessmentId,
                SkillAssessment = assessment
            };

            _context.SkillAssessmentItems.Add(newItem);
            await _context.SaveChangesAsync();

            var itemDto = new SkillAssessmentItemDto(
                newItem.Id,
                newItem.Name,
                newItem.SkillCategory.ToString(),
                newItem.Rating
            );

            return CreatedAtAction(nameof(GetSkillAssessmentItems), new { appUserId = assessment.AppUserId }, itemDto);
        }


        // Delete controllers
        [HttpDelete]
        public async Task<ActionResult> DeleteSkillAssessment(string userId)
        {
            if (!int.TryParse(userId, out int appUserId))
                return BadRequest("Invalid UserId.");

            var assessment = await _context.SkillAssessments
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.AppUserId == appUserId);

            if (assessment == null)
                return NotFound("Skill assessment not found for this user.");

            _context.SkillAssessmentItems.RemoveRange(assessment.Items);

            _context.SkillAssessments.Remove(assessment);

            await _context.SaveChangesAsync();

            return Ok("Skill assessment deleted successfully.");
        }


    }
}
