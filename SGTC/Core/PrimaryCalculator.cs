using SGTC.Models;
using SGTC.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Core
{
    public class PrimaryCalculator
    {
        private static readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private static readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;
        private static double PrimaryResonance { get; set; } = 0;
        private static double PrimaryInductance { get; set; } = 0;
        private static double PrimaryXc { get; set; } = 0;
        private static double PrimaryXl { get; set; } = 0;
        private static double PrimaryWireLength { get; set; } = 0;
        private static double PrimaryWireWeight { get; set; } = 0;
        private static double PrimaryCoilHeight { get; set; } = 0;
        private static double PrimaryCapacitance { get; set; } = 0;



        //private static readonly UnitCoverter _converter = new UnitCoverter();

        //private static readonly Unit[] Units =
        //{
        //    Unit.Pico, Unit.Nano, Unit.Micro, Unit.Milli, Unit.Base, Unit.Kilo, Unit.Mega, Unit.Giga, Unit.Tera
        //};

        public static void Run()
        {
            CalculateData();
            DisplayData();
        }

        private static void CalculateData()
        {
            CalculateCoilLength();
            CalculateInductance();
            CalculateCapacitance();
            CalculateResonance();
            CalculateCapacitiveReactance();
            CalculateInductiveReactance();
            CalculateWireLength();
            CalculateWireWeight();
        }

        private static void DisplayData()
        {
            _result.PrimaryCoilHeight = $"{PrimaryCoilHeight:F2} mm";
            _result.PrimaryInductance = UnitConverter.AutoScale(PrimaryInductance, UnitConverter.Unit.Henry);
            _result.PrimaryCapacitance = UnitConverter.AutoScale(PrimaryCapacitance, UnitConverter.Unit.Farad);
            _result.PrimaryResonance = UnitConverter.AutoScale(PrimaryResonance, UnitConverter.Unit.Hertz);
            _result.PrimaryXc = UnitConverter.AutoScale(PrimaryXc, UnitConverter.Unit.Ohm);
            _result.PrimaryXl = UnitConverter.AutoScale(PrimaryXl, UnitConverter.Unit.Ohm);
            _result.PrimaryWireLength = $"{PrimaryWireLength:F2} m";
            _result.PrimaryWireWeight = $"{PrimaryWireWeight:F2} g";

        }

        private static double ConvertMmToIn(double mmValue)
        {
            return mmValue / 25.4;
        }

        private static double ConvertInToMm(double inValue)
        {
            return inValue * 25.4;
        }



        private static void CalculateResonance()
        {
            double denominator = 2 * Math.PI * Math.Sqrt(PrimaryInductance * PrimaryCapacitance);
            if (denominator == 0)
            {
                PrimaryResonance = 0;
                return;
            }
            PrimaryResonance = 1 / denominator;
        }

        private static void CalculateCoilLength()
        {
            if (_data.PrimaryWindingType == PrimaryWindingType.Solenoid)
            {
                PrimaryCoilHeight = _data.PrimaryTurns * (_data.PrimaryWireInsulationDiameter + _data.PrimaryWireSpacing);
                
            }
        }

        private static void CalculateInductance()
        {
            
            if (_data.PrimaryWindingType == PrimaryWindingType.Solenoid)
            {
                // L = (N^2 * d^2) / (18 * d + 40 * s)
                // where N = turns, d = diameter in inches, s = spacing in inches
                double diameter = ConvertMmToIn(_data.PrimaryCoreDiameter + _data.PrimaryWireInsulationDiameter);
                double turns = _data.PrimaryTurns;
                double coilHeight = ConvertMmToIn(PrimaryCoilHeight);


                
                double denominator = (18 * diameter) + (40 * coilHeight);
                if (denominator == 0)
                {
                    PrimaryInductance = 0;
                    return;
                }
                      
                PrimaryInductance = (Math.Pow(turns, 2) * Math.Pow(diameter, 2)) / denominator;
                PrimaryInductance = PrimaryInductance * Math.Pow(10, -6);
            }
        }
        
        

        private static void CalculateCapacitance()
        {
            double capacitance = _data.PrimaryCapacitance * Math.Pow(10, -9);
            int numberOfCapacitors = _data.PrimaryCapacitorAmount;
            
            if (_data.PrimaryCapacitorConnectionType == PrimaryCapacitorConnectionType.Parallel)
            {
                PrimaryCapacitance = capacitance * _data.PrimaryCapacitorAmount;
                return;
            }

            if (_data.PrimaryCapacitorAmount == 1)
            {
                PrimaryCapacitance = capacitance;
                return;
            }

            double totalCapacitance = 0;
            for (int i = 0; i < numberOfCapacitors; i++)
            {
                totalCapacitance = (1 / capacitance) + totalCapacitance;
            }

            PrimaryCapacitance = totalCapacitance;
        }

    
        private static void CalculateCapacitiveReactance()
        {
            PrimaryXc = 1 / (2 * Math.PI * PrimaryResonance * PrimaryCapacitance);
        }
        private static void CalculateInductiveReactance()
        {
            PrimaryXl = 2 * Math.PI * PrimaryResonance * PrimaryInductance;
        }


        private static void CalculateWireLength()
        {
            PrimaryWireLength = (Math.PI * _data.PrimaryTurns * _data.PrimaryCoreDiameter) / 1000;
        }

        private static void CalculateWireWeight()
        {
            double CuDensity = 8.96;

            double CoreRadius = (_data.PrimaryCoreDiameter / 2) / 10; // convert to cm
            double WireCrossSection = Math.PI * Math.Pow(CoreRadius, 2);
            double Volume = WireCrossSection * (PrimaryWireLength / 10);
            PrimaryWireWeight = Volume * CuDensity;
        }
    }
}
