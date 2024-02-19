using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace DLGCY.FilesWatcher
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Hyperlink link = sender as Hyperlink;
                string url = link.NavigateUri.AbsoluteUri;

                #region .NET Framework 方式

                //var processStartInfo = new ProcessStartInfo(uri);
                //Process.Start(processStartInfo);

                #endregion

                #region .NET Core 方式

                Process process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        UseShellExecute = false,            //不使用shell启动
                        RedirectStandardInput = true,       //让cmd接受标准输入
                        RedirectStandardOutput = false,     //不想听cmd讲话所以不要他输出
                        RedirectStandardError = true,       //重定向标准错误输出
                        CreateNoWindow = true,              //不显示窗口
                    }
                };

                process.Start();

                //向cmd窗口发送输入信息 后面的&exit告诉cmd运行好之后就退出
                process.StandardInput.WriteLine($"start {url}&exit");
                process.StandardInput.AutoFlush = true;
                process.WaitForExit();//等待程序执行完退出进程
                process.Close();

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
