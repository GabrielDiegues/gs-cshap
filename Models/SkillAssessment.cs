namespace SkillUpPlus.Models
{
    public class SkillAssessment
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public ICollection<SkillAssessmentItem> Items { get; set; }

    }
}
