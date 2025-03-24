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
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;
        public SphereViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }

        public double TopLoadSphereDiameter
        {
            get => _data.TopLoadSphereDiameter;
            set
            {
                if (_data.TopLoadSphereDiameter != value)
                {
                    _data.TopLoadSphereDiameter = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

