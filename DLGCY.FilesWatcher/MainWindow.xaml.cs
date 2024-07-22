using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using DLGCY.FilesWatcher.ViewModels;

namespace DLGCY.FilesWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowState ws;
        WindowState wsl;
        NotifyIcon notifyIcon;
        private const string correctPassword = "zjys2024"; // 替换为你的密码
        public MainWindow()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            contextMenu();
            //保证窗体显示在上方。
            wsl = WindowState;
            this.DataContext = new MainWindowViewModel();
        }

        #region 托盘右键菜单
        private void contextMenu()
        {
            ContextMenuStrip cms = new ContextMenuStrip();

            //关联 NotifyIcon 和 ContextMenuStrip
            notifyIcon.ContextMenuStrip = cms;

            System.Windows.Forms.ToolStripMenuItem exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitMenuItem.Text = "退出";
            exitMenuItem.Click += new EventHandler(exitMenuItem_Click);

            System.Windows.Forms.ToolStripMenuItem hideMenumItem = new System.Windows.Forms.ToolStripMenuItem();
            hideMenumItem.Text = "隐藏";
            hideMenumItem.Click += new EventHandler(hideMenumItem_Click);

            System.Windows.Forms.ToolStripMenuItem showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showMenuItem.Text = "显示";
            showMenuItem.Click += new EventHandler(showMenuItem_Click);

            // cms.Items.Add(showMenuItem);
            cms.Items.Add(hideMenumItem);
            cms.Items.Add(exitMenuItem);
        }

        private async void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (await ShowPasswordPromptAsync())
            {
                notifyIcon.Visible = false;
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                // 密码错误或对话框被关闭，不做任何处理
            }
        }

        private Task<bool> ShowPasswordPromptAsync()
        {
            // 显示输入密码的对话框
            var tcs = new TaskCompletionSource<bool>();
            var passwordDialog = new PasswordDialog();
            passwordDialog.Closed += (sender, e) => tcs.SetResult(passwordDialog.Password == correctPassword);
            passwordDialog.Show();
            return tcs.Task;
        }
        private void hideMenumItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }
        #endregion
        #region 最小化至托盘

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("监控.ico"); // 替换成你的图标文件路径
            notifyIcon.Text = "日志解析程序"; // 替换成你的应用名称
            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            // 添加关闭时的处理
            Closed += MainWindow_Closed;
            // 添加窗体关闭时的处理
            Closing += MainWindow_Closing;
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = WindowState.Normal;
                notifyIcon.Visible = false;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 防止直接关闭应用，而是最小化到托盘
            e.Cancel = true;
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            notifyIcon.Dispose(); // 释放 NotifyIcon 资源
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }

            base.OnStateChanged(e);
        }
        #endregion
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
        //    if (comboBox != null && comboBox.SelectedItem != null)
        //    {
        //        // 获取所选项的内容
        //        string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

        //        // 更新Configs.URLPath的值为所选项的内容
        //        WindowConfig.Instance.SupervisMode = selectedItemContent;
        //    }
        //}



    }
}


