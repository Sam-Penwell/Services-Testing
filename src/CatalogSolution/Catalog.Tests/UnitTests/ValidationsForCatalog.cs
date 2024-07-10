using Catalog.Api.Catalog;
using FluentValidation.TestHelper;
namespace Catalog.Tests.UnitTests;

[Trait("Unit", "Catalog")]
public class ValidationsForCatalog
{
    [Fact]
    public void Thingy()
    {
        var validator = new CreateCatalogItemRequestValidator();

        var modelToValidate = new CreateCatalogItemRequest
        {

            IsCommercial = true,

        };
        var result = validator.TestValidate(modelToValidate);

        result.ShouldHaveValidationErrorFor(m => m.Version);
        result.ShouldHaveValidationErrorFor(m => m.AnnualCostPerSeat);

    }
    [Fact]
    public void Thing2()
    {
        var validator = new CreateCatalogItemRequestValidator();

        var modelToValidate = new CreateCatalogItemRequest
        {
            Version = "tacos",
            IsCommercial = true,
            AnnualCostPerSeat = 1

        };
        var result = validator.TestValidate(modelToValidate);

        result.ShouldNotHaveValidationErrorFor(m => m.Version);
        result.ShouldNotHaveValidationErrorFor(m => m.AnnualCostPerSeat);

    }
}
