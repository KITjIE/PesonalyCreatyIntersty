using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// WPF 鼠标点击和触摸操作的附加属性帮助类(点击或触摸后移开鼠标)（供参考）
    /// (参考 WpfTouchScrollHelper)
    /// 用法：引入命名空间(比如util)后，在控件上写上 util:ClickAndTouchHelper.IsEnabled="True"
    /// </summary>
    public class ClickAndTouchHelper : DependencyObject
    {
        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x"> 横坐标 </param>   
        /// <param name="y"> 纵坐标 </param>   
        [DllImport("User32")]
        public static extern void SetCursorPos(int x, int y);

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
            DependencyProperty.RegisterAttached(nameof(IsEnabled), typeof(bool), typeof(ClickAndTouchHelper), new UIPropertyMetadata(false, IsEnabledChanged));

        public static Dictionary<object, MouseCapture> _captures = new Dictionary<object, MouseCapture>();

        /// <summary>
        /// 开关触发事件
        /// </summary>
        public static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as FrameworkElement;
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
            var target = sender as FrameworkElement;
            if (target == null) return;

            //System.Diagnostics.Debug.WriteLine("Target Loaded");

            target.Unloaded += Target_Unloaded;
            //target.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            //target.PreviewMouseMove += Target_PreviewMouseMove;
            //target.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
            target.MouseUp += TargetOnMouseUp;
            target.TouchUp += Target_TouchUp;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public static void Target_Unloaded(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Target Unloaded");

            var target = sender as FrameworkElement;
            if (target == null) return;

            _captures.Remove(sender);

            target.Unloaded -= Target_Unloaded;
            //target.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            //target.PreviewMouseMove -= Target_PreviewMouseMove;
            //target.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
            target.MouseUp -= TargetOnMouseUp;
            target.TouchUp -= Target_TouchUp;
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        public static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var target = sender as FrameworkElement;
            if (target == null) return;

            _captures[sender] = new MouseCapture
            {
                HorticalOffset = e.GetPosition(target).X,
                VerticalOffset = e.GetPosition(target).Y,
                Point = e.GetPosition(target),
            };
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        public static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var target = sender as FrameworkElement;
            if (target == null) return;

            target.ReleaseMouseCapture();
        }

        /// <summary>
        /// 鼠标按键抬起
        /// </summary>
        private static void TargetOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCursorPos((int)e.GetPosition(Application.Current.MainWindow).X, 150);
        }

        /// <summary>
        /// 触摸抬起
        /// </summary>
        private static void Target_TouchUp(object sender, TouchEventArgs e)
        {
            SetCursorPos(0, 0);
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

            var target = sender as FrameworkElement;
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
