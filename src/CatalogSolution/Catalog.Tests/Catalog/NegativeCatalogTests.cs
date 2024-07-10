
using Catalog.Api.Catalog;

namespace Catalog.Tests.Catalog;
[Trait("System", "Catalog")]
[Trait("Fake", "Blammo")]
public class NegativeCatalogTests(TestingCatalogFixture fixture)
    : IClassFixture<TestingCatalogFixture>
{

    [Fact]
    public async Task GetAFourOFourForBadUrl()
    {
        var invalidUrlBecauseOfSpaces = "/catalog/Jet Brains/Rider/17";
        await fixture.Host.Scenario(api =>
        {
            api.Get.Url(invalidUrlBecauseOfSpaces);
            api.StatusCodeShouldBe(404);
        });
    }

    [Fact]
    public async Task PostsAreValidated()
    {
        var obviouslyBadInput = new CreateCatalogItemRequest
        {

        };

        await fixture.Host.Scenario(api =>
        {
            api.Post.Json(obviouslyBadInput).ToUrl("/catalog/thing/dog");
            api.StatusCodeShouldBe(400);
        });
    }
}