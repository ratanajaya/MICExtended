using MICExtended.Abstraction;
using MICExtended.Service;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MICExtended
{
    internal static class Program
    {
        private static IServiceProvider? ServiceProvider { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var services = new ServiceCollection();
            services.AddTransient<AppLogic>();
            services.AddTransient<IIoWapper, IoWrapper>();
            services.AddTransient<ImageCompressor>();
            services.AddTransient<Form1>();
            services.AddSingleton<ILogger>(new LoggerConfiguration()
                        .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log/log-.txt"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                        .CreateLogger());

            ServiceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetService<Form1>());
        }
    }
}