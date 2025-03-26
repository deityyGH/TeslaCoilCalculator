using SGTC.Core;
using SGTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTC.ViewModels
{
    public class TorusViewModel : CalculatorValidationViewModel
    {
        //private readonly CoilCalculatorData _data = CoilCalculatorData.Instance;
        private readonly ICoilDataService _dataService;
        private readonly IUnitConverterFactory _converterFactory;

        private Func<double, double> MilliToBaseConverter;
        private Func<double, double> BaseToMilliConverter;
        public TorusViewModel(ICoilDataService dataService, IUnitConverterFactory converterFactory)
        {
            _dataService = dataService;
            _converterFactory = converterFactory;

            SetupValidationRules();

            MilliToBaseConverter = _converterFactory.CreateConverter(Unit.Milli, Unit.Base);
            BaseToMilliConverter = _converterFactory.CreateConverter(Unit.Base, Unit.Milli);
        }

        public double TopLoadTorusInDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.TopLoadTorusInDiameter);
            set
            {
                _dataService.Parameters.TopLoadTorusInDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        public double TopLoadTorusOutDiameter
        {
            get => BaseToMilliConverter(_dataService.Parameters.TopLoadTorusOutDiameter);
            set
            {
                _dataService.Parameters.TopLoadTorusOutDiameter = MilliToBaseConverter(value);
                OnPropertyChanged();
            }
        }

        protected override void SetupValidationRules()
        {

            AddValidationRule(nameof(TopLoadTorusOutDiameter), () =>
            {
                if (!IsPositive(TopLoadTorusOutDiameter))
                {
                    return "Outer Diameter be greater than zero.";
                }
                return null;
            });

            AddValidationRule(nameof(TopLoadTorusInDiameter), () =>
             {
                 if (!IsPositive(TopLoadTorusInDiameter))
                 {
                     return "Inner Diameter must be greater than zero.";
                 }
                 return null;
             });
        }


        

    }
}
