using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// 禁用触摸转换为鼠标（未使用成功）
    /// As long as this object exists all mouse events created from a touch event for legacy support will be disabled.
    /// Or, at least, that's the promise. But the system mouse-cursor still gets set to the position of the last
    /// touch-event, and so far, there seems to be no way to prevent that.
    /// 原版本：https://stackoverflow.com/questions/8010104/how-to-avoid-mouse-move-on-touch
    /// 修改版本（此版本）：https://gist.github.com/ulatekh/f37b1a973c7a1b09f18a457e3a4af54a
    /// </summary>
    /// <example>
    /// <code>
    /// // As long as the instance is alive the conversion won't occur
    /// var disableTouchMouse = new DisableTouchConversionToMouse();
    /// 
    /// // To let the conversion happen again, Dispose the class.
    /// disableTouchMouse.Dispose();
    /// </code>
    /// </example>
    public class DisableTouchConversionToMouse : IDisposable
    {
        // The window on which to disable the conversion of touch-events to
        // mouse-events (including all of its child windows).
        // If null, conversion is disabled globally.
        private IntPtr m_hWnd;

        // The installed hook-callbacks.
        private HookProc hookCallback, hookCallbackLL;
        private IntPtr hookId, hookIdLL;

        // Disable touch-conversion globally.
        public DisableTouchConversionToMouse()
            : this(IntPtr.Zero)
        {
        }

        // Disable touch-conversion for a single window (including all its child windows).
        public DisableTouchConversionToMouse(IntPtr hWnd)
        {
            // Remember the window on which to disable event-conversion, if any.
            m_hWnd = hWnd;

            // Install both styles of mouse hooks.
            // Save a reference to the callback, so that it doesn't get
            // garbage-collected.
            hookCallback = this.HookCallback;
            hookId = SetHook(hookCallback);
            hookCallbackLL = this.HookCallbackLL;
            hookIdLL = SetHookLL(hookCallbackLL);
        }

        // Set a WH_MOUSE callback.
        static IntPtr SetHook(HookProc proc)
        {
            var moduleHandle = UnsafeNativeMethods.GetModuleHandle(null);
            var threadHandle = UnsafeNativeMethods.GetCurrentThreadId();

            var setHookResult = UnsafeNativeMethods.SetWindowsHookEx(WH_MOUSE, proc, moduleHandle, threadHandle);
            if (setHookResult == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return setHookResult;
        }

        // Set a WH_MOUSE_LL callback.
        static IntPtr SetHookLL(HookProc proc)
        {
            var moduleHandle = UnsafeNativeMethods.GetModuleHandle(null);

            var setHookResult = UnsafeNativeMethods.SetWindowsHookEx(WH_MOUSE_LL, proc, moduleHandle, 0);
            if (setHookResult == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return setHookResult;
        }

        // The WH_MOUSE callback.
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var info = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MOUSEHOOKSTRUCT));

                // Determine whether this mouse-event was generated from a touch event.
                // Fix for 64-bit Windows found in a comment from giangurgolo at the
                // end of http://stackoverflow.com/a/8012213 .
                bool bMouseEventFromTouch = false;
                if (IntPtr.Size == 4)
                {
                    var extraInfo = (uint)info.dwExtraInfo.ToInt32();
                    if ((extraInfo & MOUSEEVENTF_FROMTOUCH) == MOUSEEVENTF_FROMTOUCH)
                    {
                        bMouseEventFromTouch = true;
                    }
                }
                else if (IntPtr.Size == 8)
                {
                    var extraInfo = (ulong)info.dwExtraInfo.ToInt64();
                    if ((extraInfo & MOUSEEVENTF_FROMTOUCH) == MOUSEEVENTF_FROMTOUCH)
                    {
                        bMouseEventFromTouch = true;
                    }
                }

                if (bMouseEventFromTouch)
                {
                    // If we weren't initialized with a window-handle, then
                    // converting touch-events to mouse events is disabled
                    // for all windows.
                    if (m_hWnd == null)
                        return new IntPtr(1);

                    // Otherwise, disable converting touch-events to mouse events
                    // for the given window, and all its child windows.
                    IntPtr hwnd = info.hwnd;
                    while (hwnd != null)
                    {
                        if (hwnd == m_hWnd)
                            return new IntPtr(1);
                        hwnd = UnsafeNativeMethods.GetParent(hwnd);
                    }
                }
            }

            return UnsafeNativeMethods.CallNextHookEx(hookIdLL, nCode, wParam, lParam);
        }

        // The WH_MOUSE_LL callback.
        private IntPtr HookCallbackLL(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var info = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // Determine whether this mouse-event was generated from a touch event.
                // Fix for 64-bit Windows found in a comment from giangurgolo at the
                // end of http://stackoverflow.com/a/8012213 .
                bool bMouseEventFromTouch = false;
                if (IntPtr.Size == 4)
                {
                    var extraInfo = (uint)info.dwExtraInfo.ToInt32();
                    if ((extraInfo & MOUSEEVENTF_FROMTOUCH) == MOUSEEVENTF_FROMTOUCH)
                    {
                        bMouseEventFromTouch = true;
                    }
                }
                else if (IntPtr.Size == 8)
                {
                    var extraInfo = (ulong)info.dwExtraInfo.ToInt64();
                    if ((extraInfo & MOUSEEVENTF_FROMTOUCH) == MOUSEEVENTF_FROMTOUCH)
                    {
                        bMouseEventFromTouch = true;
                    }
                }

                if (bMouseEventFromTouch)
                {
                    // If we weren't initialized with a window-handle, then
                    // converting touch-events to mouse events is disabled
                    // for all windows.
                    if (m_hWnd == null)
                        return new IntPtr(1);

                    // Otherwise, disable converting touch-events to mouse events
                    // for the given window, and all its child windows.
                    IntPtr hwnd = UnsafeNativeMethods.WindowFromPoint(info.pt);
                    while (hwnd != null)
                    {
                        if (hwnd == m_hWnd)
                            return new IntPtr(1);
                        hwnd = UnsafeNativeMethods.GetParent(hwnd);
                    }
                }
            }

            return UnsafeNativeMethods.CallNextHookEx(hookIdLL, nCode, wParam, lParam);
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed) return;

            UnsafeNativeMethods.UnhookWindowsHookEx(hookId);
            UnsafeNativeMethods.UnhookWindowsHookEx(hookIdLL);
            disposed = true;
            GC.SuppressFinalize(this);
        }

        ~DisableTouchConversionToMouse()
        {
            Dispose();
        }

        #region Interop

        // ReSharper disable InconsistentNaming
        // ReSharper disable MemberCanBePrivate.Local
        // ReSharper disable FieldCanBeMadeReadOnly.Local

        const uint MOUSEEVENTF_FROMTOUCH = 0xFF515700;
        const int WH_MOUSE = 7;
        const int WH_MOUSE_LL = 14;

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEHOOKSTRUCT
        {
            public POINT pt;
            public IntPtr hwnd;
            public uint wHitTestCode;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [SuppressUnmanagedCodeSecurity]
        static class UnsafeNativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod,
                uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("kernel32.dll")]
            public static extern uint GetCurrentThreadId();

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr WindowFromPoint(POINT p);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetParent(IntPtr hWnd);
        }

        // ReSharper restore InconsistentNaming
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        // ReSharper restore MemberCanBePrivate.Local

        #endregion
    }
}
