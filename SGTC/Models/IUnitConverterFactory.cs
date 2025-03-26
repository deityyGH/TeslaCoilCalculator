using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public interface IUnitConverterFactory
    {
        Func<double, double> CreateConverter(Unit fromUnit, Unit toUnit);
    }

    public class UnitConverterFactory : IUnitConverterFactory
    {
        private readonly IUnitConverter _unitConverter;
        public UnitConverterFactory(IUnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public Func<double, double> CreateConverter(Unit fromUnit, Unit toUnit)
        {
            return (value) => _unitConverter.ConvertValue(value, fromUnit, toUnit);
        }
    }
}
