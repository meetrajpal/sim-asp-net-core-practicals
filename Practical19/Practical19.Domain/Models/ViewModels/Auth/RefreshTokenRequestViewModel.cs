namespace Practical19.Domain.Models.ViewModels.Auth;

public class RefreshTokenRequestViewModel
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}