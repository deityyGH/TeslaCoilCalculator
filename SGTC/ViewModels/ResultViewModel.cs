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
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;
        public RelayCommand CalculateCapacitanceGraphCommand { get; set; }
        //public ResultGraphViewModel ResultGraphViewModel { get; set; }

        public ResultViewModel()
        {
            CalculateCapacitanceGraphCommand = new RelayCommand(obj =>
            {
                //var userControl = obj as UserControl;
                ResultGraph ResultGraphWindow = new ResultGraph();
             //   Window parentWindow = Window.GetWindow(userControl);
                //ResultGraphWindow.Owner = parentWindow;
               // ResultGraphWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                ResultGraphWindow.Show();
            });

        }

        // ========= Primary ========= //


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
                if (_result.PrimaryCapacitance != value)
                {
                    _result.PrimaryCapacitance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PrimaryCapacitanceDisplay)); // Notify UI to update
                }
            }
        }

        public string PrimaryCapacitanceDisplay => UnitConverter.AutoScale(PrimaryCapacitance, UnitConverter.Unit.Farad);
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
