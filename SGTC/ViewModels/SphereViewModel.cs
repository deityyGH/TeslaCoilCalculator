using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{

    public class SphereViewModel : CalculatorValidationViewModel
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;
        private readonly IUnitConverterFactory _converterFactory;

        private Func<double, double> MilliToBaseConverter;
        private Func<double, double> BaseToMilliConverter;

        public SphereViewModel(ICoilDataService dataService, IUnitConverterFactory converterFactory)
        {
            _dataService = dataService;
            _converterFactory = converterFactory;

            SetupValidationRules();

            MilliToBaseConverter = _converterFactory.CreateConverter(Unit.Milli, Unit.Base);
            BaseToMilliConverter = _converterFactory.CreateConverter(Unit.Base, Unit.Milli);
        }

        public double TopLoadSphereDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.TopLoadSphereDiameter);
            set
            {
                _dataService.Parameters.TopLoadSphereDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        protected override void SetupValidationRules()
        {
            AddValidationRule(nameof(TopLoadSphereDiameter), () =>
            {
                if (!IsPositive(TopLoadSphereDiameter))
                {
                    return "Turns must be greater than zero.";
                }
                return null;
            
             });
        }
    }
}

