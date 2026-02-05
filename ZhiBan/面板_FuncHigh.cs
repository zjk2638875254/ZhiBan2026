using ExcelLib;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class 面板_FuncHigh
    {

        private struct IntersectionResult
        {
            public bool HasIntersection;
            public double X, Y, Z;  // 三维交点坐标

            public IntersectionResult(bool hasIntersection, double x, double y, double z)
            {
                HasIntersection = hasIntersection;
                X = x; Y = y; Z = z;
            }

            public override string ToString() =>
                HasIntersection ? $"交点: {this}" : "无交点";
        }

        private static IntersectionResult if_has_intersection(point linePoint, point lineDir, point segP1, point segP2, double epsilon = 1e-6)
        {
            try
            {
                point segDir = new point(segP2.x - segP1.x, segP2.y - segP1.y, segP2.y - segP1.y);
                if(lineDir.x/segDir.x == lineDir.y / segDir.y || lineDir.x / segDir.x == -1.0 * lineDir.y / segDir.y)
                    return new IntersectionResult(false, 0, 0, 0);
                else
                {
                    point p11 = new point(linePoint.x, linePoint.y, linePoint.z);
                    point p12 = new point(linePoint.x + lineDir.x, linePoint.y + lineDir.y, linePoint.z + lineDir.z);

                    ArrayList test_array_list = new ArrayList();
                    point[] ar1 = { p11, p12 };
                    point[] ar2 = { segP1, segP2 };

                    test_array_list.Add(ar1);
                    test_array_list.Add(ar2);
                    面板_Bentley.test_high(test_array_list);

                    double A1 = p12.y - p11.y;
                    double B1 = p11.x - p12.x;
                    double C1 = p12.x * p11.y - p11.x * p12.y;

                    double A2 = segP2.y - segP1.y;
                    double B2 = segP1.x - segP2.x;
                    double C2 = segP2.x * segP1.y - segP1.x * segP2.y;

                    double common_x = -1.0 * (B2 * C1 - B1 * C2) / (A1 * B2 - A2 * B1);
                    double common_y = -1.0 * (A1 * C2 - A2 * C1) / (A1 * B2 - A2 * B1);
                    Log.write_log("D:\\testmianban.txt", "x:" + common_x.ToString() + "\r\n" + "y:" + common_y.ToString() + "\r\n");
                    if (common_x >= segP1.x && common_x <= segP2.x)
                    {
                        return new IntersectionResult(true, common_x, common_y, 0);
                    }
                    else if(common_x >= segP2.x && common_x <= segP1.x)
                    {
                        return new IntersectionResult(true, common_x, common_y, 0);
                    }
                    else
                    {
                        return new IntersectionResult(false, 0, 0, 0);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new IntersectionResult(false, 0, 0, 0);
            }
        }

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

        private static point get_location_by_len(point[] points_z, double len)
        {
            point p = new point(0, 0, 0);
            try
            {
                point p_start = points_z[0];
                point p_end = points_z[points_z.Length - 1];
                double all_len = Math.Sqrt((p_start.x - p_end.x) * (p_start.x - p_end.x) + (p_start.y - p_end.y) * (p_start.y - p_end.y) + (p_start.z - p_end.z) * (p_start.z - p_end.z));

                point lineDir = new point((p_end.y - p_start.y), (p_start.x - p_end.x), 0);
                point linePoint = new point(p_start.x + len / all_len * (p_end.x - p_start.x), p_start.y + len / all_len * (p_end.y - p_start.y), 0);
                for (int i = 0; i < points_z.Length - 1; i++)
                {
                    point seg_p1 = new point(points_z[i].x, points_z[i].y, 0);
                    point seg_p2 = new point(points_z[i + 1].x, points_z[i + 1].y, 0);
                    IntersectionResult common_p = if_has_intersection(linePoint, lineDir, seg_p1, seg_p2);
                    if(common_p.HasIntersection == true)
                    {
                        p.x = common_p.X;
                        p.y = common_p.Y;
                        double len_common_p = Math.Sqrt((common_p.X - points_z[i].x) * (common_p.X - points_z[i].x) + (common_p.Y - points_z[i].y) * (common_p.Y - points_z[i].y));
                        double len_p12 = Math.Sqrt((points_z[i + 1].x - points_z[i].x) * (points_z[i + 1].x - points_z[i].x) + (points_z[i + 1].y - points_z[i].y) * (points_z[i + 1].y - points_z[i].y));
                        p.z = points_z[i].z + len_common_p / len_p12 * (points_z[i + 1].z - points_z[i].z);
                        return p;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return p;
        }

        public static bool get_location(DataTable datatable_xy, point start, point end, double dam_rate, double[] len, ref ArrayList line_p)
        {
            try
            {
                ArrayList xys = new ArrayList();
                init_xy(datatable_xy, ref xys);

                line_p.Clear();
                point[] points_z = new point[xys.Count];
                get_z_by_xy(start, end, dam_rate, xys, ref points_z);
                /*
                string message_s = "";
                for (int i = 0; i < len.Length; i++)
                {
                    message_s += len[i].ToString() + "***";
                }
                MessageBox.Show(message_s);
                */
                double len_i = 0;
                double all_len = Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y));
                //MessageBox.Show(all_len.ToString());
                for (int i = 0; i < len.Length; i++)
                {
                    len_i += len[i];
                    point[] plist = new point[2];
                    point p_high = new point(start.x + len_i / all_len * (end.x - start.x), start.y + len_i / all_len * (end.y - start.y), start.z + len_i / all_len * (end.z - start.z));
                    point p_low = get_location_by_len(points_z, len_i);
                    plist[0] = p_high;
                    plist[1] = p_low;
                    line_p.Add(plist);
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool get_len_面板(point px, point ps, point pe, double rate_dam, DataTable p_xy, ref double len)
        {
            try
            {
                ArrayList xys = new ArrayList();
                init_xy(p_xy, ref xys);
                point[] p_list = new point[xys.Count];
                get_z_by_xy(ps, pe, rate_dam, xys, ref p_list);

                double all_len = 0;
                double[] lens = new double[p_list.Length];
                lens[0] = 0;
                for (int i = 1; i < p_list.Length; i++)
                {
                    all_len += Math.Sqrt((p_list[i].x - p_list[i - 1].x) * (p_list[i].x - p_list[i - 1].x) + (p_list[i].y - p_list[i - 1].y) * (p_list[i].y - p_list[i - 1].y) + (p_list[i].z - p_list[i - 1].z) * (p_list[i].z - p_list[i - 1].z));
                    lens[i] = all_len;
                }
                //如果两侧异号则在区间内
                for (int i = 0; i < p_list.Length; i++)
                {
                    if (p_list[i].x == px.x)
                    {
                        len = lens[i];
                        return true;
                    }
                }
                for (int i = 0; i < p_list.Length; i++)
                {
                    if ((p_list[i].x - px.x) * (p_list[i + 1].x - px.x) < 0)
                    {
                        len = lens[i] + Math.Sqrt((p_list[i].x - px.x) * (p_list[i].x - px.x) + (p_list[i].y - px.y) * (p_list[i].y - px.y) + (p_list[i].z - px.z) * (p_list[i].z - px.z));
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
