using System;

namespace PetStore.API.Exceptions.Services.Order
{
    public class MessageException(string message) : Exception
    {
        public override string Message { get; } = message;
    }
}