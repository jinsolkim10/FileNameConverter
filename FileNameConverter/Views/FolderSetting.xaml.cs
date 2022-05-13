using FileNameConverter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FileNameConverter.Views
{
    /// <summary>
    /// FolderSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FolderSetting : Page
    {
        public FolderSetting()
        {
            InitializeComponent();
        }

        List<string> folderNameList = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxFolderPath.Text = dlg.SelectedPath;
            }
        }
        private void btnFolderCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateFolderList();
            CreateFolder();
        }

        private void tbxInputFolderAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            Data data = new Data();
            data.checkNum(tbxInputFolderAmount.Text);
        }

        private void tbxInputNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            Data data = new Data();
            data.checkNum(tbxInputNum.Text);
        }

        private void CreateFolderList()
        {
            Data data = new Data();
            data.startString = tbxInputStr.Text;
            data.startNum = int.Parse(tbxInputNum.Text);
            folderNameList = new List<string>();
            if (tbxInputFolderAmount.Text == null)
            {
                System.Windows.MessageBox.Show("생성할 폴더의 수를 입력해주세요");
            }
            if (cbxCheck.IsChecked == true)
            {
                for (int i = 1; i <= int.Parse(tbxInputFolderAmount.Text); i++)
                {
                    if (data.startNum > 0 & data.startNum < 10)
                    {
                        string fmt = "00.##";
                        folderNameList.Add(data.startNum.ToString(fmt) + data.startString);
                    }
                    else if (data.startNum >= 10 & data.startNum < 100)
                    {
                        string fmt = "000.##";
                        folderNameList.Add(data.startNum.ToString(fmt) + data.startString);
                    }
                    else
                    {
                        string fmt = "0000.##";
                        folderNameList.Add(data.startNum.ToString(fmt)+data.startString);
                    }
                    data.startNum++;
                }
            }
            else
            {
                for (int i = 1; i <= int.Parse(tbxInputFolderAmount.Text); i++)
                {
                    folderNameList.Add(data.startNum.ToString() + data.startString);
                    data.startNum++;
                }
            }
            lblStateChange();
            //data.startNum = 1;
        }

        private void CreateFolder()
        {
            foreach (string foldeName in folderNameList)
            {
                string path1 = tbxFolderPath.Text;
                string path2 = System.IO.Path.Combine(path1, foldeName);
                Directory.CreateDirectory(path2);
            }
        }

        private void lblStateChange()
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromTicks(13000000);
            timer.Start();
            lblState.Content = "변경이 완료되었습니다";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblState.Content = "";
            timer.Stop();
        }
    }
}
