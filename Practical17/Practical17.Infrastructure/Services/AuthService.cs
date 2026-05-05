using Practical17.Domain.DTOs.Auth;
using Practical17.Domain.Interfaces;
using Practical17.Domain.Interfaces.Repositories;
namespace Practical17.Infrastructure.Services;

public class AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IOptions<JwtSettings> jwtSettings) : IAuthService
{

    private readonly IUserRepository userRepository = unitOfWork.UserRepo;
    private readonly IRoleRepository roleRepository = unitOfWork.RoleRepo;
    private readonly ITokenService _tokenService = tokenService;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto dto)
    {

        var existingUser = await userRepository.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            return ApiResponse<string>.Failure("Email is already registered.");


        var user = new User
        {
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        var role = await roleRepository.FindByRoleNameAsync("NormalUser");
        if (role == null)
        {
            return ApiResponse<string>.Failure("Role with Name 'NormalUser' not found while creating new user.");
        }

        await userRepository.AddAsync(user);

        user.UserRoles = new List<UserRole>
        {
            new() { RoleId = role.Id }
        };

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "Registration successful.");
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto)
    {
        var user = await userRepository.FindByEmailAsync(dto.Email);

        if (user == null)
            return ApiResponse<AuthResponseDto>.Failure("Account not found.");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return ApiResponse<AuthResponseDto>.Failure("Invalid credentials.");

        var roles = user.UserRoles.Select(x => x.Role.RoleName).ToList();

        var accessToken = _tokenService.GenerateAccessToken(user, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
        await userRepository.UpdateAsync(user);

        var response = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes)
        };

        return ApiResponse<AuthResponseDto>.Success(response, "Login successful.");
    }

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return ApiResponse<AuthResponseDto>.Failure("Invalid token.");

        var user = await userRepository.GetByIdAsync(Guid.Parse(userId));

        if (user == null || user.RefreshToken != dto.RefreshToken)
            return ApiResponse<AuthResponseDto>.Failure("Invalid refresh token.");

        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return ApiResponse<AuthResponseDto>.Failure("Refresh token has expired. Please login again.");

        var roles = user.UserRoles;
        var newAccessToken = _tokenService.GenerateAccessToken(user, roles.Select(x => x.Role.RoleName).ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
        await userRepository.UpdateAsync(user);

        var response = new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes)
        };

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<AuthResponseDto>.Success(response, "Token refreshed successfully.");
    }

    public async Task<ApiResponse<string>> RevokeTokenAsync(string userId)
    {
        var user = await userRepository.GetByIdAsync(Guid.Parse(userId));
        if (user == null)
            return ApiResponse<string>.Failure("User not found.");

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "Logged out successfully.");
    }
}