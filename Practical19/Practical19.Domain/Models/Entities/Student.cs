namespace Practical19.Domain.Models.Entities;

public class Student : BaseEntity
{
    [DisplayName("Full Name")]
    public string FullName { get; set; } = string.Empty;

    [DisplayName("GR Number")]
    public long GRNumber { get; set; }

    [DisplayName("Date Of Birth")]
    public DateTime DateOfBirth { get; set; }
}
