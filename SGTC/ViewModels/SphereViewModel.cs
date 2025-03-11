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

