using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 字符串转图片资源转换器(参数填图片资源的格式化字符串，如：'pack://application:,,,/Main.Gui;component/Images/{0}.png' 或 'pack://application:,,,/Images/{0}.png')
    /// </summary>
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource image = null;
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
                    Uri uri = new Uri(imageSource, UriKind.RelativeOrAbsolute);
                    image = new BitmapImage(uri);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
