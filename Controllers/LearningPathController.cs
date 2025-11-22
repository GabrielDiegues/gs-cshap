using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUpPlus.Data;
using SkillUpPlus.Models;

namespace SkillUpPlus.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LearningPathController(SkillUpPlusDbContext context) : ControllerBase
    {
        private readonly SkillUpPlusDbContext _context = context;

        // Controllers

        // Get controllers
        [HttpGet("{learningPathId}")]
        public async Task<ActionResult<LearningPath>> GetLearningPath(string learningPathId)
        {
            if (!int.TryParse(learningPathId, out int id))
                return BadRequest("Invalid learning path ID.");

            var learningPath = await _context.LearningPaths
                .FirstOrDefaultAsync(lp => lp.Id == id);

            if (learningPath == null)
                return NotFound("Learning path not found.");

            return Ok(learningPath);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LearningPath>>> GetAllLearningPaths()
        {
            var learningPaths = await _context.LearningPaths.ToListAsync();

            return Ok(learningPaths);
        }
    }
}
