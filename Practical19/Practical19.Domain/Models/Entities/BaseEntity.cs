namespace Practical19.Domain.Models.Entities;

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }

    [DisplayName("Active")]
    public bool IsActive { get; set; }

    [ScaffoldColumn(false)]
    public byte[]? RowVersion { get; set; }

    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get; set; }

    [DisplayName("Created By")]
    public Guid CreatedById { get; set; }

    [ScaffoldColumn(false)]
    public AppUser CreatedBy { get; set; } = null!;

    [DisplayName("Updated By")]
    public Guid? UpdatedById { get; set; }

    [ScaffoldColumn(false)]
    public AppUser UpdatedBy { get; set; } = null!;
}
