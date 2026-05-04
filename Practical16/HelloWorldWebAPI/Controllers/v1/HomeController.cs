using HelloWorldWebAPI.Services.DTOs;
using HelloWorldWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebAPI.API.Controllers.v1;

[ApiController]
[Route("api/v1/home")]
public class HomeController(IHomeService homeService) : ControllerBase
{
    [HttpGet]
    public ApiResponse<string> Get()
    {
        return homeService.Hello();
    }
}
