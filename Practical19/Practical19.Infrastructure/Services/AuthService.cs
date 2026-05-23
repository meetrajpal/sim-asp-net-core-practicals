namespace Practical19.Infrastructure.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager) : IAuthService
{
    public async Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequestViewModel vm)
    {
        if (vm.Password != vm.ConfirmPassword)
            return (false, ["Passwords don't match"]);

        var existingUser = await userManager.FindByEmailAsync(vm.Email);
        if (existingUser != null)
            return (false, ["Email is already registered."]);

        var user = new AppUser
        {
            Email = vm.Email,
            UserName = vm.Email,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, vm.Password);
        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description).ToList());

        await userManager.AddToRoleAsync(user, "User");

        await signInManager.SignInAsync(user, isPersistent: false);

        return (true, []);
    }

    public async Task<(bool Success, string Error)> LoginAsync(LoginRequestViewModel vm)
    {
        var result = await signInManager.PasswordSignInAsync(
            vm.Email,
            vm.Password,
            isPersistent: true,
            lockoutOnFailure: true);

        if (!result.Succeeded)
            return (false, "Invalid email or password.");

        return (true, string.Empty);
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

}