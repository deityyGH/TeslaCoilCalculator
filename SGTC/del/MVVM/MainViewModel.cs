using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SGTC.del.MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _currentInput = "0";
        public string CurrentInput
        {
            get => _currentInput;
            set
            {
                if (_currentInput != value)
                {
                    _currentInput = value;
                    OnPropertyChanged(nameof(CurrentInput));
                }
            }
        }

        private string _expression = "";
        public string Expression
        {
            get => _expression;
            set
            {
                if (_expression != value)
                {
                    _expression = value;
                    OnPropertyChanged(nameof(Expression));
                }
            }
        }

        private ObservableCollection<HistoryItem> _history = new ObservableCollection<HistoryItem>();
        public ObservableCollection<HistoryItem> History
        {
            get => _history;
            set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
            }
        }

        // Calculator state
        private bool _isNewCalculation = true;
        private double _lastResult = 0;
        private string _lastOperation = "";

        #endregion

        #region Commands

        public ICommand NumberCommand { get; private set; }
        public ICommand OperationCommand { get; private set; }
        public ICommand EqualsCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand ClearEntryCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand DecimalCommand { get; private set; }
        public ICommand NegateCommand { get; private set; }
        public ICommand HistoryItemCommand { get; private set; }
        public ICommand ClearHistoryCommand { get; private set; }

        #endregion

        public MainViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NumberCommand = new RelayCommand<string>(AddDigit);
            OperationCommand = new RelayCommand<string>(PerformOperation);
            EqualsCommand = new RelayCommand(PerformCalculation);
            ClearCommand = new RelayCommand(Clear);
            ClearEntryCommand = new RelayCommand(ClearEntry);
            BackspaceCommand = new RelayCommand(Backspace);
            DecimalCommand = new RelayCommand(AddDecimal);
            NegateCommand = new RelayCommand(ToggleNegative);
            HistoryItemCommand = new RelayCommand<HistoryItem>(UseHistoryItem);
            ClearHistoryCommand = new RelayCommand(ClearHistory);
        }

        #region Command Methods

        private void AddDigit(string digit)
        {
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

        private void PerformOperation(string operation)
        {
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

        private void PerformCalculation()
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
                    {
                        // In MVVM we would handle this via a message service
                        // For simplicity, we'll just reset and set an error message
                        CurrentInput = "Error";
                        _isNewCalculation = true;
                        return;
                    }
                    break;
            }

            CurrentInput = _lastResult.ToString();
        }

        private void Clear()
        {
            CurrentInput = "0";
            Expression = "";
            _lastOperation = "";
            _lastResult = 0;
            _isNewCalculation = true;
        }

        private void ClearEntry()
        {
            CurrentInput = "0";
        }

        private void Backspace()
        {
            if (CurrentInput.Length > 1)
                CurrentInput = CurrentInput.Substring(0, CurrentInput.Length - 1);
            else
                CurrentInput = "0";
        }

        private void AddDecimal()
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

        private void ToggleNegative()
        {
            if (CurrentInput != "0")
            {
                if (CurrentInput.StartsWith("-"))
                    CurrentInput = CurrentInput.Substring(1);
                else
                    CurrentInput = "-" + CurrentInput;
            }
        }

        private void UseHistoryItem(HistoryItem item)
        {
            if (item != null)
            {
                CurrentInput = item.Result;
                _isNewCalculation = true;
            }
        }

        private void ClearHistory()
        {
            History.Clear();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class HistoryItem
    {
        public string Expression { get; set; }
        public string Result { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
