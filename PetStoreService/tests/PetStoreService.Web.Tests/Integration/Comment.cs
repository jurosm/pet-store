using Newtonsoft.Json;
using PetStoreService.Domain.Entities;
using PetStoreService.Web.Tests.Fixtures;
using PetStoreService.Web.Tests.Integration.Helper;
using System.Text;

namespace PetStoreService.Web.Tests.Integration;

public class CommentTests : TestBase
{
    [Fact]
    public async void CreateComment_ValidComment_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        Comment comment = CommentFixture.Generate();
        comment.ToyId = toy.Id;

        var createCommentRes = await client.PostAsync("/comment", new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Created, createCommentRes.StatusCode);
    }

    [Fact]
    public async void FetchComments_QueryByToyId_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        Comment comment = CommentFixture.Generate();
        comment.ToyId = toy.Id;

        var getCommentsRes = await client.GetAsync($"/comment?toyId={toy.Id}");

        Assert.Equal(System.Net.HttpStatusCode.OK, getCommentsRes.StatusCode);
    }
}