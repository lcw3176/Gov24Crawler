using Gov24Crawler.Model.ObservableCollection;
using Excel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gov24Crawler.Model
{
    class HomeModel
    {
        public delegate void End();
        public event End endCycle;
        public event End endLoop;

        private string excelPath;
        private string savePath;
        private string userId;
        private string startRow;
        private string endRow;
        private string userPw = string.Empty;
        private string addressCol;
        private string sizeCol;
        private string ownerCol;
        private string mainAddress;

        private int totalProgress = 0;
        private StringBuilder serverPw = new StringBuilder();
        private ObservableCollection<ProcessList> processList = new ObservableCollection<ProcessList>();

        public string MainAddress
        {
            get { return mainAddress; }
            set { mainAddress = value; }
        }

        public int TotalProgress
        {
            get { return totalProgress; }
            set { totalProgress = value; }
        }

        public string AddressCol
        {
            get { return addressCol; }
            set { addressCol = value; }
        }

        public string SizeCol
        {
            get { return sizeCol; }
            set { sizeCol = value; }
        }

        public string OwnerCol
        {
            get { return ownerCol; }
            set { ownerCol = value; }
        }

        public string StartRow
        {
            get { return startRow; }
            set { startRow = value; }
        }

        public string EndRow
        {
            get { return endRow; }
            set { endRow = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string UserPw
        {
            get
            {
                if (string.IsNullOrEmpty(userPw))
                {
                    return userPw;
                }

                else
                {
                    return new string('*', userPw.Length - 1) + userPw[userPw.Length - 1];
                }
            }

            set
            {
                // 유저가 비밀번호를 지울때
                if (userPw.Length > value.Length)
                {
                    serverPw.Remove(value.Length, userPw.Length - value.Length);
                    userPw = value;
                }

                // 유저가 비밀번호를 작성할 때
                else if (userPw.Length < value.Length)
                {
                    userPw = value;
                    serverPw.Append(UserPw[userPw.Length - 1]);
                }

            }
        }

        public ObservableCollection<ProcessList> ProcessList
        {
            get { return processList; }
            set { processList = value; }
        }

        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        public string ExcelPath
        {
            get { return excelPath; }
            set { excelPath = value; }
        }

        private List<string> ReadExcel()
        {
            Excel.Application app = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            List<string> address = new List<string>();

            try
            {
                app = new Excel.Application();

                wb = app.Workbooks.Open(ExcelPath);
                ws = wb.Worksheets.get_Item("Sheet1") as Excel.Worksheet;


                for (int i = int.Parse(startRow); i <= int.Parse(endRow); i++)
                {
                    address.Add(ws.Cells[i, int.Parse(addressCol)].value);
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            finally
            {
                wb.SaveAs(savePath + @"/" + mainAddress, Excel.XlFileFormat.xlWorkbookDefault);
                wb.Close();
                app.Quit();
            }

            return address;
        }


        private bool SetExcel(string size, string owner, int count)
        {
            Excel.Application app = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                app = new Excel.Application();

                wb = app.Workbooks.Open(savePath + @"/" + mainAddress + ".xlsx");
                ws = wb.Worksheets.get_Item("Sheet1") as Excel.Worksheet;
                ws.Cells[count + int.Parse(startRow), int.Parse(ownerCol)].value = owner;
                ws.Cells[count + int.Parse(startRow), int.Parse(sizeCol)].value = size;
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            finally
            {
                wb.Save();
                wb.Close();
                app.Quit();
            }

            return true;
        }

        // 사이트 캡쳐
        private bool Capture(ChromeDriver driver, string bunzi, string ho)
        {
            try
            {
                Dictionary<string, Object> metrics = new Dictionary<string, Object>();
                metrics["width"] = driver.ExecuteScript("return Math.max(window.innerWidth,document.body.scrollWidth,document.documentElement.scrollWidth)");
                metrics["height"] = driver.ExecuteScript("return Math.max(window.innerHeight,document.body.scrollHeight,document.documentElement.scrollHeight)");
                metrics["deviceScaleFactor"] = (double)driver.ExecuteScript("return window.devicePixelRatio");
                metrics["mobile"] = driver.ExecuteScript("return typeof window.orientation !== 'undefined'");

                driver.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);

                string path_to_save_screenshot = SavePath + @"/" + bunzi + "-" + ho + ".png";
                driver.GetScreenshot().SaveAsFile(path_to_save_screenshot, ScreenshotImageFormat.Png);
                return true;
            }

            catch
            {
                return false;
            }



        }

        // 로그인 과정
        private void Login(ChromeDriver driver)
        {
            driver.FindElementByXPath("/html/body/div[7]/ul/li[3]/a").Click();
            driver.FindElementById("userId").SendKeys(UserId);
            driver.FindElementById("pwd").SendKeys(serverPw.ToString());
            driver.FindElementById("genLogin").Click();
        }

        // 문서 발급
        private void GetDocument(ChromeDriver driver, string bunzi, string hoo)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            // 토지(임야)대장 클릭
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"container\"]/div/section[4]/div/div[2]/div/ul/li[1]/div/a[2]")));

            driver.FindElementByXPath("//*[@id=\"container\"]/div/section[4]/div/div[2]/div/ul/li[1]/div/a[2]").Click();

            // 신청 버튼 누르기
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("applyBtn")));
            driver.FindElementById("applyBtn").Click();

            // 토지 대장 열람 창 전환
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[5]/div/div[1]/div[1]/a[3]")));
            driver.FindElementByXPath("/html/body/div[5]/div/div[1]/div[1]/a[3]").SendKeys(Keys.Enter);

            // 주소 입력 창 띄우기
            var handles_before = driver.WindowHandles;

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnAddress")));
            driver.FindElementById("btnAddress").Click();

            // 주소 입력 창으로 focus 전환         
            wait.Until(e => handles_before.Count != driver.WindowHandles.Count);

            driver.SwitchTo().Window(driver.WindowHandles[1]);

            // 주소 처리     
            driver.FindElementById("txtAddr").SendKeys(mainAddress);

            // 주소 검색 버튼 클릭
            driver.FindElementByXPath("//*[@id=\"frm_popup\"]/fieldset/div/div/span/button").SendKeys(Keys.Enter);
            // 자식 요소 검색
            var children = driver.FindElementById("resultList").FindElements(By.TagName("a"));
            bool isDone = false;

            string[] detailAddress = mainAddress.Trim().Split(new string[] { " " }, StringSplitOptions.None);

            string dong = detailAddress[2];
            string address = detailAddress[0] + " " + detailAddress[1];
            string extraction;
            string compareDong, compareAddress;

            // 주소 일치 판별 여부, 없으면 첫번째 인덱스 클릭
            for (int i = 2; i <= children.Count; i++)
            {
                extraction = driver.FindElementByXPath("//*[@id=\"resultList\"]/a[" + i.ToString() + "]/dl/dd/div").Text;
                compareAddress = extraction.Split('(')[0].Split(new string[] { " " }, StringSplitOptions.None)[0] + " " +
                    extraction.Split('(')[0].Split(new string[] { " " }, StringSplitOptions.None)[1];

                compareDong = extraction.Split('(')[1].Replace(")", string.Empty);

                if (compareDong == dong && compareAddress == address)
                {
                    driver.FindElementByXPath("//*[@id=\"resultList\"]/a[" + i.ToString() + "]/dl/dd/div").Click();
                    isDone = true;
                    break;
                }
            }

            // 첫번째 인덱스 클릭
            if (!isDone)
            {
                driver.FindElementByXPath("//*[@id=\"resultList\"]/a[2]").Click();
            }

            driver.SwitchTo().Window(driver.WindowHandles[0]);

            // 번지 입력
            driver.FindElementById("토지임야대장신청서_IN-토지임야대장신청서_신청토지소재지_주소정보_상세주소_번지").SendKeys(bunzi);
            driver.FindElementById("토지임야대장신청서_IN-토지임야대장신청서_신청토지소재지_주소정보_상세주소_호").SendKeys(hoo);
           
            

            // 연혁인쇄 설정
            driver.FindElementById("토지임야대장신청서_IN-토지임야대장신청서_연혁인쇄선택_.라디오코드_1").Click();
            // 제출 버튼
            driver.FindElementById("btn_end").Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(
                           By.XPath("//*[@id=\"EncryptionAreaID_0\"]/div[1]/table/tbody/tr[1]/td[4]/p[2]/span/a")));

        }

        private bool GetItem(ChromeDriver driver, string bunzi, string ho, int count)
        {
            string size = string.Empty;
            string name = string.Empty;

            // 번지 가져온 후 파일 체크
            string temp = driver.FindElementByXPath("//*[@id=\"EncryptionAreaID_0\"]/div[1]/table[2]/tbody/tr[1]/td[1]/table/tbody/tr[3]/td[2]").Text;
            string compare = string.Empty;

            if(string.IsNullOrEmpty(ho))
            {
                compare = bunzi;
            }

            else
            {
                compare = bunzi + "-" + ho;
            }

            if(temp != compare)
            {
                return false;
            }

            int pageNum = 1;
            string page = "[" + pageNum.ToString() + "]";
            string isEmpty = string.Empty;

            while(true)
            {

                for (int i = 4; i < 12; i += 2)
                {
                    isEmpty = driver.FindElementByXPath("//*[@id=\"EncryptionAreaID_0\"]/div" + page + "/table[2]/tbody/tr[2]/td/table/tbody/tr[" + i.ToString() + "]/td[3]").Text;
                
                
                    // 두번째 페이지 시작이 여백일 때
                    if (isEmpty.Trim() == "--- 이하 여백 ---" && i == 4)
                    {
                        size = driver.FindElementByXPath(
                            "//*[@id=\"EncryptionAreaID_0\"]/div/table[2]/tbody/tr[2]/td/table/tbody/tr[10]/td[2]/span").Text;
                        break;
                    }
                
                if (isEmpty.Trim() == "--- 이하 여백 ---")
                    {
                        size = driver.FindElementByXPath(
                           "//*[@id=\"EncryptionAreaID_0\"]/div" + page + "/table[2]/tbody/tr[2]/td/table/tbody/tr[" + (i - 2).ToString() + "]/td[2]/span").Text;
                        break;
                    }
                }
                
                
                if (pageNum >= 3)
                {
                    size = driver.FindElementByXPath(
                      "//*[@id=\"EncryptionAreaID_0\"]/div[2]/table[2]/tbody/tr[2]/td/table/tbody/tr[10]/td[2]/span").Text;
                    break;
                }
                
                if (string.IsNullOrEmpty(size))
                {
                    pageNum++;
                    page = "[" + pageNum.ToString() + "]";
                }
                
                else
                {
                    break;
                }

            }


            pageNum = 1;
            page = "[" + pageNum.ToString() + "]";
            isEmpty = string.Empty;

            while (true)
            {
                
                for (int i = 4; i < 12; i+=2)
                {            
                    isEmpty = driver.FindElementByXPath(
                        "//*[@id=\"EncryptionAreaID_0\"]/div" + page + "/table[2]/tbody/tr[2]/td/table/tbody/tr[" + i.ToString() + "]/td[5]").Text;
            
                // 두번째 페이지 시작하자마자 여백일 때
                if (isEmpty.Trim() == "--- 이하 여백 ---" && i == 4)
                    {
                        name = driver.FindElementByXPath("//*[@id=\"EncryptionAreaID_0\"]/div[1]/table[2]/tbody/tr[2]/td/table/tbody/tr[11]/td[2]").Text;
                        break;
                    }
            
                    if (isEmpty.Trim() == "--- 이하 여백 ---")
                    {
            
            
                        name = driver.FindElementByXPath(
                            "//*[@id=\"EncryptionAreaID_0\"]/div" + page + "/table[2]/tbody/tr[2]/td/table/tbody/tr[" + (i - 1).ToString() + "]/td[2]").Text;
            
                        break;
                    }
                }
            
            
                if (pageNum >= 3)
                {
                    name = driver.FindElementByXPath("//*[@id=\"EncryptionAreaID_0\"]/div[2]/table[2]/tbody/tr[2]/td/table/tbody/tr[11]/td[2]").Text;
                    break;
                }
            
                if (string.IsNullOrEmpty(name))
                {
                    pageNum++;
                    page = "[" + pageNum.ToString() + "]";
                }
            
                else
                {
                    break;
                }
            }

            SetExcel(size, name, count);
            return true;
        }

        // 발급된 토지대장들과 주소 비교 후 알맞은 데이터 가져옴
        private bool CheckDocument(ChromeDriver driver, int count, string bunzi, string ho)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            int index = 1;

            try
            {
                while (true)
                {
                    var handles_before = driver.WindowHandles;

                    driver.FindElementByXPath("//*[@id=\"EncryptionAreaID_0\"]/div[1]/table/tbody/tr[" + index.ToString() + "]/td[4]/p[2]/span/a").Click();
                    wait.Until(e => handles_before.Count != driver.WindowHandles.Count);

                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    if (GetItem(driver, bunzi, ho, count))
                    {
                        break;
                    }
                    else
                    {
                        index++;
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles[0]);
                        continue;
                    }

                }
            }

            catch
            {
                return false;
            }

            return true;

        }


        public async void StartRun()
        {
            await Task.Run(Run);
        }

        // 비동기 실행 
        private void Run()
        {
            List<string> address = ReadExcel();
            bool capture;
            bool crawl;

            string bunzi;
            string ho;

            for (int i = 0; i < address.Count; i++)
            {

                try
                {
                    bunzi = address[i].Split('-')[0].Replace("산", string.Empty).Trim();
                    ho = address[i].Split('-')[1].Trim();
                }

                catch
                {
                    bunzi = address[i].Replace("산", string.Empty).Trim();
                    ho = string.Empty;
                }


                using (ChromeDriver driver = new ChromeDriver())
                {
                    try
                    {
                        driver.Navigate().GoToUrl("https://www.gov.kr/nlogin/?Mcode=10003");

                        Login(driver);
                        GetDocument(driver, bunzi, ho);
                        crawl = CheckDocument(driver, i, bunzi, ho);
                        capture = Capture(driver, bunzi, ho);

                        DispatcherService.Invoke((System.Action)(() =>
                        {
                            processList.Add(new ProcessList()
                            {
                                address = bunzi + "-" + ho,
                                isCaptureDone = capture,
                                isCrawlDone = crawl,
                            });
                        }));

                    }

                    catch
                    {
                        DispatcherService.Invoke((System.Action)(() =>
                        {
                            processList.Add(new ProcessList()
                            {
                                address = bunzi + "-" + ho,
                                isCaptureDone = false,
                                isCrawlDone = false,
                            });
                        }));
                        driver.Quit();
                        continue;
                    }

                    finally
                    {
                        bunzi = string.Empty;
                        ho = string.Empty;
                        endCycle();
                    }
                }
            }

            endLoop();

        }
    }
}
