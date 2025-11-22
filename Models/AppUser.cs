using SkillUpPlus.Utils;

namespace SkillUpPlus.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public ICollection<AppUserInterest> Interests { get; set; } = new List<AppUserInterest>();
        public ICollection<AppUserProgress> AppUserProgresses { get; set; } = new List<AppUserProgress>();
        public ICollection<AppUserRecommendation> Recommendations { get; set; } = new List<AppUserRecommendation>();
        public ICollection<AppUserAchievement> AppUserAchievements { get; set; } = new List<AppUserAchievement>();
        public SkillAssessment? SkillAssessment { get; set; }

    }
}
