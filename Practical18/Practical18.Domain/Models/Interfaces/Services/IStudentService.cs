namespace Practical18.Domain.Models.Interfaces.Services;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(Guid id);

    Task CreateAsync(CreateStudentViewModel entity);

    Task<bool> UpdateAsync(Guid id, UpdateStudentViewModel entity);

    Task<bool> DeleteAsync(Guid id);
}
