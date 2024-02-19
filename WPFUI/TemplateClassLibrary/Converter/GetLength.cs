using System;
using System.Globalization;
using System.Windows.Data;

namespace TemplateClassLibrary.Converter
{
    public class GetLength : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return value.ToString().Length > 0 ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return value.ToString().Length > 0 ? true : false;
        }
    }

    public class GetLengthValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return value.ToString().Length > 0 ? value.ToString().Length : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return value.ToString().Length > 0 ? value.ToString().Length : 0;
        }
    }
}
