using DiDemo.Abstractions;
using DiDemo.Cli.Mocks;
using DiDemo.Data;
using DiDemo.Formatting;
using DiDemo.Services.CompanyServices;
using DiDemo.Services.Stock;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using Unity;
using Unity.Lifetime;
using Unity.Microsoft.Options;

namespace DiDemo.Cli
{
    internal class Program
    {
        private const string ExitKeyword = "exit";

        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine($"Entre company name or \"{ExitKeyword}\" to close the program");

            using (var container = BuildContainer(args))
            {
                var command = Console.ReadLine();

                while (!command.Equals(ExitKeyword, StringComparison.CurrentCultureIgnoreCase))
                {
                    // BOOKMARK: 6.1 scope
                    using (var scope = container.CreateChildContainer())
                    {
                        PrintCompanyPrice(scope, command);
                    }

                    command = Console.ReadLine();
                }
            }
        }

        private static void PrintCompanyPrice(IUnityContainer scope, string companyName)
        {
            // Doesn't make much sense there, just an example
            using (var transaction = scope.Resolve<IDbConnection>().BeginTransaction())
            // Note that it only works because IDbConnection is registered using HierarchicalLifetimeManager,
            // so only one instance of IDbConnection can exist within the scope.
            // Another option is to manually create and dispose the connection
            // and call RegisterInstance<IDbConnection> to register it in the container.
            {
                var company = scope.Resolve<ICompanyRepository>().FindCompany(companyName);
                if (company != null)
                {
                    var price = scope.Resolve<ICompanyPriceProvider>().GetPrice(company.Id);
                    Console.WriteLine(price.ToStringFormat());
                }
                else
                {
                    Console.WriteLine($"Company \"{companyName}\" is not found.");
                }

                transaction.Commit();
            }
        }

        private static IUnityContainer BuildContainer(string[] args)
        {
            // BOOKMARK: 5.2 Unity
            var container = new UnityContainer();
            try
            {
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddCommandLine(args);
                var configuration = configBuilder.Build();

                // Configure options
                container
                    .AddExtension(new OptionsExtension())
                    .Configure<PriceProviderOptions>(configuration.GetSection("PriceProvider"));

                // A new instance per dependency
                container.RegisterType<ICompanyPriceProvider, CompanyPriceProvider>();
                container.RegisterType<ICompanyService, CompanyService>();
                container.RegisterType<IPriceProvider, PriceProvider>();
                container.RegisterType<ICompanyRepository, DbCompanyRepository>();
                container.RegisterType<IStockRepository, DbStockRepository>();

                // A single instance per a child container (scope)
                container.RegisterType<IDbConnection, FakeDbConnection>(new HierarchicalLifetimeManager());
                // See difference with HierarchicalLifetimeManager - it's actually singleton
                //container.RegisterType<IDbConnection, FakeDbConnection>(new ContainerControlledLifetimeManager());
                // Note, that, unlike in other containers, you MUST register using ContainerControlledTransientManager,
                // ContainerControlledLifetimeManager or HierarchicalLifetimeManager if you want Unity to dispose your instances.
                // See https://github.com/unitycontainer/unity/wiki/Unity-Lifetime-Managers for more information

                // Single instance
                container.RegisterSingleton<ILogger, ConsoleLogger>();
            }
            catch
            {
                container.Dispose();
                throw;
            }

            return container;
        }
    }
}