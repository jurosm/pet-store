namespace PetStoreService.Persistence;

public class DatabaseSettings
{
    public string getConnectionString()
    {
        return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new Exception("Env DB_CONNECTION_STRING missing.");
    }
}