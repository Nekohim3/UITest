using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UITest.Utils.PageManager
{
    public class WindowToVisConverter : IMultiValueConverter
    {
        public object Convert(object[]                           values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (values?[0]   == null || values[1] == null) return Visibility.Collapsed;
            return values[0] == values[1] ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
