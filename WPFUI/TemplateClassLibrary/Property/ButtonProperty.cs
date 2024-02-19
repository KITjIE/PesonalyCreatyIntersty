﻿
using System.Windows;
using System.Windows.Controls;

namespace TemplateClassLibrary.Property
{

    public class ButtonProperty
    {
        public static CornerRadius GetButtonCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ButtonCornerRadiusProperty);
        }

        public static void SetButtonCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ButtonCornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for ButtonCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ButtonCornerRadius", typeof(CornerRadius), typeof(ButtonProperty), new PropertyMetadata(null));




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
            DependencyProperty.RegisterAttached("Size", typeof(string), typeof(ButtonProperty), new PropertyMetadata(new PropertyChangedCallback((o,e)=> {
                string size = e.NewValue.ToString();
                if (!string.IsNullOrEmpty(size))
                {
                    switch (size)
                    {
                        case "medium":
                            (o as Button).Width = 98;
                            (o as Button).Height = 36;
                        break;
                        case "small":
                            (o as Button).Width = 80;
                            (o as Button).Height = 32;
                            break;
                        case "mini":
                            (o as Button).Width = 80;
                            (o as Button).Height = 28;
                            break;
                    }
                }
                
            })));

    }

}
