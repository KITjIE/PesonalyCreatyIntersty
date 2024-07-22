using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    public class YSXNY_KTHuiLiuHan
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string Model { get; set; }

        public string Message { get; set; }
        public override string ToString()
        {
            return $"Date: {Date}, User: {User}, Model: {Model}, Message: {Message}";
        }
    }
}
