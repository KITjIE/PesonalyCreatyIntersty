using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// WPF 鼠标点击和触摸操作后重设鼠标位置的附加属性帮助类
    /// (参考 WpfTouchScrollHelper，ClickAndTouchHelper的改进版)
    /// 用法：引入命名空间(比如wpfHelpers)后，在控件上写上：
    /// wpfHelpers:ClickAndTouchHelper2.IsEnabled="True"
    /// wpfHelpers:ClickAndTouchHelper2.TargetElement="{Binding ., RelativeSource={RelativeSource AncestorType=Window}}"
    /// wpfHelpers:ClickAndTouchHelper2.X_Offset="100"
    /// wpfHelpers:ClickAndTouchHelper2.Y_Offset="100"
    /// </summary>
    public class ClickAndTouchHelper2 : DependencyObject
    {
        #region 成员

        #region 鼠标方法

        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x"> 横坐标 </param>   
        /// <param name="y"> 纵坐标 </param>   
        [DllImport("User32")]
        public static extern void SetCursorPos(int x, int y);

        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <returns> 获取成功返回true </returns>
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out Point point);

        /// <summary>
        /// 设置鼠标显示和隐藏
        /// https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-showcursor?redirectedfrom=MSDN
        /// </summary>
        /// <param name="bShow">If bShow is TRUE, the display count is incremented by one. If bShow is FALSE, the display count is decremented by one.</param>
        /// <returns>The return value specifies the new display counter.</returns>
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);

        //模拟鼠标事件的标志
        const int MOUSEEVENTF_MOVE = 0x0001;        // 移动鼠标
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;    // 模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004;      // 模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;   // 模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010;     // 模拟鼠标右键抬起
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  // 模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;    // 模拟鼠标中键抬起
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;    // 标示是否采用绝对坐标

        /// <summary>
        /// 模拟鼠标事件
        /// </summary>
        /// <param name="dwFlags">标志之一或它们的组合（如：MOUSEEVENTF_LEFTDOWN + MOUSEEVENTF_ABSOLUTE 或 MOUSEEVENTF_LEFTUP）</param>
        /// <param name="dx">指定x方向的绝对位置或相对位置</param>
        /// <param name="dy">指定y方向的绝对位置或相对位置</param>
        /// <param name="cButtons">（未使用可传0）</param>
        /// <param name="dwExtraInfo">（未使用可传0）</param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "mouse_event")]
        private static extern int MouseEvent(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion

        #endregion

        #region 是否启用功能

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(nameof(IsEnabled), typeof(bool), typeof(ClickAndTouchHelper2), new UIPropertyMetadata(false, IsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        #endregion

        #region 目标元素

        /// <summary>
        /// 目标元素
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(ClickAndTouchHelper2), new PropertyMetadata(null));

        public static FrameworkElement GetTargetElement(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(TargetElementProperty);
        }

        public static void SetTargetElement(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(TargetElementProperty, value);
        }

        #endregion

        #region X方向偏移

        /// <summary>
        /// X偏移（左-右+）
        /// </summary>
        public int X_Offset
        {
            get { return (int)GetValue(X_OffsetProperty); }
            set { SetValue(X_OffsetProperty, value); }
        }

        public static readonly DependencyProperty X_OffsetProperty =
            DependencyProperty.Register("X_Offset", typeof(int), typeof(ClickAndTouchHelper2), new PropertyMetadata(0));

        public static int GetX_Offset(DependencyObject obj)
        {
            return (int)obj.GetValue(X_OffsetProperty);
        }

        public static void SetX_Offset(DependencyObject obj, int value)
        {
            obj.SetValue(X_OffsetProperty, value);
        }

        #endregion

        #region Y方向偏移

        /// <summary>
        /// Y偏移（上-下+）
        /// </summary>
        public int Y_Offset
        {
            get { return (int)GetValue(Y_OffsetProperty); }
            set { SetValue(Y_OffsetProperty, value); }
        }

        public static readonly DependencyProperty Y_OffsetProperty =
            DependencyProperty.Register("Y_Offset", typeof(int), typeof(ClickAndTouchHelper2), new PropertyMetadata(0));

        public static int GetY_Offset(DependencyObject obj)
        {
            return (int)obj.GetValue(Y_OffsetProperty);
        }

        public static void SetY_Offset(DependencyObject obj, int value)
        {
            obj.SetValue(Y_OffsetProperty, value);
        }

        #endregion

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

            target.Unloaded += Target_Unloaded;
            target.MouseUp += TargetOnMouseUp;
            target.TouchUp += Target_TouchUp;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public static void Target_Unloaded(object sender, RoutedEventArgs e)
        {
            var target = sender as FrameworkElement;
            if (target == null) return;

            target.Unloaded -= Target_Unloaded;
            target.MouseUp -= TargetOnMouseUp;
            target.TouchUp -= Target_TouchUp;
        }

        /// <summary>
        /// 鼠标按键抬起
        /// </summary>
        private static void TargetOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MoveCursor(sender);
        }

        /// <summary>
        /// 触摸抬起
        /// </summary>
        private static void Target_TouchUp(object sender, TouchEventArgs e)
        {
            MoveCursor(sender);
            //ShowCursor(true);
            //MouseEvent(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //MouseEvent(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// 移动鼠标
        /// </summary>
        /// <param name="sender">事件源对象</param>
        private static void MoveCursor(object sender)
        {
            var depend = sender as DependencyObject;
            var targetElement = GetTargetElement(depend) ?? sender as FrameworkElement;
            Point elementPosition = GetWpfElementScreenPosition(targetElement);
            SetCursorPos((int)elementPosition.X + GetX_Offset(depend), (int)elementPosition.Y + GetY_Offset(depend));
        }

        /// <summary>
        /// 获取WPF元素的屏幕坐标
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>坐标点</returns>
        private static Point GetWpfElementScreenPosition(FrameworkElement element)
        {
            return element.PointToScreen(new Point(0, 0));
        }
    }
}
