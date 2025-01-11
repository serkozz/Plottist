using Microsoft.Extensions.DependencyInjection;
using Plottist.ViewModels;
using Plottist.Views;
using System.Windows;

namespace Plottist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            // Creation of DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Service provider creation
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Services registration method
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            // ViewModel registration
            RegisterViewModels(services);

            // Services registration
            RegisterServices(services);

            // Windows(views) registration
            RegisterViews(services);

            // Other components registration
            RegisterOtherComponents(services);
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
             services.AddSingleton<MainViewModel>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // services.AddSingleton<IProductService, ProductService>();
        }

        private static void RegisterViews(IServiceCollection services)
        {
             services.AddSingleton<MainView>();
        }

        private static void RegisterOtherComponents(IServiceCollection services)
        {
            // services.AddSingleton<IOtherComponent, OtherComponent>();
        }

        /// <summary>
        /// OnStartup overload for DI use on startup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get mainWindow from DI container
            var mainView = ServiceProvider.GetRequiredService<MainView>();

            // Show it
            mainView.Show();
        }
    }

}
