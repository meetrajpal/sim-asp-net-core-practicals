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

    private static UserResponseDto Map(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            IsActive = user.IsActive,
            Roles = user.UserRoles?
                .Select(x => x.Role.RoleName)
                .ToList() ?? new List<string>()
        };
    }
}
