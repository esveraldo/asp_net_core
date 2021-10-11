using System;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            /*if(Environment.GetEnvironmentVariable("DATABSE").ToLower() == "SQLSERVER".ToLower()){
                serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));
            }else{
                //serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION")));
            }*/

            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=localhost;Database=estudoddd;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=esve6140"));
        }
    }
}