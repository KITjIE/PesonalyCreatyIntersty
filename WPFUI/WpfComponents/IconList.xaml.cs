using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace WpfUI.WpfComponents
{
    /// <summary>
    /// IconList.xaml 的交互逻辑
    /// </summary>
    public partial class IconList : UserControl
    {
        List<iconFonts> list = new List<iconFonts>();
        public IconList()
        {
            InitializeComponent();
            //string path = System.Environment.CurrentDirectory + "\\icon.json";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IconFonts", "icon.json");
            string jsonstring = File.ReadAllText(path, Encoding.UTF8);
            list = JsonConvert.DeserializeObject<List<iconFonts>>(jsonstring);
            this.iconBox.ItemsSource = list;
        }

        public class iconFonts
        {
            public string fontName { get; set; }
            public string fontFlag { get; set; }
            public string useFlag { get; set; }
        }
      
    }
}
