namespace Practical20.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
    Task<ApiResponse<string>> RevokeTokenAsync(string userId);
}