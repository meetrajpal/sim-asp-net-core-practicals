namespace Practical17.Infrastructure.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUserRepository userRepository = unitOfWork.UserRepo;
    public async Task<ApiResponse<List<UserResponseDto>>> GetAll()
    {
        var users = await userRepository.GetAllAsync();

        var result = users.Data?.Select(Map).ToList();

        return ApiResponse<List<UserResponseDto>>.Success(result);
    }

    public async Task<ApiResponse<UserResponseDto>> GetById(string id)
    {
        var user = await userRepository.GetByIdAsync(Guid.Parse(id));

        if (user == null)
            throw new KeyNotFoundException($"User not found with given id: {id}");

        return ApiResponse<UserResponseDto>.Success(
            Map(user),
            "User fetched successfully"
        );
    }

    public async Task<ApiResponse<UserResponseDto>> GetByEmail(string email)
    {
        var user = await userRepository.FindByEmailAsync(email);

        if (user == null)
            throw new KeyNotFoundException($"User not found with given email: {email}");

        return ApiResponse<UserResponseDto>.Success(
            Map(user),
            "User fetched successfully"
        );
    }

    public async Task<ApiResponse<string>> CreateUser(UserRequestDto dto)
    {
        var user = await userRepository.FindByEmailAsync(dto.Email);

        if (user != null)
            throw new ArgumentException($"User already present with given Email: {dto.Email}");

        await userRepository.AddAsync(new User() { FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, MobileNumber = dto.MobileNumber, CreatedAt = DateTime.UtcNow, PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), IsActive = true });

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "User created successfully");
    }

    public async Task<ApiResponse<string>> UpdateUser(Guid id, UserUpdateDto dto)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
            throw new KeyNotFoundException($"User not found with given id: {id}");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.UpdatedAt = DateTime.UtcNow;
        user.MobileNumber = dto.MobileNumber;
        user.IsActive = dto.IsActive;


        await userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(null, "User updated successfully");
    }

    public async Task<ApiResponse<string>> DeleteUser(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
            throw new KeyNotFoundException($"User not found with given id: {id}");


        await userRepository.DeleteAsync(user);

        await unitOfWork.SaveChangesAsync();

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
