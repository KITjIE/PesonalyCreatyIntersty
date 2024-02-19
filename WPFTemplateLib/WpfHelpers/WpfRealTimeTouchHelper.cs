using System;
using System.Reflection;
using System.Windows.Input;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// WPF实时触摸帮助类（好像代码中设置无效，App.config 中设置有效）
    /// </summary>
    public class WpfRealTimeTouchHelper
    {
        /// <summary>
        /// 删除 WPF 窗口添加的任何平板电脑支持，禁用 WPF 侦听触笔输入
        /// https://docs.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/disable-the-realtimestylus-for-wpf-applications?view=netframeworkdesktop-4.8
        /// </summary>
        public static void DisableWPFTabletSupport()
        {
            try
            {
                // Get a collection of the tablet devices for this window.  
                TabletDeviceCollection devices = Tablet.TabletDevices;

                if (devices.Count > 0)
                {
                    // Get the Type of InputManager.
                    Type inputManagerType = typeof(InputManager);

                    // Call the StylusLogic method on the InputManager.Current instance.
                    object stylusLogic = inputManagerType.InvokeMember("StylusLogic",
                                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, InputManager.Current, null);

                    if (stylusLogic != null)
                    {
                        //  Get the type of the device class.
                        Type devicesType = devices.GetType();

                        // Loop until there are no more devices to remove.
                        int count = devices.Count + 1;

                        while (devices.Count > 0)
                        {
                            // Remove the first tablet device in the devices collection.
                            devicesType.InvokeMember("HandleTabletRemoved", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, devices, new object[] { (uint)0 });

                            count--;

                            if (devices.Count != count)
                            {
                                //throw new Win32Exception("Unable to remove real-time stylus support.");
                                Console.WriteLine("DisableWPFTabletSupport计算设备数量不正确。 ");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableWPFTabletSupport异常：{ex}");
            }
        }

        /// <summary>
        /// 设置WPF实时触摸机制的开关状态
        /// <para> 对应于 App.config 中 runtime 节点的 AppContextSwitchOverrides value="Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport=true"/> </para>
        /// </summary>
        /// <param name="openOrClose">开启还是关闭（true-开启，false-关闭）</param>
        public static void SetRealTimeTouchStatus(bool openOrClose)
        {
            //设置是否禁用实时触摸机制（与参数相反）
            AppContext.SetSwitch("Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport", !openOrClose);
        }
    }
}
