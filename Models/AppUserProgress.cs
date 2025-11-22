using SkillUpPlus.Utils;

namespace SkillUpPlus.Models
{
    public class AppUserProgress
    {
        public int Id { get; set; }
        public int ProgressPercentage { get; set; }
        public Status Status { get; set; }
        public int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
        public int LearningPathId { get; set; }
        public required LearningPath LearningPath { get; set; }

    }
}
