namespace C06_APBD.DTOs;

public class ErrorResponseDto
{
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ErrorResponseDto(string message, string? details = null)
    {
        Message = message;
        Details = details;
    }
}