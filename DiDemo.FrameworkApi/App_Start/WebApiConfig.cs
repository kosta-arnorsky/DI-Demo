using Autofac;
using Autofac.Integration.WebApi;
using DiDemo.Abstractions;
using DiDemo.Data;
using DiDemo.FrameworkApi.Helpers;
using DiDemo.Logging;
using DiDemo.Services.CompanyServices;
using DiDemo.Services.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Http;

namespace DiDemo.FrameworkApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // BOOKMARK: 5.1 Autofac
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterWebApiFilterProvider(config);
            //builder.RegisterWebApiModelBinderProvider();

            var configuration = ConfiguratioHelper.FromAppSettings();
            builder.Register(c => Options.Create(configuration.GetSection("PriceProvider").Get<PriceProviderOptions>()));
            // or 
            //builder.Register(c => ConfiguratioHelper.FromAppSettings());
            //builder.Register(c => Options.Create(c.Resolve<IConfiguration>().GetSection("PriceProvider").Get<PriceProviderOptions>()));

            // Auto, usually a new instance per dependency
            builder
              .RegisterType<CompanyPriceProvider>()
              .As<ICompanyPriceProvider>();
            builder
               .RegisterType<CompanyService>()
               .As<ICompanyService>();
            builder
               .RegisterType<PriceProvider>()
               .As<IPriceProvider>();
            builder
               .RegisterType<DbCompanyRepository>()
               .As<ICompanyRepository>();
            builder
               .Register(c => new DbCompanyRepository(
                   c.ResolveNamed<IDbConnection>("CompanyRepositoryConnection"),
                   c.Resolve<ILogger>()))
               .As<ICompanyRepository>();
            builder
               .Register(c => new DbStockRepository(
                   c.ResolveNamed<IDbConnection>("StockRepositoryConnection")))
               .As<IStockRepository>();

            // BOOKMARK: 7.1 named dependency
            // A new instance per scope
            builder
               .Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["CompanyRepositoryDbConnectionString"].ConnectionString))
               .Named<IDbConnection>("CompanyRepositoryConnection")
               .InstancePerLifetimeScope();
            builder
               .Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["StockRepositoryDbConnectionString"].ConnectionString))
               .Named<IDbConnection>("StockRepositoryConnection")
               .InstancePerLifetimeScope();

            // Singleton
            builder
               .RegisterType<ConsoleLogger>()
               .As<ILogger>()
               .SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
