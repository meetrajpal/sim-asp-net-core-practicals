namespace Practical20.Domain.Entities;

public class User : BaseEntity, IAuditEnitity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty!;
    public string? UpdatedBy { get; set; }
}