using ExcelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class location_x
    {
        public double x;
        public ArrayList used_list;
        public location_x(double x, ArrayList used_list)
        {
            this.x = x;
            this.used_list = used_list;
        }
    }

    class 面板_location
    {
        public static bool get_location(double[] 转弯_list, double[] 面板_list, double 转弯_limit, double 面板_limit, double best_len, double min_len, double max_len)
        {
            try
            {
                Log.write_log("D://test.txt", "转弯_list:\r\n");
                for (int i = 0; i < 转弯_list.Length; i++)
                    Log.write_log("D://test.txt", 转弯_list[i].ToString() + "\r\n");
                Log.write_log("D://test.txt", "面板_list:\r\n");
                for (int i = 0; i < 面板_list.Length; i++)
                    Log.write_log("D://test.txt", 面板_list[i].ToString() + "\r\n");
                double all_x = 转弯_list[转弯_list.Length - 1];
                double step = 0.1;
                location_x[] locations = new location_x[300];
                for(int i=0;i<locations.Length;i++)
                {
                    ArrayList temp = new ArrayList();
                    locations[i] = new location_x(0, temp);
                }
                double all_x_now = 0.0;
                int 趾板_nums = 0;
                string output_message = "";
                while(all_x_now < all_x && 趾板_nums >= 0)
                {
                    Log.write_log("D://test.txt", 趾板_nums.ToString());
                    double 趾板_x = 0.0;
                    if (if_add(step, 转弯_list, 面板_list, 转弯_limit, 面板_limit, best_len, min_len, max_len, all_x_now, locations[趾板_nums].used_list, ref 趾板_x, ref output_message))
                    {
                        locations[趾板_nums].x = 趾板_x;
                        locations[趾板_nums].used_list.Add(趾板_x);
                        趾板_nums++;
                        all_x_now += 趾板_x;
                        output_message += "成功计算第" + 趾板_nums.ToString() + "个趾板长度：" + 趾板_x + "\r\n";
                        output_message += "当前总分块长度：" + all_x_now.ToString() + "\r\n";
                        Log.write_log("D://test.txt", 趾板_nums.ToString());
                    }
                    else
                    {
                        趾板_nums--;
                        all_x_now -= locations[趾板_nums].x;
                        output_message += "第" + 趾板_nums.ToString() + "个趾板长度计算失败" + "\r\n";
                        output_message += "当前总分块长度：" + all_x_now.ToString() + "\r\n";
                        Log.write_log("D://test.txt", 趾板_nums.ToString());
                    }
                }
                Log.write_log("D://test.txt", output_message);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool if_add(double step, double[] 转弯_list, double[] 面板_list, double 转弯_limit, double 面板_limit, double best_len, double min_len, double max_len, double begin_x, ArrayList wrong_x, ref double result, ref string return_message)
        {
            try
            {
                double 趾板_len = best_len;
                int i = 0;
                while (趾板_len <= max_len && 趾板_len >= min_len && if_in_arraylist(wrong_x, 趾板_len) && (!if_near(转弯_list, 转弯_limit, begin_x + 趾板_len)) || (!if_near(面板_list, 面板_limit, begin_x + 趾板_len)))
                {
                    return_message += "尝试新增段失败：" + 趾板_len.ToString() + "\r\n";
                    if(i > 0)
                        i = -1 * (Math.Abs(i) + 1);
                    else
                        i = Math.Abs(i) + 1;
                    趾板_len += step * i;
                }
                if (趾板_len <= max_len && 趾板_len >= min_len)
                {
                    return_message += "尝试新增段成功：" + 趾板_len.ToString() + "\r\n";
                    result = 趾板_len;
                    return true;
                }
                Log.write_log("D://趾板log.txt", return_message);
                return false;
            }
            catch(Exception ex)
            {
                Log.write_log("D://趾板log.txt", ex.ToString());
                return false;
            }
        }

        private static bool if_near(double[] p_list, double len, double x_location)
        {
            try
            {
                for (int i = 0; i < p_list.Length; i++)
                {
                    if (Math.Abs(p_list[i] - x_location) < len)
                        return false;
                }
                Log.write_log("D://趾板log.txt", x_location.ToString() + "can use \r\n");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool if_in_arraylist(ArrayList wrong_list, double x)
        {
            try
            {
                for(int i=0;i< wrong_list.Count;i++)
                {
                    double wrong_x = (double)wrong_list[i];
                    if (wrong_x == x)
                        return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #region 数据转化
        public static bool convert_arraylist(ArrayList original_list, ref double[] 面板_list)
        {
            try
            {
                面板_list = new double[original_list.Count];
                面板_list[0] = 0;
                double len_all = 0.0;
                for (int i = 1; i < original_list.Count; i++)
                {
                    point[] original_before = (point[])original_list[i - 1];
                    point[] original_after = (point[])original_list[i];
                    len_all += Math.Sqrt((original_after[1].x - original_before[1].x) * (original_after[1].x - original_before[1].x) + (original_after[1].y - original_before[1].y) * (original_after[1].y - original_before[1].y) + (original_after[1].z - original_before[1].z) * (original_after[1].z - original_before[1].z));
                    面板_list[i] = len_all;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void table_to_arraylist(DataTable data_xy, ref ArrayList xy)
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
        public static bool convert_datatable(point start, point end, double dam_rate, DataTable original_table, ref double[] 转弯_list)
        {
            try
            {
                ArrayList xys = new ArrayList();
                table_to_arraylist(original_table, ref xys);
                point[] points_z = new point[xys.Count];
                get_z_by_xy(start, end, dam_rate, xys, ref points_z);
                转弯_list = new double[xys.Count];

                转弯_list[0] = 0;
                double len_all = 0.0;
                for (int i = 1; i < points_z.Length; i++)
                {
                    len_all += Math.Sqrt((points_z[i].x - points_z[i-1].x) * (points_z[i].x - points_z[i-1].x) + (points_z[i].y - points_z[i-1].y) * (points_z[i].y - points_z[i-1].y) + (points_z[i].z - points_z[i-1].z) * (points_z[i].z - points_z[i-1].z));
                    转弯_list[i] = len_all;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region AI代码
        public static double[] GenerateOutputArray(double[] x1, double[] x2, double limit1, double limit2, double len, double min, double max)
        {
            // 验证输入参数有效性
            if (min >= max) throw new ArgumentException("min必须小于max");
            if (limit1 < 0 || limit2 < 0) throw new ArgumentException("间隔限制不能为负值");
            if (x1 == null || x2 == null) throw new ArgumentNullException();

            // 生成候选区间
            var forbiddenIntervals = new List<Tuple<double, double>>();

            // 添加x1的禁止区间
            foreach (var x in x1)
            {
                var left = x - limit1;
                var right = x + limit1;
                forbiddenIntervals.Add(Tuple.Create(left, right));
            }

            // 添加x2的禁止区间
            foreach (var x in x2)
            {
                var left = x - limit2;
                var right = x + limit2;
                forbiddenIntervals.Add(Tuple.Create(left, right));
            }

            // 合并重叠区间
            var mergedIntervals = MergeIntervals(forbiddenIntervals);

            // 计算有效区间
            var validIntervals = CalculateValidIntervals(mergedIntervals, min, max);

            // 在有效区间中寻找最接近len的值
            var results = new List<double>();
            foreach (var interval in validIntervals)
            {
                // 确定候选值
                double candidate = len;
                if (len < interval.Item1) candidate = interval.Item1;
                else if (len > interval.Item2) candidate = interval.Item2;

                // 验证候选值是否有效
                if (candidate >= min && candidate <= max)
                {
                    results.Add(candidate);
                }
            }

            return results.ToArray();
        }

        private static List<Tuple<double, double>> MergeIntervals(List<Tuple<double, double>> intervals)
        {
            if (!intervals.Any()) return new List<Tuple<double, double>>();

            var sorted = intervals
                .OrderBy(x => x.Item1)
                .ThenBy(x => x.Item2)
                .ToList();

            var merged = new List<Tuple<double, double>>();
            var currentStart = sorted[0].Item1;
            var currentEnd = sorted[0].Item2;

            for (int i = 1; i < sorted.Count; i++)
            {
                if (sorted[i].Item1 <= currentEnd)
                {
                    currentEnd = Math.Max(currentEnd, sorted[i].Item2);
                }
                else
                {
                    merged.Add(Tuple.Create(currentStart, currentEnd));
                    currentStart = sorted[i].Item1;
                    currentEnd = sorted[i].Item2;
                }
            }

            merged.Add(Tuple.Create(currentStart, currentEnd));
            return merged;
        }

        private static List<Tuple<double, double>> CalculateValidIntervals(List<Tuple<double, double>> mergedIntervals, double min, double max)
        {
            var validIntervals = new List<Tuple<double, double>>();

            // 添加初始区间 [min, firstInterval.Start]
            if (mergedIntervals.Count > 0)
            {
                var first = mergedIntervals[0];
                if (min < first.Item1)
                {
                    validIntervals.Add(Tuple.Create(min, first.Item1));
                }
            }

            // 添加中间区间
            for (int i = 1; i < mergedIntervals.Count; i++)
            {
                var prev = mergedIntervals[i - 1];
                var curr = mergedIntervals[i];
                if (prev.Item2 < curr.Item1)
                {
                    validIntervals.Add(Tuple.Create(prev.Item2, curr.Item1));
                }
            }

            // 添加结束区间 [lastInterval.End, max]
            if (mergedIntervals.Count > 0)
            {
                var last = mergedIntervals.Last();
                if (last.Item2 < max)
                {
                    validIntervals.Add(Tuple.Create(last.Item2, max));
                }
            }
            else
            {
                // 如果没有禁止区间，整个[min,max]都是有效区间
                validIntervals.Add(Tuple.Create(min, max));
            }

            return validIntervals.Where(x => x.Item1 < x.Item2).ToList();
        }
        #endregion
    }
}
