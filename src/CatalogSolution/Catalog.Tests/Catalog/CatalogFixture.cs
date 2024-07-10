


using Alba;
using Alba.Security;
using Catalog.Api.Catalog;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Security.Claims;
using Testcontainers.PostgreSql;
using WireMock.Server;

namespace Catalog.Tests.Catalog;
public class CatalogFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; set; } = null!;
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.2-bullseye")
        .WithUsername("user")
        .WithDatabase("catalog")
        .WithPassword("password")
        .Build();

    public WireMockServer MockApiServer = null!;

    public async Task InitializeAsync()
    {
        MockApiServer = WireMockServer.Start();
        await _postgresContainer.StartAsync();
        var fakeIdentity = new AuthenticationStub().With(
            new System.Security.Claims.Claim(ClaimTypes.Role, "SoftwareCenter"));

        Host = await AlbaHost.For<global::Program>(config =>
        {
            var connectionString = _postgresContainer.GetConnectionString();
            config.UseSetting("ConnectionStrings:data",
               connectionString);
            config.UseSetting("budgetingApiUrl", MockApiServer.Url);
            config.ConfigureServices((sp) =>
            {
                ConfigureMyServices(sp);

                var fakeDatabaseThing = Substitute.For<ILookupDatabaseStuff>();
                fakeDatabaseThing.GetValueFromSomeOtherDatabaseAsync().Returns("this is bogus");

                // BOGUS, BS, NOT REAL TESTING. BUT THIS IS BETTER THAN USING OTHER PEOPLES
                // SERVICES IN SYSTEMS TESTS.
                sp.AddScoped<ILookupDatabaseStuff>(_ => fakeDatabaseThing);



            });

        }, fakeIdentity);

    }

    protected virtual void ConfigureMyServices(IServiceCollection services)
    {
        // Template method
    }

    public async Task DisposeAsync()
    {
        MockApiServer.Stop();
        MockApiServer?.Dispose();
        await Host.DisposeAsync();
        await _postgresContainer.DisposeAsync();
    }



}

public class TestingCatalogFixture : CatalogFixture
{
    protected override void ConfigureMyServices(IServiceCollection services)
    {
        var fakeSlugThing = Substitute.For<INormalizeUrlSegmentsForTheCatalog>();
        fakeSlugThing.NormalizeForCatalog(Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>()).Throws(new Exception("Blammo"));

        services.AddScoped<INormalizeUrlSegmentsForTheCatalog>(_ => fakeSlugThing);
    }
}