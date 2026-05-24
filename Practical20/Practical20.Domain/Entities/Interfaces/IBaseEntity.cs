namespace Practical20.Domain.Entities.Interfaces;

public interface IBaseEntity
{
    Guid Id { get; set; }
    bool IsActive { get; set; }
}