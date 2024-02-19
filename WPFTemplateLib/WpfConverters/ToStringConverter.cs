using System;
using System.Globalization;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 转换为string;
    /// </summary>
    public class ToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value + "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
