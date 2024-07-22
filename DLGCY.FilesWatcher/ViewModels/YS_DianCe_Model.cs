using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    public class YS_DianCe_Model
    {
        public DateTime Date { get; set; } // 日期
        public string DeviceName { get; set; } // 设备名称
        public string DeviceNumber { get; set; } // 设备编号
        public string ProductNumber { get; set; } // 产品号
        public string BatchNumber { get; set; } // 批次号
        public int UnitOrder { get; set; } // 单元排序
        public string Barcode { get; set; } // 条码
        public string Result { get; set; } // 结果
        public int GoodUnitCount { get; set; } // 良品单元数
        public int BadUnitCount { get; set; } // 不良品单元数
        public string BadUnitInfo { get; set; } // 不良品信息
    }
}
