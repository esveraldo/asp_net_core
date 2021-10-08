using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.CrossCutting.DependencyInjection;
using Api.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Api.CrossCutting.Mappings;
using AutoMapper;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependencyService(services);
            ConfigureRepository.ConfigureDependencyRepository(services);

            var config = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new UserEntityToDtoProfile());
                cfg.AddProfile(new UserModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

           var Key = Encoding.ASCII.GetBytes(ConfigurationService.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            });

            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo{
                    Version = "V!",
                    Title = "Estudando DDD",
                    Description = "Estudando DDD",
                    TermsOfService = new Uri("https://google.com.br"),
                    Contact = new OpenApiContact{
                        Name = "Esveraldo Martins",
                        Email = "esveraldo@hotmail.com",
                        Url = new Uri("http://google.com.br"),
                    },
                    License = new OpenApiLicense{
                        Name = "Terms Of License",
                        Url = new Uri("https://google.com.br")
                    }
                });

                s.AddSecurityDefinition("Admin", new OpenApiSecurityScheme{
                    Description = "Entre com o token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference{
                                Id = "Admin",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("swagger/v1/swagger.json", "Estudo DDD");
                s.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //CRIAR AS MIGRAÇÕES AUTOMATICAMENTE PARA O BANCO DE DADOS - CRIAR O BANCO DE DADOS PRIMEIRO NA STRING
            // DE CONEXAO EM LAUNCH.JSON
            if(Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower()){

                using(var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()){

                    using(var context = service.ServiceProvider.GetService<ApplicationDbContext>()){
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
