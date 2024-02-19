using PropertyChanged;

namespace WPFTemplateLib
{
    /// <summary>
    /// 配置项
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class ConfigItems:IConfigItems
    {
        /// <summary>
        /// 消息框是否自动滚动;
        /// </summary>
        public bool IsAutoScroll { get; set; } = true;

        /// <summary>
        /// 是否同时记录到日志中;
        /// </summary>
        public bool IsRecordToLog { get; set; } = false;

        /// <summary>
        /// 信息过多是否自动删除（减半）;
        /// </summary>
        public bool IsAutoHalve { get; set; } = true;

        /// <summary>
        /// 触发信息自动减半的阈值;
        /// </summary>
        public int AutoHalveThresholdValue { get; set; } = 100000;
    }
}
