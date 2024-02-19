using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace WPFTemplateLib.WpfHelpers
{
    public class MouseHelper
    {
        #region 鼠标操作

        /// <summary>
        /// 移动鼠标
        /// </summary>
        /// <param name="sender">事件源对象</param>
        /// <param name="xOffset">X偏移</param>
        /// <param name="yOffset">Y偏移</param>
        private static void MoveCursor(object sender, int xOffset, int yOffset)
        {
            var depend = sender as DependencyObject;
            var targetElement = sender as FrameworkElement;
            Point elementPosition = GetWpfElementScreenPosition(targetElement);
            SetCursorPos((int)elementPosition.X + xOffset, (int)elementPosition.Y + yOffset);
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

        #endregion

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
    }
}
