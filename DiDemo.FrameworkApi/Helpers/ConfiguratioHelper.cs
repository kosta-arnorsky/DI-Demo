using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DiDemo.FrameworkApi.Helpers
{
    public static class ConfiguratioHelper
    {
        public static IConfiguration FromAppSettings()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(GetAppSettings())
                .Build();
        }

        private static IEnumerable<KeyValuePair<string, string>> GetAppSettings()
        {
            NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            return appSettings.AllKeys.ToDictionary(k => k, k => appSettings[k]);
        }
    }
}