using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Exceptions.Services.Order
{
    public class MessageException : Exception
    {
        public string Message { get; set; }
        public MessageException(string message)
        {
            Message = message;
        }
    }
}
