namespace Practical19.Domain.Models.Entities.Interfaces;

public interface IBaseEntity
{
    Guid Id { get; set; }

    [DisplayName("Active")]
    bool IsActive { get; set; }

    [DisplayName("Created At")]
    DateTime CreatedAt { get; set; }

    [DisplayName("Updated At")]
    DateTime? UpdatedAt { get; set; }
}
