namespace Practical18.Domain.Models.ViewModels;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string CustomMessage { get; set; } = string.Empty;

    public string ExceptionMessage { get; set; } = string.Empty;

}
