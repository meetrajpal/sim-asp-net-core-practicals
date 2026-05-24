namespace Practical19.Domain.Models.Entities;

public class Student : BaseEntity
{
    [DisplayName("Full Name")]
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [DisplayName("GR Number")]
    [Required]
    public long GRNumber { get; set; }

    [DisplayName("Date Of Birth")]
    [Required]
    public DateTime DateOfBirth { get; set; }
}
