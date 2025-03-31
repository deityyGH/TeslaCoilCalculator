using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Models
{

    public interface IUnitConverter
    {
        double ConvertValue(double value, Unit fromUnit, Unit toUnit, bool round = false);
        string AutoScale(double value, Unit baseUnit);
        (double, string) AutoScaleNumber(double value, Unit baseUnit);
        double ConvertMmToIn(double mmValue);
        double ConvertMToIn(double mValue);
        double ConvertCmToMm(double cmValue);
        double ConvertInToMm(double inValue);
        double ConvertToMm(LengthUnitType fromUnit, double value);
        double ConvertFromMm(LengthUnitType toUnit, double value);
    }

    public readonly struct Unit
    {
        public string Name { get; }
        public int Power { get; }  // Power of 10
        public string Symbol { get; }  // Hz, F, H
        public string UnitType { get; }  // Frequency, Capacitance, Inductance
        
        private Unit(string name, int power, string symbol, string unitType)
        {
            Name = name;
            Power = power;
            Symbol = symbol;
            UnitType = unitType;
            
        }

        public static readonly Unit Pico = new Unit("Pico", -12, "p", "General");
        public static readonly Unit Nano = new Unit("Nano", -9, "n", "General");
        public static readonly Unit Micro = new Unit("Micro", -6, "μ", "General");
        public static readonly Unit Milli = new Unit("Milli", -3, "m", "General");
        public static readonly Unit Base = new Unit("Base", 0, "", "General");
        public static readonly Unit Kilo = new Unit("Kilo", 3, "k", "General");
        public static readonly Unit Mega = new Unit("Mega", 6, "M", "General");
        public static readonly Unit Giga = new Unit("Giga", 9, "G", "General");
        public static readonly Unit Tera = new Unit("Tera", 12, "T", "General");

        public static readonly Unit Hertz = new Unit("Hertz", 0, "Hz", "Frequency");
        public static readonly Unit Farad = new Unit("Farad", 0, "F", "Capacitance");
        public static readonly Unit Henry = new Unit("Henry", 0, "H", "Inductance");
        public static readonly Unit Ohm = new Unit("Ohm", 0, "Ω", "Resistance");
        public static readonly Unit Meter = new Unit("Meter", 0, "m", "Length");
        public static readonly Unit Inch = new Unit("Inch", 0, "in", "Length");
        public static readonly Unit Gram = new Unit("Gram", 0, "g", "Weight");

        public override string ToString() => $"{Name} ({Symbol})";
    }

    public class UnitConverter : IUnitConverter
    {

        public readonly Unit[] Units =
        {
            Unit.Pico, Unit.Nano, Unit.Micro, Unit.Milli, Unit.Base, Unit.Kilo, Unit.Mega, Unit.Giga, Unit.Tera
        };

        public string AutoScale(double value, Unit baseUnit)
        {
            if (value == 0) return $"0 {baseUnit.Symbol}";

            int power = (int)Math.Floor(Math.Log10(value) / 3) * 3;
            Unit bestUnit = Units.Any(u => u.Power == power) ? Units.First(u => u.Power == power) : baseUnit;

            double scaledValue = value / Math.Pow(10, bestUnit.Power);
            return $"{scaledValue:0.##} {bestUnit.Symbol}{baseUnit.Symbol}";
        }

        public (double, string) AutoScaleNumber(double value, Unit baseUnit)
        {
            if (value == 0) return (value, baseUnit.Symbol);

            int power = (int)Math.Floor(Math.Log10(value) / 3) * 3;
            Unit bestUnit = Units.Any(u => u.Power == power) ? Units.First(u => u.Power == power) : baseUnit;

            double scaledValue = value / Math.Pow(10, bestUnit.Power);
            return (scaledValue, $"{bestUnit.Symbol}{baseUnit.Symbol}");
        }



        public double ConvertValue(double value, Unit fromUnit, Unit toUnit, bool round)
        {
            int powerDifference = fromUnit.Power - toUnit.Power;
            double convertedValue = value * Math.Pow(10, powerDifference);
            return round ? Math.Round(convertedValue, 2) : convertedValue;
        }

        public double ConvertMmToCm(double cmValue) => cmValue / 10;
        public double ConvertMmToIn(double mmValue) => mmValue / 25.4;

        public double ConvertCmToMm(double cmValue) => cmValue * 10;


        public double ConvertInToMm(double inValue) => inValue * 25.4;

        public double ConvertMToMm(double mValue) => mValue * 1000;
        public double ConvertMToCm(double mValue) => mValue * 100;
        public double ConvertMToIn(double mValue) => mValue * 39.370079;

        public double ConvertToMm(LengthUnitType fromUnit, double value)
        {
            return fromUnit switch
            {
                LengthUnitType.Centimeter => ConvertCmToMm(value),
                LengthUnitType.Inch => ConvertInToMm(value),
                LengthUnitType.Millimeter => value,
                _ => value
            };
        }





        public double ConvertFromMm(LengthUnitType toUnit, double value)
        {
            return toUnit switch
            {
                LengthUnitType.Centimeter => ConvertMmToCm(value),
                LengthUnitType.Inch => ConvertMmToIn(value),
                LengthUnitType.Millimeter => value,
                _ => value
            };
        }


    }
}