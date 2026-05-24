namespace Practical20.Domain.DTOs.User;

public class UserResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
