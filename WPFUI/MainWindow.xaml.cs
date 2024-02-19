using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using WpfUI.WpfComponents;

namespace WpfUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;//还原窗口（非最小化和最大化）
            //this.WindowStyle = System.Windows.WindowStyle.None;仅工作区可见，不显示标题栏和边框
            //this.ResizeMode = System.Windows.ResizeMode.NoResize;不显示最大化和最小化按钮
            this.Topmost = false;//窗口在最前
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            //Demo();
            //PopUp p = new PopUp();
            //p.Show();
            PdfBuilder();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            BtnExample btnType = new BtnExample();
            this.content.Children.Add(btnType);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            IconList icons = new IconList();
            this.content.Children.Add(icons);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            HaS haS = new HaS();
            this.content.Children.Add(haS);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            table table = new table();
            this.content.Children.Add(table);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            Input input = new Input();
            this.content.Children.Add(input);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            RadioBtnExample radioBtnExample = new RadioBtnExample();
            this.content.Children.Add(radioBtnExample);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            CheckBoxBtnExample checkBoxBtnExample = new CheckBoxBtnExample();
            this.content.Children.Add(checkBoxBtnExample);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            InputExample inputExample = new InputExample();
            this.content.Children.Add(inputExample);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            DateExample dateExample = new DateExample();
            this.content.Children.Add(dateExample);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            LoadingExample dateExample = new LoadingExample();
            this.content.Children.Add(dateExample);
            
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            this.content.Children.Clear();
            SwitchButtonControl switchButton = new SwitchButtonControl();
            this.content.Children.Add(switchButton);
            PdfBuilder();
        }

        public void PdfBuilder()
        {

            DataTable dt = new DataTable();
            DataColumn dataColumn = new DataColumn("姓名", typeof(string));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("年龄", typeof(int));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("体重^兴趣", typeof(string));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("爱好^兴趣", typeof(string));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("职业", typeof(string));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("老家^住所", typeof(string));
            dt.Columns.Add(dataColumn);
            dataColumn = new DataColumn("新家^住所", typeof(string));
            dt.Columns.Add(dataColumn);
            DataRow dataRow = dt.NewRow();
            dataRow["姓名"] = "周伟涛";
            dataRow["年龄"] = "30";
            dataRow["体重^兴趣"] = "75";
            dataRow["爱好^兴趣"] = "编程";
            dataRow["职业"] = "程序员";
            dataRow["老家^住所"] = "宝鸡";
            dataRow["新家^住所"] = "西安";
            dt.Rows.Add(dataRow.ItemArray);
            dataRow["姓名"] = "童婷婷";
            dataRow["年龄"] = "30";
            dataRow["体重^兴趣"] = "60";
            dataRow["爱好^兴趣"] = "带娃";
            dataRow["职业"] = "测试";
            dataRow["老家^住所"] = "宝鸡";
            dataRow["新家^住所"] = "西安";
            dt.Rows.Add(dataRow.ItemArray);
            dataRow["姓名"] = "周梓童";
            dataRow["年龄"] = "2";
            dataRow["体重^兴趣"] = "20";
            dataRow["爱好^兴趣"] = "打爸爸";
            dataRow["职业"] = "玩耍";
            dataRow["老家^住所"] = "宝鸡";
            dataRow["新家^住所"] = "西安";
            dt.Rows.Add(dataRow.ItemArray);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Document document = new Document(PageSize.A4, 5, 5, 5, 5);
            BaseFont bfchinese = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Font arial = new Font(bfchinese, 13, Font.NORMAL);
            Font headerFont = new Font(bfchinese, 20, Font.BOLD);
            FileStream fileStream = new FileStream("ZWT.pdf", FileMode.Create); //new FileStream("D://A09/ZWT.pdf", FileMode.Create);
            PdfWriter.GetInstance(document, fileStream);
            document.Open();
            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
            //标题
            PdfPCell header = new PdfPCell(new Phrase("浙江省人民医院", headerFont));
            header.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            header.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            header.Colspan = dt.Columns.Count;
            pdfTable.AddCell(header);



            //列头
            string hbColName = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Contains("^"))
                {
                    if (hbColName == dt.Columns[i].ColumnName.Split('^')[1]) continue;
                    hbColName = dt.Columns[i].ColumnName.Split('^')[1];
                    PdfPCell cell = new PdfPCell(new Phrase(dt.Columns[i].ColumnName.Split('^')[1], arial));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    cell.Colspan = 2;
                    pdfTable.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dt.Columns[i].ColumnName, arial));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    cell.Rowspan = 2;
                    pdfTable.AddCell(cell);
                }
             
            }

            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName.Contains("住所"))
                {
                    PdfPCell glCell = new PdfPCell(new Phrase(column.ColumnName.Split('^')[0], arial));
                    glCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    glCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    pdfTable.AddCell(glCell);
                }
            }

            ///数据
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(row[column.ColumnName].ToString(), arial));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    pdfTable.AddCell(cell);
                }
            }
            document.Add(pdfTable);
            document.Close();
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }
    }
}
