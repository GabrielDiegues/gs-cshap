using SkillUpPlus.Utils;

namespace SkillUpPlus.Models
{
    public class LearningPath
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public Categories Category { get; set; }
        public DificultyLevels DificultyLevel { get; set; }
        public required string ImageUrl { get; set; }


    }
}
