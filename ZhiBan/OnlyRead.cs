using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiBan
{
    class OnlyRead
    {
        #region
        /*
        private void math_calculate_Click(object sender, EventArgs e)
        {
            if (data_xy == null)
            {
                MessageBox.Show("请读入趾板X线坐标", "警告1");
                return;
            }

            if (data_para == null)
            {
                MessageBox.Show("请读入趾板结构体型参数", "警告2");
                return;
            }

            if (rate_dam_input.SelectedItem == null)
            {
                MessageBox.Show("请选择面板坝坡比", "警告3");
                return;
            }
            try
            {
                point start = new point(Convert.ToDouble(start_x.Value), Convert.ToDouble(start_y.Value), Convert.ToDouble(start_z.Value));
                point end = new point(Convert.ToDouble(end_x.Value), Convert.ToDouble(end_y.Value), Convert.ToDouble(end_z.Value));
                double dam_rate = Convert.ToDouble(rate_dam_input.SelectedItem);
                ArrayList xys = new ArrayList();
                init_xy(ref xys);
                ArrayList paras = new ArrayList();
                init_para(ref paras);
                string para_output_message = "";
                if (section_point_list != null)
                {
                    xys = section_point_list;
                }
                else
                {
                    MessageBox.Show("请先完成趾板分块", "警告4");
                    return;
                }
                ArrayList points_collection = new ArrayList();
                FuncDam.main_dam(start, end, dam_rate, xys, paras, ref points_collection, ref para_output_message);
                BentleyGeo.make_geo(points_collection);
                mess2 = para_output_message;
                txt_show.Text += mess2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }
        */
        #endregion
    }
}
