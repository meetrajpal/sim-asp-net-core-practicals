namespace Practical20.Domain.Entities;

public class Role : BaseEntity, IAuditEnitity
{
    public string RoleName { get; set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public string CreatedBy { get; set; } = string.Empty!;
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
