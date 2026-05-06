using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laba_9_MVVM.ViewModels;

namespace laba_9_MVVM.Models
{
    public class Contact : ObservableObject
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public bool Validate()
        {

            bool isNameValid = !string.IsNullOrWhiteSpace(Name);

            bool isPhoneValid = !string.IsNullOrWhiteSpace(Phone) &&
                ((Phone.StartsWith("+7") && Phone.Length == 12) ||
                (Phone.Length == 10));

            return (isNameValid && isPhoneValid);
        }
    }
}