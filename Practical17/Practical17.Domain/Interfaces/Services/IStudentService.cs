using Practical17.Domain.DTOs.Student;

namespace Practical17.Domain.Interfaces.Services;

public interface IStudentService
{
    Task<ApiResponse<List<StudentResponseDto>>> GetAll();

    Task<ApiResponse<StudentResponseDto>> GetById(string id);

    Task<ApiResponse<StudentResponseDto>> GetByGRNumber(long gr);

    Task<ApiResponse<string>> CreateStudent(StudentRequestDto dto);

    Task<ApiResponse<string>> UpdateStudent(Guid id, StudentUpdateDto dto);


    Task<ApiResponse<string>> DeleteStudent(Guid id);
}
