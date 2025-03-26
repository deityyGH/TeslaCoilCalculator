//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SGTC.Core
//{
//    public class UnitConverter2
//    {

//        public enum UnitType
//        {
//            Pico = -12,
//            Nano = -9,
//            Micro = -6,
//            Mili = -3,
//            Base = 0,
//            Kilo = 3,
//            Mega = 6
//        }

//        public readonly struct Unit
//        {
//            public string Name { get; }
//            public int Power { get; }  // Power of 10
//            public string Symbol { get; }  // Hz, F, H
//            public string UnitType { get; }  // Frequency, Capacitance, Inductance

//            private Unit(string name, int power, string symbol, string unitType)
//            {
//                Name = name;
//                Power = power;
//                Symbol = symbol;
//                UnitType = unitType;
//            }

//            public static readonly Unit Pico = new Unit("Pico", -12, "p", "General");
//            public static readonly Unit Nano = new Unit("Nano", -9, "n", "General");
//            public static readonly Unit Micro = new Unit("Micro", -6, "μ", "General");
//            public static readonly Unit Milli = new Unit("Milli", -3, "m", "General");
//            public static readonly Unit Base = new Unit("Base", 0, "", "General");
//            public static readonly Unit Kilo = new Unit("Kilo", 3, "k", "General");
//            public static readonly Unit Mega = new Unit("Mega", 6, "M", "General");
//            public static readonly Unit Giga = new Unit("Giga", 9, "G", "General");
//            public static readonly Unit Tera = new Unit("Tera", 12, "T", "General");

//            public static readonly Unit Hertz = new Unit("Hertz", 0, "Hz", "Frequency");
//            public static readonly Unit Farad = new Unit("Farad", 0, "F", "Capacitance");
//            public static readonly Unit Henry = new Unit("Henry", 0, "H", "Inductance");
//            public static readonly Unit Ohm = new Unit("Ohm", 0, "Ω", "Resistance");
//            public static readonly Unit Meter = new Unit("Meter", 0, "m", "Length");
//            public static readonly Unit Gram = new Unit("Gram", 0, "g", "Weight");

//            public override string ToString() => $"{Name} ({Symbol})";
//        }



//        public static readonly Unit[] Units =
//        {
//            Unit.Pico, Unit.Nano, Unit.Micro, Unit.Milli, Unit.Base, Unit.Kilo, Unit.Mega, Unit.Giga, Unit.Tera
//        };

//        public static string AutoScale(double value, Unit baseUnit)
//        {
//            if (value == 0)
//                return $"0 {baseUnit.Symbol}";

//            foreach (Unit unit in Units)
//            {
                
//                double scaledValue = value / Math.Pow(10, unit.Power);
//                if (scaledValue >= 1 && scaledValue < 1000)
//                {
//                    return $"{scaledValue:0.##} {unit.Symbol}{baseUnit.Symbol}";
//                }
//            }

//            Unit bestUnit = value < 1 ? Units.First() : Units.Last();
//            double finalValue = value / Math.Pow(10, bestUnit.Power);
//            return $"{finalValue:0.##} {bestUnit.Symbol}{baseUnit.Symbol}";
//        }

//        public static double AutoScaleNumber(double value, Unit baseUnit)
//        {
//            if (value == 0)
//                return value;

//            foreach (Unit unit in Units)
//            {

//                double scaledValue = value / Math.Pow(10, unit.Power);
//                if (scaledValue >= 1 && scaledValue < 1000)
//                {
//                    return scaledValue;
//                }
//            }

//            Unit bestUnit = value < 1 ? Units.First() : Units.Last();
//            double finalValue = value / Math.Pow(10, bestUnit.Power);
//            return finalValue;
//        }



//        public static double ConvertValue(double value, Unit fromUnit, Unit toUnit)
//        {
//            int powerDifference = fromUnit.Power - toUnit.Power;
//            double convertedValue = value * Math.Pow(10, powerDifference);
//            return Math.Round(convertedValue, 2);
//        }

//        public static double ConvertMmToIn(double mmValue)
//        {
//            return mmValue / 25.4;
//        }
//    }
//}
