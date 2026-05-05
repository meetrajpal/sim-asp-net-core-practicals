//namespace Practical17.API.Middlewares;

//public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
//{

//    public async Task InvokeAsync(HttpContext context)
//    {
//        try
//        {
//            logger.LogInformation("API started executing: {path}", context.Request.Path);
//            await next(context);
//            logger.LogInformation("API completed executing: {path}", context.Request.Path);
//        }
//        catch (Exception ex)
//        {
//            logger.LogError(ex, "Unhandled exception occurred. RequestPath: {Path}", context.Request.Path);
//            await HandleExceptionAsync(context, ex);
//        }
//    }

//    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
//    {
//        context.Response.ContentType = "application/json";

//        var (statusCode, message) = exception switch
//        {
//            ArgumentNullException => (HttpStatusCode.BadRequest, "A required value was missing."),
//            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
//            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "You are not authorized to perform this action."),
//            KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
//            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
//            NotImplementedException => (HttpStatusCode.NotImplemented, "This feature is not yet implemented."),
//            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.")
//        };

//        context.Response.StatusCode = (int)statusCode;

//        var response = ApiResponse<object>.Failure(message, new List<string> { exception.Message });

//        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
//        {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//        });

//        await context.Response.WriteAsync(json);
//    }
//}
