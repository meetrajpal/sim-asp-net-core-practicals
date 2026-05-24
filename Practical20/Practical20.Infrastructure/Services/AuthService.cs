namespace Practical20.Infrastructure.Services;

public class AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IOptions<JwtSettings> jwtSettings, ILogger<AuthService> _logger) : IAuthService
{

    private readonly IUserRepository userRepository = unitOfWork.UserRepo;
    private readonly IRoleRepository roleRepository = unitOfWork.RoleRepo;
    private readonly ITokenService _tokenService = tokenService;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto dto)
    {
        _logger.LogInformation("Creating new user started.");
        var existingUser = await userRepository.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Email is already registered with given email {email}.", dto.Email);
            return ApiResponse<string>.Failure("Email is already registered.");
        }


        var user = new User() { FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, MobileNumber = dto.MobileNumber, CreatedAt = DateTime.UtcNow, PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), IsActive = true };

        var role = await roleRepository.FindByRoleNameAsync("User");
        if (role == null)
        {
            _logger.LogError("Role with Name 'User' not found while creating new user.");
            return ApiResponse<string>.Failure("Role with Name 'User' not found while creating new user.");
        }

        await userRepository.AddAsync(user);

        user.UserRoles = [new() { RoleId = role.Id }];

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User registered successfully.");

        return ApiResponse<string>.Success(null, "Registration successful.");
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto)
    {
        _logger.LogInformation("Login process started.");
        var user = await userRepository.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            _logger.LogWarning("Account not found with given email {email}.", dto.Email);
            return ApiResponse<AuthResponseDto>.Failure("Account not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid credentials for account with email {email}.", dto.Email);
            return ApiResponse<AuthResponseDto>.Failure("Invalid credentials.");
        }

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

        _logger.LogInformation("User logged in successfully.");

        return ApiResponse<AuthResponseDto>.Success(response, "Login successful.");
    }

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        _logger.LogInformation("Refreshing token process started.");
        var principal = _tokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            _logger.LogWarning("Invalid access token was provided.");
            return ApiResponse<AuthResponseDto>.Failure("Invalid access token.");
        }

        var user = await userRepository.GetByIdAsync(Guid.Parse(userId));

        if (user == null || user.RefreshToken != dto.RefreshToken)
        {
            _logger.LogWarning("Invalid refresh token was provided.");
            return ApiResponse<AuthResponseDto>.Failure("Invalid refresh token.");
        }

        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            _logger.LogWarning("Refresh token expired.");
            return ApiResponse<AuthResponseDto>.Failure("Refresh token has expired. Please login again.");
        }

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

        _logger.LogInformation("Access token refrehed successfully.");
        return ApiResponse<AuthResponseDto>.Success(response, "Token refreshed successfully.");
    }

    public async Task<ApiResponse<string>> RevokeTokenAsync(string userId)
    {
        _logger.LogInformation("Logout process started.");
        var user = await userRepository.GetByIdAsync(Guid.Parse(userId));
        if (user == null)
        {
            _logger.LogWarning("User not found with given user id {id}.", user);
            return ApiResponse<string>.Failure("User not found.");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Logged out successfully.");
        return ApiResponse<string>.Success(null, "Logged out successfully.");
    }
}