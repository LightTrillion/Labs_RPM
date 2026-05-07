using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using laba_9_MVVM.Services;
using laba_9_MVVM.ViewModels;
using laba_9_MVVM.Views;

namespace laba_9_MVVM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Создаём коллекцию сервисов
            var services = new ServiceCollection();

            // 2. Регистрируем сервис диалогов как Singleton – один экземпляр на всё приложение.
            //    Время жизни Singleton выбрано потому, что DialogService не хранит состояние,
            //    потокобезопасен и может использоваться многократно без создания новых объектов.
            services.AddSingleton<IDialogService, DialogService>();

            // 3. Регистрируем MainViewModel как Transient – каждый запрос создаёт новый экземпляр.
            //    Transient допустим, т.к. время жизни ViewModel совпадает с окном, а окно мы создаём один раз.
            //    Но возможна и регистрация как Singleton, если гарантируется единственное окно.
            services.AddTransient<MainViewModel>();

            // 4. Главное окно регистрируем как Singleton, так как оно должно быть только одно.
            services.AddSingleton<MainWindow>();

            // 5. Строим провайдер служб
            var serviceProvider = services.BuildServiceProvider();

            // 6. Получаем главное окно (DI автоматически разрешит его зависимость — MainViewModel)
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            // 7. Отображаем окно
            mainWindow.Show();
        }
    }
}