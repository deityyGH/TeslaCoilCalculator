using SGTC.Core;
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
        
        
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;
        public RelayCommand AutoPrimaryCapacitanceCommand { get; set; }
        public RelayCommand AutoPrimaryTurnsCommand { get; set; }

        private ResultGraph _resultGraphWindow;

        public ResultViewModel()
        {
            ResultGraphViewModel.GraphCalculated += OnGraphCalculated;

            AutoPrimaryCapacitanceCommand = new RelayCommand(obj =>
            {
                ShowGraphWindow(Coil.Primary, GraphType.Capacitance);
            });

            AutoPrimaryTurnsCommand = new RelayCommand(obj =>
            {
                ShowGraphWindow(Coil.Primary, GraphType.Turns);
            });
        }
        
        private void ShowGraphWindow(Coil coilType, GraphType graphType)
        {
            // Check if window already exists
            if (_resultGraphWindow != null)
            {
                // If the window exists but is closed
                if (!_resultGraphWindow.IsVisible)
                {
                    _resultGraphWindow = new ResultGraph(coilType, graphType);
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
                _resultGraphWindow = new ResultGraph(coilType, graphType);
                _resultGraphWindow.Show();

                _resultGraphWindow.Closed += (sender, args) =>
                {
                    _resultGraphWindow = null;
                };
            }
        }

        private void UpdateAllProperties()
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
            OnPropertyChanged(nameof(PrimaryTurnsDisplay));
            OnPropertyChanged(nameof(PrimaryInductanceDisplay));
            OnPropertyChanged(nameof(PrimaryXcDisplay));
            OnPropertyChanged(nameof(PrimaryXlDisplay));
            OnPropertyChanged(nameof(PrimaryWireLengthDisplay));
            OnPropertyChanged(nameof(PrimaryWireWeightDisplay));
            OnPropertyChanged(nameof(PrimaryCoilHeightDisplay));
        }

        private void OnGraphCalculated(object sender, GraphCalculatedEventArgs e)
        {
            if (e.Coil == Coil.Primary)
            {
                switch (e.GraphType)
                {
                    case GraphType.Capacitance:
                        OptimalCapacitance = e.OptimalValue;
                        _result.PrimaryCapacitance = OptimalCapacitance;
                        break;
                    case GraphType.Turns:
                        OptimalTurns = e.OptimalValue;
                        _result.PrimaryTurns = OptimalTurns;
                        break;
                    case GraphType.TopLoad:
                        break;
                    case GraphType.CoreDiameter:
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
                PrimaryCalculator.CalculatePrimary(_capacitance: _optimalCapacitance);
                UpdateAllProperties();
                OnPropertyChanged();
            }
        }

        private double _optimalTurns;
        public double OptimalTurns
        {
            get => _optimalTurns;
            set
            {
                _optimalTurns = value;
                PrimaryCalculator.CalculatePrimary(_turns: _optimalTurns);
                UpdateAllProperties();
                OnPropertyChanged();
            }
        }

        // ========= Primary ========= //
        public double PrimaryTurns
        {
            get => _result.PrimaryTurns;
            set
            {

                _result.PrimaryTurns = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PrimaryTurnsDisplay));
                
            }
        }

        public double PrimaryResonance
        {
            get => _result.PrimaryResonance;
            set
            {
                if (_result.PrimaryResonance != value)
                {
                    _result.PrimaryResonance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryResonanceDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryInductance
        {
            get => _result.PrimaryInductance;
            set
            {
                if (_result.PrimaryInductance != value)
                {
                    _result.PrimaryInductance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryInductanceDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryXc
        {
            get => _result.PrimaryXc;
            set
            {
                if (_result.PrimaryXc != value)
                {
                    _result.PrimaryXc = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryXcDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryXl
        {
            get => _result.PrimaryXl;
            set
            {
                if (_result.PrimaryXl != value)
                {
                    _result.PrimaryXl = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryXlDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryWireLength
        {
            get => _result.PrimaryWireLength;
            set
            {
                if (_result.PrimaryWireLength != value)
                {
                    _result.PrimaryWireLength = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryWireLengthDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryWireWeight
        {
            get => _result.PrimaryWireWeight;
            set
            {
                if (_result.PrimaryWireWeight != value)
                {
                    _result.PrimaryWireWeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryWireWeightDisplay)); // Notify UI to update
                }
            }
        }

        public double PrimaryCoilHeight
        {
            get => _result.PrimaryCoilHeight;
            set
            {
                if (_result.PrimaryCoilHeight != value)
                {
                    _result.PrimaryCoilHeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCoilHeightDisplay)); // Notify UI to update
                }
            }
        }
        public double PrimaryCapacitance
        {
            get => _result.PrimaryCapacitance;
            set
            {
                //if (_result.PrimaryCapacitance != value)
                //{
                    _result.PrimaryCapacitance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay)); // Notify UI to update
                //}
            }
        }

        public string PrimaryCapacitanceDisplay => UnitConverter.AutoScale(PrimaryCapacitance, UnitConverter.Unit.Farad);
        public string PrimaryTurnsDisplay => $"{PrimaryTurns:F2}";
        public string PrimaryCoilHeightDisplay => $"{PrimaryCoilHeight:F2} mm";
        public string PrimaryInductanceDisplay => UnitConverter.AutoScale(PrimaryInductance, UnitConverter.Unit.Henry);
        public string PrimaryResonanceDisplay => UnitConverter.AutoScale(PrimaryResonance, UnitConverter.Unit.Hertz);
        public string PrimaryXcDisplay => UnitConverter.AutoScale(PrimaryXc, UnitConverter.Unit.Ohm);
        public string PrimaryXlDisplay => UnitConverter.AutoScale(PrimaryXl, UnitConverter.Unit.Ohm);
        public string PrimaryWireLengthDisplay => UnitConverter.AutoScale(PrimaryWireLength, UnitConverter.Unit.Meter);
        public string PrimaryWireWeightDisplay => UnitConverter.AutoScale(PrimaryWireWeight, UnitConverter.Unit.Gram);




        // ========= Secondary ========= //
        public double SecondaryResonance
        {
            get => _result.SecondaryResonance;
            set
            {
                if (_result.SecondaryResonance != value)
                {
                    _result.SecondaryResonance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryInductance
        {
            get => _result.SecondaryInductance;
            set
            {
                if (_result.SecondaryInductance != value)
                {
                    _result.SecondaryInductance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryXc
        {
            get => _result.SecondaryXc;
            set
            {
                if (_result.SecondaryXc != value)
                {
                    _result.SecondaryXc = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryXl
        {
            get => _result.SecondaryXl;
            set
            {
                if (_result.SecondaryXl != value)
                {
                    _result.SecondaryXl = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryWireLength
        {
            get => _result.SecondaryWireLength;
            set
            {
                if (_result.SecondaryWireLength != value)
                {
                    _result.SecondaryWireLength = value;
                    OnPropertyChanged(); 
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryWireWeight
        {
            get => _result.SecondaryWireWeight;
            set
            {
                if (_result.SecondaryWireWeight != value)
                {
                    _result.SecondaryWireWeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryCoilHeight
        {
            get => _result.SecondaryCoilHeight;
            set
            {
                if (_result.SecondaryCoilHeight != value)
                {
                    _result.SecondaryCoilHeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double TotalCapacitance
        {
            get => _result.TotalCapacitance;
            set
            {
                if (_result.TotalCapacitance != value)
                {
                    _result.TotalCapacitance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            
            }
        }
        public double NoTopLoadCapacitance
        {
            get => _result.NoTopLoadCapacitance;
            set
            {
                if (_result.NoTopLoadCapacitance != value)
                {
                    _result.NoTopLoadCapacitance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }
        public double SecondaryResonanceNoTopLoad
        {
            get => _result.SecondaryResonanceNoTopLoad;
            set
            {
                if (_result.SecondaryResonanceNoTopLoad != value)
                {
                    _result.SecondaryResonanceNoTopLoad = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay));
                }
            }
        }

        public double SecondaryTurns
        {
            get => _data.SecondaryTurns;
            set
            {
                if (_data.SecondaryTurns != value)
                {
                    _data.SecondaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryTotalCapacitanceDisplay => UnitConverter.AutoScale(TotalCapacitance, UnitConverter.Unit.Farad);
        public string SecondaryNoTopLoadCapacitanceDisplay => UnitConverter.AutoScale(NoTopLoadCapacitance, UnitConverter.Unit.Farad);
        public string SecondaryCoilHeightDisplay => $"{SecondaryCoilHeight:F2} mm";
        public string SecondaryInductanceDisplay => UnitConverter.AutoScale(SecondaryInductance, UnitConverter.Unit.Henry);
        public string SecondaryResonanceDisplay => UnitConverter.AutoScale(SecondaryResonance, UnitConverter.Unit.Hertz);
        public string SecondaryResonanceNoTopLoadDisplay => UnitConverter.AutoScale(SecondaryResonanceNoTopLoad, UnitConverter.Unit.Hertz);
        public string SecondaryXcDisplay => UnitConverter.AutoScale(SecondaryXc, UnitConverter.Unit.Ohm);
        public string SecondaryXlDisplay => UnitConverter.AutoScale(SecondaryXl, UnitConverter.Unit.Ohm);
        public string SecondaryWireLengthDisplay => UnitConverter.AutoScale(SecondaryWireLength, UnitConverter.Unit.Meter);
        public string SecondaryWireWeightDisplay => UnitConverter.AutoScale(SecondaryWireWeight, UnitConverter.Unit.Gram);

    }
}
