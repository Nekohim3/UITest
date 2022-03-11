using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UITest.Utils.PageManager
{
    public class WindowToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[]                         values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (values?[0] == null || values[1] == null) return false;
            var q = values[0] == values[1];
            return q;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NodesToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[]                         values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (values?[0] == null || values[1] == null) return false;
            var q = values[0] == values[1];
            return q;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
