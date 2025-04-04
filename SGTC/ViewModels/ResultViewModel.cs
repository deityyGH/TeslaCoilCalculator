﻿using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGTC.Views;
using System.Windows.Controls;
using System.Windows;


namespace SGTC.ViewModels
{
    public class ResultViewModel : ObservableObject
    {

        private readonly IUnitConverter _unitConverter;
        private readonly ICoilDataService _dataService;
        private readonly ICoilCalculator _calculator;
        public RelayCommand AutoPrimaryCapacitanceCommand { get; set; }
        public RelayCommand AutoPrimaryTurnsCommand { get; set; }

        private ResultGraph _resultGraphWindow;

        private readonly ResultGraphViewModel _resultGraphViewModel;

        public ResultViewModel() { }
        public ResultViewModel(ICoilDataService dataService, ICoilCalculator calculator, IUnitConverter unitConverter, ResultGraphViewModel resultGraphViewModel)
        {
            _dataService = dataService;
            _resultGraphViewModel = resultGraphViewModel;
            _unitConverter = unitConverter;
            _calculator = calculator;

            ResultGraphViewModel.GraphCalculated += OnGraphCalculated;

            AutoPrimaryCapacitanceCommand = new RelayCommand(obj =>
            {
                ShowGraphWindow(CoilType.Primary, GraphType.Capacitance);
            });

            AutoPrimaryTurnsCommand = new RelayCommand(obj =>
            {
                ShowGraphWindow(CoilType.Primary, GraphType.Turns);
            });
        }

        private void ShowGraphWindow(CoilType coilType, GraphType graphType)
        {
            _resultGraphViewModel.SetGraphParameters(coilType, graphType);
            // Check if window already exists
            if (_resultGraphWindow != null)
            {
                // If the window exists but is closed
                if (!_resultGraphWindow.IsVisible)
                {
                    _resultGraphWindow = new ResultGraph(_resultGraphViewModel);
                    _resultGraphWindow.Show();
                }
                else
                {
                    _ = _resultGraphWindow.Activate();
                }
            }
            else
            {
                // First time opening the window
                _resultGraphWindow = new ResultGraph(_resultGraphViewModel);
                _resultGraphWindow.Show();

                _resultGraphWindow.Closed += (sender, args) =>
                {
                    _resultGraphWindow = null;
                };
            }
        }

        private void UpdateAllPrimaryProperties()
        {
            OnPropertyChanged(nameof(PrimaryResonance));
            OnPropertyChanged(nameof(PrimaryCapacitance));
            OnPropertyChanged(nameof(PrimaryTurns));
            OnPropertyChanged(nameof(PrimaryInductance));
            OnPropertyChanged(nameof(PrimaryXc));
            OnPropertyChanged(nameof(PrimaryXl));
            OnPropertyChanged(nameof(PrimaryWireLength));
            OnPropertyChanged(nameof(PrimaryWireWeight));
            OnPropertyChanged(nameof(PrimaryCoilHeight));

            OnPropertyChanged(nameof(PrimaryResonanceDisplay));
            OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
            OnPropertyChanged(nameof(PrimaryInductanceDisplay));
            OnPropertyChanged(nameof(PrimaryXcDisplay));
            OnPropertyChanged(nameof(PrimaryXlDisplay));
            OnPropertyChanged(nameof(PrimaryWireLengthDisplay));
            OnPropertyChanged(nameof(PrimaryWireWeightDisplay));
            OnPropertyChanged(nameof(PrimaryCoilHeightDisplay));
        }

        private void OnGraphCalculated(object sender, GraphCalculatedEventArgs e)
        {
            if (e.CoilType == CoilType.Primary)
            {
                switch (e.GraphType)
                {
                    case GraphType.Capacitance:
                        OptimalCapacitance = e.OptimalValue;
                        break;
                    case GraphType.Turns:
                        OptimalTurns = e.OptimalValue;

                        break;
                    case GraphType.TopLoad:
                        break;
                    case GraphType.CoreDiameter:
                        break;
                    default:
                        break;
                }
            }
        }

