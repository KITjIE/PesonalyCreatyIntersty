using System.Windows;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// 承载 DataContext 的绑定代理类（用于DataGrid的列等地方）
    /// https://www.cnblogs.com/action98/p/3535934.html
    /// </summary>
    /// <example>
    /// 用法：首先创建一个可以承载 DataContext 的绑定代理类 BindingProxy;
    /// 然后将这个 BindingProxy 当作资源，获得 DataContext;
    /// <code>
    /// ＜DataGrid.Resources>
    ///     ＜wpfhelpers:BindingProxy x:Key="BindingProxy" DataContext="{Binding}"/>
    /// ＜/DataGrid.Resources>
    /// </code>
    /// 将 DataGridTextColumn 的 Visibility 的 DataContext 指定为这个 BindingProxy.
    /// 如：Visibility= "{Binding DataContext.IsShow,Source={StaticResource BindingProxy}}"
    /// </example>
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object DataContext
        {
            get => (object)GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }

        // Using a DependencyProperty as the backing store for DataContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register("DataContext", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}
