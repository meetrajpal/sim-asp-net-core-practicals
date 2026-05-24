namespace Practical20.API.Controllers.v1;

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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequestDto dto)
    {
        var result = await userService.CreateUser(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto dto)
    {
        var result = await userService.UpdateUser(Guid.Parse(id), dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await userService.DeleteUser(Guid.Parse(id));
        return Ok(result);
    }
}
