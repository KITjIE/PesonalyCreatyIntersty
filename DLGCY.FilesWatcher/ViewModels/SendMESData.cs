using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{

    public class PartDetectionInfo
    {

        /// <summary>
        /// 板面（默认：""）
        /// </summary>
        public string? BothSideCode { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        public string? CreateDate { get; set; }

        /// <summary>
        ///人员复判时间
        /// </summary>
        public string? ReviseEndDate { get; set; }

        /// <summary>
        /// 设备判定结果（默认：true）
        /// </summary>
        public bool? TestResult { get; set; }

        /// <summary>
        /// 人员复判结果（默认：true）--以该结果为准
        /// </summary>
        public bool? ReviseResult { get; set; }

        /// <summary>
        ///PCB SN （存在不良的SN条码，非拼板码）
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// 点位（未校验）
        /// </summary>
        public string? PartsName { get; set; }

        /// <summary>
        /// 不良代码（未校验）
        /// </summary>
        public string? FaultCode { get; set; }

        /// <summary>
        ///人员复判不良代码（未校验）
        /// </summary>
        public string? RevisedFaultCode { get; set; }

        /// <summary>
        ///元件位号对应的图片的文件名
        /// </summary>
        public string? ImageUrl { get; set; }
 
    }

    public class ImageInfo
    {

        /// <summary>
        /// 图片位置（默认：""）
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 第几张（默认传0）
        /// </summary>
        public int? ImageIndex { get; set; }

        /// <summary>
        ///共有几张（默认传0）
        /// </summary>
        public int? TotalImage { get; set; }

        /// <summary>
        ///上传结果是否成功（默认传false)
        /// </summary>
        public bool? UploadResult { get; set; }

    }

    public class Value
    {
        /// <summary>
        /// 程序名称（默认：""）
        /// </summary>
        public string? ProgramName { get; set; }

        /// <summary>
        /// 数量（默认：1）--无验证逻辑
        /// </summary>
        public int? Qty { get; set; }

        /// <summary>
        ///设备ID号（设备台账编码）
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        ///条码（拼板码\非拼版码）
        /// </summary>
        public string? Barcode { get; set; }


        /// <summary>
        /// 设备判定结果（默认：true）
        /// </summary>
        public bool? TestResult { get; set; }

        /// <summary>
        ///人员复判结果（默认：true）--以该结果为准
        /// </summary>
        public bool? ReviseResult { get; set; }

        /// <summary>
        //采集时间戳
        /// </summary>
        public DateTime? TimeStamp { get; set; }


        /// <summary>
        //
        /// </summary>
        public List<PartDetectionInfo>? PartDetectionInfos { get; set; }


        /// <summary>
        //
        /// </summary>
        public List<ImageInfo>? ImageInfos { get; set; }


    }

    public class Context
    {
        public string Ticket { get; set; }
        public int InvOrgId { get; set; }
    }

    public class Parameter
    {
        public Value? Value { get; set; }
    }

    public class Root
    {
        public string? ApiType { get; set; }
        public List<Parameter>? Parameters { get; set; }
        public string? Method { get; set; }
        public Context? Context { get; set; }
    }

}



