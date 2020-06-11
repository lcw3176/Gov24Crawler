using Gov24Crawler.Command;
using Gov24Crawler.Model;
using Gov24Crawler.Model.ObservableCollection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gov24Crawler.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        public ICommand setExcelPath { get; set; }
        public ICommand setSavePath { get; set; }
        public ICommand runProcess { get; set; }
        public ICommand fileButton { get; set; }
        public ICommand processButton { get; set; }

        private int MaxValue = 100;
        Button runButton;
        HomeModel homeModel;

        public string mainAddress
        {
            get { return homeModel.MainAddress; }
            set
            {
                homeModel.MainAddress = value;
                OnPropertyUpdate("mainAddress");
            }
        }

        public int maxValue
        {
            get { return MaxValue; }
            set
            {
                MaxValue = value;
                OnPropertyUpdate("maxValue");
            }
        }

        public string addressCol
        {
            get { return homeModel.AddressCol; }
            set
            {
                homeModel.AddressCol = value;
                OnPropertyUpdate("addressCol");
            }
        }

        public string sizeCol
        {
            get { return homeModel.SizeCol; }
            set
            {
                homeModel.SizeCol = value;
                OnPropertyUpdate("sizeCol");
            }
        }

        public string ownerCol
        {
            get { return homeModel.OwnerCol; }
            set
            {
                homeModel.OwnerCol = value;
                OnPropertyUpdate("ownerCol");
            }
        }

        public string startRow
        {
            get { return homeModel.StartRow; }
            set
            {
                homeModel.StartRow = value;
                OnPropertyUpdate("startRow");
            }
        }

        public string endRow
        {
            get { return homeModel.EndRow; }
            set
            {
                homeModel.EndRow = value;
                OnPropertyUpdate("endRow");
            }
        }

        public int totalProgress 
        {
            get { return homeModel.TotalProgress; }
            set
            {
                homeModel.TotalProgress = value;
                OnPropertyUpdate("totalProgress");
            }
        }


        public string userId
        {
            get { return homeModel.UserId; }
            set
            {
                homeModel.UserId = value;
                OnPropertyUpdate("userId");
            }
        }


        public string userPw
        {
            get { return homeModel.UserPw; }
            set
            {
                homeModel.UserPw = value;
                OnPropertyUpdate("userPw");
            }
        }


        public ObservableCollection<ProcessList> processList
        {
            get { return homeModel.ProcessList; }
            set
            {
                homeModel.ProcessList = value;
                OnPropertyUpdate("processList");
            }
        }

        public string excelPath
        {
            get { return homeModel.ExcelPath; }
            set 
            { 
                homeModel.ExcelPath = value;
                OnPropertyUpdate("excelPath");
            }
        }

        public string savePath
        {
            get { return homeModel.SavePath; }
            set
            {
                homeModel.SavePath = value;
                OnPropertyUpdate("savePath");
            }
        }

        public HomeViewModel()
        {
            homeModel = new HomeModel();
            homeModel.endCycle += new HomeModel.End(addProgressBar);
            homeModel.endLoop += new HomeModel.End(resetProgressBar);
            setExcelPath = new RelayCommand(setExcelPathMethod);
            setSavePath = new RelayCommand(setSaveMethod);
            runProcess = new RelayCommand(runProcessMethod);
            fileButton = new RelayCommand(fileButtonMethod);
            processButton = new RelayCommand(processButtonMethod);
        }

        private void fileButtonMethod(object obj)
        {
            (obj as TabControl).SelectedIndex = 0;
        }

        private void processButtonMethod(object obj)
        {
            if (string.IsNullOrEmpty(excelPath) || string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("경로 설정을 해주세요.");
                return;
            }

            if(string.IsNullOrEmpty(mainAddress))
            {
                MessageBox.Show("주소 입력을 확인해 주세요.");
                return;
            }

            try
            {
                int.Parse(startRow);
                int.Parse(endRow);
                int.Parse(ownerCol);
                int.Parse(sizeCol);
                int.Parse(addressCol);
                (obj as TabControl).SelectedIndex = 1;
            }

            catch
            {
                MessageBox.Show("파일 설정란 입력을 확인해 주세요.");
            }

        }

        // 크롤링 실행 버튼
        private void runProcessMethod(object obj)
        {
            if((obj as Button).Background == Brushes.Red)
            {
                MessageBox.Show("진행중입니다. 종료 후 다시 눌러주세요");
                return;
            }

            int start = int.Parse(startRow);
            int end = int.Parse(endRow);
            maxValue = end - start + 1;


            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userPw.ToString()))
            {
                MessageBox.Show("아이디나 비밀번호를 확인해 주세요.");
            }

            else
            {
                homeModel.StartRun();
                totalProgress = 0;
                runButton = (obj as Button);
                runButton.Background = Brushes.Red;
                runButton.Foreground = Brushes.White;
            }

        }

        // 저장 폴더 지정
        private void setSaveMethod(object obj)
        {
            System.Windows.Forms.FolderBrowserDialog open = new System.Windows.Forms.FolderBrowserDialog();

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savePath = open.SelectedPath;
            }
        }

        // 엑셀 파일 지정
        private void setExcelPathMethod(object obj)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (open.ShowDialog() == true)
            {
                excelPath = open.FileName;
            }
        }

        // 진행상황 프로그레스 바 증가
        private void addProgressBar()
        {
            totalProgress += 1;
        }

        // 프로그레스 바 리셋
        private void resetProgressBar()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                runButton.Background = Brushes.LightGray;
                runButton.Foreground = Brushes.Black;
            }));
        }
    }
}
