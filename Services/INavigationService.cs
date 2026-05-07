using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_9_MVVM.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : class;
    }
}
