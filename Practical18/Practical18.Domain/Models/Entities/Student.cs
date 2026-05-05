namespace Practical18.Domain.Models.Entities;

public class Student : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public long GRNumber { get; set; }
}
