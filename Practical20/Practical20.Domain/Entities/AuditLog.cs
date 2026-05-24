namespace Practical20.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}
