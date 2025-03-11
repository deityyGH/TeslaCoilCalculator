using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class SphereViewModel
    {
        private double _diameter;
        public double Diameter
        {
            get => _diameter;
            set
            {
                _diameter = value;

            }
        }
    }
}
