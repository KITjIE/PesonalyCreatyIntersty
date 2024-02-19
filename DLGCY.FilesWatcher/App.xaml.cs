using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            const string mutexName = "DLGCY.FilesWatcher"; // 请替换为你的独特互斥体名称

            mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                // 如果互斥体已存在，说明应用程序已经在运行
                MessageBox.Show("应用程序已经在运行中！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mutex.ReleaseMutex();
            mutex.Dispose();
            // 完全退出应用程序
            Environment.Exit(0);
            base.OnExit(e);
            
        }
    }
}

