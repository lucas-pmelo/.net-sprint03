namespace Sprint03.infra.exception
{
    public class InvalidIdFormatException : Exception
    {
        public InvalidIdFormatException(string message) : base(message) { }
    }
}