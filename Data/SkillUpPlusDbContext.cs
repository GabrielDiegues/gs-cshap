using Microsoft.EntityFrameworkCore;
using SkillUpPlus.Models;
using SkillUpPlus.Utils;

namespace SkillUpPlus.Data
{
    public class SkillUpPlusDbContext(DbContextOptions<SkillUpPlusDbContext> options) : DbContext(options)
    {
        // Tables
        public DbSet<ApiUser> ApiUsers => Set<ApiUser>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<AppUserInterest> AppUserInterests => Set<AppUserInterest>();
        public DbSet<AppUserProgress> AppUserProgresses => Set<AppUserProgress>();
        public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
        public DbSet<AppUserRecommendation> AppUserRecommendations => Set<AppUserRecommendation>();
        public DbSet<Achievement> Achievements => Set<Achievement>();
        public DbSet<AppUserAchievement> AppUserAchievements => Set<AppUserAchievement>();
        public DbSet<SkillAssessment> SkillAssessments => Set<SkillAssessment>();
        public DbSet<SkillAssessmentItem> SkillAssessmentItems => Set<SkillAssessmentItem>();




        // Altering the data base
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserInterest>()
                .Property(ui => ui.Interest)
                .HasConversion<string>();  // ⭐ This makes EF store the enum as string

            modelBuilder.Entity<LearningPath>()
                .Property(ui => ui.Category)
                .HasConversion<string>(); // ⭐ This makes EF store the enum as string

            modelBuilder.Entity<LearningPath>()
                .Property(ui => ui.DificultyLevel)
                .HasConversion<string>(); // ⭐ This makes EF store the enum as string

            modelBuilder.Entity<AppUserProgress>()
                .Property(ui => ui.Status)
                .HasConversion<string>(); // ⭐ This makes EF store the enum as string

            modelBuilder.Entity<SkillAssessmentItem>()
                .Property(ui => ui.SkillCategory)
                .HasConversion<string>(); // ⭐ This makes EF store the enum as string
        }

    }
}
