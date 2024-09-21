
using Microsoft.AspNetCore.Mvc.Testing;

namespace PetStoreService.Web.Tests.Integration;

public class TestBase
{
    protected readonly WebApplicationFactory<Program> _factory;

    public TestBase()
    {
        _factory = new WebApplicationFactory<Program>();
    }
}