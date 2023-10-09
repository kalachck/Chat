namespace AspNetChat.Business.Exceptions
{
    public class DeniedAccessException : Exception
    {
        public DeniedAccessException(string? message) : base(message)
        { }
    }
}
