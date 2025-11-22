namespace SkillUpPlus.Models
{
    public class AppUserAchievement
    {
        public int Id { get; set; }
        public DateOnly EarnedAt { get; set; }
        public int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
        public int AchievementId { get; set; }
        public required Achievement Achievement { get; set; }

    }
}
