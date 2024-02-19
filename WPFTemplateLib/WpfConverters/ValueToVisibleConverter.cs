using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 绑定值与参数 ToString 比较后转为是否显示（参数可用“|”分隔表示“或”的关系）
    /// </summary>
    /// <example>
    /// Visibility="{Binding EditType, Converter={StaticResource ValueToVisibleConverter}, ConverterParameter=Edit|Add}"
    /// </example>
    public class ValueToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(parameter + ""))
                throw new ArgumentNullException(nameof(parameter), "parameter can not be null or empty");

            if (string.IsNullOrWhiteSpace(value + ""))
                throw new ArgumentNullException(nameof(value), "value can not be null or empty");

            List<string> paraList = (parameter + "").Split('|').ToList();
            if (paraList.Exists(x => value + "" == x))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
