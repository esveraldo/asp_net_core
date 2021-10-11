using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    
    public class DbTest : IDisposable
    {

        private string DataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>(o => 
            o.UseSqlServer($"Server=localhost;Database={DataBaseName};Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=esve6140"),
            ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();

            using (var context = ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
