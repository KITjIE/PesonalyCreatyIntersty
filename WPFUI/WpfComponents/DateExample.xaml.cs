using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TemplateClassLibrary.ClassComponent;

namespace WpfUI.WpfComponents
{
    /// <summary>
    /// Date.xaml 的交互逻辑
    /// </summary>
    public partial class DateExample : UserControl
    {
        public DateExample()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("所选择的日期时间是： " + dateTimePicker1.DateTime.ToString());
        }
    }
}
