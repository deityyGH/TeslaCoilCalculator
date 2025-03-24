using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class TorusViewModel : ObservableObject
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;
        public TorusViewModel(ICoilDataService dataService)
        {
            _dataService = dataService;
        }

        public double TopLoadTorusInDiameter
        {
            get => _dataService.Parameters.TopLoadTorusInDiameter;
            set
            {
                if (_dataService.Parameters.TopLoadTorusInDiameter != value)
                {
                    _dataService.Parameters.TopLoadTorusInDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TopLoadTorusOutDiameter
        {
            get => _dataService.Parameters.TopLoadTorusOutDiameter;
            set
            {
                if (_dataService.Parameters.TopLoadTorusOutDiameter != value)
                {
                    _dataService.Parameters.TopLoadTorusOutDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
