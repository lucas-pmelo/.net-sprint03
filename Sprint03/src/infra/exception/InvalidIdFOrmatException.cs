using System;

namespace Sprint03.domain.exceptions
{
    public class InvalidIdFormatException : Exception
    {
        public InvalidIdFormatException(string message) : base(message) { }
    }
}