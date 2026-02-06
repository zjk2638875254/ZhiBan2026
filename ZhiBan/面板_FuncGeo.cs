using ExcelLib;
using NPOI.OpenXmlFormats.Wordprocessing;
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
    class 面板_FuncGeo
    {
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
                /*
                Log.write_log("D:\\趾板Log.txt", "all_len:" + all_len.ToString() + "\r\n");
                for(int i=0;i<lens.Length;i++)
                    Log.write_log("D:\\趾板Log.txt", "len[" + i.ToString() + "]:" + lens[i].ToString() + "\r\n");
                Log.write_log("D:\\趾板Log.txt", "all_len:" + all_len.ToString() + "    lens[id_start]:" + lens[id_start].ToString() + "\r\n");
                Log.write_log("D:\\趾板Log.txt", "id_start:" + id_start.ToString() + "    lens.Count():" + lens.Count().ToString() + "\r\n");
                */
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


        public static void auto_draw(ArrayList point_list, ArrayList para_list, ArrayList if_merge, point start, point end, double dam_rate, ref string para_output_message)
        {
            try
            {
                if (point_list.Count != point_list.Count || point_list.Count != if_merge.Count)
                {
                    MessageBox.Show("长度不等!");
                    return;
                }
                ArrayList pos = new ArrayList { };
                for (int i = 0; i < point_list.Count; i++)
                {
                    pos.Clear();
                    if (!((bool)if_merge[i]))
                    {
                        string res_message = "";
                        point[] section_points = (point[])point_list[i];
                        section_para sec_para = (section_para)para_list[i];
                        趾板_FuncDam.single_dam(start, end, dam_rate, section_points[0], section_points[1], sec_para, ref pos, ref res_message);
                        /*
                        Log.write_log("D:\\趾板Log.txt", pos.Count + "\r\n");
                        for (int j = 0; j < pos.Count; j++)
                            Log.write_log("D:\\趾板Log.txt", ((point)pos[j]).ToString() + "\r\n");
                        Log.write_log("D:\\趾板Log.txt", "-------------------\r\n");
                        */
                        para_output_message += res_message;
                    }
                    else
                    {
                        point[] section_points_before = (point[])point_list[i - 1];
                        section_para sec_para_before = (section_para)para_list[i - 1];
                        point[] section_points_after = (point[])point_list[i + 1];
                        section_para sec_para_after = (section_para)para_list[i + 1];
                        ArrayList pos_before = new ArrayList { };
                        string res_message_before = "";
                        ArrayList pos_after = new ArrayList { };
                        string res_message_after = "";
                        趾板_FuncDam.single_dam(start, end, dam_rate, section_points_before[0], section_points_before[1], sec_para_before, ref pos_before, ref res_message_before);
                        趾板_FuncDam.single_dam(start, end, dam_rate, section_points_after[0], section_points_after[1], sec_para_after, ref pos_after, ref res_message_after);
                        point[] before1 = new point[7];
                        for (int j = 0; j < 7; j++)
                            before1[j] = (point)pos_before[j];
                        point[] before2 = new point[7];
                        for (int j = 0; j < 7; j++)
                            before2[j] = (point)pos_before[j + 7];
                        point[] after1 = new point[7];
                        for (int j = 0; j < 7; j++)
                            after1[j] = (point)pos_after[j];
                        point[] after2 = new point[7];
                        for (int j = 0; j < 7; j++)
                            after2[j] = (point)pos_after[j + 7];
                        /*
                        Log.write_log("D:\\趾板Log.txt", pos_before.Count + "\r\n");
                        for (int j = 0; j < pos_before.Count; j++)
                            Log.write_log("D:\\趾板Log.txt", ((point)pos_before[j]).x.ToString() + "\r\n");
                        Log.write_log("D:\\趾板Log.txt", "-------------------\r\n");
                        Log.write_log("D:\\趾板Log.txt", pos_after.Count + "\r\n");
                        for (int j = 0; j < pos_after.Count; j++)
                            Log.write_log("D:\\趾板Log.txt", ((point)pos_after[j]).x.ToString() + "\r\n");
                        Log.write_log("D:\\趾板Log.txt", "-------------------\r\n");
                        */
                        趾板_FuncDam.merge_dam(start, end, before1, before2, after1, after2, ref pos);
                    }
                    Log.write_log("D:\\趾板Log.txt", "begin geo：" + "\r\n");
                    BentleyGeo.make_geo_single(pos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "警告5");
            }
        }

        public static bool geo_面板(DataTable data_xy, DataTable data_para, point start, point end, double dam_rate, ArrayList p_list, ref string para_output_message)
        {
            try
            {
                ArrayList paras = new ArrayList();
                init_para(data_para, ref paras);
                ArrayList xys = new ArrayList();
                init_xy(data_xy, ref xys);

                ArrayList point_list = new ArrayList();
                ArrayList para_list = new ArrayList();
                ArrayList if_merge = new ArrayList();
                cut_para(xys, paras, start, end, dam_rate, ref point_list, ref para_list, ref if_merge);

                auto_draw(point_list, para_list, if_merge, start, end, dam_rate, ref para_output_message);
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
