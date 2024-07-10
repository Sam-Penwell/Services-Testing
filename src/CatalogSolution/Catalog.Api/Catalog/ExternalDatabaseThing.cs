namespace Catalog.Api.Catalog;

public class ExternalDatabaseThing : ILookupDatabaseStuff
{
    public async Task<string> GetValueFromSomeOtherDatabaseAsync()
    {
        // write all your code to access the database here
        // SELECT food from reicipes where favorite = true;
        throw new Exception("The other database failed");
        return "Tacos";
    }
}
