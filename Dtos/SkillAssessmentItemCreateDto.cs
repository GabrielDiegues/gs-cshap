namespace SkillUpPlus.Dtos
{
    public record SkillAssessmentItemCreateDto(
        string Name,
        string SkillCategory,
        int Rating,
        int SkillAssessmentId
        );
}
