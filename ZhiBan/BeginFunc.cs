using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using ExcelLib;

namespace ZhiBan
{
    class BeginFunc
    {
        //方法2：等用户自己切分
        public static void math_calculate(DataTable data_xy, DataTable data_para, point start, point end, double dam_rate, double dam_len, ref string para_output_message)
        {
            try
            {
                ArrayList paras = new ArrayList();
                init_para(data_para, ref paras);
                ArrayList section_points = null;
                ArrayList base_points = null;
                ArrayList xys = new ArrayList();
                init_xy(data_xy, ref xys);
                all_cut_z(xys, start, end, dam_rate, ref section_points);

                Dictionary<point, double[]> dir = new Dictionary<point, double[]> { };
                regular_cut_z(xys, start, end, dam_rate, dam_len, ref base_points);

                point[] p1 = (point[])base_points[0];
                MessageBox.Show(p1[0].x.ToString());
                FuncDam.get_dir(start, end, dam_rate, base_points, paras, ref dir);
                point[] p2 = (point[])base_points[0];
                MessageBox.Show(p2[0].x.ToString());


                p1 = (point[])section_points[0];
                MessageBox.Show(p1[0].x.ToString());
                ArrayList points_collection = new ArrayList();
                FuncDam.main_dam(start, end, dam_rate, section_points, paras, ref points_collection, ref para_output_message);
                p2 = (point[])section_points[0];
                MessageBox.Show(p2[0].x.ToString());

                BentleyGeo.make_geo(points_collection);
                make_surfance(base_points, dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }


        //计算各边界点z
        private static void all_cut_z(ArrayList xys, point start, point end, double dam_rate, ref ArrayList section_points)
        {
            point[] points_z = new point[xys.Count];
            get_z_by_xy(start, end, dam_rate, xys, ref points_z);
            section_points = small_section(points_z);
        }

        private static void regular_cut_z(ArrayList xys, point start, point end, double dam_rate, double dam_len, ref ArrayList len_base_points)
        {
            point[] points_z = new point[xys.Count];
            get_z_by_xy(start, end, dam_rate, xys, ref points_z);
            len_base_points = len_section(points_z, dam_len);
        }

        #region 初始化Excel数据
        //使用DataTable读入xy点坐标
        private static void init_xy(DataTable data_xy, ref ArrayList xy)
        {
            try
            {
                Dictionary<int, string> xy_x = new Dictionary<int, string> { };
                Dictionary<int, string> xy_y = new Dictionary<int, string> { };
                excel_lib.get_col(data_xy, 0, ref xy_x);
                excel_lib.get_col(data_xy, 1, ref xy_y);
                for (int i = 1; i < xy_x.Count(); i++)
                {
                    Tuple<double, double> zs = new Tuple<double, double>(Convert.ToDouble(xy_x[i]), Convert.ToDouble(xy_y[i]));
                    xy.Add(zs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        //使用DataTable读入参数
        private static void init_para(DataTable data_para, ref ArrayList para_s)
        {
            try
            {
                Dictionary<int, string> para_ax = new Dictionary<int, string> { };
                Dictionary<int, string> para_bx = new Dictionary<int, string> { };
                Dictionary<int, string> para_ad = new Dictionary<int, string> { };
                Dictionary<int, string> para_bc = new Dictionary<int, string> { };
                Dictionary<int, string> para_t = new Dictionary<int, string> { };
                Dictionary<int, string> para_m = new Dictionary<int, string> { };
                Dictionary<int, string> para_l = new Dictionary<int, string> { };
                excel_lib.get_col(data_para, 0, ref para_ax);
                excel_lib.get_col(data_para, 1, ref para_bx);
                excel_lib.get_col(data_para, 2, ref para_ad);
                excel_lib.get_col(data_para, 3, ref para_bc);
                excel_lib.get_col(data_para, 4, ref para_t);
                excel_lib.get_col(data_para, 5, ref para_m);
                excel_lib.get_col(data_para, 6, ref para_l);

                for (int i = 1; i < para_ax.Count(); i++)
                {
                    section_para para = new section_para(Convert.ToDouble(para_ax[i]), Convert.ToDouble(para_bx[i]), Convert.ToDouble(para_ad[i]), Convert.ToDouble(para_bc[i]), Convert.ToDouble(para_t[i]), Convert.ToDouble(para_m[i]), Convert.ToDouble(para_l[i]));
                    para_s.Add(para);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        #endregion

        private static bool get_z_by_xy(point line1, point line2, double rate_dam, ArrayList xy, ref point[] results)
        {
            try
            {
                if (line1.z != line2.z)
                    return false;
                //line1+vector
                double[] line = { line1.x - line2.x, line1.y - line2.y, line1.z - line2.z };
                for (int i = 0; i < xy.Count; i++)
                {
                    Tuple<double, double> xy_i = (Tuple<double, double>)xy[i];
                    double[] xy_vector = { line1.x - xy_i.Item1, line1.y - xy_i.Item2, 0 };
                    double vec_mod = Math.Sqrt((xy_vector[0] * line[1] - xy_vector[1] * line[0]) * (xy_vector[0] * line[1] - xy_vector[1] * line[0]));
                    double z = vec_mod / Math.Sqrt(line[0] * line[0] + line[1] * line[1]);
                    results[i] = new point(xy_i.Item1, xy_i.Item2, Math.Round(line1.z - z / rate_dam, 6));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //三段采点，每个标准块采三点
        private static ArrayList small_section(point[] points)
        {
            ArrayList res = new ArrayList();
            double begin_len = 0.0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                int start_end = 0;
                if (i == 0)
                    start_end = -1;
                else if (i == points.Length - 2)
                    start_end = 1;
                else
                    start_end = 0;
                point[] sec = cut_points(points[i], points[i + 1], start_end, ref begin_len);
                res.Add(sec);
            }
            return res;
        }

        #region 间隔采点，每隔len米采点
        private static ArrayList len_section(point[] points, double len)
        {
            if (len == 0)
            {
                ArrayList original_points = new ArrayList();
                for (int i = 0; i < points.Length - 1; i++)
                {
                    point p1 = new point(points[i].x, points[i].y, points[i].z);
                    point p2 = new point(points[i + 1].x, points[i + 1].y, points[i + 1].z);
                    point[] sec = new point[2] { p1, p2 };
                    original_points.Add(sec);
                }
                return original_points;
            }

            ArrayList res = new ArrayList();
            double begin_len = 0.0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                bool is_end = false;
                if (i == points.Length - 2)
                    is_end = true;
                point[] sec = len_cut_points(points[i], points[i + 1], len, is_end, ref begin_len);
                res.Add(sec);
            }
            return res;
        }

        private static point[] len_cut_points(point A, point B, double len, bool is_end, ref double begin_len)
        {
            double sum_ab = Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y) + (A.z - B.z) * (A.z - B.z));
            int sum = (int)((sum_ab - begin_len) / len);
            double last_len = sum_ab - sum * len - begin_len;

            double[] dir_ab = new double[3] { B.x - A.x, B.y - A.y, B.z - A.z };
            unit_vec(ref dir_ab);
            point[] res_points = new point[sum + 1];
            if (!is_end)
            {
                for (int i = 0; i <= sum; i++)
                {
                    res_points[i] = new point(A.x + dir_ab[0] * (begin_len + i * len), A.y + dir_ab[1] * (begin_len + i * len), A.z + dir_ab[2] * (begin_len + i * len));
                }
            }
            else
            {
                res_points = new point[sum + 2];
                for (int i = 0; i <= sum; i++)
                {
                    res_points[i] = new point(A.x + dir_ab[0] * (begin_len + i * len), A.y + dir_ab[1] * (begin_len + i * len), A.z + dir_ab[2] * (begin_len + i * len));
                }
                res_points[sum + 1] = new point(B.x, B.y, B.z);
            }
            begin_len = len - last_len;
            return res_points;
        }

        private static bool unit_vec(ref double[] v)
        {
            try
            {
                double mod = 0;
                for (int i = 0; i < v.Length; i++)
                    mod += v[i] * v[i];
                mod = Math.Sqrt(mod);
                for (int i = 0; i < v.Length; i++)
                    v[i] = v[i] / mod;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        //强制裁段，避免拟合后出界
        private static point[] cut_points(point A, point B, int start_end, ref double begin_len)
        {
            double[] dir_ab = new double[3] { B.x - A.x, B.y - A.y, B.z - A.z };
            double mod = Math.Sqrt(dir_ab[0] * dir_ab[0] + dir_ab[1] * dir_ab[1] + dir_ab[2] * dir_ab[2]);
            dir_ab[0] = dir_ab[0] / mod; dir_ab[1] = dir_ab[1] / mod; dir_ab[2] = dir_ab[2] / mod;
            if (start_end == -1)
            {
                point[] res_points = new point[3];
                res_points[0] = new point(A.x, A.y, A.z);
                res_points[1] = new point((A.x + B.x) / 2.0 - dir_ab[0], (A.y + B.y) / 2.0 - dir_ab[1], (A.z + B.z) / 2.0 - dir_ab[2]);
                res_points[2] = new point((A.x + B.x) / 2.0 + dir_ab[0], (A.y + B.y) / 2.0 + dir_ab[1], (A.z + B.z) / 2.0 + dir_ab[2]);
                return res_points;
            }
            else if(start_end == 1)
            {
                point[] res_points = new point[3];
                res_points[0] = new point((A.x + B.x) / 2.0 - dir_ab[0], (A.y + B.y) / 2.0 - dir_ab[1], (A.z + B.z) / 2.0 - dir_ab[2]);
                res_points[1] = new point((A.x + B.x) / 2.0 + dir_ab[0], (A.y + B.y) / 2.0 + dir_ab[1], (A.z + B.z) / 2.0 + dir_ab[2]);
                res_points[2] = new point(B.x, B.y, B.z);
                return res_points;
            }
            else
            {
                point[] res_points = new point[2];
                res_points[0] = new point((A.x + B.x) / 2.0 - dir_ab[0], (A.y + B.y) / 2.0 - dir_ab[1], (A.z + B.z) / 2.0 - dir_ab[2]);
                res_points[1] = new point((A.x + B.x) / 2.0 + dir_ab[0], (A.y + B.y) / 2.0 + dir_ab[1], (A.z + B.z) / 2.0 + dir_ab[2]);
                return res_points;
            }
        }

        //构造面
        private static bool make_surfance(ArrayList plist, Dictionary<point,double[]> dic)
        {
            try
            {
                for (int i = 0; i < plist.Count; i++)
                {
                    point[] p_list = (point[])plist[i];
                    for(int j=0;j<p_list.Length;j++)
                    {
                        double[] v1 = new double[3] { dic[p_list[j]][0], dic[p_list[j]][1], dic[p_list[j]][2] };
                        double[] v2 = new double[3] { dic[p_list[j]][3], dic[p_list[j]][4], dic[p_list[j]][5] };
                        BentleyGeo.make_cut_surfance(p_list[j], v1, v2);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
