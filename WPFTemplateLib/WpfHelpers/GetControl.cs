using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfHelpers
{
    public class GetControl
    {
        public static TextBlock GetTextBlock(string name, Color color)
        {
            return new TextBlock
            {
                Text = name,
                Foreground = new SolidColorBrush(color),
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5, 0, 5, 0)
            };
        }

        public static TextBlock GetTextBlock(string name, double fontSize = 14, TextAlignment textAlignment = TextAlignment.Left, int width = 100)
        {
            return new TextBlock
            {
                Text = name,
                FontSize = fontSize,
                TextAlignment = textAlignment,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5, 0, 5, 0),
                Width = width
            };
        }

        /// <summary>
        /// 获取绑定TextBox;
        /// </summary>
        /// <param name="bindName">绑定名称</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public static TextBox GetBindTextBox(string bindName, int width = 100, int height = 20)
        {
            var textBox = new TextBox
            {
                Margin = new Thickness(5, 0, 5, 0),
                Width = width,
                Height = height,
            };

            textBox.SetBinding(TextBox.TextProperty, new Binding(bindName) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, });

            return textBox;
        }

        /// <summary>
        /// 获取由 StackPanel 包裹的名称和值的信息行;
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueColor"></param>
        /// <param name="nameWidth"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static StackPanel GetLineInfo(string name, string value, Color valueColor, int nameWidth = 100, int height = 30)
        {
            return new StackPanel()
            {
                Children = { GetTextBlock(name, width: nameWidth), GetTextBlock(value, valueColor) },
                Orientation = Orientation.Horizontal,
                Height = height,
            };
        }

        /// <summary>
        /// 获取由 StackPanel 包裹的名称和输入框的编辑行;
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindName"></param>
        /// <param name="nameWidth"></param>
        /// <param name="inputWidth"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static StackPanel GetLineInput(string name, string bindName, int nameWidth = 100, int inputWidth = 100, int height = 30)
        {
            return new StackPanel()
            {
                Children = { GetTextBlock(name, width: nameWidth), GetBindTextBox(bindName, inputWidth) },
                Orientation = Orientation.Horizontal,
                Height = height,
            };
        }
    }
}
