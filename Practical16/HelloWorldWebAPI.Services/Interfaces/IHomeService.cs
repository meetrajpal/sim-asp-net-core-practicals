using HelloWorldWebAPI.Services.DTOs;

namespace HelloWorldWebAPI.Services.Interfaces;

public interface IHomeService
{
    ApiResponse<string> Hello();
}
