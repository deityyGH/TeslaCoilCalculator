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
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;

        public double TopLoadTorusInDiameter
        {
            get => _data.TopLoadTorusInDiameter;
            set
            {
                if (_data.TopLoadTorusInDiameter != value)
                {
                    _data.TopLoadTorusInDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TopLoadTorusOutDiameter
        {
            get => _data.TopLoadTorusOutDiameter;
            set
            {
                if (_data.TopLoadTorusOutDiameter != value)
                {
                    _data.TopLoadTorusOutDiameter = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
