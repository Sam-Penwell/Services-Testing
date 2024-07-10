
namespace Catalog.Api.Catalog;

public interface ICheckForBudgets
{
    Task<bool> HasAdequateFundingFor(CatalogItemResponse response);
}
