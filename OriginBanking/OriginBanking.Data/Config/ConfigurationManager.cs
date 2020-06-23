using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
namespace OriginBanking.Data.Config
{
    class ConfigurationManager
    {
        public static IConfiguration Configuration { get; }

        static ConfigurationManager()
        {
            var launchSettings = JObject.Parse(File.ReadAllText(
               Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "OriginBanking.Api", "Properties", "launchSettings.json")));

            var environment = launchSettings["profiles"]["OriginBanking.Api"]["environmentVariables"]["ASPNETCORE_ENVIRONMENT"];
            Configuration = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "OriginBanking.Api"))
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.json", optional: true)
           .Build();
        }
    }
}
