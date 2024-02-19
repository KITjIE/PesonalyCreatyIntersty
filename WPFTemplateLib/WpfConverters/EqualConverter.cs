using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// [dlgcy]相等比较器（判断给定的数据的字符串形式是否都相等）
    /// </summary>
    public class EqualConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException(nameof(values), "values can not be null or empty");

            return values.All(x => x+"" == values[0]+""); 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
