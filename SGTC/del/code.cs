using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGTC.del
{
    public partial class code : Window, INotifyPropertyChanged
    {
        private string _currentInput = "0";
        private string _expression = "";
        private bool _isNewCalculation = true;
        private double _lastResult = 0;
        private string _lastOperation = "";
        private ObservableCollection<HistoryItem> _history = new ObservableCollection<HistoryItem>();

        // Properties with INotifyPropertyChanged implementation
        public string CurrentInput
        {
            get { return _currentInput; }
            set
            {
                if (_currentInput != value)
                {
                    _currentInput = value;
                    OnPropertyChanged(nameof(CurrentInput));
                }
            }
        }

        public string Expression
        {
            get { return _expression; }
            set
            {
                if (_expression != value)
                {
                    _expression = value;
                    OnPropertyChanged(nameof(Expression));
                }
            }
        }

        public ObservableCollection<HistoryItem> History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
            }
        }


        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            //string digit = ((Button)sender).Content.ToString();
            string digit = "digit";
            if (_isNewCalculation)
            {
                CurrentInput = digit;
                _isNewCalculation = false;
            }
            else if (CurrentInput == "0")
            {
                CurrentInput = digit;
            }
            else
            {
                CurrentInput += digit;
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            //string operation = ((Button)sender).Content.ToString();

            string operation = "operation";
            if (!_isNewCalculation)
            {
                if (string.IsNullOrEmpty(_lastOperation))
                {
                    _lastResult = double.Parse(CurrentInput);
                    Expression = $"{CurrentInput} {operation}";
                }
                else
                {
                    Calculate();
                    Expression = $"{_lastResult} {operation}";
                }

                _lastOperation = operation;
                _isNewCalculation = true;
            }
            else if (!string.IsNullOrEmpty(Expression))
            {
                // Change the last operation
                Expression = Expression.Substring(0, Expression.Length - 1) + operation;
                _lastOperation = operation;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastOperation))
            {
                string fullExpression = $"{Expression} {CurrentInput} =";
                Calculate();

                // Add to history
                History.Add(new HistoryItem
                {
                    Expression = fullExpression,
                    Result = _lastResult.ToString()
                });

                Expression = "";
                _lastOperation = "";
                _isNewCalculation = true;
            }
        }

        private void Calculate()
        {
            double currentValue = double.Parse(CurrentInput);

            switch (_lastOperation)
            {
                case "+":
                    _lastResult += currentValue;
                    break;
                case "-":
                    _lastResult -= currentValue;
                    break;
                case "×":
                    _lastResult *= currentValue;
                    break;
                case "÷":
                    if (currentValue != 0)
                        _lastResult /= currentValue;
                    else
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            CurrentInput = _lastResult.ToString();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentInput = "0";
            Expression = "";
            _lastOperation = "";
            _lastResult = 0;
            _isNewCalculation = true;
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentInput = "0";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentInput.Length > 1)
                CurrentInput = CurrentInput.Substring(0, CurrentInput.Length - 1);
            else
                CurrentInput = "0";
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewCalculation)
            {
                CurrentInput = "0.";
                _isNewCalculation = false;
            }
            else if (!CurrentInput.Contains("."))
            {
                CurrentInput += ".";
            }
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentInput != "0")
            {
                if (CurrentInput.StartsWith("-"))
                    CurrentInput = CurrentInput.Substring(1);
                else
                    CurrentInput = "-" + CurrentInput;
            }
        }

        private void HistoryItem_Click(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)sender).DataContext as HistoryItem;
            if (item != null)
            {
                CurrentInput = item.Result;
                _isNewCalculation = true;
            }
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            History.Clear();
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class HistoryItem
    {
        public string Expression { get; set; }
        public string Result { get; set; }
    }
}
