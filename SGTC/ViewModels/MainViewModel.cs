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
using SGTC.Models;
using System.Windows;

namespace SGTC.ViewModels
{
    public enum LengthUnitType
    {
        Millimeter,
        Centimeter,
        Inch
    }

    public class MainViewModel : ObservableObject
    {
        private readonly ICoilCalculator _calculator;
        private readonly ICoilDataService _dataService;

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

        public ObservableCollection<LengthUnitType> LengthUnits { get; set; }

        public LengthUnitType SelectedLengthUnit
        {
            get => _dataService.Parameters.LengthUnitType;
            set
            {
                _dataService.Parameters.LengthUnitType = value;
                OnPropertyChanged();
                PrimaryViewModel.UpdateLengthParameters();
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

        public MainViewModel(
            PrimaryCircuitViewModel primaryViewModel,
            SecondaryCircuitViewModel secondaryViewModel,
            TopLoadViewModel topLoadViewModel,
            ResultViewModel resultViewModel,
            ICoilCalculator calculator,
            ICoilDataService dataService)
        {
            PrimaryViewModel = primaryViewModel;
            SecondaryViewModel = secondaryViewModel;
            TopLoadViewModel = topLoadViewModel;
            ResultViewModel = resultViewModel;

            _calculator = calculator;
            _dataService = dataService;

            LengthUnits = new ObservableCollection<LengthUnitType>
            {
                LengthUnitType.Millimeter,
                LengthUnitType.Centimeter,
                LengthUnitType.Inch
            };

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
                //if (!ValidateAllViewModels())
                if (!PrimaryViewModel.IsFormValid)
                {
                    //PrimaryViewModel.ErrorMessage = "Please fix all errors before proceeding!";
                    MessageBox.Show("Please fix all errors before proceeding Primary!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!SecondaryViewModel.IsFormValid)
                {
                    MessageBox.Show("Please fix all errors before proceeding Secondary!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!TopLoadViewModel.IsFormValid)
                {
                    MessageBox.Show("Please fix all errors before proceeding Top!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _dataService.Results = _calculator.CalculatePrimary(_dataService.Parameters, _dataService.Results);
                _dataService.Results = _calculator.CalculateSecondary(_dataService.Parameters, _dataService.Results);

                CurrentView = ResultViewModel;
            });

        }

        


        private bool ValidateAllViewModels()
        {
            return PrimaryViewModel.IsFormValid && SecondaryViewModel.IsFormValid && TopLoadViewModel.IsFormValid;
        }
    }
}
