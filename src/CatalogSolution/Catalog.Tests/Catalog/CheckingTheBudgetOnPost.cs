using Catalog.Api.Catalog;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Catalog.Tests.Catalog;
public class CheckingTheBudgetOnPost(CatalogFixture fixture) : IClassFixture<CatalogFixture>
{
    // Use another fixture, get it to check the budget, all that stuff.
    // if that ICheckBudgetThing returns false, we get a 400 with the message.


    [Fact]
    public async Task IfNotInTheBudgetReturnAnError()
    {

        fixture.MockApiServer.Given(
            Request.Create().WithPath("/budget-allocations")

            .UsingPost()
            )
            .RespondWith(
                Response.Create()
            .WithStatusCode(400));


        var newCatalogItem = new CreateCatalogItemRequest
        {
            Version = "1.91",
            IsCommercial = true,
            AnnualCostPerSeat = 1.99M
        };

        await fixture.Host.Scenario(api =>
        {
            api.Post.Json(newCatalogItem).ToUrl("/catalog/microsoft/vscode");
            api.StatusCodeShouldBe(400);
        });
    }
}