        private double _optimalCapacitance;
        public double OptimalCapacitance
        {
            get => _optimalCapacitance;
            set
            {
                _optimalCapacitance = value;
                _dataService.Parameters.PrimaryCapacitance = value;
                //_dataService.Parameters.PrimaryCapacitorAmount = 1;
                _dataService.Results = _calculator.CalculatePrimary(_dataService.Parameters, _dataService.Results);
                UpdateAllPrimaryProperties();
                //OnPropertyChanged();
            }
        }

        private double _optimalTurns;
        public double OptimalTurns
        {
            get => _optimalTurns;
            set
            {
                _optimalTurns = value;
                _dataService.Parameters.PrimaryTurns = value;
                _dataService.Results = _calculator.CalculatePrimary(_dataService.Parameters, _dataService.Results);
                UpdateAllPrimaryProperties();
                //OnPropertyChanged();
            }
        }

        // ========= Primary ========= //
        public double PrimaryTurns
        {
            get => _dataService.Results.PrimaryTurns;
            set
            {
                _dataService.Results.PrimaryTurns = value;
                OnPropertyChanged();
            }
        }

        public double PrimaryResonance
        {
            get => _dataService.Results.PrimaryResonance;
            set
            {
                if (_dataService.Results.PrimaryResonance != value)
                {
                    _dataService.Results.PrimaryResonance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryResonanceDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryInductance
        {
            get => _dataService.Results.PrimaryInductance;
            set
            {
                if (_dataService.Results.PrimaryInductance != value)
                {
                    _dataService.Results.PrimaryInductance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryInductanceDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryXc
        {
            get => _dataService.Results.PrimaryXc;
            set
            {
                if (_dataService.Results.PrimaryXc != value)
                {
                    _dataService.Results.PrimaryXc = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryXcDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryXl
        {
            get => _dataService.Results.PrimaryXl;
            set
            {
                if (_dataService.Results.PrimaryXl != value)
                {
                    _dataService.Results.PrimaryXl = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryXlDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryWireLength
        {
            get => _dataService.Results.PrimaryWireLength;
            set
            {
                if (_dataService.Results.PrimaryWireLength != value)
                {
                    _dataService.Results.PrimaryWireLength = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryWireLengthDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryWireWeight
        {
            get => _dataService.Results.PrimaryWireWeight;
            set
            {
                if (_dataService.Results.PrimaryWireWeight != value)
                {
                    _dataService.Results.PrimaryWireWeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryWireWeightDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryCoilHeight
        {
            get => _dataService.Results.PrimaryCoilHeight;
            set
            {
                if (_dataService.Results.PrimaryCoilHeight != value)
                {
                    _dataService.Results.PrimaryCoilHeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCoilHeightDisplay)); // Notify UI to update
                }
            }
        }
        public double PrimaryCapacitance
        {
            get => _dataService.Results.PrimaryCapacitance;
            set
            {
                //if (_dataService.Results.PrimaryCapacitance != value)
                //{
                _dataService.Results.PrimaryCapacitance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PrimaryCapacitanceDisplay)); // Notify UI to update
                //}
            }
        }

        public string PrimaryCapacitanceDisplay => _unitConverter.AutoScale(PrimaryCapacitance, Unit.Farad);
        public string PrimaryCoilHeightDisplay => _unitConverter.AutoScale(PrimaryCoilHeight, Unit.Meter);
        public string PrimaryInductanceDisplay => _unitConverter.AutoScale(PrimaryInductance, Unit.Henry);
        public string PrimaryResonanceDisplay => _unitConverter.AutoScale(PrimaryResonance, Unit.Hertz);
        public string PrimaryXcDisplay => _unitConverter.AutoScale(PrimaryXc, Unit.Ohm);
        public string PrimaryXlDisplay => _unitConverter.AutoScale(PrimaryXl, Unit.Ohm);
        public string PrimaryWireLengthDisplay => _unitConverter.AutoScale(PrimaryWireLength, Unit.Meter);
        public string PrimaryWireWeightDisplay => _unitConverter.AutoScale(PrimaryWireWeight, Unit.Gram);




        // ========= Secondary ========= //
        public double SecondaryResonance
        {
            get => _dataService.Results.SecondaryResonance;
            set
            {
                _dataService.Results.SecondaryResonance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryResonanceDisplay));
            }
        }

        public double SecondaryInductance
        {
            get => _dataService.Results.SecondaryInductance;
            set
            {
                _dataService.Results.SecondaryInductance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryInductanceDisplay));
            }
        }

        public double SecondaryXc
        {
            get => _dataService.Results.SecondaryXc;
            set
            {
                _dataService.Results.SecondaryXc = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryXcDisplay));
            }
        }

        public double SecondaryXl
        {
            get => _dataService.Results.SecondaryXl;
            set
            {
                _dataService.Results.SecondaryXl = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryXlDisplay));
            }
        }

        public double SecondaryWireLength
        {
            get => _dataService.Results.SecondaryWireLength;
            set
            {
                _dataService.Results.SecondaryWireLength = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryWireLengthDisplay));
            }
        }

        public double SecondaryWireWeight
        {
            get => _dataService.Results.SecondaryWireWeight;
            set
            {
                _dataService.Results.SecondaryWireWeight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryWireWeightDisplay));
            }
        }

        public double SecondaryCoilHeight
        {
            get => _dataService.Results.SecondaryCoilHeight;
            set
            {
                _dataService.Results.SecondaryCoilHeight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryCoilHeightDisplay));
            }
        }

