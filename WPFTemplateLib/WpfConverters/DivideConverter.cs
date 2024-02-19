using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 等分转换器(参数传等分为几份的值)
    /// </summary>
    public class DivideConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(parameter+""))
                throw new ArgumentNullException(nameof(parameter), "parameter can not be null or empty");

            if (!int.TryParse(parameter + "", out int divideValue))
                throw new ArgumentException("parameter must be int type", nameof(parameter));

            if (!double.TryParse(value + "", out double originValue))
                throw new ArgumentException("value must can be convert to double type", nameof(value));

            return originValue / divideValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
