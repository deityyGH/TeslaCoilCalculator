using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class ResultViewModel : ObservableObject
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;

        // ========= Primary ========= //
        
        public string PrimaryResonance
        {
            get => _result.PrimaryResonance;
            set
            {
                if (_result.PrimaryResonance != value)
                {
                    _result.PrimaryResonance = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public string PrimaryInductance
        {
            get => _result.PrimaryInductance;
            set
            {
                if (_result.PrimaryInductance != value)
                {
                    _result.PrimaryInductance = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public string PrimaryXc
        {
            get => _result.PrimaryXc;
            set
            {
                if (_result.PrimaryXc != value)
                {
                    _result.PrimaryXc = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public string PrimaryXl
        {
            get => _result.PrimaryXl;
            set
            {
                if (_result.PrimaryXl != value)
                {
                    _result.PrimaryXl = value;
                    OnPropertyChanged();
                }
            }
        }

       
        public string PrimaryWireLength
        {
            get => _result.PrimaryWireLength;
            set
            {
                if (_result.PrimaryWireLength != value)
                {
                    _result.PrimaryWireLength = value;
                    OnPropertyChanged();
                }
            }
        }

       
        public string PrimaryWireWeight
        {
            get => _result.PrimaryWireWeight;
            set
            {
                if (_result.PrimaryWireWeight != value)
                {
                    _result.PrimaryWireWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrimaryCoilHeight
        {
            get => _result.PrimaryCoilHeight;
            set
            {
                if (_result.PrimaryCoilHeight != value)
                {
                    _result.PrimaryCoilHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrimaryCapacitance
        {
            get => _result.PrimaryCapacitance;
            set
            {
                if (_result.PrimaryCapacitance != value)
                {
                    _result.PrimaryCapacitance = value;
                    OnPropertyChanged();
                }
            }
        }


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
                }
            }
        }

        public double TopLoadCapacitance
        {
            get => _result.TopLoadCapacitance;
            set
            {
                if (_result.TopLoadCapacitance != value)
                {
                    _result.TopLoadCapacitance = value;
                    OnPropertyChanged();
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
                }
            }
        }



        // ====== Units ====== //

        public string PrimaryResonanceUnit
        {
            get => _result.PrimaryResonanceUnit;
            set
            {
                if (_result.PrimaryResonanceUnit != value)
                {
                    _result.PrimaryResonanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PrimaryInductanceUnit
        {
            get => _result.PrimaryInductanceUnit;
            set
            {
                if (_result.PrimaryInductanceUnit != value)
                {
                    _result.PrimaryInductanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PrimaryXcUnit
        {
            get => _result.PrimaryXcUnit;
            set
            {
                if (_result.PrimaryXcUnit != value)
                {
                    _result.PrimaryXcUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PrimaryXlUnit
        {
            get => _result.PrimaryXlUnit;
            set
            {
                if (_result.PrimaryXlUnit != value)
                {
                    _result.PrimaryXlUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PrimaryWireLengthUnit
        {
            get => _result.PrimaryWireLengthUnit;
            set
            {
                if (_result.PrimaryWireLengthUnit != value)
                {
                    _result.PrimaryWireLengthUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PrimaryWireWeightUnit
        {
            get => _result.PrimaryWireWeightUnit;
            set
            {
                if (_result.PrimaryWireWeightUnit != value)
                {
                    _result.PrimaryWireWeightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrimaryCoilHeightUnit
        {
            get => _result.PrimaryCoilHeightUnit;
            set
            {
                if (_result.PrimaryCoilHeightUnit != value)
                {
                    _result.PrimaryCoilHeightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrimaryCapacitanceUnit
        {
            get => _result.PrimaryCapacitanceUnit;
            set
            {
                if (_result.PrimaryCapacitanceUnit != value)
                {
                    _result.PrimaryCapacitanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }


        // ========= Secondary ========= //
        public string SecondaryResonanceUnit
        {
            get => _result.SecondaryResonanceUnit;
            set
            {
                if (_result.SecondaryResonanceUnit != value)
                {
                    _result.SecondaryResonanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryInductanceUnit
        {
            get => _result.SecondaryInductanceUnit;
            set
            {
                if (_result.SecondaryInductanceUnit != value)
                {
                    _result.SecondaryInductanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryXcUnit
        {
            get => _result.SecondaryXcUnit;
            set
            {
                if (_result.SecondaryXcUnit != value)
                {
                    _result.SecondaryXcUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryXlUnit
        {
            get => _result.SecondaryXlUnit;
            set
            {
                if (_result.SecondaryXlUnit != value)
                {
                    _result.SecondaryXlUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryWireLengthUnit
        {
            get => _result.SecondaryWireLengthUnit;
            set
            {
                if (_result.SecondaryWireLengthUnit != value)
                {
                    _result.SecondaryWireLengthUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryWireWeightUnit
        {
            get => _result.SecondaryWireWeightUnit;
            set
            {
                if (_result.SecondaryWireWeightUnit != value)
                {
                    _result.SecondaryWireWeightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryCoilHeightUnit
        {
            get => _result.SecondaryCoilHeightUnit;
            set
            {
                if (_result.SecondaryCoilHeightUnit != value)
                {
                    _result.SecondaryCoilHeightUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TopLoadCapacitanceUnit
        {
            get => _result.TopLoadCapacitanceUnit;
            set
            {
                if (_result.TopLoadCapacitanceUnit != value)
                {
                    _result.TopLoadCapacitanceUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryResonanceNoTopLoadUnit
        {
            get => _result.SecondaryResonanceNoTopLoadUnit;
            set
            {
                if (_result.SecondaryResonanceNoTopLoadUnit != value)
                {
                    _result.SecondaryResonanceNoTopLoadUnit = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
