using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGTC.Models
{
    public interface ICoilCalculator
    {
        void CalculatePrimary(double? _turns = null, double? _coreDiameter = null, double? _wireDiameter = null, double? _wireInsDiameter = null, double? _spacing = null, double? _capacitance = null);

    }
}
