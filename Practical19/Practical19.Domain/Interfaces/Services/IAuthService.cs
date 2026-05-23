namespace Practical19.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequestViewModel vm);
    Task<(bool Success, string Error)> LoginAsync(LoginRequestViewModel vm);
    Task LogoutAsync();
}