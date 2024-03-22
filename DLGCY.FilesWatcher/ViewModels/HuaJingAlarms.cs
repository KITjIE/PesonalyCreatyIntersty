using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.ViewModels
{
    public class HuaJingAlarms
    {
        public string Module { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }

        // 添加一个构造函数，用于创建带有表头的 ModuleInfo 对象
        public HuaJingAlarms(string module, string address, string account)
        {
            Module = module;
            Address = address;
            Account = account;
        }
    }
}
