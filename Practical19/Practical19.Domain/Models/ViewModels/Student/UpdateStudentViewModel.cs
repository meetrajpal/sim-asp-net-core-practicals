namespace Practical19.Domain.Models.ViewModels.Student;

public class UpdateStudentViewModel
{
    public string FullName { get; set; } = string.Empty;

    public long GRNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool IsActive { get; set; }
}
