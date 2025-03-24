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
        private readonly ICoilDataService _dataService;

        public SecondaryCircuitViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }

        public double SecondaryTurns { 
            get => _dataService.Parameters.SecondaryTurns;
            set
            {
                if (_dataService.Parameters.SecondaryTurns != value)
                {
                    _dataService.Parameters.SecondaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        public double SecondaryCoreDiameter
        {
            get => _dataService.Parameters.SecondaryCoreDiameter;
            set
            {
                if (_dataService.Parameters.SecondaryCoreDiameter != value)
                {
                    _dataService.Parameters.SecondaryCoreDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double SecondaryWireDiameter
        {
            get => _dataService.Parameters.SecondaryWireDiameter;
            set
            {
                if (_dataService.Parameters.SecondaryWireDiameter != value)
                {
                    _dataService.Parameters.SecondaryWireDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double SecondaryWireInsulationDiameter
        {
            get => _dataService.Parameters.SecondaryWireInsulationDiameter;
            set
            {
                if (_dataService.Parameters.SecondaryWireInsulationDiameter != value)
                {
                    _dataService.Parameters.SecondaryWireInsulationDiameter = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
