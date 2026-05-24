namespace Practical20.Infrastructure.Services;

public class StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> _logger) : IStudentService
{
    private readonly IStudentRepository studentRepository = unitOfWork.StudentRepo;

    public async Task<ApiResponse<List<StudentResponseDto>>> GetAll()
    {
        _logger.LogInformation("Retrieveing student list.");

        var students = await studentRepository.GetAllAsync();

        var result = students.Data?.Select(Map).ToList();

        _logger.LogInformation("Retrieved student list successfully.");
        return ApiResponse<List<StudentResponseDto>>.Success(result);
    }

    public async Task<ApiResponse<StudentResponseDto>> GetById(string id)
    {
        _logger.LogInformation("Retrieveing student with id {id}.", id);
        var student = await studentRepository.GetByIdAsync(Guid.Parse(id));

        if (student == null)
        {
            _logger.LogWarning("Student not found with given id: {id}", id);
            throw new KeyNotFoundException($"Student not found with given id: {id}");
        }

        _logger.LogInformation("Retrieved student successfully with {id}.", id);
        return ApiResponse<StudentResponseDto>.Success(Map(student), "Student fetched successfully");
    }

    public async Task<ApiResponse<StudentResponseDto>> GetByGRNumber(long gr)
    {
        _logger.LogInformation("Retrieveing student with GR number {gr}.", gr);
        var student = await studentRepository.FindByGRAsync(gr);

        if (student == null)
        {
            _logger.LogWarning("Student not found with given GR number: {gr}", gr);
            throw new KeyNotFoundException($"Student not found with given GR Number: {gr}");
        }

        _logger.LogInformation("Retrieved student successfully with GR number {gr}.", gr);
        return ApiResponse<StudentResponseDto>.Success(Map(student), "Student fetched successfully");
    }

    public async Task<ApiResponse<string>> CreateStudent(StudentRequestDto dto)
    {
        _logger.LogInformation("Creation of student process started.");
        var student = await studentRepository.FindByGRAsync(dto.GRNumber);

        if (student != null)
        {
            _logger.LogWarning("Student already present with given GR Number: {gr}", dto.GRNumber);
            throw new ArgumentException($"Student already present with given GR Number: {dto.GRNumber}");
        }

        await studentRepository.AddAsync(new Student() { FirstName = dto.FirstName, LastName = dto.LastName, GRNumber = dto.GRNumber });

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Creation of student process completed.");
        return ApiResponse<string>.Success(null, "Student created successfully");
    }

    public async Task<ApiResponse<string>> UpdateStudent(Guid id, StudentUpdateDto dto)
    {
        _logger.LogInformation("Updation of student process started with given id {id}.", id);
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            _logger.LogWarning("Student not found with given id: {id}.", id);
            throw new KeyNotFoundException($"Student not found with given id: {id}");
        }

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.GRNumber = dto.GRNumber;
        student.IsActive = dto.IsActive;


        await studentRepository.UpdateAsync(student);

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Updation of student process completed with given id {id}.", id);
        return ApiResponse<string>.Success(null, "Student updated successfully");
    }

    public async Task<ApiResponse<string>> DeleteStudent(Guid id)
    {
        _logger.LogInformation("Deletion of student process started with given id {id}.", id);
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            _logger.LogInformation("Student not found with given id: {id}", id);
            throw new KeyNotFoundException($"Student not found with given id: {id}");
        }


        await studentRepository.DeleteAsync(student);

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Deletion of student process completed with given id {id}.", id);
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
            IsActive = student.IsActive,
            CreatedBy = student.CreatedBy,
            UpdatedBy = student.UpdatedBy
        };
    }
}
