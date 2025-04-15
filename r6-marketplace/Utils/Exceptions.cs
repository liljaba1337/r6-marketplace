using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Utils.Exceptions
{
    internal class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }
    internal class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message)
        {
        }
    }
    internal class UnsuccessfulStatusCodeException : Exception
    {
        public UnsuccessfulStatusCodeException(string message) : base(message)
        {
        }
    }
    internal class JsonDeserializationException : Exception
    {
        public JsonDeserializationException(string message) : base(message)
        {
        }
    }
}
