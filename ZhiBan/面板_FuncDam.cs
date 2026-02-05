using ExcelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class 面板_FuncDam
    {
        #region 一些预定义的方法
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
        //求两个向量的共同垂直单位向量（面的法向量）
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
        //判断bp,tp与v的相对关系,v的方向应该是bp指向tp
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
        //判断ab的方向，
        private static double[] get_dir_ab(point line_p1, point line_p2, double[] v_se, point test_p)
        {
            double[] test_dir = new double[3] { v_se[1], (-1) * v_se[0], 0 };
            double[] line = new double[3] { line_p2.y - line_p1.y, line_p1.x - line_p2.x, 0 };
            double mod = line[0] * test_dir[0] + line[1] * test_dir[1];
            if(mod < 0)
            {
                test_dir[0] = -1.0 * test_dir[0];
                test_dir[1] = -1.0 * test_dir[1];
                test_dir[2] = -1.0 * test_dir[2];
            }
            unit_vec(ref test_dir);
            return test_dir;
        }
        #endregion

        private static void calculate_points(point x_point, double[] v_se, double[] v_ab, double[] v_ad, section_para para, double Lqt, double Hqt, double Ldq, ref point[] results)
        {
            //v_ab[0] = (-1) * v_ab[0];
            //v_ab[1] = (-1) * v_ab[1];
            point As = new point(x_point.x + v_ab[0] * para.Lax, x_point.y + v_ab[1] * para.Lax, x_point.z + v_ab[2] * para.Lax);
            point Bs = new point(x_point.x - v_ab[0] * para.Lbx, x_point.y - v_ab[1] * para.Lbx, x_point.z - v_ab[2] * para.Lbx);
            point Cs = new point(Bs.x + v_ad[0] * para.Lbc, Bs.y + v_ad[1] * para.Lbc, Bs.z + v_ad[2] * para.Lbc);
            point Ds = new point(As.x + v_ad[0] * para.Lad, As.y + v_ad[1] * para.Lad, As.z + v_ad[2] * para.Lad);
            point Qs = new point(Ds.x - v_ab[0] * Ldq, Ds.y - v_ab[1] * Ldq, Ds.z - v_ab[2] * Ldq);
            point Ts = new point(Qs.x - v_ab[0] * Lqt + v_ad[0] * Hqt, Qs.y - v_ab[1] * Lqt + v_ad[1] * Hqt, Qs.z - v_ab[2] * Lqt + v_ad[2] * Hqt);

            point Xs = new point(x_point.x, x_point.y, x_point.z);
            //results.Add(As);
            //results.Add(Bs);
            //results.Add(Cs);
            //results.Add(Ds)
            results[1] = Ts;
            //results.Add(Qs);
            results[2] = Xs;
        }


        private static bool calculate_math(point line_s, point line_e, double[] start_end, double line_z, section_para para, double rate_dam, point[] high_point, ref ArrayList results, ref string output_message, point base_p, point test_p)
        {
            try
            {
                double[] base_dir = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                //double mod_se = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                //double[] se_xy = new double[2] { -1 * start_end[1] / mod_se, start_end[0] / mod_se };
                point[] 面板_p_list = new point[5];

                //真倾角假倾角
                double math1 = base_dir[0] * start_end[0] + base_dir[1] * start_end[1];
                double math2 = Math.Sqrt(base_dir[0] * base_dir[0] + base_dir[1] * base_dir[1]);
                double math3 = Math.Sqrt(start_end[0] * start_end[0] + start_end[1] * start_end[1]);
                double angle_xy_level = Math.Acos(math1 / math2 / math3);

                double test_angle = angle_xy_level / Math.PI * 180.0;
                if (angle_xy_level > Math.PI / 2)
                    angle_xy_level = Math.PI - angle_xy_level;

                //output_txt("X:" + x_point.z.ToString() + "  ");
                output_message += "X：" + high_point[1].z.ToString() + "    ";

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
                double z_all = high_point[1].z + z_z;//z点真实高程
                //output_txt("Z:" + z_all.ToString() + "    ");
                output_message += "Z:" + z_all.ToString() + "  ";

                double zs_x = high_point[1].x + (z_all - high_point[1].z) / (high_point[1].z - high_point[0].z) * (high_point[1].x - high_point[0].x);
                double zs_y = high_point[1].y + (z_all - high_point[1].z) / (high_point[1].z - high_point[0].z) * (high_point[1].y - high_point[0].y);
                point Zs = new point(zs_x, zs_y, z_all);
                面板_p_list[0] = Zs;

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
                double Lxg_s = (para.Lbx + Lbe) - (point_t_z - high_point[1].z) / rate_xita1;
                double Lxt = (Hxt - para.Lbc) * Math.Tan(xita1);

                point Ts = new point(0, 0, point_t_z);
                //double Ldq = rate_xita1 * (point_t_z - x_point.z - para.Lad);
                double Lqt = Hqt / Math.Tan(xita1);
                double Htq = Hxt - para.Lad;

                double[] v_se = new double[3] { start_end[0], start_end[1], start_end[2] };
                unit_vec(ref v_se);
                /*
                double[] dir_ab = get_cz_vec(v_se, base_p, test_p);
                unit_vec(ref dir_ab);
                */
                double[] dir_ab = get_dir_ab(line_s, line_e, v_se, test_p);
                double[] v_ad = get_vec(v_se, dir_ab);

                if (v_ad[2] < 0)
                {
                    v_ad[0] = (-1) * v_ad[0];
                    v_ad[1] = (-1) * v_ad[1];
                    v_ad[2] = (-1) * v_ad[2];
                }
                
                calculate_points(high_point[1], v_se, dir_ab, v_ad, para, Lqt, Hqt, Ldq, ref 面板_p_list);

                //ZTX重置为XZT
                point px = new point(面板_p_list[2].x, 面板_p_list[2].y, 面板_p_list[2].z);
                point pz = new point(面板_p_list[0].x, 面板_p_list[0].y, 面板_p_list[0].z);
                point pt = new point(面板_p_list[1].x, 面板_p_list[1].y, 面板_p_list[1].z);

                point p_low_z = 面板_p_list[0];
                point p_low_t = 面板_p_list[1];

                double p_low_len = Math.Sqrt((p_low_t.x - p_low_z.x) * (p_low_t.x - p_low_z.x) + (p_low_t.y - p_low_z.y) * (p_low_t.y - p_low_z.y) + (p_low_t.z - p_low_z.z) * (p_low_t.z - p_low_z.z));
                point p_high_t = new point(high_point[0].x + para.T / p_low_len * (p_low_t.x - p_low_z.x), high_point[0].y + para.T / p_low_len * (p_low_t.y - p_low_z.y), high_point[0].z + para.T / p_low_len * (p_low_t.z - p_low_z.z));

                面板_p_list[0] = px;
                面板_p_list[1] = pz;
                面板_p_list[2] = pt;
                面板_p_list[3] = high_point[0];
                面板_p_list[4] = p_high_t;
                results.Add(面板_p_list);
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
        private static bool single_section(point line_s, point line_e, double max_z, section_para para, point[] start, point[] end, double rate_dam, ref ArrayList results, ref string output_message)
        {
            try
            {
                double[] start_end = new double[3] { start[1].x - end[1].x, start[1].y - end[1].y, start[1].z - end[1].z };
                results.Clear();
                point test_p = new point((start[1].x + end[1].x) / 2.0, (start[1].y + end[1].y) / 2.0, (start[1].z + end[1].z) / 2.0);
                calculate_math(line_s, line_e, start_end, max_z, para, rate_dam, start, ref results, ref output_message, start[1], test_p);
                calculate_math(line_s, line_e, start_end, max_z, para, rate_dam, end, ref results, ref output_message, start[1], test_p);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private static bool single_面板(point line_s, point line_e, double rate_dam, point[] start_p, point[] end_p, section_para SecPara, ref ArrayList points_list, ref string output_message)
        {
            try
            {
                double[] dir_se = new double[3] { line_e.x - line_s.x, line_e.y - line_s.y, line_e.z - line_s.z };
                double[] test = new double[3] { end_p[1].x - start_p[1].x, end_p[1].y - start_p[1].y, end_p[1].z - start_p[1].z };
                unit_vec(ref test);
                points_list.Clear();
                if (!single_section(line_s, line_e, line_s.z, SecPara, start_p, end_p, rate_dam, ref points_list, ref output_message))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
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
        //根据xy坐标反算Z
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
        #endregion
        private static point section_point(point section_begin, point section_end, double len)
        {
            double[] dir = new double[3] { section_end.x - section_begin.x, section_end.y - section_begin.y, section_end.z - section_begin.z };
            double mod = Math.Sqrt(dir[0] * dir[0] + dir[1] * dir[1] + dir[2] * dir[2]);
            point p = new point(section_begin.x + len / mod * dir[0], section_begin.y + len / mod * dir[1], section_begin.z + len / mod * dir[2]);
            return p;
        }

        private static bool get_points_and_para(double all_len, double len, point[] points_z, double[] lens, ref ArrayList point_list, ref ArrayList if_merge)
        {
            try
            {
                int id_start = 0, id_end = 0;
                
                Log.write_log("D:\\test.txt", "all_len:" + all_len.ToString() + "\r\n");
                for (int i = 0; i < lens.Length; i++)
                    Log.write_log("D:\\test.txt", "len[" + i.ToString() + "]:" + lens[i].ToString() + "\r\n");
                Log.write_log("D:\\test.txt", "len:" + len.ToString() + "    lens[id_start]:" + lens[id_start].ToString() + "\r\n");
                Log.write_log("D:\\test.txt", "id_start:" + id_start.ToString() + "    lens.Count():" + lens.Count().ToString() + "\r\n");
                
                while (id_start < lens.Count() && all_len >= lens[id_start])
                    id_start++;
                while (id_end < lens.Count() && all_len + len >= lens[id_end])
                    id_end++;
                if (id_start == lens.Count() || id_end == lens.Count())
                    return false;

                //start_id 一直等于0！！！！！

                double len_s = all_len - lens[id_start - 1];
                double len_e = all_len + len - lens[id_end - 1];
                point ps = section_point(points_z[id_start - 1], points_z[id_start], len_s);
                point pe = section_point(points_z[id_end - 1], points_z[id_end], len_e);
                point[] plist = new point[2] { ps, pe };
                point_list.Add(plist);
                if (id_start == id_end)
                {
                    if_merge.Add(false);
                }
                else
                {
                    if_merge.Add(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }


        private static bool cut_para(ArrayList xys, ArrayList paras, point start, point end, double dam_rate, ref ArrayList point_list, ref ArrayList para_list, ref ArrayList if_merge)
        {
            try
            {
                point[] points_z = new point[xys.Count];
                get_z_by_xy(start, end, dam_rate, xys, ref points_z);
                double[] lens = new double[points_z.Count()];
                double all_lens = 0;
                lens[0] = all_lens;
                for (int i = 1; i < points_z.Count(); i++)
                {
                    all_lens += Math.Sqrt((points_z[i].x - points_z[i - 1].x) * (points_z[i].x - points_z[i - 1].x) + (points_z[i].y - points_z[i - 1].y) * (points_z[i].y - points_z[i - 1].y) + (points_z[i].z - points_z[i - 1].z) * (points_z[i].z - points_z[i - 1].z));
                    lens[i] = all_lens;
                }

                double all_len = 0;
                point_list.Clear();
                para_list.Clear();
                if_merge.Clear();
                for (int i = 0; i < paras.Count; i++)
                {
                    double len = ((section_para)paras[i]).Len;
                    para_list.Add((section_para)paras[i]);
                    get_points_and_para(all_len, len, points_z, lens, ref point_list, ref if_merge);
                    all_len += len;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static void auto_draw(ArrayList point_list, ArrayList mianban_high, ArrayList if_merge, ArrayList para_list, point start, point end, double dam_rate, ref string para_output_message)
        {
            try
            {
                if (point_list.Count != point_list.Count)
                {
                    MessageBox.Show("长度不等!");
                    return;
                }
                ArrayList pos = new ArrayList();
                for (int i = 1; i < mianban_high.Count - 1; i++)
                {
                    pos.Clear();
                    if (!((bool)if_merge[i]))
                    {
                        pos.Clear();
                        Log.write_log("D:\\test.txt", "not merge:" + i.ToString() + "\r\n");
                        string res_message = "";
                        point[] section_points_start = (point[])mianban_high[i - 1];
                        point[] section_points_end = (point[])mianban_high[i];
                        section_para sec_para = (section_para)para_list[i];
                        single_面板(start, end, dam_rate, section_points_start, section_points_end, sec_para, ref pos, ref res_message);
                        para_output_message += res_message;
                        Log.write_log("D:\\趾板Log.txt", "begin geo：" + "\r\n");
                        面板_Bentley.test_mianban(pos);
                        para_output_message += res_message;
                    }
                    else
                    {
                        pos.Clear();
                        Log.write_log("D:\\test.txt", "merge:" + i.ToString() + "\r\n");
                        
                        point[] section_points_start_before = (point[])mianban_high[i - 2];
                        point[] section_points_end_before = (point[])mianban_high[i - 1];
                        section_para sec_para_before = (section_para)para_list[i - 2];
                        point[] section_points_start_after = (point[])mianban_high[i];
                        point[] section_points_end_after = (point[])mianban_high[i + 1];
                        section_para sec_para_after = (section_para)para_list[i];

                        
                        ArrayList pos_before = new ArrayList();
                        string res_message_before = "";
                        ArrayList pos_after = new ArrayList();
                        string res_message_after = "";

                        single_面板(start, end, dam_rate, section_points_start_before, section_points_end_before, sec_para_before, ref pos_before, ref res_message_before);
                        para_output_message += res_message_before;
                        single_面板(start, end, dam_rate, section_points_start_after, section_points_end_after, sec_para_after, ref pos_after, ref res_message_after);
                        para_output_message += res_message_after;
                        Log.write_log("D:\\趾板Log.txt", "merge 面板：" + "\r\n" + para_output_message);
                        //面板_Bentley.test_mianban(pos);
                        point[] before1, before2, after1, after2;
                        before1 = (point[])pos_before[0];
                        before2 = (point[])pos_before[1];
                        after1 = (point[])pos_after[0];
                        after2 = (point[])pos_after[1];
                        面板_FuncMerge.merge_dam(before1, before2, after1, after2, ref pos);
                        面板_Bentley.test_mianban_转角(pos);
                        
                    }
                }
                /*
                for (int i = 0; i < pos.Count; i++)
                {
                    ArrayList pos_list = (ArrayList)pos[i];
                    面板_Bentley.test_high(pos_list);
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }

        public static bool multi_面板(DataTable data_xy, ArrayList paras, point start, point end, double dam_rate, ArrayList 面板_plist, ref string para_output_message)
        {
            try
            {
                ArrayList xys = new ArrayList();
                init_xy(data_xy, ref xys);

                ArrayList point_list = new ArrayList();
                ArrayList para_list = new ArrayList();
                ArrayList if_merge = new ArrayList();
                cut_para(xys, paras, start, end, dam_rate, ref point_list, ref para_list, ref if_merge);

                auto_draw(point_list, 面板_plist, if_merge, para_list, start, end, dam_rate, ref para_output_message);
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
