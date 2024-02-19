using System;
using System.Globalization;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplateLib
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 换行转换器（未成功）
    /// </summary>
    public class NewLineConverter : IValueConverter
    {
        //此方法未成功，可使用 https://blog.csdn.net/weixin_34163553/article/details/85987075 （xml:space="preserve" + &#13;）

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = (value + "")
                .Replace("\\r\\n", "&#13;")
                .Replace("\r\n", "&#13;")
                .Replace("\n\r", "&#13;")
                .Replace("\\n\\r", "&#13;")
                .Replace("\n", "&#13;")
                .Replace("\\n", "&#13;");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}