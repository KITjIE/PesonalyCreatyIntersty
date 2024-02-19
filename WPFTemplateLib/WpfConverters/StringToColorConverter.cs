using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFTemplateLib.WpfConverters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = new SolidColorBrush(Colors.Black);

            try
            {
                Color color = (System.Windows.Media.Color)ColorConverter.ConvertFromString(value + "");
                brush = new SolidColorBrush(color);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
