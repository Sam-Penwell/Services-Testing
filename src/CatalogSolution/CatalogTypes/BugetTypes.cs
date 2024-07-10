

namespace CatalogTypes.Bugeting;
public record AllocateBudgetFor
{
    public string Vendor { get; set; } = string.Empty;
    public decimal AnnualCostPerSeat { get; set; }
}