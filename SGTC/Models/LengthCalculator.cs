using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public class LengthCalculator
    {
        private readonly ILengthConverter _lengthConverter;

        // Constructor Dependency Injection
        public LengthCalculator(ILengthConverter lengthConverter)
        {
            _lengthConverter = lengthConverter ?? throw new ArgumentNullException(nameof(lengthConverter));
        }

        public double ConvertLength(LengthUnitType fromUnit, LengthUnitType toUnit, double value)
        {
            return _lengthConverter.Convert(fromUnit, toUnit, value);
        }
    }
}
