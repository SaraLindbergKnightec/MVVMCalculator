using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_UnitTestedCalculator.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {// creates a ViewModelBase so that all our ViewModels will have the same interface
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
