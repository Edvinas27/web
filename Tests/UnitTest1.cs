
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{

    //TESTS ACTUALLY ADD TO DATABASE, BUT ATM WILL BE GOOD ENOUGH XD
    private readonly WebApplicationFactory<Program> _factory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }


    [Fact]
    public async Task GetCart_Fails_IfThereIsNoGuestId()
    {
        var client = _factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync("/api/cart");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCart_Succeeds_WhenGuestIdIsProvided()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Cookie", "guest_token=test_guest_token");
        HttpResponseMessage response = await client.GetAsync("/api/cart");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddItemToCart_Fails_IfNonExistingProductIsGiven()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Cookie", "guest_token=test_guest_token");

        var jsonRequest = new
        {
            productId = 5,
            quantity = 1
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/cart/items", jsonRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddItemToCart_Succeeds_WhenValidProductIsGiven()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Cookie", "guest_token=test_guest_token");

        var jsonRequest = new
        {
            productId = 23, 
            quantity = 11111
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/cart/items", jsonRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
