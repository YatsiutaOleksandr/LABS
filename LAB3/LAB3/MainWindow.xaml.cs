using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace LAB3
{
    public partial class MainWindow : Window
    {
        private double _firstOperand;
        private string _currentOperator = "";
        private bool _isNewInput = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnDigit(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string digit = btn.Content.ToString();

            if (_isNewInput || TxtDisplay.Text == "0")
            {
                TxtDisplay.Text = digit;
                _isNewInput = false;
            }
            else
            {
                TxtDisplay.Text += digit;
            }
        }

        private void OnDecimal(object sender, RoutedEventArgs e)
        {
            if (_isNewInput)
            {
                TxtDisplay.Text = "0.";
                _isNewInput = false;
                return;
            }

            if (!TxtDisplay.Text.Contains("."))
            {
                TxtDisplay.Text += ".";
            }
        }

        private void OnOperator(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (!_isNewInput && _currentOperator != "")
            {
                Calculate();
            }

            _firstOperand = ParseDisplay();
            _currentOperator = btn.Content.ToString();
            TxtExpression.Text = FormatNumber(_firstOperand) + " " + _currentOperator;
            _isNewInput = true;
        }

        private void OnEquals(object sender, RoutedEventArgs e)
        {
            if (_currentOperator == "") return;

            double secondOperand = ParseDisplay();

            TxtExpression.Text = FormatNumber(_firstOperand) + " " +
                                 _currentOperator + " " +
                                 FormatNumber(secondOperand) + " =";

            Calculate();

            _currentOperator = "";
            _isNewInput = true;
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            TxtDisplay.Text = "0";
            TxtExpression.Text = "";
            _firstOperand = 0;
            _currentOperator = "";
            _isNewInput = true;
        }

        private void OnToggleSign(object sender, RoutedEventArgs e)
        {
            double value = ParseDisplay();
            TxtDisplay.Text = FormatNumber(-value);
        }

        private void OnPercent(object sender, RoutedEventArgs e)
        {
            double value = ParseDisplay();
            TxtDisplay.Text = FormatNumber(value / 100.0);
        }

        private void Calculate()
        {
            double secondOperand = ParseDisplay();

            double result;

            switch (_currentOperator)
            {
                case "+":
                    result = _firstOperand + secondOperand;
                    break;
                case "−":
                    result = _firstOperand - secondOperand;
                    break;
                case "×":
                    result = _firstOperand * secondOperand;
                    break;
                case "÷":
                    result = secondOperand != 0 ? _firstOperand / secondOperand : double.NaN;
                    break;
                default:
                    result = secondOperand;
                    break;
            }

            if (double.IsNaN(result))
            {
                TxtDisplay.Text = "Помилка";
                _isNewInput = true;
                return;
            }

            TxtDisplay.Text = FormatNumber(result);
            _firstOperand = result;
        }

        private double ParseDisplay()
        {
            double value;

            if (double.TryParse(
                TxtDisplay.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out value))
            {
                return value;
            }

            return 0;
        }

        private string FormatNumber(double value)
        {
            if (value == Math.Floor(value) &&
                !double.IsInfinity(value) &&
                Math.Abs(value) < 1e15)
            {
                return value.ToString("F0", CultureInfo.InvariantCulture);
            }

            return value.ToString("G10", CultureInfo.InvariantCulture);
        }
    }
}