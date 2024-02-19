using System.Windows.Controls;
using PropertyChanged;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.UserControls
{
    public partial class UC_CustomInfo : UserControl
    {
        public UC_CustomInfo()
        {
            InitializeComponent();
        }
    }

    /// <summary>
    /// 自定义信息用户控件的基础VM;
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class CustomInfoViewModel
    {
        #region Bind

        public string TitleLeft { get; set; } = "左标题";

        public string TitleRight { get; set; } = "右标题";

        public string TextBottom { get; set; } = "底部文字";

        public object LeftContent { get; set; }

        public object RightContent { get; set; }

        /// <summary>
        /// 是否只显示单列
        /// </summary>
        public bool IsOnlyOneColumn { get; set; }

        /// <summary>
        /// 是否显示标题
        /// </summary>
        public bool IsShowTitle { get; set; } = true;

        /// <summary>
        /// 是否显示底部文字
        /// </summary>
        public bool IsShowBottom { get; set; } = true;

        #endregion
    }
}
