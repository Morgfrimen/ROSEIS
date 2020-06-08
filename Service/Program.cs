using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service
{
    public class Program
    {
        public static string NameJSONSettings { get; } = "appsettings.json";

        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = default;
            try
            {
                hostBuilder = CreateHostBuilder(args);
                hostBuilder.Build().Run();
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        //TODO: Логирование в файл
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile(NameJSONSettings);
#if DEBUG
                    config.AddCommandLine(args);
#endif
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddSingleton<SettingApp>();
                });
    }
}
