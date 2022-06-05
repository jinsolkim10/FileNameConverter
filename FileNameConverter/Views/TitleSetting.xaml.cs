using FileNameConverter.Model;
using Microsoft.VisualBasic.FileIO;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FileNameConverter.Views
{
    /// <summary>
    /// TitleSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TitleSetting : Page
    {
        public TitleSetting()
        {
            InitializeComponent();
        }
        List<string> nameList = new List<string>();
        List<string> titleList = new List<string>();
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
            if(nameList.Count == 0)
            {
                MessageBox.Show("파일을 불러와주세요");
            }
            if(cbxCheck.IsChecked == true)
            {
                foreach (string filename in nameList)
                {
                    var file = TagLib.File.Create(filename);
                    string name = System.IO.Path.GetFileNameWithoutExtension(filename);
                    file.Tag.Title = name;
                    file.Save();
                }
            }
            else
            {
                foreach (string filename in nameList)
                {              
                    var file = TagLib.File.Create(filename);
                    file.Tag.Title = tbxInputStr.Text;
                    file.Save();
                }
            }
            nameList.Clear();
        }
    }
}
