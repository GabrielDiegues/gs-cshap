namespace SkillUpPlus.Models
{
    public class AppUserRecommendation
    {
        public int Id { get; set; }
        public required string Reason { get; set; }
        public int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
        public int LearningPathId { get; set; }
        public required LearningPath LearningPath { get; set; }
    }
}
