
using CatalogTypes.Bugeting;

namespace Catalog.Api.Catalog;

public class BudgetingHttp(HttpClient client) : ICheckForBudgets
{
    public async Task<bool> HasAdequateFundingFor(CatalogItemResponse response)
    {
        var request = new AllocateBudgetFor()
        {
            AnnualCostPerSeat = response.AnnualCostPerSeat,
            Vendor = "bozo", //response.Vendor,
        };
        var httpResponse = await client.PostAsJsonAsync("/budget-allocations", request);

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return false;
        }

        httpResponse.EnsureSuccessStatusCode(); // BLOW UP
        return false;
    }


}
