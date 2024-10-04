namespace PetStoreService.Application.Exceptions;

public class BadRequestException(string message, string errorCode) : Exception(message)
{
    public readonly string ErrorCode = errorCode;
}
