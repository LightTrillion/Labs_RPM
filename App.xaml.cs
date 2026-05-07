using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using laba_9_MVVM.Services;
using laba_9_MVVM.ViewModels;
using laba_9_MVVM.Views;

namespace laba_9_MVVM
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Настройка DI-контейнера
            var services = new ServiceCollection();

            // Сервис навигации – один на всё приложение (Singleton)
            services.AddSingleton<INavigationService, NavigationService>();
            // Главная ViewModel – единственный экземпляр, контролирует Shell (Singleton)
            services.AddSingleton<MainWindowViewModel>();

            // Остальные ViewModels создаются каждый раз при навигации (Transient)
            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<AboutViewModel>();

            _serviceProvider = services.BuildServiceProvider();

            // Получаем ShellViewModel
            var mainViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            var mainWindow = new MainWindow(mainViewModel);

            // После загрузки окна выполняем начальную навигацию
            mainWindow.Loaded += (s, args) =>
            {
                mainViewModel.Initialize();
            };

            mainWindow.Show();
        }
    }
}