        public double TotalCapacitance
        {
            get => _dataService.Results.TotalCapacitance;
            set
            {
                _dataService.Results.TotalCapacitance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryTotalCapacitanceDisplay));
            }
        }
        public double TopLoadCapacitance
        {
            get => _dataService.Results.TopLoadCapacitance;
            set
            {
                _dataService.Results.TopLoadCapacitance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryTopLoadCapacitanceDisplay));
            }
        }
        public double SecondaryResonanceNoTopLoad
        {
            get => _dataService.Results.SecondaryResonanceNoTopLoad;
            set
            {
                _dataService.Results.SecondaryResonanceNoTopLoad = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecondaryResonanceNoTopLoadDisplay));
            }
        }

        public double SecondaryTurns
        {
            get => _dataService.Parameters.SecondaryTurns;
            set
            {
                _dataService.Parameters.SecondaryTurns = value;
                OnPropertyChanged();
            }
        }

        public string SecondaryTotalCapacitanceDisplay => _unitConverter.AutoScale(TotalCapacitance, Unit.Farad);
        public string SecondaryTopLoadCapacitanceDisplay => _unitConverter.AutoScale(TopLoadCapacitance, Unit.Farad);
        public string SecondaryCoilHeightDisplay => _unitConverter.AutoScale(SecondaryCoilHeight, Unit.Meter);
        public string SecondaryInductanceDisplay => _unitConverter.AutoScale(SecondaryInductance, Unit.Henry);
        public string SecondaryResonanceDisplay => _unitConverter.AutoScale(SecondaryResonance, Unit.Hertz);
        public string SecondaryResonanceNoTopLoadDisplay => _unitConverter.AutoScale(SecondaryResonanceNoTopLoad, Unit.Hertz);
        public string SecondaryXcDisplay => _unitConverter.AutoScale(SecondaryXc, Unit.Ohm);
        public string SecondaryXlDisplay => _unitConverter.AutoScale(SecondaryXl, Unit.Ohm);
        public string SecondaryWireLengthDisplay => _unitConverter.AutoScale(SecondaryWireLength, Unit.Meter);
        public string SecondaryWireWeightDisplay => _unitConverter.AutoScale(SecondaryWireWeight, Unit.Gram);

    }
}
