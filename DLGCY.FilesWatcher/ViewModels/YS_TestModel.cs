using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    [Table(Name = "dbo.YS_TestModel")]
    public class YS_TestModel
    {
        [Column(Name = "CREATEHOST")]
        public string? CREATEHOST { get; set; }
        [Column(Name = "CREATEUSER")]
        public string? CREATEUSER { get; set; }
        [Column(Name = "CREATETIME")]
        public string CREATETIME { get; set; }
        [Column(Name = "GUID")]
        public string GUID { get; set; }
        [Column(Name = "PRODNO")]
        public string? PRODNO { get; set; }
        [Column(Name = "WMINFO")]
        public string WMINFO { get; set; }
        [Column(Name = "TESTDATE")]
        public string TESTDATE { get; set; }
        //[Column(Name = "TESTIME")]
        //public string TESTIME { get; set; }
        [Column(Name = "MACHINENO")]
        public string? MACHINENO { get; set; }
        [Column(Name = "RESULT")]
        public string RESULT { get; set; }

    }
}
