namespace Practical19.Domain.Models.ViewModels.Auth;

public class LoginRequestViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public bool Remember { get; set; }
}