using System;

namespace LiveScore.Exceptions
{
    public class ClientRequestException : Exception
    {
        public ClientRequestException() : base("Client request is invalid") { }
        public ClientRequestException(string message) : base(message) { }
    }
}
