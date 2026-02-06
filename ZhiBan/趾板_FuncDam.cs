using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class 趾板_FuncDam
    {
        public static ArrayList section(point[] points, double len)
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
                point[] sec = cut_points(points[i], points[i + 1], len, is_end, ref begin_len);
                res.Add(sec);
            }
            return res;
        }

        //写入文件
        private static string save_filepath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //打开的文件选择对话框上的标题
            saveFileDialog.Title = "请选择存储位置";
            //设置文件类型
            saveFileDialog.Filter = "*.obj(Obj文件)|*.obj";
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //按下确定选择的按钮
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径
                string localFilePath = saveFileDialog.FileName.ToString();
                System.IO.FileStream ft = System.IO.File.Create(localFilePath);
                ft.Close();

                string mtl_path = localFilePath.Replace(".obj", ".mtl");
                System.IO.FileStream ft_mtl = System.IO.File.Create(mtl_path);
                ft_mtl.Close();

                return localFilePath;
            }
            return "D://default.obj";
        }

        #region 数学计算
        //趾板分块
        private static point[] cut_points(point A, point B, double len, bool is_end, ref double begin_len)
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
        //已知两个向量求第三个向量（z！= 0）
        private static double[] get_vec(double[] v1, double[] v2)
        {
            double[] v3 = new double[3] { 1, 1, 1 };
            try
            {
                //向量叉乘
                v3[0] = v1[1] * v2[2] - v2[1] * v1[2];
                v3[1] = (-1) * (v1[0] * v2[2] - v2[0] * v1[2]);
                v3[2] = v1[0] * v2[1] - v2[0] * v1[1];

                unit_vec(ref v3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告");
            }
            return v3;
        }
        private static double[] get_cz_vec(double[] v, point bp, point tp)
        {
            double[] res = new double[3] { v[1], (-1) * v[0], 0 };
            double old_len = Math.Sqrt((bp.x - tp.x) * (bp.x - tp.x) + (bp.y - tp.y) * (bp.y - tp.y) + (bp.z - tp.z) * (bp.z - tp.z));
            double new_len = Math.Sqrt((bp.x - tp.x + res[0]) * (bp.x - tp.x + res[0]) + (bp.y - tp.y + res[1]) * (bp.y - tp.y + res[1]) + (bp.z - tp.z) * (bp.z - tp.z));
            if (new_len > old_len)
            {
                res[0] = (-1) * res[0];
                res[1] = (-1) * res[1];
            }
            return res;
        }

        //向量单位化
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

        //1月更新-构造单块趾板
        public static bool single_dam(point line_s, point line_e, double rate_dam, point start_p, point end_p, section_para SecPara, ref ArrayList points_list, ref string output_message)
        {
            try
            {

                double[] dir_se = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                double[] test = new double[3] { end_p.x - start_p.x, end_p.y - start_p.y, end_p.z - start_p.z };
                unit_vec(ref test);
                points_list.Clear();
                if (!single_section(dir_se, line_s.z, SecPara, start_p, end_p, rate_dam, ref points_list, ref output_message))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        //1月更新-融合趾板
        public static bool merge_dam(point line_s, point line_e, point[] merge_before1, point[] merge_before2, point[] merge_after1, point[] merge_after2, ref ArrayList points_list)
        {
            try
            {
                double[] dir_se = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                double[] vec_dam = new double[2] { dir_se[0], dir_se[1] };
                point base_p = new point((line_s.x + line_e.x) / 2.0, (line_s.y + line_e.y) / 2.0, (line_s.z + line_e.z) / 2.0);
                point[] merge_res1 = null;
                point[] merge_res2 = null;
                
                //FuncMerge.merge_main(merge_before1, merge_before2, merge_after1, merge_after2, vec_dam, base_p, ref merge_res1, ref merge_res2);
                面板_FuncMerge.merge_main_precise(merge_before1, merge_before2, merge_after1, merge_after2, vec_dam, base_p, ref merge_res1, ref merge_res2);

                points_list.Clear();
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_before2[t]);
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_res1[t]);
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_res1[t]);
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_res2[t]);
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_res2[t]);
                for (int t = 0; t < 7; t++)
                    points_list.Add(merge_after1[t]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        #region 几何计算
        //坐标转换到原点附近
        public static bool convert_xyz(ref point first_point, ref point last_point, ref ArrayList xys)
        {
            try
            {
                ArrayList input = new ArrayList();
                double min_x = first_point.x, min_y = first_point.y;

                if (last_point.x < min_x)
                    min_x = last_point.x;
                if (last_point.y < min_y)
                    min_y = last_point.y;

                for (int i = 0; i < xys.Count; i++)
                {
                    point[] input_list = (point[])xys[i];
                    for (int j = 0; j < input_list.Length; j++)
                    {
                        if (input_list[j].x < min_x)
                            min_x = input_list[j].x;
                        if (input_list[j].y < min_y)
                            min_y = input_list[j].y;
                    }
                }
                ArrayList revise_xys = new ArrayList();
                for (int i = 0; i < xys.Count; i++)
                {
                    point[] input_list = (point[])xys[i];
                    point[] pl = new point[input_list.Length];
                    for (int j = 0; j < input_list.Length; j++)
                    {
                        point p = new point(input_list[j].x - min_x, input_list[j].y - min_y, input_list[j].z);
                        pl[j] = p;
                    }
                    revise_xys.Add(pl);
                }
                first_point.x = first_point.x - min_x;
                first_point.y = first_point.y - min_y;

                last_point.x = last_point.x - min_x;
                last_point.y = last_point.y - min_y;
                xys.Clear();
                for (int i = 0; i < revise_xys.Count; i++)
                    xys.Add(revise_xys[i]);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //获取X线上点的真实坐标和坝面法向量
        //需要先将坐标系平移至坝轴线，以坝轴线为z=0横线
        public static bool get_plane(point line1, point line2, double rate_dam, ArrayList xy, ref point[] results, ref double[] dam_dir_xy)
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

                dam_dir_xy[0] = line1.y - line2.y;
                dam_dir_xy[1] = line2.x - line1.x;

                Tuple<double, double> xy_1 = (Tuple<double, double>)xy[1];
                double mod_original = Math.Sqrt((xy_1.Item1 - line1.x) * (xy_1.Item1 - line1.x) + (xy_1.Item2 - line1.y) * (xy_1.Item2 - line1.y));
                double mod_new = Math.Sqrt((xy_1.Item1 + dam_dir_xy[0] - line1.x) * (xy_1.Item1 + dam_dir_xy[0] - line1.x) + (xy_1.Item2 + dam_dir_xy[1] - line1.y) * (xy_1.Item2 + dam_dir_xy[1] - line1.y));

                if (mod_new < mod_original)
                {
                    dam_dir_xy[0] = (-1) * dam_dir_xy[0];
                    dam_dir_xy[1] = (-1) * dam_dir_xy[1];
                }

                double mod = Math.Sqrt(dam_dir_xy[0] * dam_dir_xy[0] + dam_dir_xy[1] * dam_dir_xy[1]);
                dam_dir_xy[0] = dam_dir_xy[0] / mod;
                dam_dir_xy[1] = dam_dir_xy[1] / mod;
                dam_dir_xy[2] = -1 * rate_dam;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //计算趾板截面点坐标vector(A,B,C,D,Q,T)
        //先假定X（0,y,z）,之后平移第一个点到Z1，之后依次平移，有两种可能
        private static bool single_section(double[] base_dir, double max_z, section_para para, point start, point end, double rate_dam, ref ArrayList results, ref string output_message)
        {
            try
            {
                double[] start_end = new double[3] { start.x - end.x, start.y - end.y, start.z - end.z };
                results.Clear();
                point test_p = new point((start.x + end.x) / 2.0, (start.y + end.y) / 2.0, (start.z + end.z) / 2.0);
                calculate_math(base_dir, start_end, max_z, para, rate_dam, start, ref results, ref output_message, start, test_p);
                calculate_math(base_dir, start_end, max_z, para, rate_dam, end, ref results, ref output_message, start, test_p);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        //数学计算QT相关数值
        private static bool calculate_math(double[] base_dir, double[] start_end, double line_z, section_para para, double rate_dam, point x_point, ref ArrayList results, ref string output_message, point base_p, point test_p)
        {
            try
            {
                //double mod_se = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                //double[] se_xy = new double[2] { -1 * start_end[1] / mod_se, start_end[0] / mod_se };


                //真倾角假倾角
                double math1 = base_dir[0] * start_end[0] + base_dir[1] * start_end[1];
                double math2 = Math.Sqrt(base_dir[0] * base_dir[0] + base_dir[1] * base_dir[1]);
                double math3 = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                double angle_xy_level = Math.Acos(math1 / math2 / math3);

                double test_angle = angle_xy_level / Math.PI * 180.0;
                if (angle_xy_level > Math.PI / 2)
                    angle_xy_level = Math.PI - angle_xy_level;

                //output_txt("X:" + x_point.z.ToString() + "  ");
                output_message += "X：" + x_point.z.ToString() + "    ";

                double xita1 = Math.Asin(Math.Cos(angle_xy_level) / Math.Sqrt(1 + rate_dam * rate_dam));
                double xita2 = Math.Asin(Math.Cos(angle_xy_level) / Math.Sqrt(1 + para.rate_qt * para.rate_qt));
                //output_txt("θ：" + (xita1 / Math.PI * 180.0).ToString() + "    ");
                output_message += "θ:" + (xita1 / Math.PI * 180.0).ToString() + "  ";

                //角度计算完毕
                double rate_xita1 = 1.0 / Math.Tan(xita1);
                double rate_xita2 = 1.0 / Math.Tan(xita2);
                //BA延长线与CT相交假定点E,Z点向AB边做垂线交于点F,T点向AB边做垂线交于点G
                double Lbe = para.Lbc / rate_xita1;
                double z_z = (Lbe + para.Lbx) / (rate_xita1 + 1.0 / rate_xita1);//z与ab高差
                double z_all = x_point.z + z_z;//z点真实高程
                //output_txt("Z:" + z_all.ToString() + "    ");
                output_message += "Z:" + z_all.ToString() + "  ";

                double arc_dam = Math.Atan(1.0 / rate_xita1);
                double arc_2 = Math.Atan(1.0 / rate_xita2);
                double arc = Math.PI / 2 + arc_2 - arc_dam;
                //double t = para.T + 0.0035 * (line_z - z_all);
                double t = para.T + (line_z - z_all) * Math.Sqrt(1 + rate_xita1 * rate_xita1) / Math.Tan(arc);
                //output_txt("t：" + t.ToString() + "    ");
                output_message += "t:" + t.ToString() + "  ";
                double Hxt = z_z + t * rate_xita1 / Math.Sqrt(rate_xita1 * rate_xita1 + 1);
                double Hqt = Hxt - para.Lad;
                double Ltbh = Hqt * Math.Tan(xita1) + (para.Lad - para.Lbc) * Math.Tan(xita1);

                double Ldq = para.Lax + para.Lbx - Hqt / Math.Tan(xita1) - Ltbh;
                Log.write_log("D:\\趾板Test.txt", "xita2:"  + xita2.ToString()+ "\r\n");
                //double Ldq = para.Lax + para.Lbx - Hqt / Math.Tan(xita2) - Ltbh;

                double point_t_z = z_all + t * rate_xita1 / Math.Sqrt(1 + rate_xita1 * rate_xita1);
                double Lxg_s = (para.Lbx + Lbe) - (point_t_z - x_point.z) / rate_xita1;
                double Lxt = (Hxt - para.Lbc) * Math.Tan(xita1);

                point Ts = new point(0, 0, point_t_z);
                //double Ldq = rate_xita1 * (point_t_z - x_point.z - para.Lad);
                //double Lqt = Hqt * Math.Tan(xita1);
                double Lqt = Hqt / Math.Tan(xita1);
                double Htq = Hxt - para.Lad;

                double[] v_se = new double[3] { start_end[0], start_end[1], start_end[2] };
                unit_vec(ref v_se);
                double[] dir_ab = get_cz_vec(v_se, base_p, test_p);
                unit_vec(ref dir_ab);

                double[] v_ad = get_vec(v_se, dir_ab);

                if (v_ad[2] < 0)
                {
                    v_ad[0] = (-1) * v_ad[0];
                    v_ad[1] = (-1) * v_ad[1];
                    v_ad[2] = (-1) * v_ad[2];
                }
                calculate_points(x_point, v_se, dir_ab, v_ad, para, Lqt, Hqt, Ldq, ref results);
                point D = (point)results[results.Count - 4], Q = (point)results[results.Count - 2];
                output_message += "L2:" + Math.Sqrt((D.x - Q.x) * (D.x - Q.x) + (D.y - Q.y) * (D.y - Q.y)).ToString() + "\r\n";

                //output_txt("L2：" + Math.Sqrt(Ds.x - Qs.x) * (Ds.x - Qs.x) + (Ds.y - Qs.y) * (Ds.y - Qs.y)).ToString() + "\r\n");
                //output_txt("L2：" + "\r\n");
                //output_message += "L2:" + Math.Sqrt((Ds.x - Qs.x) * (Ds.x - Qs.x) + (Ds.y - Qs.y) * (Ds.y - Qs.y)).ToString() + "\r\n";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        //计算坐标
        private static void calculate_points(point x_point, double[] v_se, double[] v_ab, double[] v_ad, section_para para, double Lqt, double Hqt, double Ldq, ref ArrayList results)
        {
            //v_ab[0] = (-1) * v_ab[0];
            //v_ab[1] = (-1) * v_ab[1];
            Log.write_log("D://output.txt", "Lqt:" + Lqt.ToString() + " * " + "Hqt:" + Hqt.ToString() + "\r\n");
            point As = new point(x_point.x + v_ab[0] * para.Lax, x_point.y + v_ab[1] * para.Lax, x_point.z + v_ab[2] * para.Lax);
            point Bs = new point(x_point.x - v_ab[0] * para.Lbx, x_point.y - v_ab[1] * para.Lbx, x_point.z - v_ab[2] * para.Lbx);
            point Cs = new point(Bs.x + v_ad[0] * para.Lbc, Bs.y + v_ad[1] * para.Lbc, Bs.z + v_ad[2] * para.Lbc);
            point Ds = new point(As.x + v_ad[0] * para.Lad, As.y + v_ad[1] * para.Lad, As.z + v_ad[2] * para.Lad);
            point Qs = new point(Ds.x - v_ab[0] * Ldq, Ds.y - v_ab[1] * Ldq, Ds.z - v_ab[2] * Ldq);
            point Ts = new point(Qs.x - v_ab[0] * Lqt + v_ad[0] * Hqt, Qs.y - v_ab[1] * Lqt + v_ad[1] * Hqt, Qs.z - v_ab[2] * Lqt + v_ad[2] * Hqt);

            point Xs = new point(x_point.x, x_point.y, x_point.z);
            results.Add(As);
            results.Add(Bs);
            results.Add(Cs);
            results.Add(Ds);
            results.Add(Ts);
            results.Add(Qs);
            results.Add(Xs);
        }
        #endregion

        public static bool get_dir(point line_s, point line_e, double rate_dam, ArrayList xyz, ArrayList para_s, ref Dictionary<point, double[]> dir_list)
        {
            try
            {
                double[] max_min = new double[2] { 0, 0 };
                /*
                if (!convert_xyz_dir(ref line_s, ref line_e, ref xyz, ref max_min)) ;
                    return false;
                */
                double[] dir_se = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                dir_list.Clear();

                for (int i = 0; i < xyz.Count; i++)
                {
                    point[] ps = (point[])xyz[i];
                    ArrayList temp = new ArrayList();
                    section_para SecPara = (section_para)para_s[i];
                    ArrayList independent_sec = new ArrayList();
                    for (int j = 0; j < ps.Length - 1; j++)
                    {
                        ArrayList temp_save = new ArrayList();
                        string output_message = "";
                        if (!single_section(dir_se, line_s.z, SecPara, ps[j], ps[j + 1], rate_dam, ref temp_save, ref output_message))
                            return false;
                        else
                        {
                            //ABCDTQX
                            point original_A = (point)temp_save[0];
                            point original_B = (point)temp_save[1];
                            point original_D = (point)temp_save[3];

                            point new_A = (point)temp_save[7];
                            point new_B = (point)temp_save[8];
                            point new_D = (point)temp_save[10];
                            double[] dir1 = new double[6];
                            dir1[0] = original_B.x - original_A.x;
                            dir1[1] = original_B.y - original_A.y;
                            dir1[2] = original_B.z - original_A.z;

                            dir1[3] = (original_D.x - original_A.x) * 4;
                            dir1[4] = (original_D.y - original_A.y) * 4;
                            dir1[5] = (original_D.z - original_A.z) * 4;

                            double[] dir2 = new double[6];
                            dir2[0] = new_B.x - new_A.x;
                            dir2[1] = new_B.y - new_A.y;
                            dir2[2] = new_B.z - new_A.z;

                            dir2[3] = (new_D.x - new_A.x) * 4;
                            dir2[4] = (new_D.y - new_A.y) * 4;
                            dir2[5] = (new_D.z - new_A.z) * 4;

                            dir_list[ps[j]] = dir1;
                            dir_list[ps[j + 1]] = dir2;
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool convert_xyz_dir(ref point first_point, ref point last_point, ref ArrayList xys, ref double[] max_min)
        {
            try
            {
                ArrayList input = new ArrayList();
                double min_x = first_point.x, min_y = first_point.y;

                if (last_point.x < min_x)
                    min_x = last_point.x;
                if (last_point.y < min_y)
                    min_y = last_point.y;

                for (int i = 0; i < xys.Count; i++)
                {
                    point[] input_list = (point[])xys[i];
                    for (int j = 0; j < input_list.Length; j++)
                    {
                        if (input_list[j].x < min_x)
                            min_x = input_list[j].x;
                        if (input_list[j].y < min_y)
                            min_y = input_list[j].y;
                    }
                }
                ArrayList revise_xys = new ArrayList();
                for (int i = 0; i < xys.Count; i++)
                {
                    point[] input_list = (point[])xys[i];
                    point[] pl = new point[input_list.Length];
                    for (int j = 0; j < input_list.Length; j++)
                    {
                        point p = new point(input_list[j].x - min_x, input_list[j].y - min_y, input_list[j].z);
                        pl[j] = p;
                    }
                    revise_xys.Add(pl);
                }
                max_min = new double[2];
                max_min[0] = min_x;
                max_min[1] = min_y;
                first_point.x = first_point.x - min_x;
                first_point.y = first_point.y - min_y;

                last_point.x = last_point.x - min_x;
                last_point.y = last_point.y - min_y;
                xys.Clear();
                for (int i = 0; i < revise_xys.Count; i++)
                    xys.Add(revise_xys[i]);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //2026年1月更新---增加direction_ab判断
        public static bool single_dam(point line_s, point line_e, double rate_dam, point start_p, point end_p, section_para SecPara, bool if_direction_ab_reverse, ref ArrayList points_list, ref string output_message)
        {
            try
            {

                double[] dir_se = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                double[] test = new double[3] { end_p.x - start_p.x, end_p.y - start_p.y, end_p.z - start_p.z };
                unit_vec(ref test);
                points_list.Clear();
                if (!single_section(dir_se, line_s.z, SecPara, start_p, end_p, rate_dam, if_direction_ab_reverse, ref points_list, ref output_message))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool single_section(double[] base_dir, double max_z, section_para para, point start, point end, double rate_dam, bool if_direction_ab_reverse, ref ArrayList results, ref string output_message)
        {
            try
            {
                double[] start_end = new double[3] { start.x - end.x, start.y - end.y, start.z - end.z };
                results.Clear();
                point test_p = new point((start.x + end.x) / 2.0, (start.y + end.y) / 2.0, (start.z + end.z) / 2.0);
                calculate_math(base_dir, start_end, max_z, para, rate_dam, start, if_direction_ab_reverse, ref results, ref output_message, start, test_p);
                calculate_math(base_dir, start_end, max_z, para, rate_dam, end, if_direction_ab_reverse, ref results, ref output_message, start, test_p);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool calculate_math(double[] base_dir, double[] start_end, double line_z, section_para para, double rate_dam, point x_point, bool if_direction_ab_reverse, ref ArrayList results, ref string output_message, point base_p, point test_p)
        {
            try
            {
                //double mod_se = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                //double[] se_xy = new double[2] { -1 * start_end[1] / mod_se, start_end[0] / mod_se };


                //真倾角假倾角
                double math1 = base_dir[0] * start_end[0] + base_dir[1] * start_end[1];
                double math2 = Math.Sqrt(base_dir[0] * base_dir[0] + base_dir[1] * base_dir[1]);
                double math3 = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                double angle_xy_level = Math.Acos(math1 / math2 / math3);

                double test_angle = angle_xy_level / Math.PI * 180.0;
                if (angle_xy_level > Math.PI / 2)
                    angle_xy_level = Math.PI - angle_xy_level;

                //output_txt("X:" + x_point.z.ToString() + "  ");
                output_message += "X：" + x_point.z.ToString() + "    ";

                double xita1 = Math.Asin(Math.Cos(angle_xy_level) / Math.Sqrt(1 + rate_dam * rate_dam));
                double xita2 = Math.Asin(Math.Cos(angle_xy_level) / Math.Sqrt(1 + para.rate_qt * para.rate_qt));
                //output_txt("θ：" + (xita1 / Math.PI * 180.0).ToString() + "    ");
                output_message += "θ:" + (xita1 / Math.PI * 180.0).ToString() + "  ";

                //角度计算完毕
                double rate_xita1 = 1.0 / Math.Tan(xita1);
                double rate_xita2 = 1.0 / Math.Tan(xita2);
                //BA延长线与CT相交假定点E,Z点向AB边做垂线交于点F,T点向AB边做垂线交于点G
                double Lbe = para.Lbc / rate_xita1;
                double z_z = (Lbe + para.Lbx) / (rate_xita1 + 1.0 / rate_xita1);//z与ab高差
                double z_all = x_point.z + z_z;//z点真实高程
                //output_txt("Z:" + z_all.ToString() + "    ");
                output_message += "Z:" + z_all.ToString() + "  ";

                double arc_dam = Math.Atan(1.0 / rate_xita1);
                double arc_2 = Math.Atan(1.0 / rate_xita2);
                double arc = Math.PI / 2 + arc_2 - arc_dam;
                //double t = para.T + 0.0035 * (line_z - z_all);
                double t = para.T + (line_z - z_all) * Math.Sqrt(1 + rate_xita1 * rate_xita1) / Math.Tan(arc);
                //output_txt("t：" + t.ToString() + "    ");
                output_message += "t:" + t.ToString() + "  ";
                double Hxt = z_z + t * rate_xita1 / Math.Sqrt(rate_xita1 * rate_xita1 + 1);
                double Hqt = Hxt - para.Lad;
                double Ltbh = Hqt * Math.Tan(xita1) + (para.Lad - para.Lbc) * Math.Tan(xita1);
                double Ldq = para.Lax + para.Lbx - Hqt / Math.Tan(xita1) - Ltbh;


                double point_t_z = z_all + t * rate_xita1 / Math.Sqrt(1 + rate_xita1 * rate_xita1);
                double Lxg_s = (para.Lbx + Lbe) - (point_t_z - x_point.z) / rate_xita1;
                double Lxt = (Hxt - para.Lbc) * Math.Tan(xita1);

                point Ts = new point(0, 0, point_t_z);
                //double Ldq = rate_xita1 * (point_t_z - x_point.z - para.Lad);
                double Lqt = Hqt * Math.Tan(xita1);
                double Htq = Hxt - para.Lad;

                double[] v_se = new double[3] { start_end[0], start_end[1], start_end[2] };
                unit_vec(ref v_se);
                double[] dir_ab = get_cz_vec(v_se, base_p, test_p);
                unit_vec(ref dir_ab);
                if(if_direction_ab_reverse)
                {
                    dir_ab[0] = dir_ab[0] * (-1.0);
                    dir_ab[1] = dir_ab[1] * (-1.0);
                }
                double[] v_ad = get_vec(v_se, dir_ab);

                if (v_ad[2] < 0)
                {
                    v_ad[0] = (-1) * v_ad[0];
                    v_ad[1] = (-1) * v_ad[1];
                    v_ad[2] = (-1) * v_ad[2];
                }
                calculate_points(x_point, v_se, dir_ab, v_ad, para, Lqt, Hqt, Ldq, ref results);
                point D = (point)results[results.Count - 4], Q = (point)results[results.Count - 2];
                output_message += "L2:" + Math.Sqrt((D.x - Q.x) * (D.x - Q.x) + (D.y - Q.y) * (D.y - Q.y)).ToString() + "\r\n";

                //output_txt("L2：" + Math.Sqrt(Ds.x - Qs.x) * (Ds.x - Qs.x) + (Ds.y - Qs.y) * (Ds.y - Qs.y)).ToString() + "\r\n");
                //output_txt("L2：" + "\r\n");
                //output_message += "L2:" + Math.Sqrt((Ds.x - Qs.x) * (Ds.x - Qs.x) + (Ds.y - Qs.y) * (Ds.y - Qs.y)).ToString() + "\r\n";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

    }
}
