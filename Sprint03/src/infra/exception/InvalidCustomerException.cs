using System;

namespace Sprint03.domain.exceptions
{
    public class InvalidCustomerException : Exception
    {
        public InvalidCustomerException(string message) : base(message) { }
    }
}