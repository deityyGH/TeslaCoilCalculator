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
    public enum GraphType
    {
        Capacitance,
        Turns,
        TopLoad,
        CoreDiameter
    }

    public enum Coil
    {
        Primary,
        Secondary
    }

    public class GraphCalculatedEventArgs : EventArgs
    {
        public double OptimalValue { get; set; }
        public Coil Coil { get; set; }
        public GraphType GraphType { get; set; }
    }


    public class ResultGraphViewModel : ObservableObject
    {

        private delegate double ValueConverter(double value);

        private ValueConverter NanoToBaseConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Nano, UnitConverter.Unit.Base);
        private ValueConverter BaseToKiloConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Base, UnitConverter.Unit.Kilo);
        private ValueConverter BaseToNanoConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Base, UnitConverter.Unit.Nano);
        private ValueConverter BaseConverter => (value) => value;

        public static event EventHandler<GraphCalculatedEventArgs> GraphCalculated;
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
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


        private double _optimalTurns;
        public double OptimalTurns
        {
            get => _optimalTurns;
            set
            {
                if (_optimalTurns != value)
                {
                    _optimalTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _graphTitleX;
        public string GraphTitleX
        {
            get => _graphTitleX;
            set
            {
                if (_graphTitleX != value)
                {
                    _graphTitleX = value;
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

        public ResultGraphViewModel(Coil coil, GraphType graphType)
        {

            if (coil == Coil.Primary)
            {
                if (graphType == GraphType.Capacitance)
                {
                    AutoPrimaryCapacitanceCalculation();
                    GraphTitleX = "Capacitance [nF]";
                }
                else if (graphType == GraphType.Turns)
                {
                    AutoPrimaryTurnsCalculation();
                    GraphTitleX = "Turns";
                }
            }
        }

        private void AddPoint(ChartValues<ObservablePoint> chartValues, double xValue, double yValue, ValueConverter? xConverter = null , ValueConverter? yConverter = null)
        {
            xConverter ??= BaseConverter;
            yConverter ??= BaseConverter;

            double convertedX = xConverter(xValue);
            double convertedY = yConverter(yValue);

            chartValues.Add(new ObservablePoint(convertedX, convertedY));
        }

        private void AutoPrimaryTurnsCalculation()
        {

            BaseValues = new ChartValues<ObservablePoint>();
            OptimalValues = new ChartValues<ObservablePoint>();

            double baseCapacitance = _result.PrimaryCapacitance;
            double baseResonance = _result.PrimaryResonance;
            double secondaryResonance = _result.SecondaryResonance;
            double baseTurns = _data.PrimaryTurns;
            double coreDiameter = _data.PrimaryCoreDiameter;
            double wireInsDiameter = _data.PrimaryWireInsulationDiameter;
            double coilHeight = _result.PrimaryCoilHeight;
            double spacing = _data.PrimaryWireSpacing;

            AddPoint(BaseValues, baseTurns, baseResonance, yConverter: BaseToKiloConverter);

            OptimalTurns = CalculateBestTurns(secondaryResonance, baseCapacitance, coreDiameter, wireInsDiameter, coilHeight);

            GraphCalculated?.Invoke(this, new GraphCalculatedEventArgs
            {
                OptimalValue = OptimalTurns,
                Coil = Coil.Primary,
                GraphType = GraphType.Turns
            });

            PopulateChartTurns(OptimalTurns, wireInsDiameter, spacing, coreDiameter, baseCapacitance, secondaryResonance);

            AddPoint(OptimalValues, OptimalTurns, secondaryResonance, yConverter: BaseToKiloConverter);
        }


        private double CalculateBestTurns(double targetResonance, double capacitance, double coreDiameter, double wireInsDiameter, double coilHeight)
        {
            double inductanceH = CalculateBestInductance(targetResonance, capacitance);
            double inductanceUH = inductanceH * 1000000;

            double diameterInches = UnitConverter.ConvertMmToIn(coreDiameter + wireInsDiameter);
            double wireDiameterInches = UnitConverter.ConvertMmToIn(wireInsDiameter);

            double turns = _data.PrimaryTurns;
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
                    break;

                previousTurns = turns;
            }

            return Math.Round(turns * 2) / 2;
        }

        private double CalculateBestInductance(double resonance, double capacitance)
        {
            double denominator = 4 * Math.Pow(Math.PI, 2) * Math.Pow(resonance, 2) * capacitance;
            if (denominator == 0)
                return 0;

            return 1 / denominator;
        }

        private void AutoPrimaryCapacitanceCalculation()
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();
            BaseValues = new ChartValues<ObservablePoint>();
            OptimalValues = new ChartValues<ObservablePoint>();

            double baseCapacitance = _result.PrimaryCapacitance;
            double baseResonance = _result.PrimaryResonance;
            double secondaryResonance = _result.SecondaryResonance;
            double primaryInductance = _result.PrimaryInductance;

            //double convertedCapacitance = UnitConverter.ConvertValue(baseCapacitance, UnitConverter.Unit.Base, UnitConverter.Unit.Nano);
            //BaseValues.Add(new ObservablePoint(convertedCapacitance, baseResonance));

            AddPoint(BaseValues, baseCapacitance, baseResonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);

            OptimalCapacitance = CalculateBestCapacitance(secondaryResonance, primaryInductance);

            GraphCalculated?.Invoke(this, new GraphCalculatedEventArgs
            {
                OptimalValue = OptimalCapacitance,
                Coil = Coil.Primary,
                GraphType = GraphType.Capacitance
            });


            PopulateChartWithDistributedValues(OptimalCapacitance, primaryInductance, secondaryResonance, baseCapacitance);

            AddPoint(OptimalValues, OptimalCapacitance, secondaryResonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);

            XFormatter = value => $"{value}";
            YFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Hertz);
        }

        private void PopulateChartTurns(double baseTurns, double wireInsDiameter, double spacing, double coreDiameter, double capacitance, double targetResonance, int numberOfSteps = 5, double stepSize = 0.5)
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();
            for (int i = -numberOfSteps; i < numberOfSteps; i++)
            {
                double currentTurns = baseTurns + i * stepSize;

                double currentCoilHeight = PrimaryCalculator.CalculateCoilHeight(PrimaryWindingType.Solenoid, currentTurns, wireInsDiameter, spacing);
                double currentInductance = PrimaryCalculator.CalculateInductance(PrimaryWindingType.Solenoid, currentTurns, coreDiameter, wireInsDiameter, currentCoilHeight);
                double currentResonance = PrimaryCalculator.CalculateResonance(currentInductance, capacitance);

                AddPoint(PrimaryResonance, currentTurns, currentResonance, yConverter: BaseToKiloConverter);
                AddPoint(SecondaryResonance, currentTurns, targetResonance, yConverter: BaseToKiloConverter);
            }
        }

        private void PopulateChartWithDistributedValues(double optimalCapacitance, double primaryInductance, double secondaryResonance, double baseCapacitance)
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();

            // Create a range that spans from 50% to 150% of base capacitance
            double minCapacitance = optimalCapacitance * 0.5;
            if (minCapacitance < 0)
                minCapacitance = 0;

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

                AddPoint(PrimaryResonance, capacitance, resonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);
                AddPoint(SecondaryResonance, capacitance, secondaryResonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);
            }
        }

        private double CalculateBestCapacitance(double targetFrequency, double inductance)
        {
            double denominator = Math.Pow((2 * Math.PI * targetFrequency), 2) * inductance;
            return 1 / denominator;
        }
    }
}
