using System.ComponentModel.DataAnnotations;

namespace Practical18.Domain.Models.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    [DisplayName("Active")]
    public bool IsActive { get; set; }

    [ScaffoldColumn(false)]
    public byte[]? RowVersion { get; set; }

    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get; set; }
}
