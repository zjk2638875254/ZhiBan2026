using ExcelLib;
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
    public partial class Form智能设计: Form
    {
        public Form智能设计()
        {
            InitializeComponent();
        }

        static DataTable data_xy = null;
        static DataTable data_para = null;
        static double data_dam_rate = 1.0;

        private void 导入X线坐标表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xls_file = excel_lib.read_filepath();
            DataTable dt = excel_lib.GetData(xls_file);
            data_xy = dt;
            data_xys.DataSource = dt;
            data_xys.ReadOnly = true;

            //测试好看
            start_x.Value = 4813028.382M;
            start_y.Value = 43379979.896M;
            start_z.Value = 836M;
            //4813128.341, 43380128.711
            //4813177.617, 43380322.545
            //end_x.Value = 4813028.382M + 4813177.617M - 4813128.341M;
            //end_y.Value = 43379979.89M + 43380322.545M - 43380128.711M;
            end_x.Value = 4813212.294M;
            end_y.Value = 43380652.147M;
            end_z.Value = 836M;
        }

        private void 面板建模ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool direction_ab = true;
            if (DirABFalse.Checked)
                direction_ab = false;
            point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
            point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
            double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
            string message = "";


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
            面板_Bentley.test_high(mianban_high_list);


            ArrayList paras = new ArrayList();
            //section_para para = new section_para(Convert.ToDouble(para_ax[i]), Convert.ToDouble(para_bx[i]), Convert.ToDouble(para_ad[i]), Convert.ToDouble(para_bc[i]), Convert.ToDouble(para_t[i]), Convert.ToDouble(para_m[i]), Convert.ToDouble(para_l[i]));
            for (int i = 0; i < mianban_high_list.Count - 1; i++)
            {
                point[] p_list_left = (point[])mianban_high_list[i];
                point p1 = p_list_left[1];
                point[] p_list_right = (point[])mianban_high_list[i + 1];
                point p2 = p_list_right[1];
                if(p1.z >= Convert.ToDouble(HeightLimit1.Value) || p2.z >= Convert.ToDouble(HeightLimit1.Value))
                {
                    double lNum = Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.x - p2.x) * (p1.y - p2.y) + (p1.y - p2.y) * (p1.z - p2.z));
                    section_para para = new section_para(Convert.ToDouble(LaxNum1.Value), Convert.ToDouble(LbxNum1.Value), Convert.ToDouble(LadNum1.Value), Convert.ToDouble(LbcNum1.Value), Convert.ToDouble(tNum1.Value), Convert.ToDouble(mNum1.Value), lNum);
                    paras.Add(para);
                }
                else if (p1.z >= Convert.ToDouble(HeightLimit2.Value) || p2.z >= Convert.ToDouble(HeightLimit2.Value))
                {
                    double lNum = Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.x - p2.x) * (p1.y - p2.y) + (p1.y - p2.y) * (p1.z - p2.z));
                    section_para para = new section_para(Convert.ToDouble(LaxNum2.Value), Convert.ToDouble(LbxNum2.Value), Convert.ToDouble(LadNum2.Value), Convert.ToDouble(LbcNum2.Value), Convert.ToDouble(tNum2.Value), Convert.ToDouble(mNum2.Value), lNum);
                    paras.Add(para);
                }
                else
                {
                    double lNum = Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.x - p2.x) * (p1.y - p2.y) + (p1.y - p2.y) * (p1.z - p2.z));
                    section_para para = new section_para(Convert.ToDouble(LaxNum3.Value), Convert.ToDouble(LbxNum3.Value), Convert.ToDouble(LadNum3.Value), Convert.ToDouble(LbcNum3.Value), Convert.ToDouble(tNum3.Value), Convert.ToDouble(mNum3.Value), lNum);
                    paras.Add(para);
                }
            }
            面板_FuncDam.multi_面板(data_xy, paras, start, end, dam_rate, mianban_high_list, ref message);
        }

        private void Form智能设计_Load(object sender, EventArgs e)
        {
            rate_dam_input.Items.Add("0.8");
            rate_dam_input.Items.Add("0.9");
            rate_dam_input.Items.Add("1.0");
            rate_dam_input.Items.Add("1.1");
            rate_dam_input.Items.Add("1.2");
            rate_dam_input.Items.Add("1.3");
            rate_dam_input.Items.Add("1.4");
            rate_dam_input.Items.Add("1.5");
            rate_dam_input.Items.Add("1.6");
        }
    }
}
