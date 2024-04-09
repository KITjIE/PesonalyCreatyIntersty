using PropertyChanged;
using static System.Net.WebRequestMethods;

namespace WPFTemplate
{
    /// <summary>
    /// 配置项
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class ConfigItems:WPFTemplateLib.ConfigItems
    {
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// 是否显示完全路径
        /// </summary>
        public bool IsShowFullPath { get; set; } = true;

        /// <summary>
        /// 是否监控子文件夹
        /// </summary>
        public bool IsMonitorSubDir { get; set; } = true;
        /// <summary>
        /// URL地址
        /// </summary>
        public string URLPath { get; set; } = "http://10.164.19.102:1040/api/dataportal/invoke";

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool EditMode { get; set; } = true;

        /// <summary>
        /// 解析完成地址
        /// </summary>
        public string FinalPath { get; set; } = @"D:\FinalPath";

        /// <summary>
        /// 解析失败地址（网络问题）
        /// </summary>
        public string ErrorPath { get; set; } = @"C:\ErrorPath";
        /// <summary>
        /// 解析完成计数
        /// </summary>
        public long AnalysCount { get; set; } = 0;
        /// <summary>
        /// 监控模式
        /// </summary>
        public string SupervisMode { get; set; } = "文件解析模式B";
        /// <summary>
        /// 建立电脑
        /// </summary>
        public string Computer { get; set; } 
        /// <summary>
        /// 建立用户
        /// </summary>
        public string BuildUser { get; set; } 
        /// <summary>
        /// 产品型号
        /// </summary>
        public string PorductModel { get; set; } 
        /// <summary>
        /// 机器型号
        /// </summary>
        public string MachineModel { get; set; }
        /// <summary>
        /// 库存组织
        /// </summary>
        public string InvOrgId { get; set; } = string.Empty;
        /// <summary>
        /// 过站结果
        /// </summary>
        public string UploadResult { get; set; } = string.Empty;
        /// <summary>
        /// 产品条码
        /// </summary>
        public string ProductBarcode { get; set; } = string.Empty;
        /// <summary>
        /// MES返回报错信息
        /// </summary>
        public string MESErrorInfo { get; set; } = string.Empty;
        /// <summary>
        /// Excel读取最后一行
        /// </summary>
        public int ExcelLastRow { get; set; } = 0;
    }
}
