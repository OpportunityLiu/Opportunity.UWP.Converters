using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters
{
    internal interface IValueConverterFrom<TFrom> : IValueConverter
    {
        object Convert(TFrom value, Type targetType, object parameter, string language);
        TFrom ConvertBack(object value, object parameter, string language);
    }

    internal interface IValueConverterTo<TTo> : IValueConverter
    {
        TTo Convert(object value, object parameter, string language);
        object ConvertBack(TTo value, Type targetType, object parameter, string language);
    }
}
