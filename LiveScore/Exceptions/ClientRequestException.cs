using System;

namespace LiveScore.Exceptions
{
    /// <summary>
    /// Client request exception class.
    /// </summary>
    public class ClientRequestException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClientRequestException() : base("Client request is invalid") { }

        /// <summary>
        /// Constructor that recieves message parameter.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ClientRequestException(string message) : base(message) { }
    }
}
