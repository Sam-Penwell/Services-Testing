
namespace Catalog.Api.Catalog;

public interface ILookupDatabaseStuff
{
    Task<string> GetValueFromSomeOtherDatabaseAsync();
}