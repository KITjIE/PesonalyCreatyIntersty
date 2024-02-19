using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
/*
 * WPF 转换器之通用转换器
 * https://www.cnblogs.com/ShenNan/p/7244402.html
 * 需引用 System.Windows.Data; 在 PresentationFramework 程序集中.
 * 需引用 System.Windows; 在 PresentationCore 程序集中.
 */
namespace WPFTemplateLib.WpfConverters
{
    /// <summary>
    /// 单值通用转换器
    /// 参数规则【比较值1|比较值2:true返回值:false返回值】
    /// 使用示例：
    /// <wpfConverters:ObjectConverter x:Key="ObjConverter"/>
    /// IsEnabled = "{Binding CanUse, Converter={StaticResource ObjConverter}, ConverterParameter=false:False:True}"
    /// </summary>
    public class ObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //将参数字符分段 parray[0]为比较值，parray[1]为true返回值，parray[2]为false返回值;
            string[] parray = parameter.ToString().ToLower().Split(':'); 

            if (value == null)
                return parray[2]; //如果数据源为空，默认返回false返回值

            if (parray[0].Contains("|")) //判断有多个比较值的情况
                return parray[0].Split('|').Contains(value.ToString().ToLower()) ? parray[1] : parray[2]; //多值比较

            return parray[0].Equals(value.ToString().ToLower()) ? parray[1] : parray[2]; //单值比较
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = "otherValue";
            string[] parray = parameter.ToString().ToLower().Split(':');

            if (value == null)
                return returnValue;

            var valueStr = value.ToString().ToLower();
            if (valueStr != parray[1])
                return returnValue;
            else
                return parray[0].Contains('|') ? parray[0].Split('|')[0] : parray[0];
        }
    }

    /// <summary>
    /// 多值通用转换器
    /// 参数规则【各组比较值:比较条件(@ 或 |):true返回值:false返回值:返回值类型枚举】
    /// 其中各组比较值用分号分隔，每组中可设置多个目标值并以'|'分隔;目标值为对应类型ToString()不区分大小写;
    /// 比较条件中@表示&&（原为&,写在Xaml中有错误）
    /// 参数示例【v1;v2-1|v2-2;v3:@:Visible:Collapsed:1】
    /// <wpfConverters:MultiObjectConverter x:Key="MultiObjectConverter"/>
    /// </summary>
    public class MultiObjectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] param = parameter.ToString().ToLower().Split(':');     //将参数字符串分段
            string[] compareValues = param[0].Split(';');                   //将比较值段分割为数组

            if (values.Length != compareValues.Length)                                   //比较源数据和比较参数个数是否一致
                return ConvertValue(param[3], param[4]);

            var trueCount = 0; //满足条件的结果数量
            var currentValue = string.Empty;
            IList<string> currentParamArray = null;
            for (var i = 0; i < values.Length; i++)
            {
                currentValue = values[i] != null ? values[i].ToString().ToLower() : string.Empty;
                if (compareValues[i].Contains("|"))
                {
                    //当前比较值段包含多个比较值
                    currentParamArray = compareValues[i].Split('|');
                    trueCount += currentParamArray.Contains(currentValue) ? 1 : 0; //满足条件，结果+1
                }
                else
                {
                    trueCount += compareValues[i].Equals(currentValue) ? 1 : 0; //满足条件，结果+1
                }
            }

            currentParamArray = null;
            currentValue = string.Empty;
            var compareResult = param[1].Equals("@") ? trueCount == values.Length : trueCount > 0; //判断比较结果
            return ConvertValue(compareResult ? param[2] : param[3], param[4]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private object ConvertValue(string result, string enumStr)
        {
            switch ((ConvertResult)int.Parse(enumStr))
            {
                case ConvertResult.显示类型:
                    return result.Equals("collapsed") ? Visibility.Collapsed : Visibility.Visible;

                case ConvertResult.布尔类型:
                    return System.Convert.ToBoolean(result);

                case ConvertResult.字符串类型:
                    return result;

                //后续自行扩展;
                default:
                    return null; 
            }
        }

        private enum ConvertResult
        {
            显示类型 = 1,
            布尔类型 = 2,
            字符串类型 = 3,
            整型 = 4,
            小数型 = 5,
            画刷类型 = 6,
            样式类型 = 7,
            模板类型 = 8
        }
    }
}
