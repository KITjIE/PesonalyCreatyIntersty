using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// 功能：让 WPF ScrollViewer 支持触摸滚动/鼠标拖拽滚动（之前只支持滚动条或鼠标滚轮滚动）
    /// 来源：https://www.cnblogs.com/wgscd/p/10558132.html
    /// 用法：引入命名空间(比如util)后，在 ScrollViewer 上写上 util:WpfTouchScrollHelper.IsEnabled="True"
    /// </summary>
    public class WpfTouchScrollHelper : DependencyObject
    {
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WpfTouchScrollHelper), new UIPropertyMetadata(false, IsEnabledChanged));

        public static Dictionary<object, MouseCapture> _captures = new Dictionary<object, MouseCapture>();

        /// <summary>
        /// 开关触发事件
        /// </summary>
        public static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as ScrollViewer;
            if (target == null) return;

            if ((bool)e.NewValue)
            {
                target.Loaded += Target_Loaded;
            }
            else
            {
                target.Loaded -= Target_Loaded;
                Target_Unloaded(target, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// 启用
        /// </summary>
        public static void Target_Loaded(object sender, RoutedEventArgs e)
        {
            var target = sender as ScrollViewer;
            if (target == null) return;

            System.Diagnostics.Debug.WriteLine("Target Loaded");

            target.Unloaded += Target_Unloaded;
            target.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            target.PreviewMouseMove += Target_PreviewMouseMove;
            target.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public static void Target_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Target Unloaded");

            var target = sender as ScrollViewer;
            if (target == null) return;

            _captures.Remove(sender);

            target.Unloaded -= Target_Unloaded;
            target.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            target.PreviewMouseMove -= Target_PreviewMouseMove;
            target.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        public static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var target = sender as ScrollViewer;
            if (target == null) return;

            _captures[sender] = new MouseCapture
            {
                VerticalOffset = target.VerticalOffset,
                HorticalOffset = target.HorizontalOffset,
                Point = e.GetPosition(target),
            };
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        public static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var target = sender as ScrollViewer;
            if (target == null) return;

            target.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        public static void Target_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_captures.ContainsKey(sender)) return;

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _captures.Remove(sender);
                return;
            }

            var target = sender as ScrollViewer;
            if (target == null) return;

            var capture = _captures[sender];
            var point = e.GetPosition(target);
            var dy = point.Y - capture.Point.Y;
            var dx = point.X - capture.Point.X;

            if (Math.Abs(dy) > 5)
            {
                target.CaptureMouse();
            }
            if (Math.Abs(dx) > 5)
            {
                target.CaptureMouse();
            }

            target.ScrollToVerticalOffset(capture.VerticalOffset - dy);
            target.ScrollToHorizontalOffset(capture.HorticalOffset - dx);
        }

        /// <summary>
        /// 鼠标快照
        /// </summary>
        public class MouseCapture
        {
            public double VerticalOffset { get; set; }
            public double HorticalOffset { get; set; }

            public Point Point { get; set; }
        }
    }
}
