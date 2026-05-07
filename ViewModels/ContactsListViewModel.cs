using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laba_9_MVVM.Models;
using System.Windows.Input;

namespace laba_9_MVVM.ViewModels
{
    public class ContactsListViewModel : BaseViewModel
    {
        // Существующая логика работы с контактами (ObservableCollection<Contact>, команды добавления/удаления и т.д.)
        // Здесь показана минимальная заготовка; перенесите реальную реализацию из лабораторной №10.

        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        public ICommand AddContactCommand { get; }
        public ICommand RemoveContactCommand { get; }

        public ContactsListViewModel()
        {
            // Инициализация команд и данных (взять из предыдущей работы)
        }
    }
}
