using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SGTC.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateCommand { get; set; }

        private PrimaryCircuitViewModel primaryViewModel;
        private SecondaryCircuitViewModel secondaryViewModel;
        private TopLoadViewModel topLoadViewModel;
        private ResultViewModel resultViewModel;

        public MainViewModel()
        {
            primaryViewModel = new PrimaryCircuitViewModel();
            secondaryViewModel = new SecondaryCircuitViewModel();
            topLoadViewModel = new TopLoadViewModel();
            resultViewModel = new ResultViewModel();

            CurrentView = primaryViewModel;

            NavigateCommand = new RelayCommand(Navigate);

        }


        private void Navigate(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Primary":
                    CurrentView = primaryViewModel;
                    break;
                case "Secondary":
                    CurrentView = secondaryViewModel;
                    break;
                case "TopLoad":
                    CurrentView = topLoadViewModel;
                    break;
                case "Result":
                    // CalculateResults();
                    CurrentView = resultViewModel;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;

            public RelayCommand(Action<object> execute)
            {
                _execute = execute;
            }

            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute(parameter);
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
