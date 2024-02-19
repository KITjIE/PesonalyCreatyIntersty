﻿using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace TemplateClassLibrary.ClassComponent.DateTimeControl
{
    [ToolboxBitmap(typeof(DateTimePicker), "DateTimePicker.bmp")]  
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public DateTimePicker(string txt)
            : this()
        {
            // this.textBox1.Text = txt;

        }

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {
            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }

            TDateTimeView dtView = new TDateTimeView(textBlock1.Text);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {

                textBlock1.Text = dateTimeStr;
                DateTime = Convert.ToDateTime(dateTimeStr);
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭

            };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }

        /// <summary>
        /// DateTimePicker 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = this.DateTime;
            if (dt.Year == 1)
                dt = DateTime.Now;
            textBlock1.Text = dt.ToString("yyyy/MM/dd HH:mm:ss");//"yyyyMMddHHmmss"
            DateTime = dt;
            //  DateTime = Convert.ToDateTime(textBlock1.Text);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTime { get; set; }

        #endregion
    }
}
