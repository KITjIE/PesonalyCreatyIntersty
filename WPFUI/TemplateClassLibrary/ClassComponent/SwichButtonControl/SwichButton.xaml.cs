using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TemplateClassLibrary.ClassComponent.SwichButtonControl
{
    /// <summary>
    /// swichButton.xaml 的交互逻辑
    /// </summary>
    public partial class SwichButton : UserControl
    {

        public SolidColorBrush ActiveColor  
        {
            get { return (SolidColorBrush)GetValue(ActiveColorProperty); }
            set { SetValue(ActiveColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveColorProperty =
            DependencyProperty.Register("ActiveColor", typeof(SolidColorBrush), typeof(SwichButton), new PropertyMetadata(new SolidColorBrush()));



        public SolidColorBrush InactiveColor
        {               
            get { return (SolidColorBrush)GetValue(InactiveColorProperty); }
            set { SetValue(InactiveColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InactiveColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InactiveColorProperty =
            DependencyProperty.Register("InactiveColor", typeof(SolidColorBrush), typeof(SwichButton), new PropertyMetadata(new SolidColorBrush()));



        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftTextProperty =
            DependencyProperty.Register("LeftText", typeof(string), typeof(SwichButton), new PropertyMetadata(string.Empty));



        public string RightText
        {
            get { return (string)GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightTextProperty =
            DependencyProperty.Register("RightText", typeof(string), typeof(SwichButton), new PropertyMetadata(string.Empty));



        public Visibility TextIsVisibility
        {
            get { return (Visibility)GetValue(TextIsVisibilityProperty); }
            set { SetValue(TextIsVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextIsVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextIsVisibilityProperty =
            DependencyProperty.Register("TextIsVisibility", typeof(Visibility), typeof(SwichButton), new PropertyMetadata(new Visibility()));



        bool firstInit = true;

        public SwichButton()
        {
            InitializeComponent();
        }

        public Func<bool,string,string> isActive { get; set; }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            firstInit = !firstInit;
            if (firstInit)
            {
                this.ellipse.HorizontalAlignment = HorizontalAlignment.Right;
                this.border.Background = this.ActiveColor;
                this.rightText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#409eff"));
                this.leftText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303133"));
                if (isActive != null)
                {
                    isActive(true,this.rightText.Text);
                }
            }
            else
            {
                this.ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                this.border.Background = this.InactiveColor;
                this.rightText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303133"));
                this.leftText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#409eff"));
                if (isActive != null)
                {
                    isActive(false, this.rightText.Text);
                }
            }
            
        }
    }
}
