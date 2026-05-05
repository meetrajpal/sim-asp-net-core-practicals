namespace Practical17.Infrastructure.Services;

public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    private readonly IStudentRepository studentRepository = unitOfWork.StudentRepo;

    public async Task<ApiResponse<List<StudentResponseDto>>> GetAll()
    {
        var students = await studentRepository.GetAllAsync();

        var result = students.Data?.Select(Map).ToList();

        return ApiResponse<List<StudentResponseDto>>.Success(result);
    }

    public async Task<ApiResponse<StudentResponseDto>> GetById(string id)
    {
        var student = await studentRepository.GetByIdAsync(Guid.Parse(id));

        if (student == null)
            throw new KeyNotFoundException($"Student not found with given id: {id}");

        return ApiResponse<StudentResponseDto>.Success(Map(student), "Student fetched successfully");
    }

    public async Task<ApiResponse<StudentResponseDto>> GetByGRNumber(long gr)
    {
        var student = await studentRepository.FindByGRAsync(gr);

        if (student == null)
            throw new KeyNotFoundException($"Student not found with given GR Number: {gr}");

        return ApiResponse<StudentResponseDto>.Success(Map(student), "Student fetched successfully");
    }

    public async Task<ApiResponse<string>> CreateStudent(StudentRequestDto dto)
    {
        var student = await studentRepository.FindByGRAsync(dto.GRNumber);

        if (student != null)
            throw new ArgumentException($"Student already present with given GR Number: {dto.GRNumber}");

        await studentRepository.AddAsync(new Student() { FirstName = dto.FirstName, LastName = dto.LastName, GRNumber = dto.GRNumber });

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "Student created successfully");
    }

    public async Task<ApiResponse<string>> UpdateStudent(Guid id, StudentUpdateDto dto)
    {
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null)
            throw new KeyNotFoundException($"Student not found with given id: {id}");

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.GRNumber = dto.GRNumber;
        student.IsActive = dto.IsActive;


        await studentRepository.UpdateAsync(student);

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "Student updated successfully");
    }

    public async Task<ApiResponse<string>> DeleteStudent(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null)
            throw new KeyNotFoundException($"Student not found with given id: {id}");


        await studentRepository.DeleteAsync(student);

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "Student deleted successfully");
    }

    private static StudentResponseDto Map(Student student)
    {
        return new StudentResponseDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            GRNumber = student.GRNumber,
            CreatedAt = student.CreatedAt,
            UpdatedAt = student.UpdatedAt,
            IsActive = student.IsActive
        };
    }
}
