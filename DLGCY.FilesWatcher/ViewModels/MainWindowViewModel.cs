using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http; 
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media; 
using DotNet.Utilities.ConsoleHelper;
using FreeSql;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PropertyChanged;
using TemplateClassLibrary;
using WPFTemplate;
using WPFTemplate.ViewModels;
using WPFTemplateLib.UserControls;
using WPFTemplateLib.WpfHelpers;
using static DLGCY.FilesWatcher.Helper.ApiClient;
using File = System.IO.File;
using Path = System.IO.Path;


namespace DLGCY.FilesWatcher.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel : BindableBase
    {
        #region Bindable
        public string MESInfo { get; set; }
        private static IFreeSql fsql;
        private ConfigItems _configs;
        public VMTempTest vMTempTest { get; set; }
        public ConfigItems Configs
        {
            get
            {
                if (_configs == null)
                {
                    LoadConfigCommand.Execute(false);
                }

                return _configs;
            }

            set => _configs = value;
        }

        static long lastPosition = 0;
        static string directoryPath = string.Empty;
        static string currentFilePath;


        private Dictionary<string, long> _fileReadPositions = new Dictionary<string, long>();
        private string _Info = "";
        /// <summary>
        /// 信息窗内容;
        /// </summary>
        public string Info
        {
            get => _Info;

            set
            {
                //保证 选择不自动滚动消息 的效果（通过不通知界面实现）,不这样的话新消息来的时候，光标/滚动条 会跳到最上面.

                if (Configs.IsAutoScroll)
                {
                    SetProperty(ref _Info, value);
                }
                else
                {
                    _Info = value;
                }
            }
        }
        /// <summary>
        /// 底部状态栏信息
        /// </summary>
        public string Status { get; set; } = "程序版本：" + DotNet.Utilities.AssemblyHelper.AssemblyFileVersion;

        /// <summary>
        /// 弹窗VM;
        /// </summary>
        public ConfirmBoxViewModel DialogVm { get; set; } = new ConfirmBoxViewModel();

        /// <summary>
        /// 是否正在监控
        /// </summary>
        public bool IsMonitoring { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            //FileName_HansAnalys();
            Console.SetOut(new ConsoleWriter(ShowInfo));
            SetCommandMethod();
            IsMonitoring = true;
            MonitorDirectory(Configs.FolderPath, Configs.IsMonitorSubDir);
            ShowInfo(@"
            说明：
            1、文件更改可能会产生两条信息；
            2、子文件夹内出现创建删除会产生该文件夹的更改消息；");
            Configs.EditMode = false;
            Configs.AnalysCount = 0;
            //AddFreeSql();
            //fsql.CodeFirst.SyncStructure(typeof(YS_TestModel));
            vMTempTest = new VMTempTest(Configs);

        }

        ~MainWindowViewModel()
        {
            _FileSystemWatcher.EnableRaisingEvents = false;
        }

        #region Command

        /// <summary>
        /// 保存配置命令
        /// </summary>
        public ICommand SaveConfigCommand { get; set; }
        /// <summary>
        /// 修改配置命令
        /// </summary>
        public ICommand ChangeConfigCommand { get; set; }

        /// <summary>
        /// 载入配置命令
        /// </summary>
        public ICommand LoadConfigCommand { get; set; }

        /// <summary>
        /// 清空消息命令
        /// </summary>
        public ICommand ClearInfoCommand { get; set; }

        /// <summary>
        /// 关于弹窗命令
        /// </summary>
        public ICommand AboutCommand { get; set; }
        /// <summary>
        /// 解析模式弹窗命令
        /// </summary>
        public ICommand AnalysCommand { get; set; }
        /// <summary>
        /// 弹窗演示命令
        /// </summary>
        public ICommand DialogCommand { get; set; }

        /// <summary>
        /// 双列信息弹窗演示命令
        /// </summary>
        public ICommand TwoColumnInfoCommand { get; set; }

        /// <summary>
        /// 单列信息弹窗演示命令
        /// </summary>
        public ICommand OneColumnInfoCommand { get; set; }

        /// <summary>
        /// 输入信息弹窗演示命令
        /// </summary>
        public ICommand InputInfoCommand { get; set; }

        /// <summary>
        /// 等待弹框演示命令
        /// </summary>
        public ICommand WaitCommand { get; set; }

        /// <summary>
        /// 选择文件夹命令
        /// </summary>
        public ICommand ChooseFolderCommand { get; set; }

        /// <summary>
        /// 开始监控命令
        /// </summary>
        public ICommand StartWatchCommand { get; set; }

        /// <summary>
        /// 停止监控命令
        /// </summary>
        public ICommand StopWatchCommand { get; set; }

        /// <summary>
        /// 手动上传命令
        /// </summary>
        public ICommand HandWatchCommand { get; set; }

        /// <summary>
        /// 人工扫码上传窗口
        /// </summary>
        public ICommand ScanCodeUploadCommand { get; set; }
        /// <summary>
        /// 人工扫码上传命令
        /// </summary>
        public ICommand UpLoadCommand { get; set; }
        /// <summary>
        /// 人工扫码取消命令
        /// </summary>
        public ICommand CancelCommand { get; set; }
        /// <summary>
        /// 导入配置命令
        /// </summary>
        public ICommand InputConfigCommand { get; set; }
        /// <summary>
        /// 导出配置命令
        /// </summary>
        public ICommand ExportConfigCommand { get; set; }
        public string DirectoryPath { get => directoryPath; set => directoryPath = value; }
        #endregion


        public async Task DoWorkWithParam(object message)
        {
            string barCode = message.ToString();
            string testtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string result = "PASS";
            string path = "HandUpLoad";
            // 上传过站接口
            bool apiResponse2 = Post_PastationAPI(barCode, result, testtime, path);
            if (!apiResponse2)
            {
                //ShowErrorWin();
                // 在 UI 线程上调度创建和显示 FullScreenPopupWindow
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    // 创建 FullScreenPopupWindow 实例
                    MsgWindow msgWindow = new MsgWindow(Configs);
                    // 显示窗口
                    msgWindow.ShowDialog();
                });
            }
            await Task.Delay(20);
        }
        /// <summary>
        /// 命令方法赋值(在构造函数中调用)
        /// </summary>
        private void SetCommandMethod()
        {
            SaveConfigCommand ??= new RelayCommand(o => true, async o =>
            {
                Configs.SupervisMode = vMTempTest.CombboxItem.Text;
                if (vMTempTest.CombboxItem.Text == null)
                {
                    await ConfirmBoxHelper.ShowMessage(DialogVm, $"未选择“解析模式”");
                    return;
                }
                if (ConfigManager.SaveConfig(Configs))
                {
                    await ConfirmBoxHelper.ShowMessage(DialogVm, "保存配置成功");
                    Configs.EditMode = false;
                }
                else
                {
                    await ConfirmBoxHelper.ShowMessage(DialogVm, "保存配置失败");
                }
            });

            LoadConfigCommand ??= new RelayCommand(o => true, async o =>
            {
                if (ConfigManager.LoadConfig<ConfigItems>(ref _configs))
                {
                    if (o as bool? != false)
                    {
                        await ConfirmBoxHelper.ShowMessage(DialogVm, "载入配置成功");
                    }
                }
                else
                {
                    await ConfirmBoxHelper.ShowMessage(DialogVm, "载入配置失败");
                }
            });

            ChangeConfigCommand ??= new RelayCommand(o => true, async o =>
            {
                var inputVM = new InputModel()
                {
                    IsOnlyOneColumn = true,
                    TitleLeft = "请输入密码",
                    IsShowBottom = false,
                };

                inputVM.LeftContent = new StackPanel
                {
                    Children =
                {
                    GetControl.GetLineInput("请输入：", nameof(inputVM.Text)),
                },
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                };

                //DialogVm.ConfirmDialogTimeOut = 600;
                //DialogVm.CustomContentHorizontalAlignment = HorizontalAlignment.Stretch;
                DialogVm.CustomContent = new UC_CustomInfo()
                {
                    DataContext = inputVM,
                };

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", () =>
                {
                    if (inputVM.Text == "abc+123")
                    {
                        Configs.EditMode = true;
                    }
                }, msg => { Console.WriteLine(msg); }, isShowText: false);
            });
            InputConfigCommand ??= new RelayCommand(o => true, o => { InputConfig(); });
            ExportConfigCommand ??= new RelayCommand(o => true, o => { ExportConfig(); });

            ClearInfoCommand ??= new RelayCommand(o => true, o =>
            {
                Info = "";
                Configs.AnalysCount = 0;
            });

            AboutCommand ??= new RelayCommand(o => true, o =>
            {
                new AboutWindow().ShowDialog();
            });
            AnalysCommand ??= new RelayCommand(o => true, o =>
            {
                new AnalysWindow().ShowDialog();
            });
            DialogCommand ??= new RelayCommand(o => true, async o =>
            {
                await ConfirmBoxHelper.ShowMessage(DialogVm, "操作前通知", 6);

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "您确定要进行此操作吗？", async () =>
                {
                    #region 业务方法

                    ShowInfo("开始业务操作...");

                    await Task.Delay(1000 * 3);

                    ShowInfo("完成业务操作.");

                    #endregion

                }, ShowInfo);

                await ConfirmBoxHelper.ShowMessage(DialogVm, "操作后通知");
            });
            #region 暂时不用
            TwoColumnInfoCommand ??= new RelayCommand(o => true, async o =>
            {
                int leftNameWidth = 50;
                int rightNameWidth = 120;

                DialogVm.CustomContent = new UC_CustomInfo()
                {
                    DataContext = new CustomInfoViewModel()
                    {
                        TitleLeft = "个人信息",
                        TitleRight = "信用额度",
                        TextBottom = "确认继续操作吗？",

                        LeftContent = new StackPanel
                        {
                            Children =
                            {
                                GetControl.GetLineInfo("姓名:", "无名氏", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("卡号:", "A001", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("业务:", "消费", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("借款:", "$100", Colors.Red, leftNameWidth)
                            }
                        },

                        RightContent = new StackPanel
                        {
                            Children =
                            {
                                GetControl.GetLineInfo("总信用额度:", "$1000", Colors.Red, rightNameWidth),
                                GetControl.GetLineInfo("已借款金额:", "$100", Colors.Red, rightNameWidth),
                                GetControl.GetLineInfo("剩余额度:", "$900", Colors.Red, rightNameWidth),
                            }
                        },
                    }
                };

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", async () =>
                {

                }, ShowInfo, isShowText: false);
            });

            OneColumnInfoCommand ??= new RelayCommand(o => true, async o =>
            {
                int leftNameWidth = 50;

                DialogVm.CustomContent = new UC_CustomInfo()
                {
                    DataContext = new CustomInfoViewModel()
                    {
                        IsOnlyOneColumn = true,
                        TitleLeft = "标题",
                        //IsShowTitle = false,
                        //IsShowBottom = false,

                        LeftContent = new StackPanel
                        {
                            Children =
                            {
                                GetControl.GetLineInfo("姓名:", "无名氏", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("卡号:", "A001", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("业务:", "消费", Colors.Red, leftNameWidth),
                                GetControl.GetLineInfo("借款:", "$100", Colors.Red, leftNameWidth)
                            }
                        },
                    }
                };

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", async () =>
                {
                    Console.WriteLine("用户已确认");
                }, ShowInfo, isShowText: false);
            });

            InputInfoCommand ??= new RelayCommand(o => true, async o =>
            {
                var inputVM = new TestInputViewModel()
                {
                    IsOnlyOneColumn = true,
                    TitleLeft = "修改后请点击确定按钮",
                    //IsShowTitle = false,
                    IsShowBottom = false,
                };

                inputVM.LeftContent = new StackPanel
                {
                    Children =
                    {
                        GetControl.GetLineInput("宽度：", nameof(inputVM.InputHeight)),
                        GetControl.GetLineInput("高度：", nameof(inputVM.InputWidth)),
                        GetControl.GetLineInput("编号：", nameof(inputVM.InputNo)),
                    },
                };

                DialogVm.CustomContent = new UC_CustomInfo()
                {
                    DataContext = inputVM,
                };

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", () =>
                {
                    Console.WriteLine($"宽度-{inputVM.InputWidth},高度-{inputVM.InputHeight},编号-{inputVM.InputNo}");
                }, ShowInfo, isShowText: false);
            });

            WaitCommand ??= new RelayCommand(o => true, o =>
            {


                //try
                //{
                //    // 要写入的文件路径
                //    string filePath = @"C:\test\结果.xls";
                //    // 如果文件不存在，创建新文件并添加文件头
                //    if (!File.Exists(filePath))
                //    {
                //        InitializeExcel(filePath);
                //    }
                //    List<HuaJingAlarms> alarms = new List<HuaJingAlarms>
                //{
                //    new HuaJingAlarms(DateTime.Now.ToString("yyyy/MM/dd"),DateTime.Now.ToString("HH:mm:ss"),"低液位"),
                //    new HuaJingAlarms(DateTime.Now.ToString("yyyy/MM/dd"),DateTime.Now.ToString("HH:mm:ss"),"报警文本示例2"),
                //    new HuaJingAlarms(DateTime.Now.ToString("yyyy/MM/dd"),DateTime.Now.ToString("HH:mm:ss"),"天车碰撞")

                //};
                //    // 打开文件流
                //    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                //    {
                //        WriteToExcelFile(fileStream, alarms);
                //    }
                //}
                //catch (Exception)
                //{

                //}
                try
                {
                    // 要写入的文件路径
                    string filePath = currentFilePath;
                    // 如果文件不存在，创建新文件并添加文件头
                    if (!File.Exists(filePath))
                    {
                        InitializeExcel(filePath);
                    }
                    List<YS_DianCe_Model> existingRecords = new List<YS_DianCe_Model>();
                    var record = new YS_DianCe_Model
                    {
                        Date = DateTime.Now, // 解析日期
                        DeviceName = "values[1]", // 设备名称
                        DeviceNumber = "values[1]", // 设备编号
                        ProductNumber = "values[1]", // 产品号
                        BatchNumber = "values[1]", // 批次号
                        UnitOrder = 123, // 单元排序
                        Barcode = "values[1]", // 条码
                        Result = "values[1]", // 结果
                        GoodUnitCount = 123, // 良品单元数
                        BadUnitCount = 123, // 不良品单元数
                        BadUnitInfo = "1" // 不良品信息
                    };
                    existingRecords.Add(record);
                    try
                    {
                        // 使用StreamWriter以追加方式打开文件，使用GBK编码
                        using (var writer = new StreamWriter(filePath, true, Encoding.UTF8))//Encoding.GetEncoding("GBK")
                        {
                            // 写入新数据到文件末尾
                            string lineToAdd = $"{record.Date},{record.DeviceName},{record.DeviceNumber},{record.ProductNumber},{record.BatchNumber},{record.UnitOrder},{record.Barcode},{record.Result},{record.GoodUnitCount},{record.BadUnitCount},{record.BadUnitInfo}";
                            writer.WriteLine(lineToAdd);
                        }
                        Console.WriteLine("数据成功追加到CSV文件末尾。");
                    }
                    catch (Exception) { }
                }
                catch (Exception)
                {

                }
            });
            #endregion
            ChooseFolderCommand ??= new RelayCommand(o => true, o =>
            {
                System.Windows.Forms.FolderBrowserDialog chooseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
                if (chooseFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    Configs.FolderPath = chooseFolderDialog.SelectedPath;
                }
            });
            HandWatchCommand ??= new RelayCommand(o => true, o =>
            {
                if (Configs.SupervisMode == "文件解析模式A")
                {
                    OpenFileDialog_Handle();
                }
                if (Configs.SupervisMode == "文件解析模式B" || Configs.SupervisMode == "文件解析模式C")
                {
                    FileTXTName_HansAnalys();
                }
                if (Configs.SupervisMode == "文件解析模式D")
                {
                }
            });
            StartWatchCommand ??= new RelayCommand(o => !string.IsNullOrWhiteSpace(Configs.FolderPath), o =>
            {
                IsMonitoring = true;
                MonitorDirectory(Configs.FolderPath, Configs.IsMonitorSubDir);
            });

            StopWatchCommand ??= new RelayCommand(o => true, async o =>
            {
                _FileSystemWatcher.EnableRaisingEvents = false;
                IsMonitoring = false;
                await ConfirmBoxHelper.ShowMessage(DialogVm, $"已停止监控：[{Configs.FolderPath}]");
            });

            ScanCodeUploadCommand ??= new RelayCommand(o => true, async o =>
            {
                new HandUpLoadWindow().ShowDialog();
            });

            UpLoadCommand ??= new RelayCommand(o => true, async o =>
            {
                if (Configs.HandBarCode == null || Configs.HandBarCode == "")
                {
                    Configs.tips = "输入条码不能为空！！";
                    return;
                }
                else
                {
                    await Task.Run(() => DoWorkWithParam(Configs.HandBarCode));
                }

            });
            CancelCommand ??= new RelayCommand(o => true, async o =>
            {

                Configs.HandBarCode = null;
            });
        }

        #region 辅助方法

        public void ShowInfo(string info)
        {
            if (Info.Length > Configs.AutoHalveThresholdValue && Configs.IsAutoHalve)
            {
                Info = "(已删除一半信息)\r\n" + Info.Remove(0, Configs.AutoHalveThresholdValue / 2);
            }

            Info += $"[{DateTime.Now:HH:mm:ss.ffff}] {info}\r\n\r\n";
        }

        #endregion

        #region 文件夹监控

        private FileSystemWatcher _FileSystemWatcher = new FileSystemWatcher();
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        OpenFileDialog openFileDialog_B = new OpenFileDialog();

        //参考：https://www.infoworld.com/article/3185447/how-to-work-with-filesystemwatcher-in-c.html

        /// <summary>
        /// 开始监控目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="isIncludeSubDir">是否包括子目录</param>
        public async void MonitorDirectory(string path, bool isIncludeSubDir = true)
        {
            if (!Directory.Exists(path)) { return; }
            _FileSystemWatcher.EnableRaisingEvents = false;
            _FileSystemWatcher = new FileSystemWatcher();
            _FileSystemWatcher.Path = path;
            _FileSystemWatcher.IncludeSubdirectories = isIncludeSubDir;

            _FileSystemWatcher.Created += FileSystemWatcher_Created;
            _FileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _FileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _FileSystemWatcher.Changed += FileSystemWatcher_Changed;
            //开始监控
            _FileSystemWatcher.EnableRaisingEvents = true;
            await ConfirmBoxHelper.ShowMessage(DialogVm, $"已开启监控：[{Configs.FolderPath}]");
        }
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (Configs.SupervisMode == "文件解析模式D")
            {
                Thread.Sleep(1000); // 延时10秒钟
                string fileType = string.Empty;
                if (GetPath(e).Contains("ProdData"))
                {
                    fileType = "HWDC";
                }
                else if (GetPath(e).Contains("log") && GetPath(e).Contains("-"))
                {
                    fileType = "XNYKT";
                }
                switch (fileType)
                {
                    case "HWDC":
                        if (IsNewFileForToday(e.FullPath, false))
                        {
                            currentFilePath = e.FullPath;
                            //lastPosition = 0; // 重置读取位置
                            Task.Run(() => YS_DianCEReadNewLines(currentFilePath));
                        }
                        return;
                    case "XNYKT":
                        if (IsNewFileForToday(e.FullPath, true))
                        {
                            currentFilePath = e.FullPath;
                            //lastPosition = 0; // 重置读取位置
                            Task.Run(() => XNY_KTReadNewLines(currentFilePath));
                        }
                        return;
                    default:
                        break;
                }
            }
        }

        private async void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            bool skipFinally = false;
            if (e.Name == "NetworkConnect.txt") { return; }
            try
            {

                if (Configs.SupervisMode == "文件解析模式A")
                {
                    if (e.Name.Contains(".txt"))
                    {
                        if (!FileTXT_Analys(GetPath(e)))
                        {
                            //ShowErrorWin();
                        };
                    }
                }
                try
                {
                    #region excel解析
                    if (Configs.SupervisMode == "文件解析模式C")
                    {
                        await Task.Delay(10000); // 延时10秒钟
                        Console.WriteLine($"【{GetPathType(e.FullPath)}更改】{GetPath(e)}");
                        if (!e.Name.StartsWith("~$"))
                        {
                            if (e.Name.Contains(".xls") || e.FullPath.Contains(".csv"))
                            {
                                FileCSV_Analys(GetPath(e));
                            }
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {

                }
                if (Configs.SupervisMode == "文件解析模式D")
                {
                    skipFinally = true;
                    Thread.Sleep(5000); // 延时5秒钟
                    string fileType = string.Empty;
                    if (GetPath(e).Contains("ProdData"))
                    {
                        fileType = "HWDC";
                    }
                    else if (e.FullPath.Contains("log") && e.FullPath.Contains('-'))
                    {
                        fileType = "XNYKT";
                    }
                    else
                    {
                        Console.WriteLine("检测到有文件新建" + GetPath(e));
                    }
                    switch (fileType)
                    {
                        case "HWDC":
                            if (IsNewFileForToday(e.FullPath, false))
                            {
                                currentFilePath = e.FullPath;
                                lastPosition = 0; // 重置读取位置
                                await Task.Run(() => YS_DianCEReadNewLines(currentFilePath));
                            }
                            return;
                        case "XNYKT":
                            if (IsNewFileForToday(e.FullPath, true))
                            {
                                currentFilePath = e.FullPath;
                                lastPosition = 0; // 重置读取位置
                                await Task.Run(() => KTHuiLiuHanAlarm(e.FullPath));
                            }
                            var newWarningLines = KTHuiLiuHanAlarm(e.FullPath);
                            // 输出结果
                            foreach (var warningLine in newWarningLines)
                            {
                                Console.WriteLine(warningLine);
                            }
                            return;
                        default:
                            break;
                    }

                }
                if (Configs.SupervisMode == "文件解析模式E")
                {
                    if (e.FullPath.Contains(".xlsx") || e.FullPath.Contains(".xls") || e.FullPath.Contains(".csv"))
                    {
                        await Task.Delay(500); // 等待文件完全写入
                        List<YS_WaiXieZiLiao> records = YS_WaiXieExcelFile((GetPath(e)), Configs.Computer, Configs.BuildUser);
                        // 输出解析结果
                        foreach (var record in records)
                        {
                            await Task.Run(() => Post_PastationAPI(record.Code, record.Result, record.Date, record.Machine));
                            // Console.WriteLine($"时间: {record.Date}, 条码: {record.Code}, 结果: {record.Result}, 设备:{record.Machine},用户{record.User}");
                        }
                    }
                }
                if (Configs.SupervisMode == "文件解析模式B")
                {
                    string fileSuffixName = GetPath(e).Substring(GetPath(e).LastIndexOf('.') + 1);//获取文件的后缀名
                    switch (fileSuffixName)
                    {
                        case "csv":
                            if ((!FileCSVName_Analys(System.IO.Path.GetFileNameWithoutExtension(GetPath(e)), GetPath(e))) && Configs.IsMESerrorWin)
                            {
                                ShowErrorWin();
                            };
                            return;
                        case "txt":
                            if ((!FileTXTName_Analys(System.IO.Path.GetFileNameWithoutExtension(GetPath(e)), GetPath(e))) && Configs.IsMESerrorWin)
                            {
                                ShowErrorWin();
                            }
                            return;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"报错【{ex.Message}");
            }
            finally
            {
                if (!skipFinally)
                {
                    // 强制释放文件资源
                    TryForceFileRelease(GetPath(e));
                    // 获取源文件夹中的所有文件
                    FlieMove(GetPath(e), GetPath(e), Configs.FinalPath);
                }
            }
        }

        private void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"【{GetPathType(e.FullPath)}重命名】{GetOldPath((RenamedEventArgs)e)} --> {GetPath(e)}");
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"【删除】{GetPath(e)}");
        }

        //private bool IsNewFileForToday(string filePath)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(filePath);
        //    var today = DateTime.Now.ToString("yyyyMMdd");
        //    return fileName.Contains(today);
        //}
        //private bool IsNewFileForToday_(string filePath)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(filePath).Replace("-",null);
        //    var today = DateTime.Now.ToString("yyyyMdd");
        //    return fileName.Contains(today);
        //}
        private bool IsNewFileForToday(string filePath, bool removeDashes)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            if (removeDashes)
            {
                fileName = fileName.Replace("-", string.Empty);
            }

            var today = DateTime.Now.ToString("yyyyMMdd");
            return fileName.Contains(today);
        }


        #region 导入/导出配置
        /// <summary>
        /// 导入配置
        /// </summary>
        private void InputConfig()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Title = "Select a TXT file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                List<string> contents = new List<string>();
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            contents.Add(line);

                        }

                        if (contents.Count > 0)
                            Configs.Computer = contents[0];
                        if (contents.Count > 1)
                            Configs.BuildUser = contents[1];
                        if (contents.Count > 2)
                            Configs.PorductModel = contents[2];
                        if (contents.Count > 3)
                            Configs.MachineModel = contents[3];
                    }
                    if (ConfigManager.SaveConfig(Configs))
                    {
                        Console.WriteLine(filePath + "【配置导入成功】");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("配置导入失败" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 导出配置
        /// </summary>
        private void ExportConfig()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.Title = "Save a TXT file";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                List<string> contents = new List<string>
                {
                     Configs.Computer,
                     Configs.BuildUser,
                     Configs.PorductModel,
                     Configs.MachineModel
                };
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (string item in contents)
                        {
                            writer.WriteLine(item);
                        }
                        Console.WriteLine(filePath + "【配置导出成功】");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("配置导出异常" + ex.Message);
                }
            }
        }
        #endregion


        /// <summary>
        /// 元盛外协资料解析
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static List<YS_WaiXieZiLiao> YS_WaiXieExcelFile(string filePath, string machine, string usr)
        {
            List<YS_WaiXieZiLiao> records = new List<YS_WaiXieZiLiao>();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook workbook = new XSSFWorkbook(fileStream);
                ISheet sheet = workbook.GetSheetAt(0);
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    IRow currentRow = sheet.GetRow(row);
                    if (currentRow == null) continue; // 跳过空行

                    // 创建记录
                    string date = currentRow.Cells.Count > 0 ? GetCellStringValue(currentRow.GetCell(0)) : string.Empty;
                    string code = currentRow.Cells.Count > 1 ? currentRow.Cells[1].ToString() : string.Empty;
                    string result = currentRow.Cells.Count > 2 ? currentRow.Cells[2].ToString() : string.Empty;

                    // 检查所有字段是否都有值
                    if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(result))
                    {
                        YS_WaiXieZiLiao record = new YS_WaiXieZiLiao
                        {
                            Date = date,
                            Code = code,
                            Result = result,
                            Machine = machine,
                            User = usr
                        };
                        records.Add(record);
                    }

                }
                return records;
            }

        }

        private static string GetCellStringValue(ICell cell)
        {
            if (cell == null) return string.Empty;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss"); // 格式化日期
                    }
                    return cell.NumericCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    return cell.CellFormula;
                case CellType.Blank:
                    return string.Empty;
                default:
                    return cell.ToString();
            }
        }
        // 读取文件最新行内容的方法
        private void YS_DianCEReadNewLines(string filePath)
        {
            var newRecords = YS_DianCeReadCsvFile(filePath, lastPosition);

            if (newRecords.Count > 0)
            {
                foreach (var record in newRecords)
                {
                    ProcessRecord(record);
                }
            }

            lastPosition = new FileInfo(filePath).Length; // 更新最后读取位置
        }
        private void XNY_KTReadNewLines(string filePath)
        {
            Console.WriteLine("上次读取记录" + lastPosition);
            var newRecords = KTHuiLiuHanAlarm(filePath, lastPosition);
            if (newRecords.Count > 0)
            {
                foreach (var record in newRecords)
                {
                    KT_ProcessRecord(record);
                }
            }
            lastPosition = new FileInfo(filePath).Length; // 更新最后读取位置
            Console.WriteLine("最后读取记录" + lastPosition);
        }
        private void ProcessRecord(YS_DianCe_Model record)
        {
            // 在这里实现处理每条记录的逻辑
            Console.WriteLine($"新记录: {record.Date}, {record.DeviceName}, {record.DeviceNumber}, {record.ProductNumber}, {record.BatchNumber}, {record.UnitOrder}, {record.Barcode}, {record.Result}, {record.GoodUnitCount}, {record.BadUnitCount}, {record.BadUnitInfo}");
        }
        private void KT_ProcessRecord(YSXNY_KTHuiLiuHan record)
        {
            // 在这里实现处理每条记录的逻辑
            Console.WriteLine($"新记录: {record.Date}, {record.User}, {record.Model}, {record.Message}");
        }
        /// <summary>
        /// 获取变动的路径的显示字符串
        /// </summary>
        private string GetPath(FileSystemEventArgs e)
        {
            if (Configs.IsShowFullPath)
            {
                return e.FullPath;
            }
            return e.Name;
        }

        /// 获取原先路径的显示字符串
        /// </summary>
        private string GetOldPath(RenamedEventArgs e)
        {
            if (Configs.IsShowFullPath)
            {
                return e.OldFullPath;
            }
            return e.OldName;
        }

        #endregion
        private void TryForceFileRelease(string filePath)
        {
            try
            {
                // 尝试打开文件流以强制释放资源
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // Do nothing, just try to open the file
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to force file release: {ex.Message}");
            }
        }


        #region 判断是文件还是文件夹

        /// <summary>
        /// 获取路径类型（判断是文件还是文件夹）
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>PathTypeEnum</returns>
        public static PathTypeEnum GetPathType(string path)
        {
            if (File.Exists(path))
            {
                return PathTypeEnum.文件;
            }
            else if (Directory.Exists(path))
            {
                return PathTypeEnum.文件夹;
            }
            else
            {
                return PathTypeEnum.不存在;
            }
        }

        /// <summary>
        /// 路径类型枚举
        /// </summary>
        public enum PathTypeEnum
        {
            文件, 文件夹, 不存在
        }

        #endregion 
        public void FlieMove(string files, string sourceFilePath, string destFilePath)
        {
            try
            {
                if (!Directory.Exists(destFilePath))
                {
                    // 如果不存在，则创建文件夹路径
                    Directory.CreateDirectory(destFilePath);
                }
                if (!File.Exists(sourceFilePath))
                {
                    return;
                }
                string destinationFile = System.IO.Path.Combine(destFilePath, Path.GetFileName(files));
                if (File.Exists(destinationFile))
                {
                    // 移除只读属性（否则无法删除）
                    File.SetAttributes(destinationFile, FileAttributes.Normal);
                    File.Delete(destinationFile);
                }
                File.Move(sourceFilePath, destinationFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"【文件移动错误】: {ex.Message}");
            }
        }
        private void OpenFileDialog_Handle()
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            _openFileDialog.Multiselect = true;
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in _openFileDialog.FileNames)
                {
                    try
                    {
                        if ((!FileTXT_Analys(filePath)))
                        {
                            //ShowErrorWin();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"解析文件内容错误！{ex.Message}");
                    }
                    finally
                    {
                        // 强制释放文件资源
                        TryForceFileRelease(filePath);
                        // 移动文件
                        FlieMove(filePath, filePath, Configs.FinalPath);
                    }
                }
            }
        }

        public void FileTXTName_HansAnalys()
        {
            try
            {
                openFileDialog_B = new OpenFileDialog();
                openFileDialog_B.Filter = "Text Files (*.txt)|*.*|All Files (*.*)|*.*";
                openFileDialog_B.Multiselect = true;
                if (openFileDialog_B.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog_B.FileNames)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string fileSuffixName = filePath.Substring(filePath.LastIndexOf('.') + 1);//获取文件的后缀名
                        if (fileSuffixName == "csv" && Configs.SupervisMode == "文件解析模式B")
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                if ((!FileCSVName_Analys(fileName, filePath)) && Configs.IsMESerrorWin)
                                {
                                    ShowErrorWin();
                                }
                            }
                        }
                        if (fileSuffixName == "txt" && Configs.SupervisMode == "文件解析模式B")
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                if ((!FileTXTName_Analys(fileName, filePath)) && Configs.IsMESerrorWin)
                                {
                                    ShowErrorWin();
                                }
                            }
                        }
                        if (fileSuffixName == "csv" && Configs.SupervisMode == "文件解析模式C")
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                //if (!FileTXTName_FrontFile(fileName, filePath)&& Configs.IsMESerrorWin)
                                //{
                                //    ShowErrorWin();
                                //}
                            }
                        }

                        // 强制释放文件资源
                        TryForceFileRelease(filePath);
                        // 获取源文件夹中的所有文件
                        FlieMove(filePath, filePath, Configs.FinalPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"手动解析错误！{ex.Message}");
            }
        }
        /// <summary>
        /// （模式B）文件格式：
        /// ① Y01111111111111B00_PASS_SYNA_20240326_165100_65.csv
        /// ② V216006A020155_五楼LCM自动2号_20240319_035050_Y00000000_PASS_F2.csv
        ///    V216006A020155_五楼LCM自动2号_20240320_032742_Y011111111100_FAIL_F2.csv
        /// ③ OK/HD125_GT9916T_No0_01167_30635_Y44-9733331A9D40405003897_20240408_134405_OK.csv
        ///    NG/HD125_GT9916T_No0_01167_30635_Y44-9733331A9D40405003897_20240408_134405_NG.csv
        /// ④ Y0000000_FAIL_0_1_3_38_36_30_31_27_32_2_37_SYNA_20240326_165100_66.csv
        /// ⑤ 20231205_002245_Y04736103C30000431B00_FAIL.csv
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FileCSVName_Analys(string fileName, string filePath)
        {
            string fileName_;
            // 条码特殊符号替换
            if (Configs.IsCodeReplace)
            {
                fileName_ = ReplaceDclWithAsterisk(fileName);
            }
            else
            {
                fileName_ = fileName;
            }
            Configs.ProductBarcode = "";
            Configs.MESErrorInfo = "";
            Configs.UploadResult = "";
            string result_ = null;
            string BarCode_ = null;
            string Timeresult = null;
            try
            {
                string[] parts = null;
                string[] result = null;
                if (fileName_.Length < 20)
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    Configs.MESErrorInfo = "文件名格式错误,解析失败！";
                    return true;
                }
                if (fileName_.Contains("BarCode"))
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    return true;
                }
                int underscoreCount = fileName_.Split('_').Length - 1;
                //①Y01111111111111B00_PASS_SYNA_20240326_165100_65.csv
                if (underscoreCount == 5 && !(fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    // 使用正则表达式匹配两个下划线之间的文本
                    parts = fileName_.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[0];
                    if (result[1].Contains("PASS"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    Timeresult = TimeChange(result[3], result[4]); // 将日期字符串和时间字符串转换为DateTime对象
                }
                //②V216006A020155_五楼LCM自动2号_20240319_035050_Y00000000_PASS_F2.csv
                if (underscoreCount == 6 && !(fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    parts = fileName_.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[4];
                    if (result[5].Contains("PASS"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    Timeresult = TimeChange(result[2], result[3]); // 将日期字符串和时间字符串转换为DateTime对象
                }
                //③OK/HD125_GT9916T_No0_01167_30635_Y44-9733331A9D40405003897_20240408_134405_OK.csv
                if (underscoreCount == 8 && (fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    parts = fileName_.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[5];
                    if (result[8].Contains("OK"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    Timeresult = TimeChange(result[6], result[7]); // 将日期字符串和时间字符串转换为DateTime对象
                }
                //④Y0000000_FAIL_0_1_3_38_36_30_31_27_32_2_37_SYNA_20240326_165100_66.csv
                if (underscoreCount == 16 && !(fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    parts = fileName_.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[0];
                    if (result[1].Contains("PASS"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    Timeresult = TimeChange(result[14], result[15]); // 将日期字符串和时间字符串转换为DateTime对象
                }
                //⑤20231205_002245_Y04736103C30000431B00_FAIL.csv
                if (underscoreCount == 3 && !(fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    parts = fileName_.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[2];
                    if (result[3].Contains("PASS"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    Timeresult = TimeChange(result[0], result[1]); // 将日期字符串和时间字符串转换为DateTime对象
                }
                // 检测时间是否为空
                Timeresult ??= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // 输出提取的信息
                if (result.Length >= 3)
                {

                    // 上传电子档案接口
                    bool apiResponse1 = Post_ElectricalRecord(BarCode_, result_, Timeresult, filePath);
                    // 上传电测过站接口
                    bool apiResponse2 = Post_ElectricalPastation(BarCode_, result_, Timeresult, filePath);
                    if (apiResponse1 && apiResponse2)
                    {
                        Configs.AnalysCount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("文件名解析错误！");
                    Configs.MESErrorInfo = "文件名解析错误！";
                    return false;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"解析模式B，解析错误！{ex.Message}");
                return true;
            }
        }

        /// <summary>
        /// （模式B）文件格式：
        /// ①__F0000000000000_1959292_2023-12-06_09-09-21_OK.txt
        /// ②D:\test\PASS\YYYYYYYYY25.txt | D:\test\FAIL\YYYYYYYYY25.txt
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileTXTName_Analys(string fileName, string filePath)
        {
            string fileName_;
            // 条码特殊符号替换
            if (Configs.IsCodeReplace)
            {
                fileName_ = ReplaceDclWithAsterisk(fileName);
            }
            else
            {
                fileName_ = fileName;
            }
            Configs.ProductBarcode = "";
            Configs.UploadResult = "";
            Configs.MESErrorInfo = "";
            string result_ = null;
            string BarCode_ = null;
            string Timeresult = null;
            try
            {
                string[] parts = null;
                string[] result = null;
                if (fileName.Length < 18)
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    Configs.MESErrorInfo = "文件名格式错误,解析失败！";
                    return true;
                }
                int underscoreCount = fileName_.Split('_').Length - 1;
                //①__F0000000000000_1959292_2023-12-06_09-09-21_OK.txt
                if (underscoreCount == 6 && (fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    // 使用正则表达式匹配两个下划线之间的文本
                    parts = fileName.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[0];
                    if (result[4].Contains("OK"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                    // 合并日期和时间字符串
                    string combinedDateTimeStr = $"{result[2]} {result[3]}";

                    // 将合并后的字符串转换为DateTime对象
                    DateTime combinedDateTime = DateTime.ParseExact(combinedDateTimeStr, "yyyy-MM-dd HH-mm-ss", null);

                    // 格式化输出
                    Timeresult = combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                //②D:\test\PASS\YYYYYYYYY25.txt | D:\test\FAIL\YYYYYYYYY25.txt
                if (underscoreCount == 0 && !(fileName_.Contains("OK") || fileName_.Contains("NG")))
                {
                    // 使用正则表达式匹配两个下划线之间的文本
                    parts = fileName.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[0];
                    if (filePath.Contains("PASS"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                }
                //③#1_20240718062746_Y048235147E0075879.txt
                if (underscoreCount == 2 && fileName.Contains('#'))
                {
                    // 使用正则表达式匹配两个下划线之间的文本
                    parts = fileName.Split('_');
                    result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    BarCode_ = result[2];
                    if (filePath.Contains("PassLog"))
                    {
                        result_ = "PASS";
                    }
                    else
                    {
                        result_ = "FAIL";
                    }
                }
                // 检测时间是否为空
                Timeresult ??= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // 输出提取的信息
                if (result.Length > 0)
                {
                    // 上传电子档案接口
                    bool apiResponse1 = Post_ElectricalRecord(BarCode_, result_, Timeresult, filePath);
                    // 上传电测过站接口
                    bool apiResponse2 = Post_ElectricalPastation(BarCode_, result_, Timeresult, filePath);
                    if (apiResponse1 && apiResponse2)
                    {
                        Configs.AnalysCount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("文件名解析错误！");
                    Configs.MESErrorInfo = "文件名解析错误！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"文件名解析模式，解析错误！{ex.Message}");
                Configs.MESErrorInfo = "文件名解析错误！";
                return true;
            }
        }
        /// <summary>
        /// 模式D）元盛新能源--文件格式 2024-07-03.txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<YSXNY_KTHuiLiuHan> KTHuiLiuHanAlarm(string filePath, long startPosition = 0)
        {
            List<YSXNY_KTHuiLiuHan> warningLines = new List<YSXNY_KTHuiLiuHan>();

            if (!File.Exists(filePath))
            {
                return warningLines; // 文件不存在时直接返回空列表
            }

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(lastPosition, SeekOrigin.Begin);
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var columns = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (columns.Length > 3 && columns[2] == "警告")
                        {
                            var _KTHuiLiuHan = new YSXNY_KTHuiLiuHan
                            {
                                Date = columns[0],
                                User = columns[1],
                                Model = columns[2],
                                Message = columns[3]
                            };
                            warningLines.Add(_KTHuiLiuHan);
                        }
                    }
                    lastPosition = fs.Position; // 更新 lastPosition 到文件流的位置
                }
            }

            return warningLines;
        }
        /// <summary>
        /// （模式A）文件格式 7700SII plus_20231219144327.txt（AOI）
        /// </summary>
        /// <param name="filePath"></param>
        public bool FileTXT_Analys(string filePath)
        {
            Thread.Sleep(10);
            try
            {
                if (filePath.Length < 20)
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    return false;
                }
                List<AOIData> aoiDataList = new List<AOIData>();
                // 用于存储当前整体的数据
                List<string> currentData = new List<string>();
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                // 遍历每一行
                foreach (string line in lines)
                {
                    // 如果遇到空行，则处理当前整体的数据并开始新的整体
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        ProcessCurrentData(aoiDataList, currentData);
                        currentData.Clear();  // 清空当前整体的数据，准备处理新的整体
                    }

                    else
                    {
                        // 将当前行添加到当前整体的数据中
                        currentData.Add(line);
                    }
                }
                // 处理文件末尾可能存在的最后一个整体
                ProcessCurrentData(aoiDataList, currentData);
                //SKIP的意思是报废板，不上传
                aoiDataList.RemoveAll(item => item.PCBStatus.Contains("SKIP"));
                aoiDataList.RemoveAll(item => item.SubBoardBarCode.Contains(':'));
                // 显示解析的数据
                foreach (var entry in aoiDataList)
                {
                    if (Configs.IsMatchAOIModel && entry.EQPTypeName != Configs.PorductModel)
                    {
                        Console.WriteLine("AOI产品型号不是目标型号，无需解析！");
                        return false;
                    }
                    lock (entry)
                    {
                        bool re = SendHttpPostRequest(entry, filePath);
                        if (!re && Configs.IsMESerrorWin)
                        {
                            ShowErrorWin();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void FileCSV_Analys(string filePath)
        {
            try
            {
                string path = filePath;
                DataTable excelasTable = new DataTable();
                if (!File.Exists(path))
                {
                    Console.WriteLine("Excel 文件不存在！");
                    return;
                }

                // 获取文件中的实际行数
                int currentRowCount;
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new HSSFWorkbook(fs); // 使用 HSSFWorkbook 来处理 .xls 格式文件
                    ISheet sheet = workbook.GetSheetAt(0);
                    currentRowCount = sheet.PhysicalNumberOfRows;
                }

                // 读取 Excel 文件
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new HSSFWorkbook(fs); // 使用 HSSFWorkbook 来处理 .xls 格式文件
                    ISheet sheet = workbook.GetSheetAt(0);

                    // 读取第一行作为表头
                    IRow headerRow = sheet.GetRow(0);
                    List<string> headers = new List<string>();
                    for (int i = 0; i < headerRow.LastCellNum; i++)
                    {
                        ICell cell = headerRow.GetCell(i);
                        headers.Add(cell.StringCellValue);
                    }

                    // 创建列表
                    List<HuaJingAlarms> alarms = new List<HuaJingAlarms>();

                    // 从上次读取的行数之后开始读取数据
                    for (int row = 1; row < currentRowCount; row++)
                    {
                        IRow currentRow = sheet.GetRow(row);

                        // 如果当前行为空，则跳过
                        if (currentRow == null)
                            continue;

                        // 创建对象并添加到列表中
                        string module = currentRow.GetCell(0) != null ? currentRow.GetCell(0).StringCellValue : "";
                        string address = currentRow.GetCell(1) != null ? currentRow.GetCell(1).StringCellValue : "";
                        string account = currentRow.GetCell(2) != null ? currentRow.GetCell(2).StringCellValue : "";

                        HuaJingAlarms moduleInfo = new HuaJingAlarms(module, address, account);
                        if (account.Contains("车定位异常") || account.Contains("低液位") || account.Contains("温度异常") || account.Contains("电导率") || account.Contains("天车碰撞"))
                        {
                            alarms.Add(moduleInfo);
                        }
                    }

                    // 更新 Configs.ExcelLastRow
                    Configs.ExcelLastRow = currentRowCount;

                    // 输出表头
                    Console.WriteLine($"表头：{string.Join(", ", headers)}");

                    // 输出每个 ModuleInfo 对象的信息
                    foreach (var moduleInfo in alarms)
                    {
                        Console.WriteLine($"日期: {moduleInfo.Date_}, 时间: {moduleInfo.Time_}, 报警: {moduleInfo.AlarmText_}");
                        // 发送API请求
                        WechatMsgPushModel pushModel = new()
                        {
                            chatId = "wrh5MlCwAAqPYkBuAJEBdNnNfSXDPKOw",
                            messageContent = $"【发生报警】\n设备名称：{Configs.MachineModel}\n设备编号：{Configs.Computer}\n报警信息：{moduleInfo.AlarmText_}\n日期时间：{moduleInfo.Date_}{" "}{moduleInfo.Time_}"
                        };

                        Console.WriteLine($"报警推送：\n{string.Join(", ", pushModel.messageContent)}");
                        MESRespon apiResponse = PostResponse<MESRespon>(Configs.AlarmURLPath, "", JsonConvert.SerializeObject(pushModel));
                    }
                }

                Console.WriteLine("Excel 文件读取完成！");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excel 文件读取错误: {e.Message}");
            }
        }
        /// <summary>
        /// 洪湾电测数据解析：ProdData_20240710.csv
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public List<YS_DianCe_Model> YS_DianCeReadCsvFile(string filePath, long startPosition = 0)
        {
            var records = new List<YS_DianCe_Model>();

            // 注册编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 尝试使用不同的编码读取文件
            Encoding[] encodings = new Encoding[] { Encoding.GetEncoding("GBK") };
            foreach (var encoding in encodings)
            {
                try
                {
                    using (var reader = new StreamReader(filePath, encoding))
                    {
                        // 从指定位置开始读取
                        reader.BaseStream.Seek(startPosition, SeekOrigin.Begin);

                        // 如果起始位置为0，读取表头
                        if (startPosition == 0)
                        {
                            var header = reader.ReadLine();
                            Console.WriteLine($"尝试使用编码 {header}");
                        }

                        // 逐行读取文件内容
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            // 解析不良品信息并汇总为一个字符串
                            var badUnitInfo = new StringBuilder();
                            if (values.Length < 11)
                            {
                                badUnitInfo = null;
                            }
                            for (int i = 10; i < values.Length; i++)
                            {
                                if (i > 10)
                                {
                                    badUnitInfo.Append(", ");
                                }
                                badUnitInfo.Append(values[i]);
                            }
                            // 创建一个YS_DianCe_Model对象，并填充数据
                            var record = new YS_DianCe_Model
                            {
                                Date = DateTime.Parse(values[0], CultureInfo.InvariantCulture), // 解析日期
                                DeviceName = values[1], // 设备名称
                                DeviceNumber = values[2], // 设备编号
                                ProductNumber = values[3], // 产品号
                                BatchNumber = values[4], // 批次号
                                UnitOrder = int.Parse(values[5]), // 单元排序
                                Barcode = values[6], // 条码
                                Result = values[7], // 结果
                                GoodUnitCount = int.Parse(values[8]), // 良品单元数
                                BadUnitCount = int.Parse(values[9]), // 不良品单元数
                                BadUnitInfo = badUnitInfo.ToString()  // 不良品信息
                            };

                            records.Add(record);
                        }
                    }
                    break; // 如果成功读取，跳出循环
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"尝试使用编码 {encoding.EncodingName} 读取文件失败: {ex.Message}");
                }
            }

            return records;
        }

        //此处是将list集合写入excel表，Supply也是自己定义的类，每一个字段对应需要写入excel表的每一列的数据
        //一次最多能写65535行数据，超过需将list集合拆分，分多次写入
        #region 写入excel
        static void WriteToExcelFile(Stream fileStream, List<HuaJingAlarms> huaJingAlarms)
        {
            // 读取现有的Excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(fileStream);
            ISheet sheet = workbook.GetSheetAt(0);

            // 获取当前行数
            int lastRowNum = sheet.LastRowNum;
            foreach (var huaJingAlarm in huaJingAlarms)
            {
                lastRowNum++;
                // 创建新行并写入数据
                IRow newRow = sheet.CreateRow(lastRowNum);
                newRow.CreateCell(0).SetCellValue(huaJingAlarm.Date_);
                newRow.CreateCell(1).SetCellValue(huaJingAlarm.Time_);
                newRow.CreateCell(2).SetCellValue(huaJingAlarm.AlarmText_);
            }

            // 将文档写入到内存流中
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.Write(memoryStream);

                // 将内存流中的内容写入到文件
                using (FileStream file = new FileStream(((FileStream)fileStream).Name, FileMode.Create))
                {
                    byte[] content = memoryStream.ToArray();
                    file.Write(content, 0, content.Length);
                }
            }
        }
        static void InitializeExcel(string filePath)
        {
            // 创建一个新的Excel文档
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");

            // 创建文件头行
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("日期");
            headerRow.CreateCell(1).SetCellValue("时间");
            headerRow.CreateCell(2).SetCellValue("报警内容");

            // 将文档写入到文件
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(file);
            }
        }


        #endregion
        /// <summary>
        /// 时间转化
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public string TimeChange(string date, string time)
        {
            // 将日期字符串和时间字符串转换为DateTime对象
            DateTime dateObj = DateTime.ParseExact(date, "yyyyMMdd", null);
            DateTime timeObj = DateTime.ParseExact(time, "HHmmss", null);

            // 合并日期和时间
            DateTime combinedDateTime = new DateTime(dateObj.Year, dateObj.Month, dateObj.Day, timeObj.Hour, timeObj.Minute, timeObj.Second);

            // 格式化输出
            return combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 上传电子档案接口
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="result"></param>
        /// <param name="testTime"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Post_ElectricalRecord(string BarCode, string result, string testTime, string filePath)
        {
            try
            {
                string errortype = "";
                // 对象
                YS_ElectrRecords electrRecords = new YS_ElectrRecords
                {
                    BarCode = BarCode,
                    TestResult = result,
                    TestTime = testTime,
                    TestComputer = Configs.Computer,
                    Tester = Configs.BuildUser,
                    ProductModel = Configs.PorductModel,
                    MachineNumber = Configs.MachineModel,
                    IsOK = result == "PASS",
                    ElectricalDetailDownInfos = new List<ElectricalDetailDownInfo>
                    {
                        new ElectricalDetailDownInfo
                        {
                            SetupComputer = Configs.Computer,
                            TestTime = testTime,
                            BarCode = BarCode,
                            Testers = Configs.BuildUser,
                            TestResult = result,
                            TestFileName = filePath,
                        }
                    }
                };

                // 构造API请求参数
                var apiParameters = new List<ApiParameter>
                {
                new ApiParameter { Value = electrRecords }
                };

                // 构造API请求对象
                ApiRequest apiRequest = new ApiRequest
                {
                    ApiType = "ScadaApiController",
                    Method = "WipElectricalDown",
                    Parameters = apiParameters,
                    Context = new RequestContext
                    {
                        //正式 fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A  
                        Ticket = "fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A",
                        InvOrgId = 2
                    }
                };

                // 发送API请求
                MESRespon apiResponse = PostResponse<MESRespon>(Configs.URLPath, "", JsonConvert.SerializeObject(apiRequest));

                // 处理API响应
                if (apiResponse.Success)
                {
                    LogHelper.Debug($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse.Success}】--{apiResponse.Message}");
                }
                else
                {
                    LogHelper.Error($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse.Success}】--{apiResponse.Message}");
                    if (apiResponse.Message.Contains(BarCode))
                    {
                        errortype = Regex.Replace(apiResponse.Message, BarCode, "");
                        errortype = Regex.Replace(errortype, @"[\\/:\*\?\""<>\|]", "");
                    }
                    else
                    {
                        errortype = Regex.Replace(apiResponse.Message, @"[\\/:\*\?\""<>\|]", "");

                    };
                    // 强制释放文件资源
                    TryForceFileRelease(filePath);
                    // 获取源文件夹中的所有文件
                    FlieMove(filePath, filePath, Configs.ErrorPath + @"\" + $"{errortype}");
                }

                return apiResponse.Success;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"处理电子档案时出错：{ex.Message}");
                // 强制释放文件资源
                TryForceFileRelease(filePath);
                // 获取源文件夹中的所有文件
                FlieMove(filePath, filePath, Configs.ErrorInetrnetPath);
                return false;
            }

        }

        /// <summary>
        /// 上传电测过站接口
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="result"></param>
        /// <param name="testTime"></param>
        /// <returns></returns>
        public bool Post_ElectricalPastation(string BarCode, string result, string testTime, string filePath)
        {
            try
            {
                string errortype = "";
                // 构造值对象
                Value valuePastation = new Value
                {
                    Qty = 1,
                    DeviceId = Configs.MachineModel,
                    Barcode = BarCode,
                    TestResult = result == "PASS",
                    TimeStamp = DateTime.Now,
                    PartDetectionInfos = new List<PartDetectionInfo>
                    {
                        new PartDetectionInfo
                        {
                            BothSideCode = BarCode,
                            CreateDate = testTime,
                            TestResult = result == "PASS",
                            Barcode = BarCode,
                            FaultCode = result == "PASS" ? "" : "BF372",
                            RevisedFaultCode = null,
                            ImageUrl = Configs.FinalPath
                        }
                    },
                    ImageInfos = new List<ImageInfo>
                    {
                       new ImageInfo
                       {
                           ImageUrl = Configs.FinalPath,
                           ImageIndex = 0,
                           TotalImage = 0,
                           UploadResult = false
                       }
                    }
                };

                // 构造API请求参数
                var apiParameters = new List<ApiParameter>
                {
                  new ApiParameter { Value = valuePastation }
                };

                // 构造API请求对象
                ApiRequest apiRequestPastation = new ApiRequest
                {
                    ApiType = "ScadaApiController",
                    Method = "SMTWipMove",
                    Parameters = apiParameters,
                    Context = new RequestContext
                    {
                        //正式 fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A 
                        Ticket = "fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A",
                        InvOrgId = 2
                    }
                };

                // 发送API请求
                MESRespon apiResponsePastation = PostResponse<MESRespon>(Configs.URLPath, "", JsonConvert.SerializeObject(apiRequestPastation));

                // 处理API响应
                if (apiResponsePastation.Success)
                {
                    LogHelper.Debug($"【电子过站接口】设备编码：{Configs.MachineModel};条码：{BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                    Configs.ProductBarcode = BarCode;
                    Configs.MESErrorInfo = apiResponsePastation.Message;
                    Configs.UploadResult = "成功";
                }
                else
                {
                    LogHelper.Error($"【电子过站接口】设备编码：{Configs.MachineModel};条码：{BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                    Configs.ProductBarcode = BarCode;
                    Configs.MESErrorInfo = apiResponsePastation.Message;
                    Configs.UploadResult = "失败";
                    if (apiResponsePastation.Message.Contains(BarCode))
                    {
                        errortype = Regex.Replace(apiResponsePastation.Message, BarCode, "");
                        errortype = Regex.Replace(errortype, @"[\\/:\*\?\""<>\|]", "");

                    }
                    else { errortype = Regex.Replace(apiResponsePastation.Message, @"[\\/:\*\?\""<>\|]", ""); };
                    // 强制释放文件资源
                    TryForceFileRelease(filePath);
                    // 获取源文件夹中的所有文件
                    FlieMove(filePath, filePath, Configs.ErrorPath + @"\" + $"{errortype}");
                }

                return apiResponsePastation.Success;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"处理电子过站时出错：{ex.Message}");
                Configs.ProductBarcode = BarCode;
                Configs.MESErrorInfo = ex.Message;
                Configs.UploadResult = "失败";
                // 强制释放文件资源
                TryForceFileRelease(filePath);
                // 获取源文件夹中的所有文件
                FlieMove(filePath, filePath, Configs.ErrorInetrnetPath);
                return false;
            }
        }

        /// <summary>
        /// 上传MES过站接口
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="result"></param>
        /// <param name="testTime"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Post_PastationAPI(string BarCode, string result, string testTime, string filePath)
        {
            try
            {
                string errortype = "";
                // 构造值对象
                Value valuePastation = new Value
                {
                    Qty = 1,
                    DeviceId = Configs.MachineModel,
                    Barcode = BarCode,
                    TestResult = result == "PASS",
                    TimeStamp = DateTime.Now,
                    PartDetectionInfos = new List<PartDetectionInfo>
                    {
                        new PartDetectionInfo
                        {
                            BothSideCode = BarCode,
                            CreateDate = testTime,
                            TestResult = result == "PASS",
                            Barcode = BarCode,
                            FaultCode = result == "PASS" ? "" : "BF372",
                            RevisedFaultCode = null,
                            ImageUrl = null
                        }
                    },
                    ImageInfos = new List<ImageInfo>
                    {
                       new ImageInfo
                       {
                           ImageUrl = null,
                           ImageIndex = 0,
                           TotalImage = 0,
                           UploadResult = false
                       }
                    }
                };

                // 构造API请求参数
                var apiParameters = new List<ApiParameter>
                {
                  new ApiParameter { Value = valuePastation }
                };

                // 构造API请求对象
                ApiRequest apiRequestPastation = new ApiRequest
                {
                    ApiType = "ScadaApiController",
                    Method = "SMTWipMove",
                    Parameters = apiParameters,
                    Context = new RequestContext
                    {
                        //正式 fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A 
                        Ticket = "fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A",
                        InvOrgId = 2
                    }
                };

                // 发送API请求
                MESRespon apiResponsePastation = PostResponse<MESRespon>(Configs.URLPath, "", JsonConvert.SerializeObject(apiRequestPastation));

                // 处理API响应
                if (apiResponsePastation.Success)
                {
                    LogHelper.Debug($"【MES过站接口】设备编码：{Configs.MachineModel};条码：{BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                    Configs.ProductBarcode = BarCode;
                    //Configs.MESErrorInfo = apiResponsePastation.Message;
                    Configs.UploadResult = "成功";
                    Configs.MESErrorInfo = "--过站成功--";
                    Configs.AnalysCount++;
                }
                else
                {
                    LogHelper.Error($"【MES过站接口】设备编码：{Configs.MachineModel};条码：{BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                    Configs.ProductBarcode = BarCode;
                    Configs.MESErrorInfo = apiResponsePastation.Message;
                    Configs.UploadResult = "失败";
                    if (apiResponsePastation.Message.Contains(BarCode))
                    {
                        errortype = Regex.Replace(apiResponsePastation.Message, BarCode, "");
                        errortype = Regex.Replace(errortype, @"[\\/:\*\?\""<>\|]", "");

                    }
                    else { errortype = Regex.Replace(apiResponsePastation.Message, @"[\\/:\*\?\""<>\|]", ""); };

                }

                return apiResponsePastation.Success;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"处理MES过站时出错：{ex.Message}");
                Configs.ProductBarcode = BarCode;
                Configs.MESErrorInfo = ex.Message;
                Configs.UploadResult = "失败";
                return false;
            }
        }

        private bool SendHttpPostRequest(AOIData aoidata, string filePath)
        {
            try
            {
                string errortype = "";
                // 创建 HttpClient 实例
                using (HttpClient client = new HttpClient())
                {
                    // 设置要 POST 的 API 地址
                    string apiUrl = Configs.URLPath;

                    // 创建实例并赋值
                    Root requestData = new Root
                    {
                        ApiType = "ScadaApiController",
                        Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Value = new Value
                            {
                                ProgramName = "V1.0",
                                Qty = Convert.ToInt16(aoidata.CurrentNumber),
                                DeviceId = Configs.MachineModel,
                                Barcode = aoidata.SubBoardBarCode,
                                TestResult = aoidata.DefectiveNumber=="0" ? true : false,
                                ReviseResult = null,
                                TimeStamp = DateTime.Now,
                                PartDetectionInfos = new List<PartDetectionInfo>
                                {
                                    new PartDetectionInfo
                                    {
                                        BothSideCode = null,
                                        CreateDate = aoidata.TestDate,
                                        ReviseEndDate = aoidata.RecheckDate,
                                        TestResult = true,
                                        ReviseResult = null,
                                        Barcode = aoidata.SubBoardBarCode,
                                        PartsName = null,
                                        FaultCode = "",
                                        RevisedFaultCode = null,
                                        ImageUrl = null
                                    }
                                },
                                ImageInfos = new List<ImageInfo>
                                {
                                    new ImageInfo
                                    {
                                        ImageUrl = null,
                                        ImageIndex = 1,
                                        TotalImage = 1,
                                        UploadResult = true
                                    }
                                }
                            }
                        }
                    },
                        Method = "SMTWipMove",
                        Context = new Context
                        {
                            Ticket = "fqqPHVfSUjtah6cjKXVnaJ6VfQYxIvmBfLApllYUjC/rhIr6rpaa6XERYTVoOX1A",
                            InvOrgId = 2
                        }
                    };

                    // 将对象序列化为 JSON 字符串
                    string jsonData = JsonConvert.SerializeObject(requestData);

                    // 发送 POST 请求
                    HttpResponseMessage response = client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json")).Result;
                    // 获取响应内容
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    MESRespon responseObject = JsonConvert.DeserializeObject<MESRespon>(responseContent);
                    // 处理响应
                    if (responseObject.Success)
                    {
                        LogHelper.Debug($"【AOI数据接口】设备编码：{aoidata.EQPTypeName};板码：{aoidata.SubBoardBarCode};MES结果：{responseContent}");
                        Configs.ProductBarcode = aoidata.SubBoardBarCode;
                        Configs.MESErrorInfo = responseContent;
                        Configs.UploadResult = "成功";
                        Configs.AnalysCount++;
                    }
                    else
                    {
                        LogHelper.Error($"【AOI数据接口】设备编码：{aoidata.EQPTypeName};板码：{aoidata.SubBoardBarCode};MES结果：{responseContent}");
                        Configs.ProductBarcode = aoidata.SubBoardBarCode;
                        Configs.MESErrorInfo = responseObject.Message;
                        Configs.UploadResult = "失败";
                        //if (responseObject.Message.Contains(aoidata.SubBoardBarCode))
                        //{
                        //    errortype = Regex.Replace(responseObject.Message, aoidata.SubBoardBarCode, "");
                        //    errortype = Regex.Replace(errortype, @"[\\/:\*\?\""<>\|]", "");

                        //}
                        //else { errortype = Regex.Replace(responseObject.Message, @"[\\/:\*\?\""<>\|]", ""); };
                        //// 强制释放文件资源
                        //TryForceFileRelease(filePath);
                        //// 获取源文件夹中的所有文件
                        //FlieMove(filePath, filePath, Configs.ErrorPath + @"\" + $"{errortype}");
                    }
                    return responseObject.Success;
                }

            }

            catch (Exception ex)
            {
                LogHelper.Error($"上传【AOI数据接口】异常：{ex.Message}");
                Configs.ProductBarcode = aoidata.SubBoardBarCode;
                Configs.MESErrorInfo = ex.Message;
                Configs.UploadResult = "失败";
                // 强制释放文件资源
                TryForceFileRelease(filePath);
                // 获取源文件夹中的所有文件
                FlieMove(filePath, filePath, Configs.ErrorInetrnetPath);
                return false;
            }

        }
        // 处理当前整体的数据并添加到列表中
        static void ProcessCurrentData(List<AOIData> list, List<string> data)
        {
            if (data.Count > 0)
            {
                // 将当前整体的数据合并成一个字符串，以换行符分隔
                string joinedData = string.Join("@", data);

                // 解析当前整体的数据并添加到列表中
                AOIData entry = ParseData(joinedData);
                list.Add(entry);
            }
        }
        // 解析整体数据并返回对应的 AOIData 实例
        static AOIData ParseData(string data)
        {
            AOIData entry = new AOIData();
            string[] lines = data.Split('@');
            List<string> badcod = new List<string>();
            if (lines.Length >= 13)
            {
                entry.EQPTypeName = lines[0].Trim();
                entry.SubBoardBarCode = lines[1].Trim();
                entry.LineName = lines[2].Trim();
                entry.BoardNumber = lines[3].Trim();
                entry.OperatorMark = lines[4].Trim();
                entry.WorkOrderNumber = lines[5].Trim();
                entry.TestDate = lines[6].Trim();
                entry.RecheckDate = lines[7].Trim();
                entry.PCBStatus = lines[8].Trim();
                entry.BoardIdentifi = lines[9].Trim();
                entry.CurrentNumber = lines[10].Trim();
                entry.DefectiveNumber = lines[11].Trim();
                for (int i = 12; i < lines.Length; i++)
                {
                    badcod.Add(lines[i].Trim());
                }
                entry.BadCode = string.Join(",", badcod);
            }
            else
            {
                entry.EQPTypeName = lines[0].Trim();
                entry.SubBoardBarCode = lines[1].Trim();
                entry.LineName = lines[2].Trim();
                entry.BoardNumber = lines[3].Trim();
                entry.OperatorMark = lines[4].Trim();
                entry.WorkOrderNumber = lines[5].Trim();
                entry.TestDate = lines[6].Trim();
                entry.RecheckDate = lines[7].Trim();
                entry.PCBStatus = lines[8].Trim();
                entry.BoardIdentifi = lines[9].Trim();
                entry.CurrentNumber = lines[10].Trim();
                entry.DefectiveNumber = lines[11].Trim();
                entry.BadCode = "";
            }

            return entry;
        }

        private void ShowErrorWin()
        {
            // 在 UI 线程上调度创建和显示 FullScreenPopupWindow
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                // 创建 FullScreenPopupWindow 实例
                MsgWindow msgWindow = new MsgWindow(Configs);
                // 显示窗口
                msgWindow.ShowDialog();
            });
        }
        public static bool AddFreeSql()
        {
            try
            {
                // 尝试建立数据库连接
                fsql = new FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer,
                @"Data Source=192.168.5.250;User Id=sa;Password=Abc1234%;Initial Catalog=SLSYS_YSMES;Encrypt=True;
                TrustServerCertificate=True;Pooling=true;Min Pool Size=1")
                .UseAutoSyncStructure(false)
                .UseNoneCommandParameter(true)
                .UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText))
                .Build();
                // 尝试执行查询
                var result = fsql.Select<YS_TestModel>().Limit(1).ToList();
                if (result.Count == 0)
                {
                    Console.WriteLine("数据库连接失败！");
                    return false;
                }
                // 如果没有抛出异常，说明连接成功
                Console.WriteLine("数据库连接成功！");
                return true;
            }
            catch (Exception ex)
            {
                // 捕获异常并进行处理，例如记录错误日志
                LogHelper.Error($"数据库连接错误: {ex.Message}");
                return false;
            }

        }
        public string ReplaceDclWithAsterisk(string input)
        {
            return input.Replace(Configs.IsCodeReplaceBefore, Configs.IsCodeReplaceAfter);
        }
    }
}