using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfUI.WpfComponents
{
    /// <summary>
    /// DataTable.xaml 的交互逻辑
    /// </summary>
    public partial class table : UserControl
    {
        public table()
        {
            InitializeComponent();
            this.Loaded += Table_Loaded;
        }

        private void Table_Loaded(object sender, RoutedEventArgs e)
        {
            List<person> list = new List<person>()
            {
                new person{ FirstName="Mark",LastName="Otto",Username="@mdo"},
                new person{ FirstName="cob",LastName="Thornton",Username="@fat"},
                new person{ FirstName="Larry",LastName="the Bird",Username="@twitter"},
            };

            this.dataGrid.ItemsSource = list;
            this.dataGrid1.ItemsSource = list;
        }

        public class person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
        }
    }
}
