using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SGTC.Models;
using SGTC.Core;

namespace SGTC.ViewModels
{
    public class PrimaryCircuitViewModel : ObservableObject
    {
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;

        // Combo box
        private ObservableCollection<PrimaryWindingType> _primaryWindingTypes;
        public ObservableCollection<PrimaryWindingType> PrimaryWindingTypes
        {
            get => _primaryWindingTypes;
            set
            {
                _primaryWindingTypes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PrimaryCapacitorConnectionType> _primaryCapacitorConnectionTypes;
        public ObservableCollection<PrimaryCapacitorConnectionType> PrimaryCapacitorConnectionTypes
        {
            get => _primaryCapacitorConnectionTypes;
            set
            {
                _primaryCapacitorConnectionTypes = value;
                OnPropertyChanged();
            }
        }


        public double PrimaryTurns
        {
            get => _data.PrimaryTurns;
            set
            {
                if (_data.PrimaryTurns != value)
                {
                    _data.PrimaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryCoreDiameter
        {
            get => _data.PrimaryCoreDiameter;
            set
            {
                if (_data.PrimaryCoreDiameter != value)
                {
                    _data.PrimaryCoreDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryWireDiameter
        {
            get => _data.PrimaryWireDiameter;
            set
            {
                if (_data.PrimaryWireDiameter != value)
                {
                    _data.PrimaryWireDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        

        public double PrimaryWireInsulationDiameter
        {
            get => _data.PrimaryWireInsulationDiameter;
            set
            {
                if (_data.PrimaryWireInsulationDiameter != value)
                {
                    _data.PrimaryWireInsulationDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryWireSpacing
        {
            get => _data.PrimaryWireSpacing;
            set
            {
                if (_data.PrimaryWireSpacing != value)
                {
                    _data.PrimaryWireSpacing = value;
                    OnPropertyChanged();
                }
            }
        }

        public PrimaryWindingType SelectedPrimaryWindingType
        {
            get => _data.PrimaryWindingType;
            set
            {
                if (_data.PrimaryWindingType != value)
                {
                    _data.PrimaryWindingType = value;
                    OnPropertyChanged();
                }
            }
        }


        public PrimaryCapacitorConnectionType SelectedPrimaryCapacitorConnectionType
        {
            get => _data.PrimaryCapacitorConnectionType;
            set
            {
                Console.WriteLine(_data.PrimaryCapacitorConnectionType);
                if (_data.PrimaryCapacitorConnectionType != value)
                {
                    _data.PrimaryCapacitorConnectionType = value;
                    OnPropertyChanged();
                }
            }
        }


        public double PrimaryCapacitance
        {
            get => _data.PrimaryCapacitance;
            set
            {
                if (_data.PrimaryCapacitance != value)
                {
                    _data.PrimaryCapacitance = value;
                    OnPropertyChanged();
                }
            }
        }


        public int PrimaryCapacitorAmount
        {
            get => _data.PrimaryCapacitorAmount;
            set
            {
                if (_data.PrimaryCapacitorAmount != value)
                {
                    _data.PrimaryCapacitorAmount = value;
                    OnPropertyChanged();
                }
            }
        }


        public PrimaryCircuitViewModel()
        {

            PrimaryWindingTypes = new ObservableCollection<PrimaryWindingType>
            {
                PrimaryWindingType.Solenoid,
                PrimaryWindingType.FlatSpiral,
                PrimaryWindingType.Conical
            };

            PrimaryCapacitorConnectionTypes = new ObservableCollection<PrimaryCapacitorConnectionType>
            {
                PrimaryCapacitorConnectionType.Series,
                PrimaryCapacitorConnectionType.Parallel
            };
        }

        

    }
}
