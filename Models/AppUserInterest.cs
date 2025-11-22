using SkillUpPlus.Utils;

namespace SkillUpPlus.Models
{
    public class AppUserInterest
    {
        public int Id { get; set; }
        public Interests Interest { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
