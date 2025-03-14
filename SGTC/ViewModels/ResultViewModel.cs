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
        public string SecondaryResonance
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

        public string SecondaryInductance
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

        public string SecondaryXc
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

        public string SecondaryXl
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

        public string SecondaryWireLength
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

        public string SecondaryWireWeight
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

        public string SecondaryCoilHeight
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

        public string TopLoadCapacitance
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

        public string SecondaryResonanceNoTopLoad
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
    }
}
