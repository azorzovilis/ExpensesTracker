namespace ExpensesTrackerAPI.IntegrationTests
{
    using System;
    using System.Linq;
    using DAL;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class AppTestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ExpensesContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ExpensesContext>(options =>
                {
                    options.UseInMemoryDatabase("ExpensesTracker");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                    using (var appContext = scope.ServiceProvider.GetRequiredService<ExpensesContext>())
                    {
                        var logger = scope.ServiceProvider
                            .GetRequiredService<ILogger<AppTestFixture<TStartup>>>();

                        try
                        {
                            appContext.Database.EnsureCreated();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
                        }
                    }
            });
        }
    }
}