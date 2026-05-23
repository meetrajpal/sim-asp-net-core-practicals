namespace Practical19.Infrastructure.Services;

public class StudentService(IUnitOfWork unitOfWork, IMapper mapper) : IStudentService
{
    private readonly IStudentRepository _studentRepo = unitOfWork.StudentRepository;
    public async Task<List<Student>> GetAllAsync()
    {
        return await _studentRepo.GetAllAsync();
    }
    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await _studentRepo.GetByIdAsync(id);
    }

    public async Task CreateAsync(CreateStudentViewModel viewModel)
    {
        Student entity = mapper.Map<Student>(viewModel);
        await _studentRepo.CreateAsync(entity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStudentViewModel viewModel)
    {
        var result = await _studentRepo.GetByIdAsync(id);
        if (result == null) return false;
        mapper.Map(viewModel, result);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _studentRepo.GetByIdAsync(id);
        if (result == null) return false;
        await _studentRepo.DeleteAsync(result);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}
