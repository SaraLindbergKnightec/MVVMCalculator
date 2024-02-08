using MVVM_UnitTestedCalculator.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace MVVM_UnitTestedCalculator
{
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {// we tell our View that it has another datacontext
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
        }
    }
}
