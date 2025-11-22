namespace SkillUpPlus.Dtos
{
    public record AppUserDto(
        int Id,
        string Name,
        string Email,
        string? ProfilePhotoUrl
        );
}
