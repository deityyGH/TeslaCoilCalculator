using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using SGTC.Models;

namespace SGTC.ViewModels
{
    public class PrimaryCircuitViewModel : INotifyPropertyChanged
    {
        private double _primaryTurns;
        public double PrimaryTurns
        {
            get => _primaryTurns;
            set
            {
                if (_primaryTurns != value)
                {
                    _primaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _primaryWireDiameter;
        public double PrimaryWireDiameter
        {
            get => _primaryWireDiameter;
            set
            {
                _primaryWireDiameter = value;
                OnPropertyChanged();
            }
        }

        private double _primaryWireInsulationDiameter;
        public double PrimaryWireInsulationDiameter
        {
            get => _primaryWireInsulationDiameter;
            set
            {
                _primaryWireInsulationDiameter = value;
                OnPropertyChanged();
            }
        }

        private double _primaryWireSpacing;
        public double PrimaryWireSpacing
        {
            get => _primaryWireSpacing;
            set
            {
                _primaryWireSpacing = value;
                OnPropertyChanged();
            }
        }

        private double _primaryWindingType;
        public double PrimaryWindingType
        {
            get => _primaryWindingType;
            set
            {
                _primaryWindingType = value;
                OnPropertyChanged();
            }
        }

        private string _primaryCapacitorConnectionType;
        public string PrimaryCapacitorConnectionType
        {
            get => _primaryCapacitorConnectionType;
            set
            {
                _primaryCapacitorConnectionType = value;
                OnPropertyChanged();
            }
        }

        private double _primaryCapacitance;
        public double PrimaryCapacitance
        {
            get => _primaryCapacitance;
            set
            {
                _primaryCapacitance = value;
                OnPropertyChanged();
            }
        }

        private int _primaryCapacitorAmount;
        public int PrimaryCapacitorAmount
        {
            get => _primaryCapacitorAmount;
            set
            {
                _primaryCapacitorAmount = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
