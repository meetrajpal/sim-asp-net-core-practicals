namespace Practical17.Domain.Interfaces.Services;

public interface IUserService
{
    Task<ApiResponse<List<UserResponseDto>>> GetAll();
    Task<ApiResponse<UserResponseDto>> GetById(string id);
    Task<ApiResponse<UserResponseDto>> GetByEmail(string email);

    Task<ApiResponse<string>> CreateUser(UserRequestDto dto);

    Task<ApiResponse<string>> UpdateUser(Guid id, UserUpdateDto dto);


    Task<ApiResponse<string>> DeleteUser(Guid id);
}
