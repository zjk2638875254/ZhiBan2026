using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    public partial class Form_面板: Form
    {
        public Form_面板()
        {
            InitializeComponent();
        }

        public delegate void RefreshEvent(string name, object value);
        public event RefreshEvent refresh = Form_V3.refresh_para;

        private void Cut面板_Click(object sender, EventArgs e)
        {
            point start = Form_V3.point_start;
            point end = Form_V3.point_end;
            DataTable data_xy = Form_V3.data_x;
            double dam_rate = Form_V3.dam_rate;

            double all_len = Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y) + (start.z - end.z) * (start.z - end.z));
            int num1 = Convert.ToInt32(面板Num1.Value);
            //int num2 = Convert.ToInt32(面板Num1.Value);
            int num3 = Convert.ToInt32(面板Num3.Value);
            int num2 = Convert.ToInt32(Math.Ceiling((all_len - num1 * Convert.ToDouble(面板Len1.Value) - num3 * Convert.ToDouble(面板Len3.Value)) / Convert.ToDouble(面板Len2.Value))) - 1;
            double[] len_list = new double[num1 + num2 + num3];
            for (int i = 0; i < num1; i++)
                len_list[i] = Convert.ToDouble(面板Len1.Value);
            for (int i = num1; i < num1 + num2; i++)
                len_list[i] = Convert.ToDouble(面板Len2.Value);
            for (int i = num1 + num2; i < num1 + num2 + num3 - 1; i++)
                len_list[i] = Convert.ToDouble(面板Len3.Value);
            len_list[num1 + num2 + num3 - 1] = all_len - num1 * Convert.ToDouble(面板Len1.Value) - num2 * Convert.ToDouble(面板Len2.Value) - num3 * Convert.ToDouble(面板Len3.Value) + Convert.ToDouble(面板Len3.Value);
            ArrayList mianban_high_list = new ArrayList();
            面板_FuncHigh.get_location(data_xy, start, end, dam_rate, len_list, ref mianban_high_list);
            //返回
            refresh("MianBanArray", mianban_high_list);
            string message = "";
            for (int i = 0; i < mianban_high_list.Count; i++)
            {
                point[] list = (point[])mianban_high_list[i];
                message += "第" + i.ToString() + "个面板分界点坐标为：(" + list[0].x.ToString() + "," + list[0].y.ToString() + "," + list[0].z.ToString() + ")\r\n";
            }
            TextBox.Text = message;
            面板_Bentley.test_high(mianban_high_list);
        }
    }
}
