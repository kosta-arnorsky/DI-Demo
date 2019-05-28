﻿using DiDemo.Abstractions;
using DiDemo.Api.Logging;
using DiDemo.Api.Staging;
using DiDemo.Data;
using DiDemo.Logging;
using DiDemo.Services.CompanyServices;
using DiDemo.Services.Stock;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DiDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();

            // Example 1, part 3: DI setup
            services.Configure<PricesProviderOptions>(Configuration.GetSection("PricesProvider"));

            // A new instance per dependency
            services.AddTransient<ICompanyPriceProvider, CompanyPriceProvider>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IPricesProvider, PricesProvider>();
            services.AddTransient<ICompanyRepository, DbCompanyRepository>();
            services.AddTransient<IStockRepository, StockRepositoryMock>();

            // A new instance per scope (request in case of ASP.NET)
            services.AddScoped<IDbConnection>(sp => new SqlConnection(Configuration.GetValue<string>("DbConnectionString")));

            // Single instance
            services.AddSingleton<ILogger, ConsoleLogger>();
            // Example 4: swap implantations
            //services.AddSingleton<ILogger, WebLogger>();

            // Example 6: scope
            //services.AddTransient<JustAnExample> (sp =>
            //{
            //    using (var scope = sp.CreateScope())
            //    {
            //        var stockRepository = scope.ServiceProvider.GetService<IStockRepository>();
            //        var prices = stockRepository.GetPrices("RGEN");
            //    }
            //});
        }

        private int SqlConnection(string v)
        {
            throw new NotImplementedException();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}