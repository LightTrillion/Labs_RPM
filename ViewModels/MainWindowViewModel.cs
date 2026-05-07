using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using laba_9_MVVM.Services;

namespace laba_9_MVVM.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private object _currentViewModel;

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // Начальная навигация будет выполнена явно через метод Initialize()
            // после полной загрузки окна, чтобы избежать deadlock'ов.
        }

        public void Initialize()
        {
            _navigationService.NavigateTo<ContactsListViewModel>();
        }

        // Команды для меню
        private RelayCommand _showContactsCommand;
        public RelayCommand ShowContactsCommand => _showContactsCommand ??= new RelayCommand(_ =>
            _navigationService.NavigateTo<ContactsListViewModel>());

        private RelayCommand _showAboutCommand;
        public RelayCommand ShowAboutCommand => _showAboutCommand ??= new RelayCommand(_ =>
            _navigationService.NavigateTo<AboutViewModel>());

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
