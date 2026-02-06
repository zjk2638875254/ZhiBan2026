using ExcelLib;
using System;
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
    public partial class Form_堆石坝: Form
    {
        public Form_堆石坝()
        {
            InitializeComponent();
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

        public delegate void RefreshEvent(string name, object value);
        public event RefreshEvent refresh = Form_V3.refresh_para;
        DataTable data_xy;

        private void 导入X线坐标表_Click(object sender, EventArgs e)
        {
            string xls_file = excel_lib.read_filepath();
            DataTable dt = excel_lib.GetData(xls_file);
            data_xy = dt;
            data_xys.DataSource = dt;
            data_xys.ReadOnly = true;
            refresh("TableX", data_xy);

            //测试好看
            start_x.Value = 4813028.382M;
            start_y.Value = 43379979.896M;
            start_z.Value = 836M;
            end_x.Value = 4813212.294M;
            end_y.Value = 43380652.147M;
            end_z.Value = 836M;
        }

        private void point_ValueChanged(object sender, EventArgs e)
        {
            point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
            point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
            refresh("PointStart", start);
            refresh("PointEnd", end);
        }

        private void rate_dam_input_SelectedIndexChanged(object sender, EventArgs e)
        {
            double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
            refresh("DamRate", dam_rate);

        }
    }
}
