using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            timer_Display.Start();//启动显示定时器
            label_name.Text = "";
            label_default.Text = "中水东北\r\n勘测设计研究\r\n有限责任公司";
            label1.Text = "水工设计处";
            label_name.Parent = pictureBox1;
            //pictureBox2.Image = Pic.办公猫;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private const string flash = "趾板结构体型\r\n参数化设计\r\n系统";
        private int stopCount = flash.Length + 30;//停止时间
        private int count = 0;
        private bool finish = false;


        /// <summary>
        /// 窗口定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Display_Tick(object sender, EventArgs e)
        {
            if (finish == false)
            {
                label_name.Text += flash.Substring(count, 1); //逐个显示文字 
            }
            //累加计数
            count++;
            if (count == flash.Length)
            {
                //CheckProgramProcess();
                finish = true;//文字显示完成
            }
            else if (count >= stopCount)
            {
                timer_Display.Stop();
                this.Close();//关闭窗口
            }
        }

        private bool CheckProgramProcess()
        {
            Process[] myProcesses = Process.GetProcessesByName(Application.ProductName);
            if (myProcesses.Length > 1) //如果可以获取到本进程名大于1个，则说明在此之前已经启动过
            {
                timer_Display.Stop();
                MessageBox.Show("检测到程序已经运行，请先关闭多余的程序或进程！\n");
                Application.Exit();//关闭
            }
            return true;
        }
    }
}
