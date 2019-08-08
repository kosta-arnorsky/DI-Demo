using DiDemo.Abstractions;
using DiDemo.Api.DependencyInjection;
using DiDemo.Api.Logging;
using DiDemo.Api.Services;
using DiDemo.Api.Services.Background;
using DiDemo.Api.Staging;
using DiDemo.Data;
using DiDemo.Logging;
using DiDemo.Services.CompanyServices;
using DiDemo.Services.NamedExample;
using DiDemo.Services.Stock;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            // BOOKMARK: 5.3 ASP.NET Core
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();

            services.AddHostedService<BackgroundWorkService>();

            // BOOKMARK: 1.3 DI setup
            services.Configure<PricesProviderOptions>(Configuration.GetSection("PricesProvider"));

            // A new instance per dependency
            services.AddTransient<ICompanyPriceProvider, CompanyPriceProvider>();
            services.AddTransient<IBackgroundCompanyPriceService, BackgroundCompanyPriceService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IPricesProvider, PricesProvider>();
            services.AddTransient<ICompanyRepository, DbCompanyRepository>(sp
                => new DbCompanyRepository(sp.GetSqlConnection<DbCompanyRepository>(), sp.GetService<ILogger>()));
            services.AddTransient<IStockRepository, StockRepositoryMock>(sp
                => new StockRepositoryMock(sp.GetSqlConnection<StockRepositoryMock>()));

            services.AddTransient(sp => new SomeConsumer(sp.GetService<Tuple<IService, SomeConsumer>>().Item1, sp.GetService<ILogger>()));
            services.AddTransient(sp => new AnotherConsumer(sp.GetService<Tuple<IService, AnotherConsumer>>().Item1));

            // BOOKMARK: 7.2 "named" dependency
            // A new instance per scope (request in case of ASP.NET)
            // Replace with AddTransient to see the difference
            services.AddScoped(sp => Tuple.Create<IService, SomeConsumer>(
                new SomeService(sp.GetService<ILogger>()),
                null));
            services.AddScoped(sp => Tuple.Create<IService, AnotherConsumer>(
                new AnotherService(),
                null));

            // BOOKMARK: 7.3 "named" dependency
            services.AddScopedSqlConnection<DbCompanyRepository>(sp
                => new SqlConnection(Configuration.GetValue<string>("CompanyRepositoryDbConnectionString")));
            services.AddScopedSqlConnection<StockRepositoryMock>(sp
                => new SqlConnection(Configuration.GetValue<string>("StockRepositoryDbConnectionString")));

            // Single instance
            services.AddSingleton<ILogger, ConsoleLogger>();
            // BOOKMARK: 4.1 swap implantations
            //services.AddSingleton<ILogger, WebLogger>();

            // It's crucial for the queue to be a singleton
            services.AddSingleton<BackgroundWorkQueue>();
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
