using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 字符串转是否可见转换器
    /// </summary>
    public class StringToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
