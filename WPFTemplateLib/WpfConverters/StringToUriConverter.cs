using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 字符串转Uri转换器(参数填Uri的格式化字符串，如：'pack://application:,,,/Main.Gui;component/Images/{0}.png' 或 'pack://application:,,,/Images/{0}.png')
    /// </summary>
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = null;
            if (string.IsNullOrWhiteSpace(parameter + ""))
            {
                throw new ArgumentException("parameter must be set", nameof(parameter));
            }

            try
            {
                string imageName = value + "";
                if (!string.IsNullOrWhiteSpace(imageName))
                {
                    string imageSource = string.Format(parameter + "", imageName);
                    uri = new Uri(imageSource, UriKind.RelativeOrAbsolute);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
