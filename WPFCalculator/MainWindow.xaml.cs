using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum CalcOperator
        {
            None = 0,
            Addition = 1,
            Substraction = 2,
            Multiplication = 3,
            Division = 4,
            Power = 5,
            Modulo = 6
        }

        enum SingleCalcOperation
        {
            Factorial = 1,
            Sine = 2,
            Cosine = 3,
            Tangent = 4,
            Logarithm = 5,
            SquareRoot = 6,
            TenToPowerOf = 7,
            Squared = 8,
            Exponential = 9
        }

        const string defaultString = "0";
        string numberOneBuffer;
        string numberTwoBuffer;
        double numberDiff;
        bool wasSolvedBefore;
        bool wasLastOperationInvalid;
        CalcOperator currentCalcOperator;

        private void Reset()
        {
            inputTextBox.Text = defaultString;
            numberOneBuffer = String.Empty;
            numberTwoBuffer = String.Empty;
            numberDiff = 0.0;
            wasSolvedBefore = false;
            wasLastOperationInvalid = false;
            currentCalcOperator = CalcOperator.None;
        }

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));

            Reset();
        }

        private void AddChars(string s, bool replace = false)
        {
            if (!wasLastOperationInvalid)
            {
                if (replace || numberOneBuffer == "0")
                {
                    numberOneBuffer = s;
                }
                else
                {
                    numberOneBuffer += s;
                }
                inputTextBox.Text = numberOneBuffer;
            }
        }

        private void One_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("1");
        }

        private void Two_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("2");
        }

        private void Three_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("3");
        }

        private void Four_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("4");
        }

        private void Five_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("5");
        }

        private void Six_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("6");
        }

        private void Seven_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("7");
        }

        private void Eight_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("8");
        }

        private void Nine_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars("9");
        }

        private void Zero_OnClick(object sender, RoutedEventArgs e)
        {
            if (numberOneBuffer != "0")
            {
                AddChars("0");
            }
        }

        private void Decipoint_OnClick(object sender, RoutedEventArgs e)
        {
            if (!wasLastOperationInvalid)
            {
                if (numberOneBuffer == String.Empty)
                {
                    numberOneBuffer = "0";
                }
                if (!numberOneBuffer.Contains(','))
                {
                    numberOneBuffer += ",";
                }
            }
        }

        private string CheckDecimal(string s)
        {
            if (s[s.Length - 1] == ',')
            {
                s += "0";
            }
            return s;
        }

        private double OperateTwoNumbers()
        {
            if (numberOneBuffer == String.Empty)
            {
                numberOneBuffer = "0";
                if (currentCalcOperator != CalcOperator.None)
                {
                    numberOneBuffer = inputTextBox.Text;
                }
            }
            else if (numberOneBuffer != inputTextBox.Text)
            {
                numberOneBuffer = inputTextBox.Text;
            }
            CheckDecimal(numberOneBuffer);
            if (numberTwoBuffer == String.Empty)
            {
                numberTwoBuffer = "0";
            }
            CheckDecimal(numberTwoBuffer);

            double numberOne = Convert.ToDouble(numberOneBuffer);
            double numberTwo = Convert.ToDouble(numberTwoBuffer);
            if (numberDiff != 0.0)
            {
                numberTwo = numberDiff;
            }
            else if (numberDiff == 0.0 && numberTwo != 0 && currentCalcOperator != CalcOperator.None)
            {
                numberDiff = numberOne;

                numberOne = numberTwo;
                numberTwo = numberDiff;
            }

            double result;
            switch (currentCalcOperator)
            {
                case CalcOperator.Addition:
                    result = numberOne + numberTwo;
                    break;
                case CalcOperator.Substraction:
                    result = numberOne - numberTwo;
                    break;
                case CalcOperator.Multiplication:
                    result = numberOne * numberTwo;
                    break;
                case CalcOperator.Division:
                    result = numberOne / numberTwo;
                    break;
                case CalcOperator.Power:
                    result = Math.Pow(numberOne, numberTwo);
                    break;
                case CalcOperator.Modulo:
                    result = numberOne % numberTwo;
                    break;
                default:
                    result = numberOne;
                    break;
            }

            return result;
        }

        private string ReturnProperAnswerBasedIfInfinityOrNaN(double answer)
        {
            string answerString;
            if (Double.IsInfinity(answer) || Double.IsNaN(answer))
            {
                answerString = "Niedozwolona operacja.";
                wasLastOperationInvalid = true;
            }
            else
            {
                answerString = answer.ToString();
            }
            return answerString;
        }

        private void PerformOperation()
        {
            if (!wasLastOperationInvalid)
            {
                if (numberOneBuffer == String.Empty && currentCalcOperator == CalcOperator.None)
                {
                    numberOneBuffer = numberTwoBuffer;
                }

                double answer = OperateTwoNumbers();
                string answerString = ReturnProperAnswerBasedIfInfinityOrNaN(answer);

                inputTextBox.Text = answerString;
                numberTwoBuffer = numberOneBuffer;
                numberOneBuffer = String.Empty;
            }
        }

        private void DoAllOperationThings()
        {
            if (wasSolvedBefore)
            {
                numberDiff = 0.0;
                currentCalcOperator = CalcOperator.None;
                wasSolvedBefore = false;
            }
            PerformOperation();
        }

        public double CalculateFactorial(double number)
        {
            double result = 1;
            while (number > 1)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }

        private double OperateOneNumber(SingleCalcOperation currentSingleCalcOperation)
        {
            if (numberOneBuffer == String.Empty)
            {
                numberOneBuffer = "0";
                if (currentCalcOperator != CalcOperator.None)
                {
                    numberOneBuffer = inputTextBox.Text;
                }
            }
            else if (numberOneBuffer != inputTextBox.Text)
            {
                numberOneBuffer = inputTextBox.Text;
            }
            CheckDecimal(numberOneBuffer);

            double numberOne = Convert.ToDouble(numberOneBuffer);

            double result;
            switch (currentSingleCalcOperation)
            {
                case SingleCalcOperation.Factorial:
                    if (numberOne < 0)
                    {
                        result = Double.NaN;
                    }
                    else
                    {
                        double roundedNumberOne = Math.Floor(numberOne);
                        result = CalculateFactorial(roundedNumberOne);
                    }
                    break;
                case SingleCalcOperation.Sine:
                    result = Math.Sin(numberOne);
                    break;
                case SingleCalcOperation.Cosine:
                    result = Math.Cos(numberOne);
                    break;
                case SingleCalcOperation.Tangent:
                    result = Math.Tan(numberOne);
                    break;
                case SingleCalcOperation.Logarithm:
                    result = Math.Log(numberOne);
                    break;
                case SingleCalcOperation.SquareRoot:
                    result = Math.Sqrt(numberOne);
                    break;
                case SingleCalcOperation.TenToPowerOf:
                    result = Math.Pow(10, numberOne);
                    break;
                case SingleCalcOperation.Squared:
                    result = Math.Pow(numberOne, 2);
                    break;
                case SingleCalcOperation.Exponential:
                    result = Math.Exp(numberOne);
                    break;
                default:
                    result = numberOne;
                    break;
            }

            return result;
        }

        private void PerformSingleOperation(SingleCalcOperation currentSingleCalcOperation)
        {
            wasSolvedBefore = true;

            if (!wasLastOperationInvalid)
            {
                double answer = OperateOneNumber(currentSingleCalcOperation);
                string answerString = ReturnProperAnswerBasedIfInfinityOrNaN(answer);

                numberOneBuffer = answerString;
                inputTextBox.Text = numberOneBuffer;
            }
        }

        private void Enter_OnClick(object sender, RoutedEventArgs e)
        {
            wasSolvedBefore = true;
            PerformOperation();
        }

        private void Sign_OnClick(object sender, RoutedEventArgs e)
        {
            if (numberOneBuffer.Length > 0 && !wasLastOperationInvalid)
            {
                if (numberOneBuffer[0] == '-')
                {
                    numberOneBuffer = numberOneBuffer.Substring(1, numberOneBuffer.Length - 1);
                }
                else
                {
                    numberOneBuffer = "-" + numberOneBuffer;
                }
                inputTextBox.Text = numberOneBuffer;
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Addition;
        }

        private void Minus_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Substraction;
        }

        private void Multiple_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Multiplication;
        }

        private void Divide_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Division;
        }

        private void Factorial_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Factorial);
        }

        private void Pi_OnClick(object sender, RoutedEventArgs e)
        {
            AddChars(Math.PI.ToString(), true);
        }

        private void Sine_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Sine);
        }

        private void Cosine_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Cosine);
        }

        private void Tangent_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Tangent);
        }

        private void Logarithm_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Logarithm);
        }

        private void SquareRoot_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.SquareRoot);
        }

        private void TenToPowerOf_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.TenToPowerOf);
        }

        private void Squared_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Squared);
        }

        private void ToPowerOf_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Power;
        }

        private void Exponential_OnClick(object sender, RoutedEventArgs e)
        {
            PerformSingleOperation(SingleCalcOperation.Exponential);
        }

        private void Modulo_OnClick(object sender, RoutedEventArgs e)
        {
            DoAllOperationThings();
            currentCalcOperator = CalcOperator.Modulo;
        }
        
        private void Clean_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void ClearEntry_OnClick(object sender, RoutedEventArgs e)
        {
            if (!wasLastOperationInvalid)
            {
                numberOneBuffer = String.Empty;
                inputTextBox.Text = defaultString;
            }
        }
    }
}
