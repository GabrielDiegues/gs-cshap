using SkillUpPlus.Utils;

namespace SkillUpPlus.Dtos
{
    public record AppUserProgressDto(
        int Id,
        int ProgressPercentage,
        Status Status,
        int AppUserId,
        int LearningPathId
        );
}
