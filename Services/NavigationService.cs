using System;
using Microsoft.Extensions.DependencyInjection;
using laba_9_MVVM.ViewModels;

namespace laba_9_MVVM.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            // Запрашиваем экземпляр нужной ViewModel из DI-контейнера.
            // ViewModel регистрируются как Transient, поэтому каждый вызов создаёт новый объект.
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            // Получаем ShellViewModel (Singleton), избегая цикла.
            var shell = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            shell.CurrentViewModel = viewModel;
        }
    }
}