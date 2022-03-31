using MICExtended.Services;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<Form1>();

            ServiceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetService<Form1>());
        }
    }
}