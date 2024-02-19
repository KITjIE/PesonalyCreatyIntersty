using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// [dlgcy]是否全为真 转换器;
    /// </summary>
    public class IsAllTrueConverter : IMultiValueConverter
    {
        /// <summary>
        /// bool 类型直接判断；string 类型判断IsNullOrWhiteSpace；其它类型使用 double.TryParse 转换，判断是否大于零。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException(nameof(values), "values can not be null or empty");

            string result = "False";

            List<bool> boolList = new List<bool>();
            foreach (object obj in values)
            {
                bool boolItem = false;

                if (obj is bool b)
                {
                    boolItem = b;
                }
                else if (obj is string s)
                {
                    boolItem = !string.IsNullOrWhiteSpace(s);
                }
                else if (double.TryParse(obj+"", out double d))
                {
                    boolItem = d > 0;
                }

                boolList.Add(boolItem);
            }

            if (boolList.TrueForAll(x => x))
            {
                result = "True";
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
