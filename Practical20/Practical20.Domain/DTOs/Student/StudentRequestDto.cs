namespace Practical20.Domain.DTOs.Student;

public class StudentRequestDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public long GRNumber { get; set; }
}
