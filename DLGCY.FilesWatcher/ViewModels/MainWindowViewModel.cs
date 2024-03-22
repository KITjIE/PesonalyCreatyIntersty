using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DLGCY.FilesWatcher.Helper;
using DotNet.Utilities.ConsoleHelper;
using FreeSql;
using Newtonsoft.Json;
using NPOI.HPSF;
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

namespace DLGCY.FilesWatcher.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel : BindableBase
    {
        #region Bindable
        public string MESInfo { get; set; }
        private static IFreeSql fsql;
        private ConfigItems _configs;
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
        public string Status { get; set; } = "中京元盛";

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
        #endregion

        /// <summary>
        /// 命令方法赋值(在构造函数中调用)
        /// </summary>
        private void SetCommandMethod()
        {
            SaveConfigCommand ??= new RelayCommand(o => true, async o =>
            {
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

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", async () =>
                {
                    if (inputVM.Text == "abc+123")
                    {
                        Configs.EditMode = true;
                    }
                }, msg => { Console.WriteLine(msg); }, isShowText: false);
            });

            ClearInfoCommand ??= new RelayCommand(o => true, o =>
            {
                Info = "";
            });

            AboutCommand ??= new RelayCommand(o => true, o =>
            {
                new AboutWindow().ShowDialog();
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

                await ConfirmBoxHelper.ShowConfirm(DialogVm, "", async () =>
                {
                    Console.WriteLine($"宽度-{inputVM.InputWidth},高度-{inputVM.InputHeight},编号-{inputVM.InputNo}");
                }, ShowInfo, isShowText: false);
            });

            WaitCommand ??= new RelayCommand(o => true, async o =>
            {
                await ConfirmBoxHelper.ShowWait(DialogVm, "正在执行业务操作...", async () =>
                {
                    await Task.Delay(1000 * 10);
                    Console.WriteLine("操作完成");
                });
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
                if (Configs.SupervisMode == "文件解析模式B")
                {
                    FileTXTName_HansAnalys();
                }
                else
                {
                    OpenFileDialog_Handle();

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
            Console.WriteLine($"【{GetPathType(e.FullPath)}更改】{GetPath(e)}");
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.Name == "NetworkConnect.txt") { return; }
            try
            {
                if (Configs.SupervisMode == "文件解析模式A")
                {
                    if (e.Name.Contains(".txt"))
                    {
                        if (true)
                        {

                        }
                        FileTXT_Analys(GetPath(e));
                    }
                }
                if (Configs.SupervisMode == "文件解析模式C")
                {
                    if (e.Name.Contains(".txt"))
                    {
                        if (!FileTXTName_SonFile(GetPath(e), System.IO.Path.GetFileNameWithoutExtension(GetPath(e))))
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
                    }
                }
                if (Configs.SupervisMode == "文件解析模式D")
                {
                    if (e.Name.Contains(".xls") || e.FullPath.Contains(".csv"))
                    {
                        FileCSV_Analys(GetPath(e));
                    }
                }
                if (Configs.SupervisMode == "文件解析模式B")
                {
                    string fileSuffixName = GetPath(e).Substring(GetPath(e).LastIndexOf('.') + 1);//获取文件的后缀名
                    switch (fileSuffixName)
                    {
                        case "csv":
                            if (!FileCSVName_Analys(System.IO.Path.GetFileNameWithoutExtension(GetPath(e))))
                            {
                                // 在 UI 线程上调度创建和显示 FullScreenPopupWindow
                                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                                {
                                    // 创建 FullScreenPopupWindow 实例
                                    MsgWindow msgWindow = new MsgWindow(Configs);
                                    // 显示窗口
                                    msgWindow.ShowDialog();
                                });
                            };
                            return;
                        case "txt":
                            if (!FileTXTName_Analys(System.IO.Path.GetFileNameWithoutExtension(GetPath(e))))
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
                // 强制释放文件资源
                TryForceFileRelease(GetPath(e));
                // 获取源文件夹中的所有文件
                FlieMove(GetPath(e), GetPath(e), Configs.FinalPath);

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
                        FileTXT_Analys(filePath);
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


        /// <summary>
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

        private async Task SendHttpPostRequest(AOIData aoidata)
        {
            try
            {
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
                                DeviceId = aoidata.EQPTypeName,
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
                            Ticket = "Ar35u6PEo5msFA+fBGV7XZO4gct5EbjAnN/LlOfoi97Trzl99Tlk2IGJSiVxi/Qpm8vvKtvpbdM=",
                            InvOrgId = 1
                        }
                    };

                    // 将对象序列化为 JSON 字符串
                    string jsonData = JsonConvert.SerializeObject(requestData);

                    // 发送 POST 请求
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                    // 获取响应内容
                    string responseContent = await response.Content.ReadAsStringAsync();
                    MESRespon responseObject = JsonConvert.DeserializeObject<MESRespon>(responseContent);
                    // 处理响应
                    if (response.IsSuccessStatusCode)
                    {
                        Configs.AnalysCount++;
                        LogHelper.Debug($"【AOI数据接口】设备编码：{aoidata.EQPTypeName};板码：{aoidata.SubBoardBarCode};MES结果：{responseContent}");
                    }
                    else
                    {
                        LogHelper.Error($"【AOI数据接口】设备编码：{aoidata.EQPTypeName};板码：{aoidata.SubBoardBarCode};MES结果：{responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
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
                // string sourceFile = System.IO.Path.Combine(sourceFilePath, System.IO.Path.GetFileName(files));
                string destinationFile = System.IO.Path.Combine(destFilePath, System.IO.Path.GetFileName(files));

                if (File.Exists(destinationFile))
                {
                    // 移除只读属性（否则无法删除）
                    File.SetAttributes(destinationFile, System.IO.FileAttributes.Normal);
                    File.Delete(destinationFile);
                }
                File.Move(sourceFilePath, destinationFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"【文件移动错误】: {ex.Message}");
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
                        string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
                        string fileSuffixName = filePath.Substring(filePath.LastIndexOf('.') + 1);//获取文件的后缀名
                        if (fileSuffixName == "csv")
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                if (!FileCSVName_Analys(fileNameWithoutExtension))
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
                            }
                        }
                        else if (fileSuffixName == "txt")
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                if (!FileTXTName_Analys(fileNameWithoutExtension))
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

        public bool FileCSVName_Analys(string fileNameWithoutExtension)
        {
            string fileName = fileNameWithoutExtension;
            Configs.ProductBarcode = "";
            Configs.MESErrorInfo = "";
            Configs.UploadResult = "";
            try
            {
                if (fileNameWithoutExtension.Contains("BarCode"))
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    return true;
                }
                int underscoreCount = fileName.Split('_').Length - 1;
                if (underscoreCount > 5)
                {
                    // 找到第三个下划线的索引
                    int thirdUnderscoreIndex = fileName.IndexOf('_', fileName.IndexOf('_') + 1);

                    // 如果找到了第三个下划线
                    if (thirdUnderscoreIndex != -1)
                    {
                        // 使用 Substring 方法截取从第三个下划线之后到字符串末尾的部分
                        fileName = fileName.Substring(thirdUnderscoreIndex + 1);
                    }
                }
                if (fileName.Length < 20)
                {
                    Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    Configs.MESErrorInfo = "文件名格式错误,解析失败！";
                    return true;
                }

                // 使用正则表达式匹配两个下划线之间的文本
                string[] parts = fileName.Split('_');
                string[] result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                // 将日期字符串和时间字符串转换为DateTime对象
                DateTime dateObj = DateTime.ParseExact(result[0], "yyyyMMdd", null);
                DateTime timeObj = DateTime.ParseExact(result[1], "HHmmss", null);

                // 合并日期和时间
                DateTime combinedDateTime = new DateTime(dateObj.Year, dateObj.Month, dateObj.Day, timeObj.Hour, timeObj.Minute, timeObj.Second);

                // 格式化输出
                string Timeresult = combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                // 输出提取的信息
                if (result.Length >= 3)
                {
                    if (result[3].Contains("PASS"))
                    {
                        result[3] = "PASS";
                    }
                    else
                    {
                        result[3] = "FAIL";
                    }
                    #region 上传电子档案接口

                    YS_ElectrRecords electrRecords = new()
                    {
                        BarCode = result[2],
                        TestResult = result[3],
                        TestTime = Timeresult,
                        TestComputer = Configs.Computer,
                        Tester = Configs.BuildUser,
                        ProductModel = Configs.PorductModel,
                        MachineNumber = Configs.MachineModel,
                        ProductOjectName = Configs.PorductModel,
                        IsOK = result[3] == "PASS" ? true : false,
                        ElectricalDetailDownInfos = new List<ElectricalDetailDownInfo>
                        {
                            new ElectricalDetailDownInfo
                            {
                                SetupComputer = Configs.Computer,
                                TestTime = Timeresult,
                                BarCode = result[2],
                                Testers = Configs.BuildUser,
                                TestResult = result[3],
                                TestFileName = fileNameWithoutExtension,
                            }
                        }
                    };

                    var apiParameters = new List<ApiParameter>();
                    apiParameters.Add(new ApiParameter()
                    {
                        Value = electrRecords
                    });


                    RequestContext Context1 = new RequestContext()
                    {
                        Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                        InvOrgId = 2,
                    };

                    ApiRequest apiRequest1 = new ApiRequest();
                    apiRequest1.ApiType = "ScadaApiController";
                    apiRequest1.Method = "WipElectricalDown";
                    apiRequest1.Parameters = apiParameters;
                    apiRequest1.Context = Context1;

                    //调用接口
                    MESRespon apiResponse1 = ApiClient.PostResponse<MESRespon>(
                    Configs.URLPath, "", JsonConvert.SerializeObject(apiRequest1));

                    if (apiResponse1.Success)
                    {
                        LogHelper.Debug($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
                    }
                    else
                    {
                        LogHelper.Error($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
                    }
                    #endregion

                    #region 上传电测过站接口


                    Value valuePastation = new Value
                    {
                        Qty = 1,
                        DeviceId = Configs.MachineModel,
                        Barcode = result[2],
                        TestResult = result[3] == "PASS" ? true : false,
                        TimeStamp = DateTime.Now,
                        PartDetectionInfos = new List<PartDetectionInfo>
                                {
                                    new PartDetectionInfo
                                    {
                                        BothSideCode = result[2],
                                        CreateDate =Timeresult ,
                                        TestResult = result[3]=="PASS"? true:false,
                                        Barcode =  result[2],
                                        FaultCode =result[3]=="PASS"? "":"短路",
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


                    var apiPastation = new List<ApiParameter>();
                    apiPastation.Add(new ApiParameter()
                    {
                        Value = valuePastation
                    });

                    RequestContext ContextPastation = new RequestContext()
                    {
                        Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                        InvOrgId = 2,
                    };
                    ApiRequest apiRequesPastation = new ApiRequest();
                    apiRequesPastation.ApiType = "ScadaApiController";
                    apiRequesPastation.Method = "SMTWipMove";
                    apiRequesPastation.Parameters = apiPastation;
                    apiRequesPastation.Context = ContextPastation;

                    //调用接口
                    MESRespon apiResponsePastation = ApiClient.PostResponse<MESRespon>(
                    Configs.URLPath, "", JsonConvert.SerializeObject(apiRequesPastation));

                    if (apiResponsePastation.Success)
                    {
                        LogHelper.Debug($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                        Configs.ProductBarcode = electrRecords.BarCode;
                        Configs.UploadResult = "成功";
                    }
                    else
                    {
                        LogHelper.Error($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                        Configs.ProductBarcode = electrRecords.BarCode;
                        Configs.MESErrorInfo = apiResponsePastation.Message;
                        Configs.UploadResult = "失败";

                    }
                    #endregion
                    if (apiResponsePastation.Success && apiResponse1.Success)
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


        public bool FileTXTName_Analys(string fileNameWithoutExtension)
        {
            Configs.ProductBarcode = "";
            Configs.UploadResult = "";
            Configs.MESErrorInfo = "";
            try
            {
                if (fileNameWithoutExtension.Length < 18)
                {
                    //Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                    //Configs.MESErrorInfo = "文件名格式错误,解析失败！";
                    //return true;
                }
                if (fileNameWithoutExtension.Length < 28)
                {

                }
                // 使用正则表达式匹配两个下划线之间的文本
                string[] parts = fileNameWithoutExtension.Split('_');
                string[] result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                // 合并日期和时间字符串
                string combinedDateTimeStr = $"{result[2]} {result[3]}";

                // 将合并后的字符串转换为DateTime对象
                DateTime combinedDateTime = DateTime.ParseExact(combinedDateTimeStr, "yyyy-MM-dd HH-mm-ss", null);

                // 格式化输出
                string Timeresult = combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                // 输出提取的信息
                if (result.Length >= 5)
                {
                    if (result[4].Contains("OK"))
                    {
                        result[4] = "PASS";
                    }
                    else
                    {
                        result[4] = "FAIL";
                    }
                    #region 上传电子档案接口
                    YS_ElectrRecords electrRecords = new()
                    {
                        BarCode = result[0],
                        TestResult = result[4],
                        TestTime = Timeresult,
                        TestComputer = Configs.Computer,
                        Tester = Configs.BuildUser,
                        ProductModel = Configs.PorductModel,
                        MachineNumber = Configs.MachineModel,
                        IsOK = result[4] == "PASS" ? true : false,
                        ElectricalDetailDownInfos = new List<ElectricalDetailDownInfo>
                        {
                            new ElectricalDetailDownInfo
                            {
                                SetupComputer = Configs.Computer,
                                TestTime = Timeresult,
                                BarCode = result[0],
                                Testers = Configs.BuildUser,
                                TestResult = result[4],
                                TestFileName = fileNameWithoutExtension,
                            }
                        }
                    };

                    var apiParameters = new List<ApiParameter>();
                    apiParameters.Add(new ApiParameter()
                    {
                        Value = electrRecords
                    });

                    ApiRequest apiRequest1 = new ApiRequest();
                    apiRequest1.ApiType = "ScadaApiController";
                    apiRequest1.Method = "WipElectricalDown";
                    apiRequest1.Parameters = apiParameters;

                    RequestContext Context1 = new RequestContext()
                    {
                        Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                        InvOrgId = 2,
                    };
                    apiRequest1.Context = Context1;

                    //调用接口
                    MESRespon apiResponse1 = ApiClient.PostResponse<MESRespon>(
                    Configs.URLPath, "", JsonConvert.SerializeObject(apiRequest1));
                    if (apiResponse1.Success)
                    {
                        LogHelper.Debug($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
                    }
                    else
                    {
                        LogHelper.Error($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
                    }
                    #endregion

                    #region 上传电测过站接口


                    Value valuePastation = new Value
                    {
                        Qty = 1,
                        DeviceId = Configs.MachineModel,
                        Barcode = result[0],
                        TestResult = result[4] == "PASS" ? true : false,
                        TimeStamp = DateTime.Now,
                        PartDetectionInfos = new List<PartDetectionInfo>
                               {
                                    new PartDetectionInfo
                                    {
                                        BothSideCode = result[0],
                                        CreateDate =Timeresult,
                                        TestResult = result[4]=="PASS"? true:false,
                                        Barcode =  result[0],
                                        FaultCode =result[4]=="PASS"? "":"短路",
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


                    var apiPastation = new List<ApiParameter>();
                    apiPastation.Add(new ApiParameter()
                    {
                        Value = valuePastation
                    });

                    RequestContext ContextPastation = new RequestContext()
                    {
                        Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                        InvOrgId = 2,
                    };
                    ApiRequest apiRequesPastation = new ApiRequest();
                    apiRequesPastation.ApiType = "ScadaApiController";
                    apiRequesPastation.Method = "SMTWipMove";
                    apiRequesPastation.Parameters = apiPastation;
                    apiRequesPastation.Context = ContextPastation;

                    //调用接口
                    MESRespon apiResponsePastation = ApiClient.PostResponse<MESRespon>(
                    Configs.URLPath, "", JsonConvert.SerializeObject(apiRequesPastation));

                    if (apiResponsePastation.Success)
                    {
                        LogHelper.Debug($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                        Configs.ProductBarcode = electrRecords.BarCode;
                        Configs.UploadResult = "成功";
                    }
                    else
                    {
                        LogHelper.Error($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                        Configs.ProductBarcode = electrRecords.BarCode;
                        Configs.MESErrorInfo = apiResponsePastation.Message;
                        Configs.UploadResult = "失败";
                    }
                    #endregion
                    if (apiResponsePastation.Success && apiResponse1.Success)
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
        public bool FileTXTName_SonFile(string filePath, string fileName)
        {
            string filePath_ = filePath;
            string Result = "PASS";
            string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (filePath_.Contains("FAIL"))
            {
                Result = "FAIL";
            }
            #region 上传电子档案接口
            YS_ElectrRecords electrRecords = new()
            {
                BarCode = fileName,
                TestResult = Result,
                TestTime = DateTime_,
                TestComputer = Configs.Computer,
                Tester = Configs.BuildUser,
                ProductModel = Configs.PorductModel,
                MachineNumber = Configs.MachineModel,
                IsOK = Result == "PASS" ? true : false,
                ElectricalDetailDownInfos = new List<ElectricalDetailDownInfo>
                        {
                            new ElectricalDetailDownInfo
                            {
                                SetupComputer = Configs.Computer,
                                TestTime = DateTime_,
                                BarCode = fileName,
                                Testers = Configs.BuildUser,
                                TestResult = Result,
                                TestFileName = filePath,
                            }
                        }
            };

            var apiParameters = new List<ApiParameter>();
            apiParameters.Add(new ApiParameter()
            {
                Value = electrRecords
            });

            ApiRequest apiRequest1 = new ApiRequest();
            apiRequest1.ApiType = "ScadaApiController";
            apiRequest1.Method = "WipElectricalDown";
            apiRequest1.Parameters = apiParameters;

            RequestContext Context1 = new RequestContext()
            {
                Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                InvOrgId = 2,
            };
            apiRequest1.Context = Context1;

            //调用接口
            MESRespon apiResponse1 = ApiClient.PostResponse<MESRespon>(
            Configs.URLPath, "", JsonConvert.SerializeObject(apiRequest1));
            if (apiResponse1.Success)
            {
                LogHelper.Debug($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
            }
            else
            {
                LogHelper.Error($"【电子档案接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponse1.Success}】--{apiResponse1.Message}");
            }
            #endregion

            #region 上传电测过站接口


            Value valuePastation = new Value
            {
                Qty = 1,
                DeviceId = Configs.MachineModel,
                Barcode = fileName,
                TestResult = Result == "PASS" ? true : false,
                TimeStamp = DateTime.Now,
                PartDetectionInfos = new List<PartDetectionInfo>
                               {
                                    new PartDetectionInfo
                                    {
                                        BothSideCode =fileName,
                                        CreateDate =DateTime_,
                                        TestResult = Result=="PASS"? true:false,
                                        Barcode =  fileName,
                                        FaultCode =Result=="PASS"? "":"短路",
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


            var apiPastation = new List<ApiParameter>();
            apiPastation.Add(new ApiParameter()
            {
                Value = valuePastation
            });

            RequestContext ContextPastation = new RequestContext()
            {
                Ticket = "Zu5wt35NMs4OvENNGUwRgfLxE3PLwBxMJp8hFAbeXYGuUzC4cC8CreHCD2qD48QfynpOD3nvzB8=",
                InvOrgId = 2,
            };
            ApiRequest apiRequesPastation = new ApiRequest();
            apiRequesPastation.ApiType = "ScadaApiController";
            apiRequesPastation.Method = "SMTWipMove";
            apiRequesPastation.Parameters = apiPastation;
            apiRequesPastation.Context = ContextPastation;

            //调用接口
            MESRespon apiResponsePastation = ApiClient.PostResponse<MESRespon>(
            Configs.URLPath, "", JsonConvert.SerializeObject(apiRequesPastation));

            if (apiResponsePastation.Success)
            {
                LogHelper.Debug($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                Configs.ProductBarcode = electrRecords.BarCode;
                Configs.UploadResult = "成功";
            }
            else
            {
                LogHelper.Error($"【电子过站接口】设备编码：{electrRecords.MachineNumber};条码：{electrRecords.BarCode};MES结果：【{apiResponsePastation.Success}】--{apiResponsePastation.Message}");
                Configs.ProductBarcode = electrRecords.BarCode;
                Configs.MESErrorInfo = apiResponsePastation.Message;
                Configs.UploadResult = "失败";
            }
            #endregion
            if (apiResponsePastation.Success && apiResponse1.Success)
            {
                Configs.AnalysCount++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FileTXT_Analys(string filePath)
        {
            if (filePath.Length < 20)
            {
                Console.WriteLine($"【文件名格式错误,解析失败！！！】");
                return;
            }
            List<AOIData> aoiDataList = new List<AOIData>();
            // 用于存储当前整体的数据
            List<string> currentData = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
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

            // 显示解析的数据
            foreach (var entry in aoiDataList)
            {
                lock (entry)
                {
                    _ = SendHttpPostRequest(entry);
                }
            }
        }
        public void FileCSV_Analys(string filePath)
        {
            string? path = filePath;
            DataTable excelasTable = new DataTable();
            if (!File.Exists(path))
            {
                Console.WriteLine("Excel 文件不存在！");
                return;
            }
            // 读取 Excel 文件
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
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

                // 从第二行开始读取数据
                for (int row = 1; row <= sheet.LastRowNum; row++)
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
                    alarms.Add(moduleInfo);
                }

                // 输出表头
                Console.WriteLine($"表头：{string.Join(", ", headers)}");

                // 输出每个 ModuleInfo 对象的信息
                foreach (var moduleInfo in alarms)
                {
                    Console.WriteLine($"Module: {moduleInfo.Module}, Address: {moduleInfo.Address}, Account: {moduleInfo.Account}");
                }
            }

            Console.WriteLine("Excel 文件读取完成！");

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
    }
}



