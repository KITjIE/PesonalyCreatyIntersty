//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace DLGCY.FilesWatcher
//{
//    public class HandUpLoadViewModel : INotifyPropertyChanged
//    {
//        private string _handBarCode;
//        public string HandBarCode
//        {
//            get { return _handBarCode; }
//            set
//            {
//                _handBarCode = value;
//                OnPropertyChanged(nameof(HandBarCode));
//            }
//        }

//        public ICommand UpLoadCommand { get; }

//        public HandUpLoadViewModel()
//        {
//            UpLoadCommand = new RelayCommand(async () => await UploadDataAsync());
//        }

//        private async Task UploadDataAsync()
//        {
//            using (var client = new HttpClient())
//            {
//                var content = new StringContent($"{{\"data\":\"{HandBarCode}\"}}", Encoding.UTF8, "application/json");
//                var response = await client.PostAsync("http://yourapiendpoint.com/upload", content);
//                if (response.IsSuccessStatusCode)
//                {
//                    // 上传成功的逻辑
//                    // 例如，通知用户上传成功
//                }
//                else
//                {
//                    // 处理错误的逻辑
//                    // 例如，通知用户上传失败
//                }
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    public class RelayCommand : ICommand
//    {
//        private readonly Func<Task> _execute;
//        private readonly Func<bool> _canExecute;

//        public RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
//        {
//            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
//            _canExecute = canExecute;
//        }

//        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

//        public async void Execute(object parameter) => await _execute();

//        public event EventHandler CanExecuteChanged
//        {
//            add { CommandManager.RequerySuggested += value; }
//            remove { CommandManager.RequerySuggested -= value; }
//        }
//    }
//}
