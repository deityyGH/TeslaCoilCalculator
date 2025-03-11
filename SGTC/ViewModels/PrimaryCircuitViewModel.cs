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
                _data.PrimaryCoreDiameter = value;
                OnPropertyChanged();
            }
        }

        public double PrimaryWireDiameter
        {
            get => _data.PrimaryWireDiameter;
            set
            {
                _data.PrimaryWireDiameter = value;
                OnPropertyChanged();
            }
        }


        public double PrimaryWireInsulationDiameter
        {
            get => _data.PrimaryWireInsulationDiameter;
            set
            {
                _data.PrimaryWireInsulationDiameter = value;
                OnPropertyChanged();
            }
        }

        public double PrimaryWireSpacing
        {
            get => _data.PrimaryWireSpacing;
            set
            {
                _data.PrimaryWireSpacing = value;
                OnPropertyChanged();
            }
        }

        public double PrimaryWindingType
        {
            get => _data.PrimaryWindingType;
            set
            {
                _data.PrimaryWindingType = value;
                OnPropertyChanged();
            }
        }


        public string PrimaryCapacitorConnectionType
        {
            get => _data.PrimaryCapacitorConnectionType;
            set
            {
                _data.PrimaryCapacitorConnectionType = value;
                OnPropertyChanged();
            }
        }


        public double PrimaryCapacitance
        {
            get => _data.PrimaryCapacitance;
            set
            {
                _data.PrimaryCapacitance = value;
                OnPropertyChanged();
            }
        }


        public int PrimaryCapacitorAmount
        {
            get => _data.PrimaryCapacitorAmount;
            set
            {
                _data.PrimaryCapacitorAmount = value;
                OnPropertyChanged();
            }
        }



    }
}
