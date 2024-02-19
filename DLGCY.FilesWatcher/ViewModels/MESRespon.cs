using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    public class MESRespon
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public object Context { get; set; }

    }
}

