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
using System.Net;
using System.Text.RegularExpressions;

namespace 电费
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<ComboxBind> lstCmbBind1 = new List<ComboxBind>();//用于绑定数据源
        List<ComboxBind> lstCmbBind2 = new List<ComboxBind>();//用于绑定数据源


       private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //初始化数据源
            for (int i = 1; i <= 20; i++)
            {

                ComboxBind louceng = new ComboxBind(i.ToString());
                lstCmbBind1.Add(louceng);
            }
            for (int j = 1; j <= 27; j++)
            {
                if (j <= 9)
                {
                    string j_value = "0" + j.ToString();
                    ComboxBind qinshi = new ComboxBind(j_value);
                    lstCmbBind2.Add(qinshi);
                }
                else
                {
                    ComboxBind qinshi = new ComboxBind(j.ToString());
                    lstCmbBind2.Add(qinshi);
                }

            }

            this.comBox1.ItemsSource = lstCmbBind1;
            this.comBox2.ItemsSource = lstCmbBind2;
            comBox1.DisplayMemberPath = "CmdText";//类ComboxBind中的属性

            comBox2.DisplayMemberPath = "CmdText";//类ComboxBind中的属性
        }

        public string DownloadCode()
        {
            string Lou, Qin, Posit;
            Lou = comBox1.Text;
            Qin = comBox2.Text;
            
            if (button_west.IsChecked == false&&button_east.IsChecked==true)
            {
                Posit = "1";
            }
            else
            {
                Posit = "2";
            }

            string Url = "http://h5cloud.17wanxiao.com:8080/CloudPayment/user/getRoomState.do?payProId=726&schoolcode=43&businesstype=2&roomverify=-36--" + Lou + "-" + Posit + Lou + Qin;
            try
            {
                WebClient webClient = new WebClient();
                Byte[] pageData = webClient.DownloadData(Url);
                return Encoding.GetEncoding("utf-8").GetString(pageData);
            }
            catch (Exception ec)
            {
                throw new Exception(ec.Message.ToString());
            }
        }

        public string DianFei()
        {
            string pattern = "(?<=quantity\":\")(.*?)(?=\",\"q)";
            Match match = Regex.Match(DownloadCode(), pattern);
            while(!match.Success)
            {
                return null;
            }
            return match.Value;
        }


        private void BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DianFei()=="0.0")
                {
                    MessageBox.Show("出错，请核实输入是否有误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("您的电量剩余"+DianFei()+"度","电量余额",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                     
            }
            catch (Exception)
            {
                MessageBox.Show("出错，请核实输入是否有误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void button_west_MouseMove(object sender, MouseEventArgs e)
        {
            button_west.Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x9A, 0xFE));
            button_west.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0XFF, 0XFF));
        }

        private void button_west_MouseLeave(object sender, MouseEventArgs e)
        {

            button_west.Foreground = new SolidColorBrush(Color.FromRgb(0x2E, 0x9A, 0xFE));
            button_west.Background = new SolidColorBrush(Color.FromRgb(0xFA, 0XFA, 0XFA));
        }

        private void button_east_MouseMove(object sender, MouseEventArgs e)
        {
            button_east.Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x9A, 0xFE));
            button_east.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0XFF, 0XFF));
        }

        private void button_east_MouseLeave(object sender, MouseEventArgs e)
        {
            button_east.Foreground = new SolidColorBrush(Color.FromRgb(0x2E, 0x9A, 0xFE));
            button_east.Background = new SolidColorBrush(Color.FromRgb(0xFA, 0XFA, 0XFA)); 
        }

    }
}
