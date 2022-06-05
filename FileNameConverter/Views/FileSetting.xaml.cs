using FileNameConverter.Model;
using FileNameConverter.ViewModels;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FileNameConverter.Views
{
    /// <summary>
    /// FileSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FileSetting : Page
    {
        public FileSetting()
        {
            InitializeComponent();
        }

        List<string> nameList = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Data data = new Data();
            tbxConut.Text = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == true)
            {
                foreach (string filename in dlg.FileNames)
                {
                    data.count++;
                    nameList.Add(filename);
                }
                tbxConut.Text = data.count.ToString() + " 개의 파일이 발견되었습니다";
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Data data = new Data();
            data.startString = tbxInputStr.Text;
            data.startNum = int.Parse(tbxInputNum.Text);
            if (tbxInputNum.Text == null)
            {
                MessageBox.Show("생성할 파일의 번호를 입력해주세요");
            }
            if (cbxCheck.IsChecked == true)
            {
                foreach (string filename in nameList)
                {
                    if (data.startNum > 0 & data.startNum < 10)
                    {
                        string fmt = "00.##";
                        FileSystem.RenameFile(filename, data.startNum.ToString(fmt) + data.startString);
                    }
                    else if (data.startNum >= 10 & data.startNum < 100)
                    {
                        string fmt = "000.##";
                        FileSystem.RenameFile(filename, data.startNum.ToString(fmt) + data.startString);
                    }
                    else
                    {
                        string fmt = "0000.##";
                        FileSystem.RenameFile(filename, data.startNum.ToString(fmt) + data.startString);
                    }
                    data.startNum++;
                }
                nameList.Clear();
            }
            else
            {
                foreach (string filename in nameList)
                {
                    FileSystem.RenameFile(filename, data.startNum.ToString() + data.startString);
                    data.startNum++;
                }
                nameList.Clear();
            }
            lblStateChange();
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
