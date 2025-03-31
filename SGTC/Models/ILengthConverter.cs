using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{
    public interface ILengthConverter
    {
        double Convert(LengthUnitType fromUnit, LengthUnitType toUnit, double value);
    }

    public class BaseLengthConverter : ILengthConverter
    {
        // Conversion constants
        protected const double MillimetersPerCentimeter = 10.0;
        protected const double MillimetersPerInch = 25.4;
        protected const double MillimetersPerMeter = 1000.0;

        public virtual double Convert(LengthUnitType fromUnit, LengthUnitType toUnit, double value)
        {
            // First, convert to millimeters as a standard intermediate unit
            double millimeters = ConvertToMillimeters(fromUnit, value);

            // Then convert from millimeters to the target unit
            return ConvertFromMillimeters(toUnit, millimeters);
        }

        protected virtual double ConvertToMillimeters(LengthUnitType fromUnit, double value)
        {
            return fromUnit switch
            {
                LengthUnitType.Millimeter => value,
                LengthUnitType.Centimeter => value * MillimetersPerCentimeter,
                LengthUnitType.Inch => value * MillimetersPerInch,
                _ => throw new ArgumentException("Unsupported unit type", nameof(fromUnit))
            };
        }

        protected virtual double ConvertFromMillimeters(LengthUnitType toUnit, double millimeters)
        {
            return toUnit switch
            {
                LengthUnitType.Millimeter => millimeters,
                LengthUnitType.Centimeter => millimeters / MillimetersPerCentimeter,
                LengthUnitType.Inch => millimeters / MillimetersPerInch,
                _ => throw new ArgumentException("Unsupported unit type", nameof(toUnit))
            };
        }
    }
}
