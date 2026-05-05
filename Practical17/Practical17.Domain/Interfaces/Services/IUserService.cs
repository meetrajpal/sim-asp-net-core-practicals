using Practical17.Domain.DTOs.User;

namespace Practical17.Domain.Interfaces.Services;

public interface IUserService
{
    Task<ApiResponse<List<UserResponseDto>>> GetAll();
    Task<ApiResponse<UserResponseDto>> GetById(string id);
    Task<ApiResponse<UserResponseDto>> GetByEmail(string email);
}
