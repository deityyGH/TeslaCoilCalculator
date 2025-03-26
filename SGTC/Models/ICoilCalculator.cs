using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGTC.Models
{
    public interface ICoilCalculator
    {
        CoilResults CalculatePrimary(CoilParameters parameters, CoilResults results);
        CoilResults CalculateSecondary(CoilParameters parameters, CoilResults results);
        double CalculateResonance(double primaryInductance, double primaryCapacitance);
        double CalculateCoilHeight(PrimaryWindingType windingType, double turns, double wireInsDiameter, double wireSpacing);
        double CalculateInductance(PrimaryWindingType windingType, double turns, double coreDiameter, double wireInsDiameter, double coilHeight);

    }

    public class CoilCalculator : ICoilCalculator
    {
        private readonly IUnitConverter _unitConverter;
        private readonly IUnitConverterFactory _converterFactory;

        private readonly Func<double, double> MicroToBaseConverter;
        private readonly Func<double, double> PicoToBaseConverter;

        public CoilCalculator(IUnitConverter unitConverter, IUnitConverterFactory converterFactory)
        {
            _unitConverter = unitConverter;
            _converterFactory = converterFactory;

            MicroToBaseConverter = _converterFactory.CreateConverter(Unit.Micro, Unit.Base);
            PicoToBaseConverter = _converterFactory.CreateConverter(Unit.Pico, Unit.Base);
        }

        public CoilResults CalculatePrimary(CoilParameters parameters, CoilResults results)
        {
            double Capacitance = CalculateCapacitance(
                parameters.PrimaryCapacitance);

            double CoilHeight = CalculateCoilHeight(
                parameters.PrimaryWindingType,
                parameters.PrimaryTurns,
                parameters.PrimaryWireInsulationDiameter,
                parameters.PrimaryWireSpacing);

            double Inductance = CalculateInductance(
                parameters.PrimaryWindingType,
                parameters.PrimaryTurns,
                parameters.PrimaryCoreDiameter,
                parameters.PrimaryWireInsulationDiameter,
                CoilHeight);

            double Resonance = CalculateResonance(
                Inductance,
                Capacitance);

            double Xc = CalculateCapacitiveReactance(
                Resonance,
                Capacitance);

            double Xl = CalculateInductiveReactance(
                Resonance,
                Inductance);

            double WireLength = CalculateWireLength(
                parameters.PrimaryTurns,
                parameters.PrimaryCoreDiameter);

            double WireWeight = CalculateWireWeight(
                parameters.PrimaryWireDiameter,
                WireLength);

            results.PrimaryTurns = parameters.PrimaryTurns;
            results.PrimaryCoilHeight = CoilHeight;
            results.PrimaryInductance = Inductance;
            results.PrimaryCapacitance = Capacitance;
            results.PrimaryResonance = Resonance;
            results.PrimaryXc = Xc;
            results.PrimaryXl = Xl;
            results.PrimaryWireLength = WireLength;
            results.PrimaryWireWeight = WireWeight;

            return results;
        }

        public CoilResults CalculateSecondary(CoilParameters parameters, CoilResults results)
        {
            double CoilHeight = CalculateCoilHeight(
                parameters.PrimaryWindingType,
                parameters.SecondaryTurns,
                parameters.SecondaryWireInsulationDiameter,
                0);

            double Inductance = CalculateInductance(
                parameters.PrimaryWindingType,
                parameters.SecondaryTurns,
                parameters.SecondaryCoreDiameter,
                parameters.SecondaryWireInsulationDiameter,
                CoilHeight);

            double TopLoadCapacitance = CalculateTopLoadCapacitance(
                parameters.TopLoadType,
                parameters.TopLoadTorusOutDiameter,
                parameters.TopLoadTorusInDiameter,
                parameters.TopLoadSphereDiameter);

            double Capacitance = CalculateSecondaryCoilCapacitance(
                parameters.SecondaryWireInsulationDiameter,
                CoilHeight,
                parameters.SecondaryCoreDiameter,
                TopLoadCapacitance,
                true);

            double CapacitanceNoTopLoad = CalculateSecondaryCoilCapacitance(
                parameters.SecondaryWireInsulationDiameter,
                CoilHeight,
                parameters.SecondaryCoreDiameter,
                0,
                true);

            double Resonance = CalculateResonance(
                Inductance,
                Capacitance);

            double ResonanceNoTopLoad = CalculateResonance(
                Inductance,
                CapacitanceNoTopLoad);

            double Xc = CalculateCapacitiveReactance(
                Resonance,
                Capacitance);

            double Xl = CalculateInductiveReactance(
                Resonance,
                Inductance);

            double WireLength = CalculateWireLength(
                parameters.SecondaryTurns,
                parameters.SecondaryCoreDiameter);

            double WireWeight = CalculateWireWeight(
                parameters.SecondaryWireDiameter,
                WireLength);

            results.SecondaryCoilHeight = CoilHeight;
            results.SecondaryInductance = Inductance;
            results.SecondaryResonance = Resonance;
            results.SecondaryResonanceNoTopLoad = ResonanceNoTopLoad;
            results.SecondaryXc = Xc;
            results.SecondaryXl = Xl;
            results.SecondaryWireLength = WireLength;
            results.SecondaryWireWeight = WireWeight;
            results.TotalCapacitance = Capacitance;
            results.TopLoadCapacitance = TopLoadCapacitance;

            return results;
        }


        public double CalculateTopLoadCapacitance(TopLoadType topLoadType, double torusOutDiameter, double torusInDiameter, double sphereDiameter)
        {
            if (topLoadType == TopLoadType.Torus)
            {
                return CalculateSecondaryTorusCapacitance(torusOutDiameter, torusInDiameter);
            }

            else if (topLoadType == TopLoadType.Sphere)
            {
                return 0;
            }
            else
            {
                return 0;
            }

        }

        private double CalculateSecondaryTorusCapacitance(double outDiameter, double inDiameter)
        {
            // C = 2.8 * (1.2781 - (D2/D1)) * SQRT((2 * pi^2 * (D1-D2) * (D2/2)) \ 4 * pi)
            // D1 -> outside diameter of toroid in inches
            // D2 -> diameter of cross section of toroid in inches
            // capacitance in picofarads

            double TorusThickness = (outDiameter - inDiameter) / 2;

            double D1 = _unitConverter.ConvertMToIn(outDiameter);
            double D2 = _unitConverter.ConvertMToIn(TorusThickness);

            double firstPart = 1.2781 - (D2 / D1);
            double secondPart = (2 * Math.Pow(Math.PI, 2) * (D1 - D2) * (D2 / 2)) / (4 * Math.PI);
            double TopLoadCapacitance = 2.8 * firstPart * Math.Sqrt(secondPart);
            //TopLoadCapacitance *= Math.Pow(10, -12);
            //TopLoadCapacitance = _unitConverter.ConvertValue(TopLoadCapacitance, Unit.Pico, Unit.Base);
            return PicoToBaseConverter(TopLoadCapacitance);

        }

        public double CalculateSecondaryCoilCapacitance(double wireInsDiameter, double coilHeight, double coreDiameter, double topLoadCapacitance, bool useStrayC = true)
        {
            double strayCapacitance = 7;

            double SecondaryCoilCapacitance = wireInsDiameter / 10 * ((coilHeight / 10) * (coreDiameter / 10));
            //SecondaryCoilCapacitance *= Math.Pow(10, -12);

            SecondaryCoilCapacitance = PicoToBaseConverter(SecondaryCoilCapacitance);

            if (useStrayC)
            {
                strayCapacitance = PicoToBaseConverter(strayCapacitance);
            }

            return SecondaryCoilCapacitance + strayCapacitance + topLoadCapacitance;
        }

        public double CalculateResonance(double primaryInductance, double primaryCapacitance)
        {
            double denominator = 2 * Math.PI * Math.Sqrt(primaryInductance * primaryCapacitance);
            return denominator == 0 ? 0 : 1 / denominator;
        }

        public double CalculateCoilHeight(PrimaryWindingType windingType, double turns, double wireInsDiameter, double wireSpacing)
        {
            return windingType == PrimaryWindingType.Solenoid ? turns * (wireInsDiameter + wireSpacing) : 0;
        }

        public double CalculateInductance(PrimaryWindingType windingType, double turns, double coreDiameter, double wireInsDiameter, double coilHeight)
        {

            if (windingType == PrimaryWindingType.Solenoid)
            {
                // L = (N^2 * d^2) / (18 * d + 40 * s)
                // where N = turns, d = diameter in inches, s = spacing in inches
                // result in uH
                double diameter = _unitConverter.ConvertMToIn(coreDiameter + wireInsDiameter);
                double coilHeightIn = _unitConverter.ConvertMToIn(coilHeight);

                double denominator = (18 * diameter) + (40 * coilHeightIn);
                if (denominator == 0)
                {
                    return 0;
                }

                double PrimaryInductance = Math.Pow(turns, 2) * Math.Pow(diameter, 2) / denominator;
                //PrimaryInductance = _unitConverter.ConvertValue(PrimaryInductance, Unit.Micro, Unit.Base);

                return MicroToBaseConverter(PrimaryInductance);
            }

            return 0;
        }



        public double CalculateCapacitance(double capacitance)
        {

            return capacitance;
        }


        public double CalculateCapacitiveReactance(double resonance, double capacitance)
        {
            return 1 / (2 * Math.PI * resonance * capacitance);
        }
        public double CalculateInductiveReactance(double resonance, double inductance)
        {
            return 2 * Math.PI * resonance * inductance;
        }


        public double CalculateWireLength(double turns, double coreDiameter)
        {
            return Math.PI * turns * coreDiameter;
        }

        public double CalculateWireWeight(double wireDiameter, double wireLength)
        {
            double CuDensity = 8.96;  // g/cm³

            double WireRadius = wireDiameter / 2;  // m
            double WireCrossSection = Math.PI * Math.Pow(WireRadius * 100, 2); // cm²
            double Volume = WireCrossSection * (wireLength * 100); // cm³
            double WireWeight = Volume * CuDensity; // g

            return WireWeight;
        }
    }
}
