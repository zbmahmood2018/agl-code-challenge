using System.IO;
using agl_code_challenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace agl_code_challenge.Configuration
{
    public static class IocConfig
    {
        public static void Configuration(IServiceCollection serviceCollection)
        {
            //add logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // add services
            serviceCollection.AddTransient<IHttpClient, HttpClientService>();
            serviceCollection.AddTransient<IParser, ParseService>();
            serviceCollection.AddTransient<IPeopleService, PeopleService>();
        }
    }
}