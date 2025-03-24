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
        //IUnitConverter _unitConverter = new UnitConverter();
        public static void Run(bool calculatePrimary = true, bool calculateSecondary = true)
        {
            if (calculatePrimary) CalculatePrimary();
            if (calculateSecondary) CalculateSecondary();
        }


        public static void CalculatePrimary(double? _turns = null, double? _coreDiameter = null, double? _wireDiameter = null, double? _wireInsDiameter = null, double? _spacing = null, double? _capacitance = null)
        {
            double primaryTurns = (double)(_turns == null ? _data.PrimaryTurns : _turns);
            double coreDiameter = (double)(_coreDiameter == null ? _data.PrimaryCoreDiameter : _coreDiameter);
            double wireDiameter = (double)(_wireDiameter == null ? _data.PrimaryWireDiameter : _wireDiameter);
            double wireInsDiameter = (double)(_wireInsDiameter == null ? _data.PrimaryWireInsulationDiameter : _wireInsDiameter);
            double spacing = (double)(_spacing == null ? _data.PrimaryWireSpacing : _spacing);
            double Capacitance = _capacitance == null
                ? CalculateCapacitance(_data.PrimaryCapacitorConnectionType, _data.PrimaryCapacitance * 1e-9, _data.PrimaryCapacitorAmount)
                : (double)_capacitance;

            double CoilHeight = CalculateCoilHeight(_data.PrimaryWindingType, primaryTurns, wireInsDiameter, spacing);
            double Inductance = CalculateInductance(_data.PrimaryWindingType, primaryTurns, coreDiameter, wireInsDiameter, CoilHeight);
            double Resonance = CalculateResonance(Inductance, Capacitance);
            double Xc = CalculateCapacitiveReactance(Resonance, Capacitance);
            double Xl = CalculateInductiveReactance(Resonance, Inductance);
            double WireLength = CalculateWireLength(primaryTurns, coreDiameter);
            double WireWeight = CalculateWireWeight(wireDiameter, WireLength);

            _result.PrimaryTurns = primaryTurns;
            _result.PrimaryCoilHeight = CoilHeight;
            _result.PrimaryInductance = Inductance;
            _result.PrimaryCapacitance = Capacitance;
            _result.PrimaryResonance = Resonance;
            _result.PrimaryXc = Xc;
            _result.PrimaryXl = Xl;
            _result.PrimaryWireLength = WireLength;
            _result.PrimaryWireWeight = WireWeight;
        }

        public static void CalculateSecondary()
        {
            double CoilHeight = CalculateCoilHeight(PrimaryWindingType.Solenoid, _data.SecondaryTurns, _data.SecondaryWireInsulationDiameter, 0);
            double Inductance = CalculateInductance(PrimaryWindingType.Solenoid, _data.SecondaryTurns, _data.SecondaryCoreDiameter, _data.SecondaryWireInsulationDiameter, CoilHeight);
            double Capacitance = CalculateSecondaryCoilCapacitance(_data.SecondaryWireInsulationDiameter, CoilHeight, _data.SecondaryCoreDiameter, true, true);
            double CapacitanceNoTopLoad = CalculateSecondaryCoilCapacitance(_data.SecondaryWireInsulationDiameter, CoilHeight, _data.SecondaryCoreDiameter, false, true);

            double Resonance = CalculateResonance(Inductance, Capacitance);
            double ResonanceNoTopLoad = CalculateResonance(Inductance, CapacitanceNoTopLoad);
            double Xc = CalculateCapacitiveReactance(Resonance, Capacitance);
            double Xl = CalculateInductiveReactance(Resonance, Inductance);
            double WireLength = CalculateWireLength(_data.SecondaryTurns, _data.SecondaryCoreDiameter);
            double WireWeight = CalculateWireWeight(_data.SecondaryWireDiameter, WireLength);

            _result.SecondaryCoilHeight = CoilHeight;
            _result.SecondaryInductance = Inductance;
            _result.SecondaryResonance = Resonance;
            _result.SecondaryResonanceNoTopLoad = ResonanceNoTopLoad;
            _result.SecondaryXc = Xc;
            _result.SecondaryXl = Xl;
            _result.SecondaryWireLength = WireLength;
            _result.SecondaryWireWeight = WireWeight;
            _result.TotalCapacitance = Capacitance;
            _result.NoTopLoadCapacitance = CapacitanceNoTopLoad;
        }


        public static double CalculateSecondaryTorusCapacitance(double outDiameter, double inDiameter)
        {
            // C = 2.8 * (1.2781 - (D2/D1)) * SQRT((2 * pi^2 * (D1-D2) * (D2/2)) \ 4 * pi)
            // D1 -> outside diameter of toroid in inches
            // D2 -> diameter of cross section of toroid in inches
            // capacitance in picofarads
            
            double TorusThickness = (outDiameter - inDiameter) / 2;

            double D1 = UnitConverter2.ConvertMmToIn(outDiameter);
            double D2 = UnitConverter2.ConvertMmToIn(TorusThickness);

            double firstPart = 1.2781 - (D2 / D1);
            double secondPart = (2 * Math.Pow(Math.PI, 2) * (D1 - D2) * (D2 / 2)) / (4 * Math.PI);
            double TopLoadCapacitance = 2.8 * firstPart * Math.Sqrt(secondPart);
            TopLoadCapacitance *= Math.Pow(10, -12);

            return TopLoadCapacitance;

        }

        public static double CalculateSecondaryCoilCapacitance(double wireInsDiameter, double coilHeight, double coreDiameter, bool useTopLoad, bool useStrayC = true)
        {
            double strayCapacitance = 0;
            double TopLoadCapacitance = 0;

            double SecondaryCoilCapacitance = (wireInsDiameter / 10) * ((coilHeight / 10) * (coreDiameter / 10));
            SecondaryCoilCapacitance *= Math.Pow(10, -12);
            if (useTopLoad)
            {
                TopLoadCapacitance = CalculateSecondaryTorusCapacitance(_data.TopLoadTorusOutDiameter, _data.TopLoadTorusInDiameter);
            }

            if (useStrayC)
            {
                strayCapacitance = 7 * Math.Pow(10, -12);
            }

            return SecondaryCoilCapacitance + strayCapacitance + TopLoadCapacitance;
        }

        public static double CalculateResonance(double primaryInductance, double primaryCapacitance)
        {
            double denominator = 2 * Math.PI * Math.Sqrt(primaryInductance * primaryCapacitance);
            return denominator == 0 ? 0 : 1 / denominator;
        }

        public static double CalculateCoilHeight(PrimaryWindingType windingType, double turns, double wireInsDiameter, double wireSpacing)
        {
            return windingType == PrimaryWindingType.Solenoid ? turns * (wireInsDiameter + wireSpacing) : 0;
        }

        public static double CalculateInductance(PrimaryWindingType windingType, double turns, double coreDiameter, double wireInsDiameter, double coilHeight)
        {
            
            if (windingType == PrimaryWindingType.Solenoid)
            {
                // L = (N^2 * d^2) / (18 * d + 40 * s)
                // where N = turns, d = diameter in inches, s = spacing in inches
                double diameter = UnitConverter2.ConvertMmToIn(coreDiameter + wireInsDiameter);
                double coilHeightIn = UnitConverter2.ConvertMmToIn(coilHeight);

                double denominator = (18 * diameter) + (40 * coilHeightIn);
                if (denominator == 0)
                {
                    return 0;
                }

                double PrimaryInductance = Math.Pow(turns, 2) * Math.Pow(diameter, 2) / denominator;
                PrimaryInductance *= Math.Pow(10, -6);

                return PrimaryInductance;
            }

            return 0;
        }



        public static double CalculateCapacitance(PrimaryCapacitorConnectionType connectionType, double capacitance, int numberOfCapacitors)
        {
            //double capacitanceBase = capacitance * Math.Pow(10, -9);
            
            if (connectionType == PrimaryCapacitorConnectionType.Parallel)
            {
                return capacitance * numberOfCapacitors;
            }

            if (numberOfCapacitors == 1)
            {
                return capacitance;
            }

            double totalCapacitance = 0;
            for (int i = 0; i < numberOfCapacitors; i++)
            {
                totalCapacitance = (1 / capacitance) + totalCapacitance;
            }

            return totalCapacitance;
        }


        public static double CalculateCapacitiveReactance(double resonance, double capacitance)
        {
            return 1 / (2 * Math.PI * resonance * capacitance);
        }
        public static double CalculateInductiveReactance(double resonance, double inductance)
        {
            return 2 * Math.PI * resonance * inductance;
        }


        public static double CalculateWireLength(double turns, double coreDiameter)
        {
            return Math.PI * turns * coreDiameter / 1000;
        }

        public static double CalculateWireWeight(double wireDiameter, double wireLength)
        {
            double CuDensity = 8.96;

            double WireRadius = wireDiameter / 2 / 10; // convert to cm
            double WireCrossSection = Math.PI * Math.Pow(WireRadius, 2);
            double Volume = WireCrossSection * (wireLength * 100);
            return Volume * CuDensity;
        }
    }
}
