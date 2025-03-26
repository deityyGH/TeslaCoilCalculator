using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SGTC.Core;

namespace SGTC.Models
{

    public interface IAutoCalculatorService
    {
        double CalculateOptimalCapacitance(double targetFrequency, double inductance);
        double CalculateOptimalTurns(double targetResonance, double capacitance, double coreDiameter, double wireInsDiameter, double baseTurns = 10, bool round = false);
        double GetInductance(double resonance, double capacitance);
    }

    public class AutoCalculatorService : IAutoCalculatorService
    {
        private readonly IUnitConverter _unitConverter;
        private readonly IUnitConverterFactory _converterFactory;

        private readonly Func<double, double> BaseToMicroConverter;

        public AutoCalculatorService(IUnitConverter unitConverter, IUnitConverterFactory converterFactory)
        {
            _unitConverter = unitConverter;
            _converterFactory = converterFactory;

            BaseToMicroConverter = _converterFactory.CreateConverter(Unit.Base, Unit.Micro);
        }

        public double CalculateOptimalCapacitance(double targetFrequency, double inductance)
        {
            double denominator = Math.Pow(2 * Math.PI * targetFrequency, 2) * inductance;
            return 1 / denominator;
        }

        public double CalculateOptimalTurns(double targetResonance, double capacitance, double coreDiameter, double wireInsDiameter, double baseTurns, bool round)
        {
            double inductance = GetInductance(targetResonance, capacitance);
            inductance = BaseToMicroConverter(inductance);

            double diameterInches = _unitConverter.ConvertMToIn(coreDiameter + wireInsDiameter);
            double wireDiameterInches = _unitConverter.ConvertMToIn(wireInsDiameter);

            double turns = baseTurns;
            double previousTurns = 0;

            int maxIterations = 100;
            double tolerance = 0.01; // 1% change tolerance

            for (int i = 0; i < maxIterations; i++)
            {
                double estimatedHeightInches = turns * wireDiameterInches;
                double numerator = inductance * ((18 * diameterInches) + (40 * estimatedHeightInches));
                double denominator = Math.Pow(diameterInches, 2);

                turns = Math.Sqrt(numerator / denominator);

                if (Math.Abs(turns - previousTurns) / turns < tolerance)
                {
                    break;
                }

                previousTurns = turns;
            }
            return round ? Math.Round(turns * 2) / 2 : turns;
        }

        public double GetInductance(double resonance, double capacitance)
        {
            double denominator = 4 * Math.Pow(Math.PI, 2) * Math.Pow(resonance, 2) * capacitance;
            return denominator == 0 ? 0 : 1 / denominator;
        }
    }

}
