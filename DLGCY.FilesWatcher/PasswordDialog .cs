using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DLGCY.FilesWatcher
{
    public class PasswordDialog : Window
    {
        private PasswordBox passwordBox;
        private Button okButton;
        private DispatcherTimer countdownTimer;
        private int countdownTime; // 倒计时秒数
        public string Password { get; private set; }

        public PasswordDialog(int countdownSeconds = 30) // 默认30秒倒计时
        {
            Title = "需要密码";
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.Manual; // 手动设置启动位置

            // 屏幕中间位置
            Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;

            countdownTime = countdownSeconds;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);

            Label label = new Label();
            label.Content = "请输入密码以退出：";
            label.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.Children.Add(label);

            passwordBox = new PasswordBox();
            passwordBox.Margin = new Thickness(0, 10, 0, 0);
            passwordBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanel.Children.Add(passwordBox);

            okButton = new Button();
            okButton.Content = $"确定 ({countdownTime}s)";
            okButton.Margin = new Thickness(0, 10, 0, 0);
            okButton.HorizontalAlignment = HorizontalAlignment.Center;
            okButton.Width = 75;
            okButton.Click += OkButton_Click;
            stackPanel.Children.Add(okButton);

            Content = stackPanel;

            // 初始化倒计时器
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
            Close(); // 关闭对话框
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownTime--;
            okButton.Content = $"确定 ({countdownTime}s)";

            if (countdownTime <= 0)
            {
                countdownTimer.Stop();
                Password = null; // 设置密码为空
                Close(); // 关闭对话框
            }
        }
    }
}
