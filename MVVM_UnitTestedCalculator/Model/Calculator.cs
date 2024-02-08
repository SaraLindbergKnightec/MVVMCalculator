using System.Collections.Generic;
using System.Windows.Controls;
using System;

namespace MVVM_UnitTestedCalculator.Model
{
    interface IBinaryOperator
    {
        char Symbol { get; } // Symbol auto-property generates the private field for us
        decimal PerformCalculation(decimal num1, decimal num2); //interface method doesn't have a body
        decimal? FindOperatorIndex(string expression); // int? means that int or null can be returned
    }

    class Plus : IBinaryOperator // this class uses the IBinaryOperator interface
    {
        public char Symbol => '+'; // read-only property
        public decimal PerformCalculation(decimal num1, decimal num2) // Method to use on an instance of Plus with integers as parameters
        {
            return num1 + num2;
        }

        public decimal? FindOperatorIndex(string expression) // FindOperatorIndex method to use on an instance of Plus - null can be returned and a string is input
        {
            int operatorIndex = expression.LastIndexOf(Symbol); //looking for the last index of the symbol
            if (operatorIndex <= 0)
            {
                return null;
            }
            return new Calculator().UserInputHandler(this, expression, operatorIndex);// this = self i Python
            // anropar Operatorhandler med Plus som IBinaryOperator om det finns '+' i uttrycket
        }
    }

    class Minus : IBinaryOperator
    {
        public char Symbol => '-';
        public decimal PerformCalculation(decimal num1, decimal num2)
        {
            return num1 - num2;
        }

        public decimal? FindOperatorIndex(string expression)
        {
            int operatorIndex = expression.LastIndexOf(Symbol);
            if (operatorIndex <= 0)
            { return null; }

            switch (expression[operatorIndex - 1])
            {
                case '-':
                    return new Calculator().UserInputHandler(this, expression, operatorIndex - 1);
                case '*':
                    return new Calculator().UserInputHandler(new Multiplication(), expression, operatorIndex - 1);
                case '/':
                    return new Calculator().UserInputHandler(new Division(), expression, operatorIndex - 1);
                default:
                    return new Calculator().UserInputHandler(this, expression, operatorIndex);

            }
        }
    }

    class Multiplication : IBinaryOperator
    {
        public char Symbol => '*';

        public decimal PerformCalculation(decimal num1, decimal num2)
        {
            return num1 * num2;
        }

        public decimal? FindOperatorIndex(string expression)
        {
            int operatorIndex = expression.LastIndexOf(Symbol);
            if (operatorIndex <= 0)
            {
                return null;
            }
            return new Calculator().UserInputHandler(this, expression, operatorIndex);
        }
    }

    class Division : IBinaryOperator
    {
        public char Symbol => '/';

        public decimal PerformCalculation(decimal num1, decimal num2)
        {
            try
            {
                return num1 / num2;
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException();
            }

        }

        public decimal? FindOperatorIndex(string expression)
        {
            int operatorIndex = expression.LastIndexOf(Symbol);
            if (operatorIndex <= 0)
            {
                return null;
            }
            return new Calculator().UserInputHandler(this, expression, operatorIndex);
        }
    }


    public class Calculator
    {
        private string? calculatedResult;
        public Calculator()
        {
            UserInput = string.Empty;
            calculatedResult = string.Empty;
        }
        public string? UserInput { get; set; }
        public string? CalculatedResult { get { return calculatedResult; } }

        public void CalculateResult()
        {
            decimal? result;
            result = Calculate(UserInput);
            calculatedResult = result.ToString();
        }

        public decimal Calculate(string expression)
        {
            expression = expression.Trim().Replace(" ", "");
            expression = expression.ToLower();
            decimal? operatorIndex;

            foreach (KeyValuePair<char, IBinaryOperator> kvp in operators)
            {
                operatorIndex = kvp.Value.FindOperatorIndex(expression);
                Console.WriteLine($"{kvp.Key} {operatorIndex}");

                if (operatorIndex != null)
                {
                    return operatorIndex.Value; // returns the value - not null
                }
            }
            return Decimal.Parse(expression); // basfallet vid rekursion
        }

        internal decimal UserInputHandler(IBinaryOperator op, string expression, int operatorIndex)
        {
            var string1 = expression.Substring(0, operatorIndex);
            var string2 = expression.Substring(operatorIndex + 1);
            var num1 = Calculate(string1);
            var num2 = Calculate(string2);
            return op.PerformCalculation(num1, num2);
        }

        private static readonly IDictionary<char, IBinaryOperator> operators;
        static Calculator() // static initializer (will only be run once)
        {
            operators = new Dictionary<char, IBinaryOperator>
            {
                // Add Key, Value to the Dictionary
                { '+', new Plus() },
                { '-', new Minus() },
                { '*', new Multiplication() },
                { '/', new Division() }
            };
        }
    }
}


