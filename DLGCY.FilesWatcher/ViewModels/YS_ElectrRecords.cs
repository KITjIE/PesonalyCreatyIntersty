using System.Collections.Generic; 

namespace DLGCY.FilesWatcher.ViewModels
{
    public class YS_ElectrRecords
    {
        /// <summary>
        /// 测试电脑
        /// </summary>
        public string? TestComputer { get; set; }

        /// <summary>
        /// 测试人
        /// </summary>
        public string? Tester { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public string? TestTime { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public string? BarCode { get; set; }

        /// <summary>
        /// 产品工程名
        /// </summary>
        public string? ProductOjectName { get; set; }

        /// <summary>
        /// 机器编码
        /// </summary>
        public string? MachineNumber { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string? ProductModel { get; set; }

        /// <summary>
        /// Temp
        /// </summary>
        public string? Temp { get; set; }

        /// <summary>
        /// 测试ID
        /// </summary>
        public string? TestID { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public string? TestResult { get; set; }

        /// <summary>
        /// 是否OK
        /// </summary>
        public bool? IsOK { get; set; }

        /// <summary>
        /// 电测档案明细接口
        /// </summary>
        public List<ElectricalDetailDownInfo>? ElectricalDetailDownInfos { get; set; }
    }

    public class ElectricalDetailDownInfo
    {
        /// <summary>
        /// 建立电脑
        /// </summary>
        public string? SetupComputer { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public string? TestTime { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string? TestSerialNumber { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public string? BarCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? ProjectName { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string? ProductModel { get; set; }

        /// <summary>
        /// 临时
        /// </summary>
        public string? Temporary { get; set; }

        /// <summary>
        /// 建立用户
        /// </summary>
        public string? EstablishUsers { get; set; }

        /// <summary>
        /// 测试设备ID
        /// </summary>
        public string? TestEquipmentId { get; set; }

        /// <summary>
        /// 测试者
        /// </summary>
        public string? Testers { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public string? TestResult { get; set; }

        /// <summary>
        /// 是否OK
        /// </summary>
        public bool? IsOK { get; set; }

        /// <summary>
        /// 测试文件名
        /// </summary>
        public string? TestFileName { get; set; }

        /// <summary>
        /// 电流项目名称
        /// </summary>
        public string? CurrentItemName { get; set; }

        /// <summary>
        /// 项目类别
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 组别名称
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// 测试电位
        /// </summary>
        public string? TestPoint { get; set; }

        /// <summary>
        /// 测试范围
        /// </summary>
        public string? TestScope { get; set; }

        /// <summary>
        /// 最低限制
        /// </summary>
        public string? MinimumLimit { get; set; }

        /// <summary>
        /// 最高限制
        /// </summary>
        public string? MaximumLimit { get; set; }

        /// <summary>
        /// 数据单位
        /// </summary>
        public string? DataUnit { get; set; }

        /// <summary>
        /// 测试值
        /// </summary>
        public string? TestValue { get; set; }

        /// <summary>
        /// 详细结果
        /// </summary>
        public string? DetailedResults { get; set; }

        /// <summary>
        /// 详细是否OK
        /// </summary>
        public bool? DetailedIsOK { get; set; }
    }
}