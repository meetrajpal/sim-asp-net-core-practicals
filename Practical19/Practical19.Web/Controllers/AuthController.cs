namespace Practical19.Web.Controllers;

public class AuthController(IAuthService authService) : Controller
{

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        (bool success, List<string> errors) = await authService.RegisterAsync(vm);

        if (!success)
        {
            errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
            return View(vm);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestViewModel vm, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(vm);

        (bool success, string error) = await authService.LoginAsync(vm);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, error);
            return View(vm);
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => View();
}

