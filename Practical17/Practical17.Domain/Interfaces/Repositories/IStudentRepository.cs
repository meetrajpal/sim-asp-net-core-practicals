namespace Practical17.Domain.Interfaces.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<Student?> FindByGRAsync(long gr);
}
