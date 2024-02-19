using System.ComponentModel;
using System.Runtime.CompilerServices;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// WPF绑定属性基类;
    /// </summary>
    /// <example>
    /// <code>
    /// class Sample : BindableBase
    /// {
    ///     private List&lt;string&gt; _stuList;
    ///     public List&lt;string&gt; StuList
    ///     {
    ///         get => _stuList;
    ///         set => SetProperty(ref _stuList, value);
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region BindableBase

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性变动通知
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetPropertyWithoutCompare<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
