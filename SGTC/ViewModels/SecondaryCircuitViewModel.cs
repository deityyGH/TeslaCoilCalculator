using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class SecondaryCircuitViewModel : CalculatorValidationViewModel
    {
        private readonly ICoilDataService _dataService;
        private readonly IUnitConverterFactory _converterFactory;

        private Func<double, double> BaseToMilliConverter;
        private Func<double, double> MilliToBaseConverter;
        public SecondaryCircuitViewModel(ICoilDataService dataService, IUnitConverterFactory converterFactory)
        {
            _dataService = dataService;
            _converterFactory = converterFactory;

            SetupValidationRules();

            BaseToMilliConverter = _converterFactory.CreateConverter(Unit.Base, Unit.Milli);
            MilliToBaseConverter = _converterFactory.CreateConverter(Unit.Milli, Unit.Base);
        }

        protected override void SetupValidationRules()
        {
            AddValidationRule(nameof(SecondaryTurns), () =>
                {
                if (!IsPositive(SecondaryTurns))
                {
                    return "Turns must be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(SecondaryCoreDiameter), () =>
            {
                if (!IsPositive(SecondaryCoreDiameter))
                {
                    return "Core Diameter must be greater than zero.";
                }
                return null;
            });
            AddValidationRule(nameof(SecondaryWireDiameter), () =>
            {
                if (!IsPositive(SecondaryWireDiameter))
                {
                    return "Wire Diameter must be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(SecondaryWireInsulationDiameter), () =>
            {
                if (!IsPositive(SecondaryWireInsulationDiameter))
                {
                    return "Wire Insulation Diameter must be greater than zero.";
                }
                return null;
            });
        }

        public double SecondaryTurns
        {
            get => _dataService.Parameters.SecondaryTurns;
            set
            {
                _dataService.Parameters.SecondaryTurns = value;
                OnPropertyChanged();
            }
        }

        public double SecondaryCoreDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.SecondaryCoreDiameter);
            set
            {
                _dataService.Parameters.SecondaryCoreDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public double SecondaryWireDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.SecondaryWireDiameter);
            set
            {
                _dataService.Parameters.SecondaryWireDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public double SecondaryWireInsulationDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.SecondaryWireInsulationDiameter);
            set
            {
                _dataService.Parameters.SecondaryWireInsulationDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }
    }
}
