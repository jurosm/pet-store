namespace PetStoreService.Application.Models.Response;

public class ErrorResponse
{
    public required string Message { get; set; }
    public string? ErrorCode {get; set;}
}