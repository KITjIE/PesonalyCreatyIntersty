using DLGCY.FilesWatcher.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTemplate;
using WPFTemplateLib.WpfHelpers;

namespace DLGCY.FilesWatcher
{
    /// <summary>
    /// MsgWindow.xaml 的交互逻辑
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MsgWindow : Window, INotifyPropertyChanged
    {
        //public ConfigItems Configs;

        private string _logContent;
        private string _logContent1;

        public event PropertyChangedEventHandler PropertyChanged;

        public string LogContent
        {
            get { return _logContent; }
            set
            {
                _logContent = value;
                OnPropertyChanged();
            }
        }

        public string ProductBarcode
        {
            get { return _logContent1; }
            set
            {
                _logContent1 = value;
                OnPropertyChanged();
            }
        }
        public MsgWindow(ConfigItems configs)
        {
            InitializeComponent();
            DataContext = this; // 设置窗口的数据上下文为当前窗口实例
            LogContent = configs.MESErrorInfo; // 你可以根据需要设置初始值
            ProductBarcode = configs.ProductBarcode;

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
