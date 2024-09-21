namespace PetStoreService.Application.Exceptions.Services.Order;

public class MessageException(string message) : Exception
{
    public override string Message { get; } = message;
}
