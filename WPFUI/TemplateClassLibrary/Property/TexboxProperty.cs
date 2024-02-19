using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TemplateClassLibrary.Property
{
    public class TexboxProperty
    {
        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(TexboxProperty), new PropertyMetadata(string.Empty));




        public static double GetIconFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(IconFontSizeProperty);
        }

        public static void SetIconFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(IconFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.RegisterAttached("IconFontSize", typeof(double), typeof(TexboxProperty), new PropertyMetadata(0.0));



        public static string GetIconText(DependencyObject obj)
        {
            return (string)obj.GetValue(IconTextProperty);
        }

        public static void SetIconText(DependencyObject obj, string value)
        {
            obj.SetValue(IconTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconTextProperty =
            DependencyProperty.RegisterAttached("IconText", typeof(string), typeof(TexboxProperty), new PropertyMetadata(string.Empty));


        public static string GetIconTextB(DependencyObject obj)
        {
            return (string)obj.GetValue(IconTextBProperty);
        }

        public static void SetIconTextB(DependencyObject obj, string value)
        {
            obj.SetValue(IconTextBProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconTextB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconTextBProperty =
            DependencyProperty.RegisterAttached("IconTextB", typeof(string), typeof(TexboxProperty), new PropertyMetadata(string.Empty));




        public static Thickness GetIconThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(IconThicknessProperty);
        }

        public static void SetIconThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(IconThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconThicknessProperty =
            DependencyProperty.RegisterAttached("IconThickness", typeof(Thickness), typeof(TexboxProperty), new PropertyMetadata(null));



        public static Thickness GetIconThicknessB(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(IconThicknessBProperty);
        }

        public static void SetIconThicknessB(DependencyObject obj, Thickness value)
        {
            obj.SetValue(IconThicknessBProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconThicknessB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconThicknessBProperty =
            DependencyProperty.RegisterAttached("IconThicknessB", typeof(Thickness), typeof(TexboxProperty), new PropertyMetadata(null));




        public static Brush GetIconBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconBorderBrushProperty);
        }

        public static void SetIconBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconBorderBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBorderBrushProperty =
            DependencyProperty.RegisterAttached("IconBorderBrush", typeof(Brush), typeof(TexboxProperty), new PropertyMetadata(null));



        public static Brush GetIconBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconBackgroundProperty);
        }

        public static void SetIconBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.RegisterAttached("IconBackground", typeof(Brush), typeof(TexboxProperty), new PropertyMetadata(null));




        public static Brush GetIconForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconForegroundProperty);
        }

        public static void SetIconForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.RegisterAttached("IconForeground", typeof(Brush), typeof(TexboxProperty), new PropertyMetadata(null));



        public static CornerRadius GetTextBoxCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(TextBoxCornerRadiusProperty);
        }

        public static void SetTextBoxCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(TextBoxCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxCornerRadiusProperty =
            DependencyProperty.RegisterAttached("TextBoxCornerRadius", typeof(CornerRadius), typeof(TexboxProperty), new PropertyMetadata(null));



        public static string GetPrefix(DependencyObject obj)
        {
            return (string)obj.GetValue(PrefixProperty);
        }

        public static void SetPrefix(DependencyObject obj, string value)
        {
            obj.SetValue(PrefixProperty, value);
        }

        // Using a DependencyProperty as the backing store for Prefix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrefixProperty =
            DependencyProperty.RegisterAttached("Prefix", typeof(string), typeof(TexboxProperty), new PropertyMetadata(string.Empty));




        public static string GetSuffix(DependencyObject obj)
        {
            return (string)obj.GetValue(SuffixProperty);
        }

        public static void SetSuffix(DependencyObject obj, string value)
        {
            obj.SetValue(SuffixProperty, value);
        }

        // Using a DependencyProperty as the backing store for Suffix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuffixProperty =
            DependencyProperty.RegisterAttached("Suffix", typeof(string), typeof(TexboxProperty), new PropertyMetadata(string.Empty));




        public static string GetSize(DependencyObject obj)
        {
            return (string)obj.GetValue(SizeProperty);
        }

        public static void SetSize(DependencyObject obj, string value)
        {
            obj.SetValue(SizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.RegisterAttached("Size", typeof(string), typeof(TexboxProperty), new PropertyMetadata(new PropertyChangedCallback((o, e) => {
                string size = e.NewValue.ToString();
                if (!string.IsNullOrEmpty(size))
                {
                    switch (size)
                    {
                        case "large":
                            (o as TextBox).Width = 180;
                            (o as TextBox).Height = 40;
                            break;
                        case "medium":
                            (o as TextBox).Width = 180;
                            (o as TextBox).Height = 37;
                            break;
                        case "small":
                            (o as TextBox).Width = 180;
                            (o as TextBox).Height = 32;
                            break;
                        case "mini":
                            (o as TextBox).Width = 180;
                            (o as TextBox).Height = 28;
                            break;
                    }
                }
            })));




        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(TexboxProperty), new PropertyMetadata(new PropertyChangedCallback((o,e)=>{
                if (e.NewValue != null)
                {
                    Boolean isShow = Boolean.Parse((String)e.NewValue);
                    if (isShow)
                    {
                        (o as PasswordBox).Tag = (o as PasswordBox).Password;
                    }
                }
                
            })));





    }
}
