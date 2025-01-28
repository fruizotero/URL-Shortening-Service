namespace URL_Shortening_Service.Exceptions
{
    [Serializable]
    public class ShortUrlAlreadyExists : Exception
    {
        public ShortUrlAlreadyExists()
        {
        }

        public ShortUrlAlreadyExists(string? message) : base(message)
        {
        }

     
    }
}