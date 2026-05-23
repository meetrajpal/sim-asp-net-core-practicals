namespace Practical19.Domain.Models.Entities;

public class AppUser : IdentityUser<Guid>, IBaseEntity
{

    [DisplayName("Active")]
    public bool IsActive { get; set; }

    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Student> StudentsCreated { get; set; } = [];
    public ICollection<Student> StudentsUpdated { get; set; } = [];
}
