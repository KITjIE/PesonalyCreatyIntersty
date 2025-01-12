using FreeSql.DataAnnotations;
using System; 

namespace DLGCY.FilesWatcher.ViewModels
{
    public class YSXNY_KTHuiLiuHan
    {
        //[Column(Name = "ID", IsPrimary = true, IsIdentity = true)]
        //public long? ID { get; set; }
        [Column(Name = "WORKSTATION")]
        public string? WORKSTATION { get; set; }
        [Column(Name = "MODEL")]
        public string? MODEL { get; set; }
        [Column(Name = "VERSION")]
        public string? VERSION { get; set; }
        [Column(Name = "LOT_NO")]
        public string? LOT_NO { get; set; }
        [Column(Name = "PARTNO")]
        public string? PARTNO { get; set; }
        [Column(Name = "OPERATOR")]
        public string? OPERATOR { get; set; }
        [Column(Name = "ALARMCODE")]
        public string? ALARMCODE { get; set; }
        [Column(Name = "ALARMINFO")]
        public string? ALARMINFO { get; set; }
        [Column(Name = "ALARM_START_TIME")]
        public string? ALARM_START_TIME { get; set; }
        [Column(Name = "ALARM_STOP_TIME")]
        public string? ALARM_STOP_TIME { get; set; }
        [Column(Name = "FACTORYCODE")]
        public string? FACTORYCODE { get; set; }
        [Column(Name = "UPLOADTIME")]
        public string? UPLOADTIME { get; set; }
    }
}
