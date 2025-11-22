using SkillUpPlus.Utils;

namespace SkillUpPlus.Models
{
    public class SkillAssessmentItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public SkillCategories SkillCategory { get; set; }
        public int Rating { get; set; }
        public int SkillAssessmentId { get; set; }
        public required SkillAssessment SkillAssessment { get; set; }
    }
}
