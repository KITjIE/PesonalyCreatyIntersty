using System;
using System.Globalization;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 隐藏转换器(bool转为显示隐藏值，true的时候隐藏)
    /// </summary>
    public class CollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "Hidden";

            switch (value)
            {
                case true:
                    result = "Collapsed";
                    break;
                case false:
                    result = "Visible";
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 显示转换器(bool转为显示隐藏值，true的时候显示)
    /// </summary>
    public class VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "Hidden";

            switch (value)
            {
                case true:
                    result = "Visible";
                    break;
                case false:
                    result = "Collapsed";
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
