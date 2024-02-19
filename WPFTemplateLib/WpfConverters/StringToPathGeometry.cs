using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// Path 的 Data 属性可接受的对象（派生自 Geometry 抽象类）的 Xaml 字符串转为相关对象。
    /// </summary>
    public class StringToPathGeometry : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Geometry geometry = null;
            string content = value + "";
            try
            {
                int index = content.IndexOf(' ');
                if (!content.Contains("xmlns"))
                {
                    content = content.Insert(index, " xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'");
                }

                //StringReader stringReader = new StringReader(content);
                //XmlTextReader xmlTextReader = new XmlTextReader(stringReader); //为 None，可能是字符串被转义了，识别不了。

                Stream stream = new MemoryStream(Encoding.Default.GetBytes(content));
                geometry = (Geometry)XamlReader.Load(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return geometry;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
