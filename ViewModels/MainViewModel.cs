using laba_9_MVVM.Models;
using laba_9_MVVM.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace laba_9_MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // Сервис диалогов – внедряется через DI
        private readonly IDialogService _dialogService;

        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        // Конструктор с внедрением зависимости IDialogService
        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<object>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            // Проверка на дубликат по номеру телефона
            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером телефона уже существует.");
                return;
            }

            Contact newCont = new Contact(Name, Phone);
            Contacts.Add(newCont);

            // Информационное сообщение об успешном добавлении
            _dialogService.ShowInfo($"Контакт \"{Name}\" успешно добавлен.");

            // Очистка полей ввода
            Name = string.Empty;
            Phone = string.Empty;
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact(object? param)
        {
            if (SelectedContact == null) return;

            // Запрос подтверждения удаления
            bool confirmed = _dialogService.ShowConfirmation(
                $"Вы действительно хотите удалить контакт \"{SelectedContact.Name}\"?");

            if (confirmed)
            {
                Contacts.Remove(SelectedContact);
            }
        }

        private bool CanDeleteContact(object? param)
        {
            return SelectedContact != null;
        }
    }
}