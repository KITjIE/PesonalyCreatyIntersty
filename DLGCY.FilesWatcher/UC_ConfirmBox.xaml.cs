using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyChanged;
using WPFTemplateLib.WpfHelpers;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.UserControls
{
    /// <summary>
    /// [dlgcy] WPF MVVM 确认弹框;
    /// </summary>
    public partial class UC_ConfirmBox : UserControl
    {
        public UC_ConfirmBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定 VM 中的 IsShowDialog
        /// </summary>
        public bool IsShowDialog
        {
            get { return (bool)GetValue(IsShowDialogProperty); }
            set { SetValue(IsShowDialogProperty, value); }
        }

        public static readonly DependencyProperty IsShowDialogProperty = DependencyProperty.Register("IsShowDialog", typeof(bool), typeof(UC_ConfirmBox), new PropertyMetadata(false, (obj, args) =>
        {
            if (args.NewValue is bool newValue)
            {
                try
                {
                    var control = obj as UC_ConfirmBox;
                    control.Visibility = newValue ? Visibility.Visible : Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }));

        /// <summary>
        /// 消息框宽度
        /// </summary>
        public int DialogWidth
        {
            get { return (int)GetValue(DialogWidthProperty); }
            set { SetValue(DialogWidthProperty, value); }
        }

        public static readonly DependencyProperty DialogWidthProperty =
            DependencyProperty.Register("DialogWidth", typeof(int), typeof(UC_ConfirmBox), new PropertyMetadata(400));

        /// <summary>
        /// 消息框高度
        /// </summary>
        public int DialogHeight
        {
            get { return (int)GetValue(DialogHeightProperty); }
            set { SetValue(DialogHeightProperty, value); }
        }

        public static readonly DependencyProperty DialogHeightProperty =
            DependencyProperty.Register("DialogHeight", typeof(int), typeof(UC_ConfirmBox), new PropertyMetadata(300));

    }

    [AddINotifyPropertyChangedInterface] //Fody;
    public class ConfirmBoxViewModel : BindableBase
    {
        #region 成员

        public AutoResetEvent AutoResetEvent { get; set; } = new AutoResetEvent(false); //缺省为阻塞;

        private System.Timers.Timer _timer = new System.Timers.Timer(1000);

        #endregion

        #region Bindable

        private bool _IsShowDialog = false;
        /// <summary>
        /// 是否显示弹窗;
        /// </summary>
        public bool IsShowDialog
        {
            get => _IsShowDialog;

            set
            {
                SetProperty(ref _IsShowDialog, value);

                if (!value)
                {
                    AutoResetEvent.Set(); //弹框隐藏时放开阻塞;

                    //清空数据;
                    DialogMessage = string.Empty;
                    CustomContent = null;
                }

                if (value)
                {
                    if (IsWaitDialog)
                    {
                        LeftTime = 0;
                    }
                    else
                    {
                        LeftTime = IsMessageDialog ? MessageDialogTimeOut : ConfirmDialogTimeOut;
                    }

                    if (LeftTime >= 0)
                    {
                        _timer.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 用户是否点击确认;
        /// </summary>
        public bool? IsConfirm { get; set; } = null;

        /// <summary>
        /// 弹窗消息;
        /// </summary>
        public string DialogMessage { get; set; }

        /// <summary>
        /// 是否是消息弹窗
        /// </summary>
        public bool IsMessageDialog { get; set; } = false;

        /// <summary>
        /// 是否是等待框
        /// </summary>
        public bool IsWaitDialog { get; set; } = false;

        /// <summary>
        /// 确认弹窗超时时间（单位：秒，默认20）
        /// </summary>
        public int ConfirmDialogTimeOut { get; set; } = 20;

        /// <summary>
        /// 消息弹窗超时时间（单位：秒，默认5）
        /// </summary>
        public int MessageDialogTimeOut { get; set; } = 5;

        /// <summary>
        /// 倒计时
        /// </summary>
        public int LeftTime { get; set; }

        /// <summary>
        /// 弹窗标题
        /// </summary>
        public string DialogTitle { get; set; } = "注意";

        /// <summary>
        /// 确认按钮文字
        /// </summary>
        public string DialogConfirmBtnText { get; set; } = "确认";

        /// <summary>
        /// 取消按钮文字
        /// </summary>
        public string DialogCancelBtnText { get; set; } = "取消";

        /// <summary>
        /// 是否显示文本信息;
        /// </summary>
        public bool IsShowText { get; set; } = true;

        /// <summary>
        /// 是否显示自定义内容;
        /// </summary>
        public bool IsShowCustom { get; set; } = false;

        /// <summary>
        /// 自定义内容
        /// </summary>
        public FrameworkElement CustomContent { get; set; }

        /// <summary>
        /// 自定义内容部分水平对齐类型
        /// </summary>
        public HorizontalAlignment CustomContentHorizontalAlignment { get; set; } = HorizontalAlignment.Stretch;

        /// <summary>
        /// 是否显示按钮;
        /// </summary>
        public bool IsShowButton { get; set; } = true;

        #endregion

        #region Command

        public ICommand CloseCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        #endregion

        public ConfirmBoxViewModel()
        {
            SetCommandMethod();

            _timer.Elapsed += Timer_Elapsed;
        }

        #region 私有方法

        /// <summary>
        /// 命令方法赋值(在构造函数中调用)
        /// </summary>
        private void SetCommandMethod()
        {
            CloseCommand ??= new RelayCommand(o => true, o =>
            {
                IsConfirm = null;
                IsShowDialog = false;
            });

            ConfirmCommand ??= new RelayCommand(o => true, o =>
            {
                IsConfirm = true;
                IsShowDialog = false;
            });

            CancelCommand ??= new RelayCommand(o => true, o =>
            {
                IsConfirm = false;
                IsShowDialog = false;
            });
        }

        /// <summary>
        /// 倒计时计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsWaitDialog)
            {
                LeftTime++;
            }
            else
            {
                LeftTime--;
                if (LeftTime <= 0)
                {
                    _timer.Stop();
                    CloseCommand.Execute(null);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 确认框帮助类
    /// </summary>
    public class ConfirmBoxHelper
    {
        /// <summary>
        /// 弹出确认框;
        /// </summary>
        /// <param name="vm">相关ViewModel</param>
        /// <param name="message">消息内容</param>
        /// <param name="action">业务方法</param>
        /// <param name="logAction">日志方法</param>
        /// <param name="title">弹窗标题</param>
        /// <param name="isShowText">是否显示文本信息</param>
        /// <param name="isShowCustom">是否显示自定义内容</param>
        public static async Task ShowConfirm(ConfirmBoxViewModel vm, string message, Action action = null, Action<string> logAction = null, string title = "请确认或取消", bool isShowText = true, bool isShowCustom = true)
        {
            await Task.Run(() =>
            {
                vm.IsMessageDialog = false;
                vm.IsWaitDialog = false;
                vm.IsShowDialog = true;
                vm.IsShowText = isShowText;
                vm.IsShowCustom = isShowCustom;
                vm.IsShowButton = true;
                //vm.CustomContentHorizontalAlignment = HorizontalAlignment.Center;

                if (!string.IsNullOrWhiteSpace(message))
                {
                    vm.DialogMessage = message;
                }

                if (!string.IsNullOrWhiteSpace(title))
                {
                    vm.DialogTitle = title;
                }

                vm.AutoResetEvent.Reset();
                bool resultWaitOne = vm.AutoResetEvent.WaitOne(1000 * vm.ConfirmDialogTimeOut);
                if (!resultWaitOne)
                {
                    logAction?.Invoke($"超时[{vm.ConfirmDialogTimeOut}s]");
                    vm.IsShowDialog = false;
                    return;
                }

                if (vm.IsConfirm != true)
                {
                    logAction?.Invoke($"用户{(vm.IsConfirm == null ? "关闭" : "取消")}（超时时间[{vm.ConfirmDialogTimeOut}s],剩余时间[{vm.LeftTime}s]）");
                    return;
                }

                Task.Run(() => action?.Invoke());
            });
        }

        /// <summary>
        /// 弹出消息框;
        /// </summary>
        /// <param name="vm">相关ViewModel</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageTimeout">超时时间（留空/小等0 则使用默认的消息弹窗超时时间）</param>
        /// <param name="title">弹窗标题</param>
        /// <param name="isShowText">是否显示文本信息</param>
        /// <param name="isShowCustom">是否显示自定义内容</param>
        /// <returns></returns>
        public static async Task ShowMessage(ConfirmBoxViewModel vm, string message, int messageTimeout = 0, string title = "请知悉", bool isShowText = true, bool isShowCustom = true)
        {
            await Task.Run(() =>
            {
                vm.IsMessageDialog = true;
                vm.IsWaitDialog = false;
                vm.IsShowDialog = true;
                vm.IsShowText = isShowText;
                vm.IsShowCustom = isShowCustom;
                vm.IsShowCustom = true;
                vm.IsShowButton = true;
                //vm.CustomContentHorizontalAlignment = HorizontalAlignment.Center.ToString();

                if (!string.IsNullOrWhiteSpace(message))
                {
                    vm.DialogMessage = message;
                }

                if (!string.IsNullOrWhiteSpace(title))
                {
                    vm.DialogTitle = title;
                }

                int timeout = messageTimeout > 0 ? messageTimeout : vm.MessageDialogTimeOut;
                vm.LeftTime = timeout;

                vm.AutoResetEvent.Reset();
                vm.AutoResetEvent.WaitOne(1000 * timeout);
                vm.IsShowDialog = false;
            });
        }

        /// <summary>
        /// 弹出等待框
        /// </summary>
        /// <param name="vm">相关ViewModel</param>
        /// <param name="message">消息内容</param>
        /// <param name="action">业务方法</param>
        /// <param name="title">弹窗标题</param>
        /// <returns></returns>
        public static async Task ShowWait(ConfirmBoxViewModel vm, string message, Func<Task> action = null, string title = "请耐心等待")
        {
            vm.CustomContent = new UC_Wait();

            await Task.Run(async () =>
            {
                vm.IsMessageDialog = false;
                vm.IsWaitDialog = true;
                vm.IsShowDialog = true;
                vm.IsShowText = true;
                vm.IsShowCustom = true;
                vm.IsShowButton = false;
                //vm.CustomContentHorizontalAlignment = HorizontalAlignment.Stretch.ToString();

                if (!string.IsNullOrWhiteSpace(message))
                {
                    vm.DialogMessage = message;
                }

                if (!string.IsNullOrWhiteSpace(title))
                {
                    vm.DialogTitle = title;
                }

                Console.WriteLine($"等待框就绪，业务操作开始执行...");

                await Task.Run(async () =>
                {
                    await action?.Invoke();

                }).ContinueWith(_ =>
                {
                    vm.IsShowDialog = false;
                    Console.WriteLine($"业务操作执行完毕，等待框关闭.");
                });
            });
        }
    }
}
