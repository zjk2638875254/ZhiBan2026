using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelLib;
namespace ZhiBan
{
    public partial class MainForm : Form
    {
        static DataTable data_xy = null;
        static DataTable data_para = null;
        static double data_dam_rate = 1.0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Show_Load(object sender, EventArgs e)
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

        private void SaveFile_Click(object sender, EventArgs e)
        {
            string file_path = DataFile.get_file_path();
            DamData dd = new DamData(data_xy, data_para, data_dam_rate);
            DataFile.save_as_byte(file_path, dd);
        }

        private void ReadFile_Click(object sender, EventArgs e)
        {
            string file_path = DataFile.read_file_path();
            DataFile.read_as_byte(file_path);
        }

        private void 清除文本框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_show.Text = "";
        }

        private void 分块调整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ReviseBlock rb = new ReviseBlock(len_xyz);
            //rb.refresh += MainForm.refresh_para;
        }

        private void 导入X线坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xls_file = excel_lib.read_filepath();
            DataTable dt = excel_lib.GetData(xls_file);
            data_xy = dt;
            data_xys.DataSource = dt;
            data_xys.ReadOnly = true;


            //4813128.341, 43380128.711
            //4813177.617, 43380322.545
            //end_x.Value = 4813028.382M + 4813177.617M - 4813128.341M;
            //end_y.Value = 43379979.89M + 43380322.545M - 43380128.711M;

            /*上库盆
            start_x.Value = 4813028.382M;
            start_y.Value = 43379979.896M;
            start_z.Value = 836M;
            end_x.Value = 4813212.294M;
            end_y.Value = 43380652.147M;
            end_z.Value = 836M;
            */
            start_x.Value = 37566495.0994M;
            start_y.Value = 3886257.9662M;
            start_z.Value = 388.5M;
            end_x.Value = 37566275.1199M;
            end_y.Value = 3886118.945M;
            end_z.Value = 388.5M;
        }

        private void 导入体型参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xls_file = excel_lib.read_filepath();
            DataTable dt = excel_lib.GetData(xls_file);
            data_para = dt;
            data__paras.DataSource = dt;
            data__paras.ReadOnly = true;
        }

        private void 规则块建模ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
                point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
                double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
                double data_dam_len = Convert.ToDouble(dam_len.Value);
                string message = "";
                BeginFunc_V1.math_calculate(data_xy, data_para, start, end, dam_rate, data_dam_len, ref message);
                txt_show.Text += message;
                */
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }

        private void 整体建模ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
                point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
                double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
                double data_dam_len = Convert.ToDouble(dam_len.Value);
                string message = "";
                BeginFunc.math_calculate(data_xy, data_para, start, end, dam_rate, data_dam_len, ref message);
                txt_show.Text += message;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }

        private void 导入分段信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 分块自由建模ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
            point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
            double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
            string message = "";
            BeginFunc_AutoCut._init_(data_xy, data_para, start, end, dam_rate, ref message);
            txt_show.Text += message;
        }

        private void 测试按板长建模ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 导入坝轴线坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xls_file = excel_lib.read_filepath();
            DataTable dt = excel_lib.GetData(xls_file);

            start_x.Value = Convert.ToDecimal(dt.Rows[1][1].ToString());
            start_y.Value = Convert.ToDecimal(dt.Rows[1][2].ToString());
            start_z.Value = Convert.ToDecimal(dt.Rows[1][3].ToString());
            end_x.Value = Convert.ToDecimal(dt.Rows[2][1].ToString());
            end_y.Value = Convert.ToDecimal(dt.Rows[2][2].ToString());
            end_z.Value = Convert.ToDecimal(dt.Rows[2][3].ToString());
        }
    }
}
