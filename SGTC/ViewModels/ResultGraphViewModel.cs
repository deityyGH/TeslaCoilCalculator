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
    public class ResultGraphViewModel : ObservableObject
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;
        private const int NumberOfTries = 5;

        public ChartValues<ObservablePoint> PrimaryResonance { get; set; }
        public ChartValues<ObservablePoint> SecondaryResonance { get; set; }
        public ChartValues<ObservablePoint> BaseValues { get; set; }

        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ResultGraphViewModel()
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();
            BaseValues = new ChartValues<ObservablePoint>();

            double baseCapacitance = _result.PrimaryCapacitance;
            double baseResonance = _result.PrimaryResonance;
            double secondaryResonance = _result.SecondaryResonance;

            BaseValues.Add(new ObservablePoint(baseCapacitance, baseResonance));

            // Add Secondary Resonance as a constant line
            for (int i = -NumberOfTries; i <= NumberOfTries; i++)
            {
                double capacitance = baseCapacitance * (1 + (i * 0.05));
                double resonance = PrimaryCalculator.CalculateResonance(_result.PrimaryInductance, capacitance);

                PrimaryResonance.Add(new ObservablePoint(capacitance, resonance));
                SecondaryResonance.Add(new ObservablePoint(capacitance, secondaryResonance));
            }

            // Format X (Capacitor Value) and Y (Frequency) for better display
            //XFormatter = value => $"{value * Math.Pow(10, 9):0.00} nF"; // Display capacitance in nanofarads
            //YFormatter = value => $"{value * Math.Pow(10, -3):0.00} kHz"; // Display frequency in Hz
            XFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Farad); // Display capacitance in nanofarads
            YFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Hertz); // Display frequency in Hz
        }
    }
}
