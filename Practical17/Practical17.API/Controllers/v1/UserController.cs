namespace Practical17.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
public class UserController(IUserService userService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await userService.GetAll();
        return Ok(result);
    }

    [HttpGet("id/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var result = await userService.GetById(userId);
        return Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await userService.GetByEmail(email);
        return Ok(result);
    }
}
