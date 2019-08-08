using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace DiDemo.Api.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddScopedStipulate<TConsumer, TDependency>(this IServiceCollection services, Func<IServiceProvider, TDependency> factory)
            where TDependency : IDisposable
        {
            services.AddScoped(sp => new StipulatedDisposableDependency<TConsumer, TDependency>(factory(sp)));
        }

        public static TDependency GetServiceStipulate<TConsumer, TDependency>(this IServiceProvider serviceProvider)
            where TDependency : IDisposable
        {
            return serviceProvider.GetService<StipulatedDisposableDependency<TConsumer, TDependency>>().Dependency;
        }

        public static void AddScopedSqlConnection<TConsumer>(this IServiceCollection services, Func<IServiceProvider, SqlConnection> factory)
        {
            services.AddScopedStipulate<TConsumer, SqlConnection>(factory);
        }

        public static SqlConnection GetSqlConnection<TConsumer>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServiceStipulate<TConsumer, SqlConnection>();
        }

        private class StipulatedDisposableDependency<TConsumer, TDependency> : IDisposable
            where TDependency : IDisposable
        {
            public StipulatedDisposableDependency(TDependency dependency)
            {
                Dependency = dependency;
            }

            public TDependency Dependency { get; private set; }

            public void Dispose()
            {
                Dependency.Dispose();
            }
        }
    }
}
