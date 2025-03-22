using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SGTC.Core;
using SGTC.Models;

namespace SGTC.ViewModels
{
    public class CapacitanceCalculatedEventArgs : EventArgs
    {
        public double OptimalCapacitance { get; set; }
    }

    public class ResultGraphViewModel : ObservableObject
    {
        public static event EventHandler<CapacitanceCalculatedEventArgs> CapacitanceCalculated;
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;

        private double _optimalCapacitance;
        public double OptimalCapacitance
        {
            get => _optimalCapacitance;
            set
            {
                if (_optimalCapacitance != value)
                {
                    _optimalCapacitance = value;
                    OnPropertyChanged();
                }
            }
        }

        public ChartValues<ObservablePoint> PrimaryResonance { get; set; }
        public ChartValues<ObservablePoint> SecondaryResonance { get; set; }
        public ChartValues<ObservablePoint> BaseValues { get; set; }
        public ChartValues<ObservablePoint> OptimalValues { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ResultGraphViewModel()
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();
            BaseValues = new ChartValues<ObservablePoint>();
            OptimalValues = new ChartValues<ObservablePoint>();

            double baseCapacitance = _result.PrimaryCapacitance;
            double baseResonance = _result.PrimaryResonance;
            double secondaryResonance = _result.SecondaryResonance;
            double primaryInductance = _result.PrimaryInductance;

            BaseValues.Add(new ObservablePoint(baseCapacitance, baseResonance));

            OptimalCapacitance = CalculateBestCapacitance(secondaryResonance, primaryInductance);

            CapacitanceCalculated?.Invoke(this, new CapacitanceCalculatedEventArgs
            {
                OptimalCapacitance = OptimalCapacitance
            });


            PopulateChartWithDistributedValues(OptimalCapacitance, primaryInductance, secondaryResonance, baseCapacitance);


            //double optimalCapacitance = FindOptimalCapacitance();
            OptimalValues.Add(new ObservablePoint(
                OptimalCapacitance,
                PrimaryCalculator.CalculateResonance(primaryInductance, OptimalCapacitance)
            ));



            XFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Farad);
            YFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Hertz);
        }

        private void PopulateChartWithDistributedValues(double optimalCapacitance, double primaryInductance, double secondaryResonance, double baseCapacitance)
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();

            // Create a range that spans from 50% to 150% of base capacitance
            double minCapacitance = optimalCapacitance * 0.5;
            double maxCapacitance = optimalCapacitance * 1.5;

            if (baseCapacitance > optimalCapacitance)
            {
                maxCapacitance = baseCapacitance * 1.5;
            }
            else
            {
                minCapacitance = baseCapacitance * 0.5;
            }

            const int maxPoints = 10;

            // Calculate capacitance step size for even distribution
            double range = maxCapacitance - minCapacitance;
            double step = range / (maxPoints - 1);

            // Add points with meaningful differences
            for (int i = 0; i < maxPoints; i++)
            {
                double capacitance = minCapacitance + (step * i);
                double resonance = PrimaryCalculator.CalculateResonance(primaryInductance, capacitance);

                PrimaryResonance.Add(new ObservablePoint(capacitance, resonance));
                SecondaryResonance.Add(new ObservablePoint(capacitance, secondaryResonance));
            }
          
        }

        private double CalculateBestCapacitance(double frequency, double inductance)
        {
            double denominator = Math.Pow((2 * Math.PI * frequency), 2) * inductance;
            return 1 / denominator;
        }

        private double FindOptimalCapacitance()
        {
            double primaryInductance = _result.PrimaryInductance;
            double primaryCapacitance = _result.PrimaryCapacitance;
            double secondaryResonance = _result.SecondaryResonance;

            double targetFrequency = secondaryResonance;

            double lowerCapacitance = 0.1 * primaryCapacitance;
            double upperCapacitance = 10 * primaryCapacitance;

            double currentCapacitance;
            double currentFrequency;
            double bestCapacitance = primaryCapacitance;
            double bestDifference = Math.Abs(PrimaryCalculator.CalculateResonance(primaryInductance, bestCapacitance) - targetFrequency);


            int maxIterations = 100;
            for (int i = 0; i < maxIterations; i++)
            {
                // Calculate midpoint for binary search
                currentCapacitance = (lowerCapacitance + upperCapacitance) / 2;
                currentFrequency = PrimaryCalculator.CalculateResonance(primaryInductance, currentCapacitance);

                // Calculate difference percentage
                double difference = Math.Abs(currentFrequency - targetFrequency);
                double percentDifference = (difference / targetFrequency) * 100;

                // If this is better than our previous best, save it
                if (difference < bestDifference)
                {
                    bestCapacitance = currentCapacitance;
                    bestDifference = difference;
                }

                // If we're within acceptable range (3%), we're done
                if (percentDifference <= 2.0)
                {
                    return currentCapacitance;
                }

                // Adjust search boundaries
                if (currentFrequency > targetFrequency)
                {
                    // Increase capacitance to decrease frequency
                    lowerCapacitance = currentCapacitance;
                }
                else
                {
                    // Decrease capacitance to increase frequency
                    upperCapacitance = currentCapacitance;
                }

                // Break if the search range becomes too small (convergence)
                if (Math.Abs(upperCapacitance - lowerCapacitance) < 1e-12)
                {
                    break;
                }
            }
            return bestCapacitance;
        }
    }
}
