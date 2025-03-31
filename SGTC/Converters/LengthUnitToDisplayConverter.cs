using SGTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SGTC.Converters
{
    public class LengthUnitToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is LengthUnitType unitType))
            {
                return string.Empty;
            }

            return unitType switch
            {
                LengthUnitType.Millimeter => "mm",
                LengthUnitType.Centimeter => "cm",
                LengthUnitType.Inch => "in",
                _ => "mm"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
