namespace PetStoreService.Web.Tests;

public static class EnvironmentVariables
{
    public static string Auth0Username = Environment.GetEnvironmentVariable("AUTH0_EMAIL");
    public static string Auth0Password = Environment.GetEnvironmentVariable("AUTH0_PASSWORD");
}