using System.Windows;
using System.Windows.Controls;

namespace WPFTemplateLib.Attached
{
    /// <summary>
    /// RadioButton 附加属性类
    /// </summary>
    public class RadioButtonAttached : DependencyObject
    {
        #region IsCanUncheck

        public static bool GetIsCanUncheck(FrameworkElement item)
        {
            return (bool)item.GetValue(IsCanUncheckProperty);
        }

        public static void SetIsCanUncheck(FrameworkElement item, bool value)
        {
            item.SetValue(IsCanUncheckProperty, value);
        }

        /// <summary>
        /// 是否能取消选中
        /// </summary>
        public static readonly DependencyProperty IsCanUncheckProperty =
            DependencyProperty.RegisterAttached(
                "IsCanUncheck",
                typeof(bool),
                typeof(RadioButtonAttached),
                new UIPropertyMetadata(false, OnIsCanUncheckChanged));

        static void OnIsCanUncheckChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement item = depObj as FrameworkElement;

            if (item == null)
                return;

            switch (depObj)
            {
                case RadioButton radioButton:
                {
                    if ((bool)e.NewValue)
                    {
                        radioButton.PreviewMouseDown += RadioButton_PreviewMouseDown;
                    }
                    else
                    {
                        radioButton.PreviewMouseDown -= RadioButton_PreviewMouseDown;
                    }
                    break;
                }
                default:
                    break;
            }
        }

        private static void RadioButton_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null)
            {
                return;
            }

            if (rb.IsChecked == true)
            {
                rb.IsChecked = false;
                e.Handled = true;
            }
        }

        #endregion
    }
}
