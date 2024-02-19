using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// object(如Tag属性内容)转图片资源转换器(填图片资源的格式化字符串，如：'pack://application:,,,/Main.Gui;component/Images/pic.png' 或 'pack://application:,,,/Images/pic.png')
    /// </summary>
    public class ObjectToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource image = null;

            try
            {
                string uriStr = value + "";
                if (!string.IsNullOrWhiteSpace(uriStr))
                {
                    Uri uri = new Uri(uriStr, UriKind.RelativeOrAbsolute);
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