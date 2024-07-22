using GalaSoft.MvvmLight;
using System; 
using System.Collections.ObjectModel; 

namespace DLGCY.FilesWatcher.ViewModels
{
    public class WindowConfig
    {
        private static WindowConfig _instance;
        public string SupervisMode { get; set; }

        public static WindowConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WindowConfig();
                }
                return _instance;
            }
        }

        private WindowConfig()
        {
            // 初始化属性
            SupervisMode = ""; // 设置默认值，如果需要的话
        }
    }
    public class VMTempTest : ViewModelBase
    {
        public VMTempTest(WPFTemplate.ConfigItems _configItems)
        {
            CombboxList = new ObservableCollection<ComplexInfoModel>() {
               new ComplexInfoModel(){ Key="1",Text="文件解析模式A" },
               new ComplexInfoModel(){ Key="2",Text="文件解析模式B" },
               new ComplexInfoModel(){ Key="3",Text="文件解析模式C" },
               new ComplexInfoModel(){ Key="4",Text="文件解析模式D" },
               new ComplexInfoModel(){ Key="5",Text="文件解析模式E" },

            };
            for (int i = 0; i < CombboxList.Count; i++)
            {
                if (CombboxList[i].Text == _configItems.SupervisMode)
                {
                    combboxItem = CombboxList[i];//初始化下拉框选中项显示
                    break;
                }
                else
                {
                    combboxItem = null;//初始化下拉框选中项显示
                }   
            }
         }


        private ComplexInfoModel combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }

        private ObservableCollection<ComplexInfoModel> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public ObservableCollection<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(() => CombboxList); }
        }
    }

    public class ComplexInfoModel : ObservableObject
    {
        private String key;
        /// <summary>
        /// Key值
        /// </summary>
        public String Key
        {
            get { return key; }
            set { key = value; RaisePropertyChanged(() => Key); }
        }

        private String text;
        /// <summary>
        /// Text值
        /// </summary>
        public String Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }
    }
}
