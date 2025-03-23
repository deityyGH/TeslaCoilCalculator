using System;
using LiveCharts;
using LiveCharts.Defaults;
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

    public enum CoilType
    {
        Primary,
        Secondary
    }

    public class GraphCalculatedEventArgs : EventArgs
    {
        public double OptimalValue { get; set; }
        public CoilType CoilType { get; set; }
        public GraphType GraphType { get; set; }
    }


    public class ResultGraphViewModel : ObservableObject
    {
        public static event EventHandler<GraphCalculatedEventArgs> GraphCalculated;

        private readonly IAutoCalculatorService _calculatorService;
        private readonly GraphType _graphType;
        private readonly CoilType _coilType;
        private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly CoilCalculatorResult _result = CoilCalculatorResult.Instance;

        private delegate double ValueConverter(double value);
        private ValueConverter NanoToBaseConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Nano, UnitConverter.Unit.Base);
        private ValueConverter BaseToKiloConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Base, UnitConverter.Unit.Kilo);
        private ValueConverter BaseToNanoConverter => (value) => UnitConverter.ConvertValue(value, UnitConverter.Unit.Base, UnitConverter.Unit.Nano);
        private ValueConverter BaseConverter => (value) => value;

        public ChartValues<ObservablePoint> PrimaryResonance { get; private set; }
        public ChartValues<ObservablePoint> SecondaryResonance { get; private set; }
        public ChartValues<ObservablePoint> BaseValues { get; private set; }
        public ChartValues<ObservablePoint> OptimalValues { get; private set; }

        public Func<double, string> XFormatter { get; private set; }
        public Func<double, string> YFormatter { get; private set; }
        public string XAxisTitle { get; private set; }
        public string YAxisTitle { get; private set; }

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


        public ResultGraphViewModel(CoilType coilType, GraphType graphType, IAutoCalculatorService calculatorService = null)
        {
            _graphType = graphType;
            _coilType = coilType;

            _calculatorService = calculatorService ?? new AutoCalculatorService();

            SetupChart(_coilType, _graphType);
        }

        private void SetupChart(CoilType coilType, GraphType graphType)
        {
            if (coilType == CoilType.Primary)
            {
                if (graphType == GraphType.Capacitance)
                {
                    SetupCapacitanceGraph();
                    XAxisTitle = "Capacitance [nF]";
                    YAxisTitle = "Resonance [kHz]";
                }
                else if (graphType == GraphType.Turns)
                {
                    SetupTurnsGraph();
                    XAxisTitle = "Turns";
                    YAxisTitle = "Resonance [kHz]";
                }
            }
        }

        private void AddPoint(ChartValues<ObservablePoint> chartValues, double xValue, double yValue, ValueConverter xConverter = null, ValueConverter yConverter = null)
        {
            xConverter ??= BaseConverter;
            yConverter ??= BaseConverter;

            double convertedX = xConverter(xValue);
            double convertedY = yConverter(yValue);

            chartValues.Add(new ObservablePoint(convertedX, convertedY));
        }

        private void SetupTurnsGraph()
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

            OptimalTurns = _calculatorService.CalculateOptimalTurns(secondaryResonance, baseCapacitance, coreDiameter, wireInsDiameter);

            GraphCalculated?.Invoke(this, new GraphCalculatedEventArgs
            {
                OptimalValue = OptimalTurns,
                CoilType = CoilType.Primary,
                GraphType = GraphType.Turns
            });

            PopulateTurnsChart(OptimalTurns, wireInsDiameter, spacing, coreDiameter, baseCapacitance, secondaryResonance);
            AddPoint(OptimalValues, OptimalTurns, secondaryResonance, yConverter: BaseToKiloConverter);

        }

        private void SetupCapacitanceGraph()
        {
            PrimaryResonance = new ChartValues<ObservablePoint>();
            SecondaryResonance = new ChartValues<ObservablePoint>();
            BaseValues = new ChartValues<ObservablePoint>();
            OptimalValues = new ChartValues<ObservablePoint>();

            double baseCapacitance = _result.PrimaryCapacitance;
            double baseResonance = _result.PrimaryResonance;
            double secondaryResonance = _result.SecondaryResonance;
            double primaryInductance = _result.PrimaryInductance;

            AddPoint(BaseValues, baseCapacitance, baseResonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);

            OptimalCapacitance = _calculatorService.CalculateOptimalCapacitance(secondaryResonance, primaryInductance);

            GraphCalculated?.Invoke(this, new GraphCalculatedEventArgs
            {
                OptimalValue = OptimalCapacitance,
                CoilType = CoilType.Primary,
                GraphType = GraphType.Capacitance
            });


            PopulateCapacitanceChart(OptimalCapacitance, primaryInductance, secondaryResonance, baseCapacitance);

            AddPoint(OptimalValues, OptimalCapacitance, secondaryResonance, xConverter: BaseToNanoConverter, yConverter: BaseToKiloConverter);

            XFormatter = value => $"{value}";
            YFormatter = value => UnitConverter.AutoScale(value, UnitConverter.Unit.Hertz);
        }

        private void PopulateTurnsChart(double baseTurns, double wireInsDiameter, double spacing, double coreDiameter, double capacitance, double targetResonance, int numberOfSteps = 5, double stepSize = 1)
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

        private void PopulateCapacitanceChart(double optimalCapacitance, double primaryInductance, double secondaryResonance, double baseCapacitance)
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
    }
}
