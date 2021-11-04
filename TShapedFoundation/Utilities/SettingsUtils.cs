using Microsoft.Extensions.Configuration;
using System.IO;

namespace TShapedFoundation.Utilities
{
    public class SettingsUtils
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static T GetApplicationConfiguration<T>(string sectionName)
            where T: new()
        {
            var configuration = new T();

            var iConfig = GetIConfigurationRoot();

            iConfig
                .GetSection(sectionName)
                .Bind(configuration);

            return configuration;
        }
    }
}
