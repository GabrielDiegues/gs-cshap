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
    public class AppUserProgressController(SkillUpPlusDbContext context) : ControllerBase
    {
        private readonly SkillUpPlusDbContext _context = context;

        // Controllers

        // Get controllers
        [HttpGet]
        public async Task<ActionResult<AppUserProgressDto>> GetAppUserProgress(string appUserId, string learningPathId)
        {
            // Validate and convert AppUserId
            if (!int.TryParse(appUserId, out int userId))
                return BadRequest("Invalid AppUserId.");

            // Validate and convert LearningPathId
            if (!int.TryParse(learningPathId, out int lpId))
                return BadRequest("Invalid LearningPathId.");

            // Query WITHOUT navigation properties
            var userProgress = await _context.AppUserProgresses
                .Where(p => p.AppUserId == userId && p.LearningPathId == lpId)
                .Select(p => new AppUserProgressDto(
                    p.Id,
                    p.ProgressPercentage,
                    p.Status,
                    p.AppUserId,
                    p.LearningPathId
                ))
                .FirstOrDefaultAsync();

            if (userProgress == null)
                return NotFound("No progress found for the given user and learning path.");

            return Ok(userProgress);
        }


        // Post controllers
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AppUserProgress>>> GetFilteredUserProgresses(
            string userId,
            List<LearningPath> learningPaths)
        {
            if (!int.TryParse(userId, out int appUserId))
                return BadRequest("Invalid user ID.");

            if (learningPaths == null || learningPaths.Count == 0)
                return BadRequest("Learning path list cannot be empty.");

            var results = new List<AppUserProgress>();

            foreach (var path in learningPaths)
            {
                var existingProgress = await _context.AppUserProgresses
                    .Include(p => p.LearningPath)
                    .Include(p => p.AppUser)
                    .FirstOrDefaultAsync(p =>
                        p.AppUserId == appUserId &&
                        p.LearningPathId == path.Id);

                if (existingProgress != null)
                {
                    results.Add(existingProgress);
                    continue;
                }


                var newProgress = new AppUserProgress
                {
                    ProgressPercentage = 0,
                    Status = Status.NotStarted,
                    AppUserId = appUserId,
                    LearningPathId = path.Id,
                    AppUser = await _context.AppUsers.FirstAsync(u => u.Id == appUserId),
                    LearningPath = await _context.LearningPaths.FirstAsync(lp => lp.Id == path.Id)
                };

                _context.AppUserProgresses.Add(newProgress);
                await _context.SaveChangesAsync();

                results.Add(newProgress);
            }

            return Ok(results);
        }


        // Put controllers
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(
            string appUserId,
            string learningPathId,
            Status newStatus)
        {
            if (!int.TryParse(appUserId, out int userId))
                return BadRequest("Invalid AppUserId.");

            if (!int.TryParse(learningPathId, out int lpId))
                return BadRequest("Invalid LearningPathId.");

            var progress = await _context.AppUserProgresses
                .FirstOrDefaultAsync(p =>
                    p.AppUserId == userId &&
                    p.LearningPathId == lpId);

            if (progress == null)
                return NotFound("User progress not found.");

            progress.Status = newStatus;


            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Status updated successfully.",
                newStatus = progress.Status.ToString()
            });
        }


        [HttpPut("update-percentage")]
        public async Task<IActionResult> UpdateProgressPercentage(
            string appUserId,
            string learningPathId,
            int newProgressPercentage)
        {
            if (!int.TryParse(appUserId, out int userId))
                return BadRequest("Invalid AppUserId.");

            if (!int.TryParse(learningPathId, out int lpId))
                return BadRequest("Invalid LearningPathId.");

            if (newProgressPercentage < 0 || newProgressPercentage > 100)
                return BadRequest("Progress percentage must be between 0 and 100.");

            var progress = await _context.AppUserProgresses
                .FirstOrDefaultAsync(p =>
                    p.AppUserId == userId &&
                    p.LearningPathId == lpId);

            if (progress == null)
                return NotFound("No progress found for the given user and learning path.");

            progress.ProgressPercentage = newProgressPercentage;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Progress percentage updated successfully.",
                newPercentage = progress.ProgressPercentage
            });
        }



    }
}
