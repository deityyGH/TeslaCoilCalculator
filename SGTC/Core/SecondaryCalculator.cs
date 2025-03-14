using SGTC.Models;
using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.Core
{
    public class SecondaryCalculator
    {
        private static readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private static readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;

        private static double SecondaryResonance { get; set; } = 0;
        private static double SecondaryInductance { get; set; } = 0;
        private static double SecondaryXc { get; set; } = 0;
        private static double SecondaryXl { get; set; } = 0;
        private static double SecondaryWireLength { get; set; } = 0;
        private static double SecondaryWireWeight { get; set; } = 0;
        private static double SecondaryCoilHeight { get; set; } = 0;
        private static double TopLoadCapacitance { get; set; } = 0;
        private static double SecondaryResonanceNoTopLoad { get; set; } = 0;

        private static double SecondaryCapacitance { get; set; } = 0;
        private static double StrayCapacitance { get; set; } = 7; // pF

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
            _result.SecondaryCoilHeight = $"{SecondaryCoilHeight:F2} mm";
            _result.SecondaryInductance = UnitConverter.AutoScale(SecondaryInductance, UnitConverter.Unit.Henry);
            _result.SecondaryResonance = UnitConverter.AutoScale(SecondaryResonance, UnitConverter.Unit.Hertz);
            _result.TopLoadCapacitance = UnitConverter.AutoScale(TopLoadCapacitance, UnitConverter.Unit.Farad);
            _result.SecondaryResonanceNoTopLoad = UnitConverter.AutoScale(SecondaryResonanceNoTopLoad, UnitConverter.Unit.Hertz);
            _result.SecondaryXc = UnitConverter.AutoScale(SecondaryXc, UnitConverter.Unit.Ohm);
            _result.SecondaryXl = UnitConverter.AutoScale(SecondaryXl, UnitConverter.Unit.Ohm);
            _result.SecondaryWireLength = UnitConverter.AutoScale(SecondaryWireLength, UnitConverter.Unit.Meter);
            _result.SecondaryWireWeight = UnitConverter.AutoScale(SecondaryWireWeight, UnitConverter.Unit.Gram);

        }


        private static void CalculateResonance()
        {
            double denominator = 2 * Math.PI * Math.Sqrt(SecondaryInductance * SecondaryCapacitance);
            double denominatorNoTopLoad = 2 * Math.PI * Math.Sqrt(SecondaryInductance * (SecondaryCapacitance - TopLoadCapacitance));
            if (denominator == 0 || denominatorNoTopLoad == 0) return;

            SecondaryResonance = 1 / denominator;
            SecondaryResonanceNoTopLoad = 1 / denominatorNoTopLoad;
        }

        private static void CalculateCoilLength()
        {
            SecondaryCoilHeight = _data.SecondaryTurns * _data.SecondaryWireInsulationDiameter;
        }

        private static void CalculateInductance()
        {

            // L = (N^2 * d^2) / (18 * d + 40 * s)
            // where N = turns, d = diameter in inches, s = spacing in inches
            double diameter = UnitConverter.ConvertMmToIn(_data.SecondaryCoreDiameter + _data.SecondaryWireInsulationDiameter);
            double turns = _data.SecondaryTurns;
            double coilHeight = UnitConverter.ConvertMmToIn(SecondaryCoilHeight);



            double denominator = (18 * diameter) + (40 * coilHeight);

            if (denominator == 0) return;

            SecondaryInductance = (Math.Pow(turns, 2) * Math.Pow(diameter, 2)) / denominator;
            SecondaryInductance *= Math.Pow(10, -6);

            
        }



        private static void CalculateCapacitance()
        {
            double SecondaryCoilCapacitance = (_data.SecondaryWireInsulationDiameter / 10) * ((SecondaryCoilHeight / 10) * (_data.SecondaryCoreDiameter / 10));
            SecondaryCoilCapacitance *= Math.Pow(10, -12);
            CalculateTopLoadCapacitance();

            SecondaryCapacitance = SecondaryCoilCapacitance + (StrayCapacitance * Math.Pow(10, -12)) + TopLoadCapacitance;
        }

        private static void CalculateTopLoadCapacitance()
        {
            // C = 2.8 * (1.2781 - (D2/D1)) * SQRT((2 * pi^2 * (D1-D2) * (D2/2)) \ 4 * pi)
            // D1 -> outside diameter of toroid in inches
            // D2 -> diameter of cross section of toroid in inches
            // capacitance in picofarads
            if (_data.TopLoadType == TopLoadType.Torus)
            {
                double TorusThickness = (_data.TopLoadTorusOutDiameter - _data.TopLoadTorusInDiameter) / 2;

                double D1 = UnitConverter.ConvertMmToIn(_data.TopLoadTorusOutDiameter);
                double D2 = UnitConverter.ConvertMmToIn(TorusThickness);

                double firstPart = 1.2781 - (D2 / D1);
                double secondPart = (2 * Math.Pow(Math.PI, 2) * (D1 - D2) * (D2 / 2)) / (4 * Math.PI);
                TopLoadCapacitance = 2.8 * firstPart * Math.Sqrt(secondPart);
                TopLoadCapacitance *= Math.Pow(10, -12);
            }
        }

        private static void CalculateCapacitiveReactance()
        {
            SecondaryXc = 1 / (2 * Math.PI * SecondaryResonance * SecondaryCapacitance);
        }
        private static void CalculateInductiveReactance()
        {
            SecondaryXl = 2 * Math.PI * SecondaryResonance * SecondaryInductance;
        }


        private static void CalculateWireLength()
        {
            SecondaryWireLength = (Math.PI * _data.SecondaryTurns * (_data.SecondaryCoreDiameter + _data.SecondaryWireInsulationDiameter)) / 1000;
        }

        private static void CalculateWireWeight()
        {
            double CuDensity = 8.96;

            double WireRadius = (_data.SecondaryWireDiameter / 2) / 10;
            double WireCrossSection = Math.PI * Math.Pow(WireRadius, 2);
            double Volume = WireCrossSection * (SecondaryWireLength * 100); // from m to cm 
            SecondaryWireWeight = Volume * CuDensity;
        }


    }
}
