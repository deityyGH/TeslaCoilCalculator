using System;
using System.Collections.ObjectModel;
using SGTC.Models;
using SGTC.Core;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Microsoft.Extensions.Logging;

namespace SGTC.ViewModels
{
    public enum CapacitanceUnit
    {
        Base,
        Milli,
        Micro,
        Nano,
        Pico
    }
    
    public class PrimaryCircuitViewModel : CalculatorValidationViewModel
    {
        private readonly ICoilDataService _dataService;
        private readonly IUnitConverterFactory _converterFactory;
        private readonly IUnitConverter _unitConverter;

        private ILengthConverter _lengthConverter;
        private LengthCalculator _lengthCalculator;

        private Func<double, double> MilliToBaseConverter;
        private Func<double, double> BaseToMilliConverter;
        private Func<double, double> XToBaseConverter => _converterFactory.CreateConverter(SelectedCapacitanceUnit, Unit.Base);
        private Func<double, double> BaseToXConverter => _converterFactory.CreateConverter(Unit.Base, SelectedCapacitanceUnit);

        public PrimaryCircuitViewModel() { }
        public PrimaryCircuitViewModel(ICoilDataService dataService, IUnitConverterFactory converterFactory, IUnitConverter converter, ILengthConverter lengthConverter)
        {
            _dataService = dataService;
            _converterFactory = converterFactory;
            _unitConverter = converter;
            _lengthConverter = lengthConverter;

            //ILengthConverter baseConverter = new BaseLengthConverter();
            _lengthCalculator = new LengthCalculator(_lengthConverter);
            

            SetupValidationRules();
            MilliToBaseConverter = _converterFactory.CreateConverter(Unit.Milli, Unit.Base);
            BaseToMilliConverter = _converterFactory.CreateConverter(Unit.Base, Unit.Milli);

            PrimaryWindingTypes = new ObservableCollection<PrimaryWindingType>
            {
                PrimaryWindingType.Solenoid,
                PrimaryWindingType.FlatSpiral,
                PrimaryWindingType.Conical
            };
        }

        protected override void SetupValidationRules()
        {
            AddValidationRule(nameof(PrimaryCapacitance), () =>
            {
                if (!IsPositive(PrimaryCapacitance))
                {
                    return "Primarycap wrong";
                }
                return null;
            });

            
            AddValidationRule(nameof(PrimaryTurns), () =>
            {
                if (!IsPositive(PrimaryTurns))
                {
                    return "Turns must be greater than zero.";
                }
                return null;
            });


            AddValidationRule(nameof(PrimaryCoreDiameter), () =>
            {
                if (!IsPositive(PrimaryCoreDiameter))
                {
                    return "Core Diameter must be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(PrimaryWireDiameter), () =>
            {
                if (!IsPositive(PrimaryWireDiameter))
                {
                    return "Wire Diameter must be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(PrimaryWireInsulationDiameter), () =>
            {
                if (!IsPositive(PrimaryWireInsulationDiameter))
                {
                    return "Wire Insulation Diameter must be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(PrimaryWireSpacing), () =>
            {
                if (!IsPositiveOrZero(PrimaryWireSpacing))
                {
                    return "Wire Spacing must be greater or equal to zero.";
                }
                return null;
            });

        }

        private ObservableCollection<PrimaryWindingType> _primaryWindingTypes;
        public ObservableCollection<PrimaryWindingType> PrimaryWindingTypes
        {
            get => _primaryWindingTypes;
            set
            {
                _primaryWindingTypes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PrimaryCapacitorConnectionType> _primaryCapacitorConnectionTypes;
        public ObservableCollection<PrimaryCapacitorConnectionType> PrimaryCapacitorConnectionTypes
        {
            get => _primaryCapacitorConnectionTypes;
            set
            {
                _primaryCapacitorConnectionTypes = value;
                OnPropertyChanged();
            }
        }

        public List<Unit> CapacitanceUnits { get; } = new List<Unit>
        {
            Unit.Pico,   // pF
            Unit.Nano,   // nF
            Unit.Micro,  // µF
            Unit.Milli   // mF
        };

        private Unit _selectedCapacitanceUnit = Unit.Nano;
        public Unit SelectedCapacitanceUnit
        {
            get => _selectedCapacitanceUnit;
            set
            {
                _selectedCapacitanceUnit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PrimaryCapacitance));

            }
        }

        public void UpdateLengthParameters()
        {
            OnPropertyChanged(nameof(PrimaryCoreDiameter));
            OnPropertyChanged(nameof(PrimaryWireDiameter));
            OnPropertyChanged(nameof(PrimaryWireInsulationDiameter));
            OnPropertyChanged(nameof(PrimaryWireSpacing));
        }

        public double PrimaryTurns
        {
            get => _dataService.Parameters.PrimaryTurns;
            set
            {
                if (_dataService.Parameters.PrimaryTurns != value)
                {
                    _dataService.Parameters.PrimaryTurns = value;
                    OnPropertyChanged();
                }
            }
        }

        public double PrimaryCoreDiameter
        {
            get => _unitConverter.ConvertFromMm(_dataService.Parameters.LengthUnitType, BaseToMilliConverter(_dataService.Parameters.PrimaryCoreDiameter));//BaseToMilliConverter(_dataService.Parameters.PrimaryCoreDiameter);
            set
            {
                double convertedValue = _unitConverter.ConvertToMm(_dataService.Parameters.LengthUnitType, value);
                _dataService.Parameters.PrimaryCoreDiameter = MilliToBaseConverter(convertedValue);
                OnPropertyChanged();
            }
        }

        public double PrimaryWireDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.PrimaryWireDiameter);
            set
            {
                _dataService.Parameters.PrimaryWireDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public double PrimaryWireInsulationDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.PrimaryWireInsulationDiameter);
            set
            {
                _dataService.Parameters.PrimaryWireInsulationDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public double PrimaryWireSpacing
        {
            get => BaseToMilliConverter(_dataService.Parameters.PrimaryWireSpacing);
            set
            {
                _dataService.Parameters.PrimaryWireSpacing = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public PrimaryWindingType SelectedPrimaryWindingType
        {
            get => _dataService.Parameters.PrimaryWindingType;
            set
            {
                if (_dataService.Parameters.PrimaryWindingType != value)
                {
                    _dataService.Parameters.PrimaryWindingType = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public double PrimaryCapacitance
        {
            get => BaseToXConverter(_dataService.Parameters.PrimaryCapacitance);
            set
            {
                _dataService.Parameters.PrimaryCapacitance = XToBaseConverter(value);
                OnPropertyChanged();
            }
        }
    }
}
