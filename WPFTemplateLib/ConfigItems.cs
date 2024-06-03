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
        /// 是否MES返回报错弹窗;
        /// </summary>
        public bool IsMESerrorWin { get; set; } = true;
        /// <summary>
        /// 是否匹配AOI产品型号;
        /// </summary>
        public bool IsMatchAOIModel { get; set; } = false;
        /// <summary>
        /// 是否同时记录到日志中;
        /// </summary>
        public bool IsRecordToLog { get; set; } = false;

        /// <summary>
        /// 信息过多是否自动删除（减半）;
        /// </summary>
        public bool IsAutoHalve { get; set; } = true;
        /// <summary>
        /// 条码特殊符号是否替换;
        /// </summary>
        public bool IsCodeReplace { get; set; } = false;
        /// <summary>
        /// 条码特殊符号替换前;
        /// </summary>
        public string IsCodeReplaceBefore { get; set; } = "";
        /// <summary>
        /// 条码特殊符号替换后;
        /// </summary>
        public string IsCodeReplaceAfter { get; set; } = "";
        /// <summary>
        /// 触发信息自动减半的阈值;
        /// </summary>
        public int AutoHalveThresholdValue { get; set; } = 100000;
    }
}
