using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_9_MVVM.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        // Здесь могут быть свойства, например, версия, название.
        public string AppTitle => "Телефонная книга";
        public string Version => "1.0.0";
    }
}
