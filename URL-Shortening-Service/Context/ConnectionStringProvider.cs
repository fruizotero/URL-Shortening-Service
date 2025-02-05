namespace URL_Shortening_Service.Context
{
    public class ConnectionStringProvider
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string userId = Environment.GetEnvironmentVariable("DB_USER_ID") ?? "sa";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "sa.Bd.123";
            string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "LOCALHOST,1533";

            string baseConnection = configuration.GetConnectionString("DefaultConnection");

            return $"Server={server};{baseConnection}User Id={userId};Password={password};";
        }
    }
}
