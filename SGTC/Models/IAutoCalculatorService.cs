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
        double CalculateOptimalTurns(double targetResonance, double capacitance, double coreDiameter, double wireInsDiameter, double baseTurns = 10);
        double GetInductance(double resonance, double capacitance);
    }

    public class AutoCalculatorService : IAutoCalculatorService
    {
        private readonly IUnitConverter _unitConverter;
        public AutoCalculatorService(IUnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public double CalculateOptimalCapacitance(double targetFrequency, double inductance)
        {
            double denominator = Math.Pow(2 * Math.PI * targetFrequency, 2) * inductance;
            return 1 / denominator;
        }

        public double CalculateOptimalTurns(double targetResonance, double capacitance, double coreDiameter, double wireInsDiameter, double baseTurns)
        {
            double inductanceH = GetInductance(targetResonance, capacitance);
            double inductanceUH = _unitConverter.ConvertValue(inductanceH, Unit.Base, Unit.Micro);

            double diameterInches = _unitConverter.ConvertMmToIn(coreDiameter + wireInsDiameter);
            double wireDiameterInches = _unitConverter.ConvertMmToIn(wireInsDiameter);

            double turns = baseTurns;
            double previousTurns = 0;

            int maxIterations = 100;
            double tolerance = 0.01; // 1% change tolerance

            for (int i = 0; i < maxIterations; i++)
            {
                double estimatedHeightInches = turns * wireDiameterInches;
                double numerator = inductanceUH * ((18 * diameterInches) + (40 * estimatedHeightInches));
                double denominator = Math.Pow(diameterInches, 2);

                turns = Math.Sqrt(numerator / denominator);

                if (Math.Abs(turns - previousTurns) / turns < tolerance)
                {
                    break;
                }

                previousTurns = turns;
            }

            return Math.Round(turns * 2) / 2;
        }

        public double GetInductance(double resonance, double capacitance)
        {
            double denominator = 4 * Math.Pow(Math.PI, 2) * Math.Pow(resonance, 2) * capacitance;
            return denominator == 0 ? 0 : 1 / denominator;
        }
    }

}
