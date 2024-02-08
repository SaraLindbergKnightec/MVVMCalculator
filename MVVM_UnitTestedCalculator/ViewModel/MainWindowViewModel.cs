using MVVM_UnitTestedCalculator.Model;
using MVVM_UnitTestedCalculator.MVVM;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

//TODO - fix Enter Key

namespace MVVM_UnitTestedCalculator.ViewModel
{ // we replace our code behind (MainWindow.xaml.cs) with this ViewModel
    internal class MainWindowViewModel : ViewModelBase
    {
        private Calculator calculator;

        public RelayCommand SevenCommand => new RelayCommand(execute => Seven());
        public RelayCommand EightCommand => new RelayCommand(execute => Eight());
        public RelayCommand NineCommand => new RelayCommand(execute => Nine());
        public RelayCommand DivCommand => new RelayCommand(execute => Div());
        public RelayCommand FourCommand => new RelayCommand(execute => Four());
        public RelayCommand FiveCommand => new RelayCommand(execute => Five());
        public RelayCommand SixCommand => new RelayCommand(execute => Six());
        public RelayCommand MultCommand => new RelayCommand(execute => Mult());
        public RelayCommand OneCommand => new RelayCommand(execute => One());
        public RelayCommand TwoCommand => new RelayCommand(execute => Two());
        public RelayCommand ThreeCommand => new RelayCommand(execute => Three());
        public RelayCommand MinusCommand => new RelayCommand(execute => Minus());
        public RelayCommand ZeroCommand => new RelayCommand(execute => Zero());
        public RelayCommand ClearCommand => new RelayCommand(execute => Clear()); 
        public RelayCommand EqualtoCommand => new RelayCommand(execute => Equalto(), canExecute => !string.IsNullOrEmpty(FullExpression)); // TODO - fix this Button
        public RelayCommand PlusCommand => new RelayCommand(execute => Plus());

        public MainWindowViewModel() 
        {
            calculator = new Calculator(); // instantiate new Calculator
            UserInput = string.Empty;
            //fullExpression = "0";
        }

        // property which calls UserInput in the class Calculator
        public string? UserInput
        {
            get { return calculator.UserInput; }
            set { calculator.UserInput = value; }
        }

        // property which calls CalculatedResult in the class Calculator
        public string? CalculatedResult
        {
            get { return calculator.CalculatedResult; }
        }

        // property which calls the OnPropertyChanged method in ViewModelBase
        // this code is bound to the Bindings FullExpression in the .xaml file
        private string fullExpression;
        public string FullExpression
        {
            get { return fullExpression; }
            set
            {
                fullExpression = value;
                OnPropertyChanged();
            }

        }

        private string? result;
        public string? Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }



        private void Seven()
        {
            Result = string.Empty;
            FullExpression += "7";
        }
        private void Eight()
        {
            Result = string.Empty;
            FullExpression += "8";
        }
        private void Nine()
        {
            Result = string.Empty;
            FullExpression += "9";
        }
        private void Div()
        {
            Result = string.Empty;
            FullExpression += "/";

        }
        private void Four()
        {
            Result = string.Empty;
            FullExpression += "4";
        }

        private void Five()
        {
            Result = string.Empty;
            FullExpression += "5";
        }

        private void Six()
        {
            Result = string.Empty;
            FullExpression += "6";
        }

        private void Mult()
        {
            Result = string.Empty;
            FullExpression += "*";

        }
        private void One()
        {
            Result = string.Empty;
            FullExpression += "1";
        }

        private void Two()
        {
            Result = string.Empty;
            FullExpression += "2";
        }

        private void Three()
        {
            Result = string.Empty;
            FullExpression += "3";
        }
        private void Minus()
        {
            Result = string.Empty;
            FullExpression += "-";
        }

        private void Zero()
        {
            Result = string.Empty;
            FullExpression += "0";
        }
        private void Clear()
        {
            FullExpression = string.Empty;
            Result = string.Empty;
        }

        private void Equalto()
        {
            try
            {
                UserInput = fullExpression; // set the expression as UserInput 
                calculator.CalculateResult(); // call method in class Calculator
                FullExpression = CalculatedResult;
            }
            catch (FormatException)
            {
                FullExpression = "Wrong input format";
                return;
            }
            catch (DivideByZeroException)
            {
                FullExpression = "Division by zero is not allowed";
                return;
            }

        }

        private void Plus()
        {
            FullExpression += "+";
        }
    }
}
