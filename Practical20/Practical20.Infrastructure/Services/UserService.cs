namespace Practical20.Infrastructure.Services;

public class UserService(IUnitOfWork unitOfWork, ILogger<UserService> _logger) : IUserService
{
    private readonly IUserRepository userRepository = unitOfWork.UserRepo;
    private readonly IRoleRepository roleRepository = unitOfWork.RoleRepo;
    public async Task<ApiResponse<List<UserResponseDto>>> GetAll()
    {
        _logger.LogInformation("Retrieval of user records started.");

        var users = await userRepository.GetAllAsync();

        var result = users.Data?.Select(Map).ToList();

        _logger.LogInformation("Retrieval of user records completed.");
        return ApiResponse<List<UserResponseDto>>.Success(result);
    }

    public async Task<ApiResponse<UserResponseDto>> GetById(string id)
    {
        _logger.LogInformation("Retrieval of user record started with given id {id}.", id);
        var user = await userRepository.GetByIdAsync(Guid.Parse(id));

        if (user == null)
        {
            _logger.LogWarning("User not found with given id: {id}", id);
            throw new KeyNotFoundException($"User not found with given id: {id}");
        }

        _logger.LogInformation("Retrieval of user record completed with given id {id}.", id);

        return ApiResponse<UserResponseDto>.Success(
            Map(user),
            "User fetched successfully"
        );
    }

    public async Task<ApiResponse<UserResponseDto>> GetByEmail(string email)
    {
        _logger.LogInformation("Retrieval of user record started with given email {email}.", email);
        var user = await userRepository.FindByEmailAsync(email);

        if (user == null)
        {
            _logger.LogWarning("User not found with given email: {email}", email);
            throw new KeyNotFoundException($"User not found with given email: {email}");
        }

        _logger.LogInformation("Retrieval of user record completed with given email {email}.", email);
        return ApiResponse<UserResponseDto>.Success(
            Map(user),
            "User fetched successfully"
        );
    }

    public async Task<ApiResponse<string>> CreateUser(UserRequestDto dto)
    {
        _logger.LogInformation("Creation process of new user started");
        var user = await userRepository.FindByEmailAsync(dto.Email);

        if (user != null)
        {
            _logger.LogWarning("User already peresent with given email {email}", dto.Email);
            throw new ArgumentException($"User already present with given Email: {dto.Email}");
        }

        var newUser = await userRepository.AddAsync(new User() { FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, MobileNumber = dto.MobileNumber, CreatedAt = DateTime.UtcNow, PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), IsActive = true });

        string roleName = dto.IsAdmin ? "Admin" : "User";

        var role = await roleRepository.FindByRoleNameAsync(roleName);

        if (role == null)
        {
            _logger.LogError("Role not found with name - {roleName} while creating new user", roleName);
            throw new KeyNotFoundException($"Role not found with name - {roleName}");
        }

        newUser.UserRoles = [new() { RoleId = role.Id }];

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Creation process of new user completed successfully");
        return ApiResponse<string>.Success(null, "User created successfully");
    }

    public async Task<ApiResponse<string>> UpdateUser(Guid id, UserUpdateDto dto)
    {
        _logger.LogInformation("Updation process of user started");
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning("User not found with given id: {id}", id);

            throw new KeyNotFoundException($"User not found with given id: {id}");
        }

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.UpdatedAt = DateTime.UtcNow;
        user.MobileNumber = dto.MobileNumber;
        user.IsActive = dto.IsActive;


        await userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Updation process of user completed successfully");
        return ApiResponse<string>.Success(null, "User updated successfully");
    }

    public async Task<ApiResponse<string>> DeleteUser(Guid id)
    {
        _logger.LogInformation("Deletion process of user started");
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logger.LogWarning("User not found with given id: {id}", id);
            throw new KeyNotFoundException($"User not found with given id: {id}");
        }


        await userRepository.DeleteAsync(user);

        await unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Deletion process of user completed successfully");
        return ApiResponse<string>.Success(null, "User deleted successfully");
    }

    private static UserResponseDto Map(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MobileNumber = user.MobileNumber,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            IsActive = user.IsActive,
            Roles = user.UserRoles?
                .Select(x => x.Role.RoleName)
                .ToList() ?? new List<string>()
        };
    }
}
