using HelloWorldWebAPI.Services.DTOs;
using HelloWorldWebAPI.Services.Interfaces;

namespace HelloWorldWebAPI.Services;

public class HomeService : IHomeService
{
    public ApiResponse<string> Hello()
    {
        return ApiResponse<string>.Success("Hello World", "Request completed successfully.");
    }
}
