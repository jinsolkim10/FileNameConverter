using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
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

namespace FileNameConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int startnum = 1;
        string startstring = "";
        List<string> nameList = new List<string>();
        List<string> folderNameList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        protected void fileOpen()
        {
            nameList.Clear();
            tbxConut.Text = "";
            int cnt = 0;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == true)
            {
                foreach (string filename in dlg.FileNames)
                {
                    cnt++;
                    nameList.Add(filename);
                }
                tbxConut.Text = cnt.ToString();
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            fileOpen();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            startstring = tbxInputStr.Text;
            startnum = int.Parse(tbxInputNum.Text);
            foreach (string filename in nameList)
            {
                if (startnum >= 1 & startnum < 9)
                {
                    string fmt = "00.##";
                    FileSystem.RenameFile(filename, startnum.ToString(fmt) + startstring);
                }
                else if (startnum > 10 & startnum < 100)
                {
                    string fmt = "000.##";
                    FileSystem.RenameFile(filename, startnum.ToString(fmt) + startstring);
                }
                else
                {
                    string fmt = "0000.##";
                    FileSystem.RenameFile(filename, startnum.ToString(fmt) + startstring);
                }
                startnum++;
            }
            nameList.Clear();
        }

        private void btnFolderCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateFolderList();
            CreateFolder();
        }

        private void CreateFolderList()
        {
            startstring = tbxInputStr.Text;
            startnum = int.Parse(tbxInputNum.Text);
            folderNameList = new List<string>();

            if (tbxInputFolderAmount.Text == null)
            {
                System.Windows.MessageBox.Show("생성할 폴더의 수를 입력해주세요");
            }
            for (int i = 1; i <= int.Parse(tbxInputFolderAmount.Text); i++)
            {
                if (startnum >= 1 & startnum < 100)
                {
                    string fmt = "00.##";
                    folderNameList.Add(startnum.ToString(fmt) + startstring);
                }
                else if (startnum >= 100 & startnum < 1000)
                {
                    string fmt = "000.##";
                    folderNameList.Add(startnum.ToString(fmt) + startstring);
                }
                startnum++;
            }
            startnum = 1;
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

        private void tbxInputNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;

            if (int.TryParse(tbxInputNum.Text.Replace(",", ""), out i))
            {
                tbxInputNum.Text = i.ToString();
            }
            else
            {
                System.Windows.MessageBox.Show("숫자만 입력해주세요");
            }
        }

        private void tbxInputFolderAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;

            if (int.TryParse(tbxInputFolderAmount.Text.Replace(",", ""), out i))
            {
                tbxInputFolderAmount.Text = i.ToString();
            }
            else
            {
                System.Windows.MessageBox.Show("숫자만 입력해주세요");
            }
        }

        private void btnFindDirectory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxFolderPath.Text = dlg.SelectedPath;
            }
        }



    }
}
