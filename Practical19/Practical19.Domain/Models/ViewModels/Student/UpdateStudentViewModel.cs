namespace Practical19.Domain.Models.ViewModels.Student;

public class UpdateStudentViewModel
{
    [DisplayName("Full Name")]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [DisplayName("GR Number")]
    public long GRNumber { get; set; }

    [DisplayName("Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [DisplayName("Active")]
    public bool IsActive { get; set; }
}
