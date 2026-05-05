namespace Practical18.Domain.Models.Entities;

public class Role : BaseEntity
{
    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = [];
}
