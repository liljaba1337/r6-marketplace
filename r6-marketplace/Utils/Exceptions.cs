using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Utils.Exceptions
{
    internal class InvalidTokenException : Exception
    {
        public InvalidTokenException() { }
        public InvalidTokenException(string message) : base(message) { }
    }
    internal class AuthenticationRequiredException : Exception
    {
        public AuthenticationRequiredException() { }
        public AuthenticationRequiredException(string message) : base(message) { }
    }
    internal class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() { }
        public InvalidCredentialsException(string message) : base(message) { }
    }
    internal class HttpRequestException : Exception
    {
        public HttpRequestException() { }
        public HttpRequestException(string message) : base(message) { }
    }
    internal class UnsuccessfulStatusCodeException : Exception
    {
        public UnsuccessfulStatusCodeException() { }
        public UnsuccessfulStatusCodeException(string message) : base(message) { }
    }
    internal class JsonDeserializationException : Exception
    {
        public JsonDeserializationException() { }
        public JsonDeserializationException(string message) : base(message) { }
    }
}
