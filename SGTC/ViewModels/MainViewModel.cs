using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SGTC.Core;

namespace SGTC.ViewModels
{
    public class MainViewModel : ObservableObject
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

        // Buttons for changing tabs
        public RelayCommand PrimaryViewCommand { get; set; }
        public RelayCommand SecondaryViewCommand { get; set; }
        public RelayCommand TopLoadViewCommand { get; set; }
        public RelayCommand ResultViewCommand { get; set; }

        // View models
        public PrimaryCircuitViewModel PrimaryViewModel { get; set; }
        public SecondaryCircuitViewModel SecondaryViewModel { get; set; }
        public TopLoadViewModel TopLoadViewModel { get; set; }
        public ResultViewModel ResultViewModel { get; set; }

        public MainViewModel()
        {
            PrimaryViewModel = new PrimaryCircuitViewModel();
            SecondaryViewModel = new SecondaryCircuitViewModel();
            TopLoadViewModel = new TopLoadViewModel();
            ResultViewModel = new ResultViewModel();

            CurrentView = PrimaryViewModel;

            PrimaryViewCommand = new RelayCommand(o =>
            {
                CurrentView = PrimaryViewModel;
            });

            SecondaryViewCommand = new RelayCommand(o =>
            {
                CurrentView = SecondaryViewModel;
            });

            TopLoadViewCommand = new RelayCommand(o =>
            {
                CurrentView = TopLoadViewModel;
            });

            ResultViewCommand = new RelayCommand(o =>
            {
                
                //SecondaryCalculator.Run(ResultViewModel);
                PrimaryCalculator.Run();
                //SecondaryCalculator.Run();
                CurrentView = ResultViewModel;
            });

        }



    }
}
