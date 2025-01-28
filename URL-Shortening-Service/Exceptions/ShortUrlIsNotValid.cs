namespace URL_Shortening_Service.Exceptions
{
    public class ShortUrlIsNotValid: Exception
    {
        public ShortUrlIsNotValid(string message)
            : base(message)
        {
        }
    }
}
