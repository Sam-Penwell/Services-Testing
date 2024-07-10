

using Alba;
using Catalog.Api.Catalog;
using System.Security.Claims;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;


namespace Catalog.Tests.Catalog;

public class AddingToTheCatalog : IClassFixture<CatalogFixture>
{

    private IAlbaHost _host;
    public AddingToTheCatalog(CatalogFixture fixture)
    {
        /*{
    "request": {
        "urlPath": "/budget-allocations",
        "method": "POST"
    },
    "response": {
        "status": 200
    }
}*/
        _host = fixture.Host;
        fixture.MockApiServer.Given(
            Request.Create().WithPath("/budget-allocations").UsingPost()
            )
            .RespondWith(
                Response.Create()
            .WithStatusCode(200));
    }


    [Fact]
    [Trait("System", "All")]
    [Trait("System", "Catalog")]

    public async Task CanAddAnItemToTheCatalog()
    {

        var newCatalogItem = new CreateCatalogItemRequest
        {
            Version = "1.91",
            IsCommercial = true,
            AnnualCostPerSeat = 1.99M
        };

        var expectedResponse = new CatalogItemResponse
        {
            Vendor = "microsoft",
            Application = "visualstudio",
            AnnualCostPerSeat = 1.99M,
            Version = "1-91"
        };
        var postResponse = await _host.Scenario(api =>
         {
             api.Post.Json(newCatalogItem).ToUrl("/catalog/microsoft/visualstudio");
             api.StatusCodeShouldBe(201);
         });

        var locationHeader = postResponse.Context.Response.Headers["location"].Single();
        Assert.NotNull(locationHeader);
        var postBody = await postResponse.ReadAsJsonAsync<CatalogItemResponse>();
        Assert.NotNull(postBody);

        Assert.Equal(expectedResponse, postBody);

        var getResponse = await _host.Scenario(api =>
        {
            api.Get.Url(locationHeader);
            api.WithClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "SoftwareCenter"));
            api.StatusCodeShouldBeOk();
        });

        var getBody = await getResponse.ReadAsJsonAsync<CatalogItemResponse>();

        Assert.Equal(postBody, getBody);

    }

    [Fact]
    [Trait("System", "Catalog")]
    public async Task GettingAnItemThatIsntInTheCatalog()
    {
        await _host.Scenario(api =>
        {
            api.Get.Url("/catalog/microsoft/vscode/1.17");
            api.StatusCodeShouldBe(404);
        });
    }


}
