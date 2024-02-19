using System;
using System.Globalization;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    public class TrueFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "False";

            switch (value)
            {
                case true:
                    result = "True";
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
