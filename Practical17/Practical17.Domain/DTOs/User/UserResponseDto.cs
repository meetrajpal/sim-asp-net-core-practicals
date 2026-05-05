namespace Practical17.Domain.DTOs.User;

public class UserResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new();

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
