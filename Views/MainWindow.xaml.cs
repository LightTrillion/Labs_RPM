using System.Windows;
using laba_9_MVVM.ViewModels;

namespace laba_9_MVVM.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}