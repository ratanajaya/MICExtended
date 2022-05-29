using MICExtended.Abstraction;
using MICExtended.Model;
using MICExtended.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MICExtended
{
    internal static class Program
    {
        private static IServiceProvider? ServiceProvider { get; set; }
        // public static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddTransient<AppLogic>();
            services.AddTransient<IIoWapper, IoWrapper>();
            services.AddTransient<ImageCompressor>();
            services.AddTransient<Form1>();
            services.AddSingleton<ILogger>(new LoggerConfiguration()
                        .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log/log-.txt"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                        .CreateLogger());
            services.AddSingleton<AppSettingJson>(new AppSettingJson {
                AllowedExtensions = configuration.GetSection("AllowedExtensions").Get<string[]>(),
                AllowedRawExtensions = configuration.GetSection("AllowedRawExtensions").Get<string[]>()
            });

            ServiceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetService<Form1>());
        }
    }
}