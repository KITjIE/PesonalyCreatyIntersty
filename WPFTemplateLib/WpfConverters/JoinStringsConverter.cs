using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 连接字符串转换器;
    /// https://bbs.csdn.net/topics/390267668?list=1194969
    /// </summary>
	public class JoinStringsConverter : MarkupExtension, IMultiValueConverter
    {
        public string Separator { get; set; }

        public JoinStringsConverter()
        {
        }

        public JoinStringsConverter(string separator)
        {
            Separator = separator;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(Separator ?? "", values.Select(x => x + "").ToArray());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
