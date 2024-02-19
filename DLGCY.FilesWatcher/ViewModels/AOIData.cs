using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    public class AOIData
    {
        /// <summary>
        /// 機種名稱
        /// </summary>
        public string EQPTypeName { get; set; }
        /// <summary>
        /// 子板條碼
        /// </summary>
        public string SubBoardBarCode { get; set; }
        /// <summary>
        /// SMT 綫別名稱
        /// </summary>
        public string LineName { get; set; }
        /// <summary>
        /// 小板編號
        /// </summary>
        public string BoardNumber { get; set; }
        /// <summary>
        /// 作業員標示
        /// </summary>
        public string OperatorMark { get; set; }
        /// <summary>
        /// 工單號
        /// </summary>
        public string WorkOrderNumber { get; set; }
        /// <summary>
        /// 檢測日期與時間
        /// </summary>
        public string TestDate { get; set; }
        /// <summary>
        /// 作業員復判日期與時間
        /// </summary>
        public string RecheckDate { get; set; }
        /// <summary>
        /// PCB 狀態
        /// </summary>
        public string PCBStatus { get; set; }
        /// <summary>
        /// 板面標示
        /// </summary>
        public string BoardIdentifi { get; set; }
        /// <summary>
        /// 當前小板上經過 AOI 檢測的元件數量
        /// </summary>
        public string CurrentNumber { get; set; }
        /// <summary>
        /// 實際不良的元件數量,若為“Pass、Rpass 、Skip、UnTest”，則為“0”
        /// </summary>
        public string DefectiveNumber { get; set; }
        /// <summary>
        /// 元件名稱;元件類型或料號;不良代碼;不良檢測框數量
        /// </summary>
        public string BadCode { get; set; }

    }
}
