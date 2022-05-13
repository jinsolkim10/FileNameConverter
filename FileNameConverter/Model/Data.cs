using FileNameConverter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileNameConverter.Model
{
    public class Data
    {
        public string startString = "";
        public int startNum = 1;
        public int count = 0;
        public FileSettingViewModel fileView { get; set; }

        public void checkNum(string text)
        {
            int i;
            if (int.TryParse(text.Replace(",", ""), out i))
            {
                text = i.ToString();
            }
            else
            {
                System.Windows.MessageBox.Show("숫자만 입력해주세요");
            }
        }
    }
}
