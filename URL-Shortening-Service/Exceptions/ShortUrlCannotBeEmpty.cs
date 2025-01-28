namespace URL_Shortening_Service.Exceptions
{
    public class ShortUrlCannotBeEmpty : Exception
    {
        public ShortUrlCannotBeEmpty(string message)
            : base(message) { }
    }
}
