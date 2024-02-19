using System.Windows;
using System.Windows.Controls;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplateLib
 */
namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// 功能：列表项被选中时带到视野中
    /// 参考：http://dlgcy.com/introduction-to-attached-behaviors-in-wpf/
    /// 说明：用于 DataGrid 时需要设置 EnableRowVirtualization="False"
    /// </summary>
    /// <example>
    /// <code>
    /// Setter Property="wpfHelpers:BringIntoViewBehavior.IsBroughtIntoViewWhenSelected" Value="True"/>
    /// </code>
    /// </example>
    public class BringIntoViewBehavior
    {
        #region IsBroughtIntoViewWhenSelected
        public static bool GetIsBroughtIntoViewWhenSelected(FrameworkElement item)
        {
            return (bool)item.GetValue(IsBroughtIntoViewWhenSelectedProperty);
        }
        public static void SetIsBroughtIntoViewWhenSelected(FrameworkElement item, bool value)
        {
            item.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
        }

        /// <summary>
        /// 是否在选中时带到视野中
        /// </summary>
        public static readonly DependencyProperty IsBroughtIntoViewWhenSelectedProperty =
            DependencyProperty.RegisterAttached(
                "IsBroughtIntoViewWhenSelected",
                typeof(bool),
                typeof(BringIntoViewBehavior),
                new UIPropertyMetadata(false, OnIsBroughtIntoViewWhenSelectedChanged));

        static void OnIsBroughtIntoViewWhenSelectedChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement item = depObj as FrameworkElement;

            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            switch (depObj)
            {
                case DataGridRow row:
                {
                    if ((bool)e.NewValue)
                        row.Selected += OnItemSelected;
                    else
                        row.Selected -= OnItemSelected;
                    break;
                }
                case TreeViewItem treeViewItem:
                {
                    if ((bool)e.NewValue)
                        treeViewItem.Selected += OnItemSelected;
                    else
                        treeViewItem.Selected -= OnItemSelected;
                    break;
                }
                default:
                    break;
            }
        }

        static void OnItemSelected(object sender, RoutedEventArgs e)
        {
            //忽略所有只是报告子孙的 Selected 被触发的祖先。
            if (!ReferenceEquals(sender, e.OriginalSource))
                return;

            if (e.OriginalSource is FrameworkElement item)
                item.BringIntoView();
        }

        #endregion
    }
}
