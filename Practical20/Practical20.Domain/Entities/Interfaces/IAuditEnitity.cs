namespace Practical20.Domain.Entities.Interfaces;

public interface IAuditEnitity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    string CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}
