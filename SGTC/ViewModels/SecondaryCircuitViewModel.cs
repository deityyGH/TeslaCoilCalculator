using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class SecondaryCircuitViewModel : ObservableObject
    {
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;

        public SecondaryCircuitViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }

        public double SecondaryTurns { 
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

        public double SecondaryCoreDiameter
        {
            get => _data.SecondaryCoreDiameter;
            set
            {
                if (_data.SecondaryCoreDiameter != value)
                {
                    _data.SecondaryCoreDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double SecondaryWireDiameter
        {
            get => _data.SecondaryWireDiameter;
            set
            {
                if (_data.SecondaryWireDiameter != value)
                {
                    _data.SecondaryWireDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double SecondaryWireInsulationDiameter
        {
            get => _data.SecondaryWireInsulationDiameter;
            set
            {
                if (_data.SecondaryWireInsulationDiameter != value)
                {
                    _data.SecondaryWireInsulationDiameter = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
