
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
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

        //Non existing productId
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

        //Valid productId
        var jsonRequest = new
        {
            productId = 23,
            quantity = 1
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/cart/items", jsonRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task RemoveItemFromCart_Fails_IfItemDoesNotExistInCart()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Cookie", "guest_token=test_guest_token");

        var cartItemId = 999999;

        HttpResponseMessage response = await client.DeleteAsync($"/api/cart/items/{cartItemId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task RemoveItemFromCart_Succeeds_WhenItemExistsInCart()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Cookie", "guest_token=test_guest_token");

        await client.PostAsJsonAsync("/api/cart/items", new
        {
            productId = 23,
            quantity = 1
        });

        var cart = await client.GetAsync("/api/cart");

        var body = await cart.Content.ReadAsStringAsync();

        var id = JsonDocument.Parse(body)
        .RootElement
        .GetProperty("data")
        .GetProperty("cartItems")[0]
        .GetProperty("id")
        .GetInt64();

        
        HttpResponseMessage response = await client.DeleteAsync($"/api/cart/items/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
