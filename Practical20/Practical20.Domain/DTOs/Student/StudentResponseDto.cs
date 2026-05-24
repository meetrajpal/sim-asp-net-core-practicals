namespace Practical20.Domain.DTOs.Student;

public class StudentResponseDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public long GRNumber { get; set; }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public string CreatedBy { get; set; } = string.Empty!;
    public string? UpdatedBy { get; set; }
}
