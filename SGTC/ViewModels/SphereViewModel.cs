using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{

    public class SphereViewModel : ObservableObject
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;
        public SphereViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }

        public double TopLoadSphereDiameter
        {
            get => _dataService.Parameters.TopLoadSphereDiameter;
            set
            {
                if (_dataService.Parameters.TopLoadSphereDiameter != value)
                {
                    _dataService.Parameters.TopLoadSphereDiameter = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

