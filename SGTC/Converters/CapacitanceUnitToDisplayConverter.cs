using SGTC.Models;
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
    public class CapacitanceUnitToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Unit unit))
            {
                return string.Empty;
            }

            return unit switch
            {
                { Name: "Base", Symbol: "F" } => "F",
                { Name: "Milli", Symbol: "m" } => "mF",
                { Name: "Micro", Symbol: "μ" } => "µF",
                { Name: "Nano", Symbol: "n" } => "nF",
                { Name: "Pico", Symbol: "p" } => "pF",
                _ => string.Empty,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
            {
                return null;
            }

            return stringValue switch
            {
                "F" => Unit.Base,
                "mF" => Unit.Milli,
                "µF" => Unit.Micro,
                "nF" => Unit.Nano,
                "pF" => Unit.Pico,
                _ => null,
            };
        }
    }
}
