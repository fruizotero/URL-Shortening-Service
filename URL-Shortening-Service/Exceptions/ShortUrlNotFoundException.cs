namespace URL_Shortening_Service.Exceptions
{
    public class ShortUrlNotFoundException : Exception
    {
        public ShortUrlNotFoundException(string message)
            : base(message)
        {
        }
    }
}
