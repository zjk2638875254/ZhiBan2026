using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class 趾板_FuncMerge
    {
        public static bool merge_main(point[] sur1_1, point[] sur1_2, point[] sur2_1, point[] sur2_2, double[] dam_dir, point base_point, ref point[] sur1, ref point[] sur2)
        {
            try
            {
                //ABCDQTX
                sur1 = new point[7];
                sur2 = new point[7];
                //QT延长线原则上相交
                point common_Q = new point(0, 0, 0), common_T = new point(0, 0, 0), common_X = new point(0, 0, 0);
                Log.write_log("D:\\趾板Test.txt", "CommonQ:  ");
                common_xoy(sur1_1[4], sur1_2[4], sur2_1[4], sur2_2[4], ref common_Q);
                Log.write_log("D:\\趾板Test.txt", "CommonT:  " + "\r\n");
                common_xoy(sur1_1[5], sur1_2[5], sur2_1[5], sur2_2[5], ref common_T);
                Log.write_log("D:\\趾板Test.txt", "CommonX:  " + "\r\n");
                common_xoy(sur1_1[6], sur1_2[6], sur2_1[6], sur2_2[6], ref common_X);
                //锁定向里或向外
                bool near_or_far = true;

                //方法1
                double[] dir1 = new double[2] { sur1_2[6].x - sur1_1[6].x, sur1_2[6].y - sur1_1[6].y };
                double[] dir2 = new double[2] { sur2_2[6].x - sur2_1[6].x, sur2_2[6].y - sur2_1[6].y };

                double angle1 = Vector_In_Merge.get_angle(dir1, dam_dir);
                double angle2 = Vector_In_Merge.get_angle(dir2, dam_dir);
                //MessageBox.Show(angle1.ToString() + "    " + angle2.ToString());
                if (angle2 > angle1)
                    near_or_far = false;

                #region 填写AD
                //AX面交线
                point[] AX_list1 = new point[4] { sur1_1[0], sur1_2[0], sur1_2[6], sur1_1[6] };
                point[] AX_list2 = new point[4] { sur2_1[0], sur2_2[0], sur2_2[6], sur2_1[6] };
                point vec_ad1 = new point(sur1_2[3].x - sur1_2[0].x, sur1_2[3].y - sur1_2[0].y, sur1_2[3].z - sur1_2[0].z);
                point vec_ad2 = new point(sur2_1[3].x - sur2_1[0].x, sur2_1[3].y - sur2_1[0].y, sur2_1[3].z - sur2_1[0].z);
                point[] res1 = new point[2], res2 = new point[2];
                ad_and_bc(AX_list1, AX_list2, vec_ad1, vec_ad2, near_or_far, ref res1, ref res2);
                sur1[0] = res1[0]; sur1[3] = res1[1]; sur2[0] = res2[0]; sur2[3] = res2[1];
                #endregion
                #region 填写BC
                //BX面交线
                near_or_far = !near_or_far;
                point[] BX_list1 = new point[4] { sur1_1[1], sur1_2[1], sur1_2[6], sur1_1[6] };
                point[] BX_list2 = new point[4] { sur2_1[1], sur2_2[1], sur2_2[6], sur2_1[6] };
                point vec_bc1 = new point(sur1_2[2].x - sur1_2[1].x, sur1_2[2].y - sur1_2[1].y, sur1_2[2].z - sur1_2[1].z);
                point vec_bc2 = new point(sur2_1[2].x - sur2_1[1].x, sur2_1[2].y - sur2_1[1].y, sur2_1[2].z - sur2_1[1].z);
                res1 = new point[2]; res2 = new point[2];
                ad_and_bc(BX_list1, BX_list2, vec_bc1, vec_bc2, near_or_far, ref res1, ref res2);
                sur1[1] = res1[0]; sur1[2] = res1[1]; sur2[1] = res2[0]; sur2[2] = res2[1];
                #endregion


                //near_or_far = !near_or_far;

                sur1[5] = new point(common_T.x, common_T.y, common_T.z);
                sur2[5] = new point(common_T.x, common_T.y, common_T.z);
                sur1[4] = new point(common_Q.x, common_Q.y, common_Q.z);
                sur2[4] = new point(common_Q.x, common_Q.y, common_Q.z);
                sur1[6] = new point(common_X.x, common_X.y, common_X.z);
                sur2[6] = new point(common_X.x, common_X.y, common_X.z);


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "融合警告");
                return false;
            }
        }

        #region 面面交点
        //面面交线：保留近的一个
        private static bool surfance_point_near(point[] sur1, point[] sur2, ref point res, ref int line_id)
        {
            try
            {
                //AAXX-DDQQ-BBXX-
                //X和Q为起点，point[3]，不能用point[2]
                double[] sur1_v1 = new double[3] { sur1[0].x - sur1[1].x, sur1[0].y - sur1[1].y, sur1[0].z - sur1[1].z };
                double[] sur1_v2 = new double[3] { sur1[1].x - sur1[2].x, sur1[1].y - sur1[2].y, sur1[1].z - sur1[2].z };
                double[] sur1_vec = Vector_In_Merge.vertical(sur1_v1, sur1_v2);
                double[] sur2_v1 = new double[3] { sur2[0].x - sur2[1].x, sur2[0].y - sur2[1].y, sur2[0].z - sur2[1].z };
                double[] sur2_v2 = new double[3] { sur2[1].x - sur2[2].x, sur2[1].y - sur2[2].y, sur2[1].z - sur2[2].z };
                double[] sur2_vec = Vector_In_Merge.vertical(sur2_v1, sur2_v2);

                double[] line_vec = Vector_In_Merge.vertical(sur1_vec, sur2_vec);

                point p_start = new point(0, 0, 0);
                common_xoy(sur1[2], sur1[3], sur2[2], sur2[3], ref p_start);

                point p_end = new point(p_start.x + line_vec[0], p_start.y + line_vec[1], p_start.z + line_vec[2]);
                point common_p1 = new point(0, 0, 0);
                point common_p2 = new point(0, 0, 0);
                //面面交线在两面上不同的截止点
                common_line_point(p_start, p_end, sur1[0], sur1[1], ref common_p1);
                common_line_point(p_start, p_end, sur2[0], sur2[1], ref common_p2);

                double len1 = Math.Sqrt((p_start.x - common_p1.x) * (p_start.x - common_p1.x) + (p_start.y - common_p1.y) * (p_start.y - common_p1.y) + (p_start.z - common_p1.z) * (p_start.z - common_p1.z));
                double len2 = Math.Sqrt((p_start.x - common_p2.x) * (p_start.x - common_p2.x) + (p_start.y - common_p2.y) * (p_start.y - common_p2.y) + (p_start.z - common_p2.z) * (p_start.z - common_p2.z));
                if (len1 < len2)
                {
                    res.x = common_p1.x;
                    res.y = common_p1.y;
                    res.z = common_p1.z;
                    line_id = 1;
                }
                else
                {
                    res.x = common_p2.x;
                    res.y = common_p2.y;
                    res.z = common_p2.z;
                    line_id = 2;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "面交线警告");
                return false;
            }
        }
        //面面交线：保留远的一个
        private static bool surfance_point_far(point[] sur1, point[] sur2, ref point res, ref int line_id)
        {
            try
            {
                //AAXX-DDQQ-BBXX-
                //X和Q为起点，point[3]，不能用point[2]
                double[] sur1_v1 = new double[3] { sur1[0].x - sur1[1].x, sur1[0].y - sur1[1].y, sur1[0].z - sur1[1].z };
                double[] sur1_v2 = new double[3] { sur1[1].x - sur1[2].x, sur1[1].y - sur1[2].y, sur1[1].z - sur1[2].z };
                double[] sur1_vec = Vector_In_Merge.vertical(sur1_v1, sur1_v2);
                double[] sur2_v1 = new double[3] { sur2[0].x - sur2[1].x, sur2[0].y - sur2[1].y, sur2[0].z - sur2[1].z };
                double[] sur2_v2 = new double[3] { sur2[1].x - sur2[2].x, sur2[1].y - sur2[2].y, sur2[1].z - sur2[2].z };
                double[] sur2_vec = Vector_In_Merge.vertical(sur2_v1, sur2_v2);

                double[] line_vec = Vector_In_Merge.vertical(sur1_vec, sur2_vec);

                point p_start = new point(0, 0, 0);
                common_xoy(sur1[2], sur1[3], sur2[2], sur2[3], ref p_start);

                point p_end = new point(p_start.x + line_vec[0], p_start.y + line_vec[1], p_start.z + line_vec[2]);
                point common_p1 = new point(0, 0, 0);
                point common_p2 = new point(0, 0, 0);
                //面面交线在两面上不同的截止点
                common_line_point(p_start, p_end, sur1[0], sur1[1], ref common_p1);
                common_line_point(p_start, p_end, sur2[0], sur2[1], ref common_p2);

                double len1 = Math.Sqrt((p_start.x - common_p1.x) * (p_start.x - common_p1.x) + (p_start.y - common_p1.y) * (p_start.y - common_p1.y) + (p_start.z - common_p1.z) * (p_start.z - common_p1.z));
                double len2 = Math.Sqrt((p_start.x - common_p2.x) * (p_start.x - common_p2.x) + (p_start.y - common_p2.y) * (p_start.y - common_p2.y) + (p_start.z - common_p2.z) * (p_start.z - common_p2.z));
                if (len1 > len2)
                {
                    res.x = common_p1.x;
                    res.y = common_p1.y;
                    res.z = common_p1.z;
                    line_id = 1;
                }
                else
                {
                    res.x = common_p2.x;
                    res.y = common_p2.y;
                    res.z = common_p2.z;
                    line_id = 2;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "面交线警告");
                return false;
            }
        }

        //两个共面直线的交点
        private static bool common_line_point(point line1_p1, point line1_p2, point line2_p1, point line2_p2, ref point common_p)
        {
            try
            {
                //Ax+By+C=0直线在XoY面上的投影
                double A1 = line1_p2.y - line1_p1.y;
                double B1 = line1_p1.x - line1_p2.x;
                double C1 = line1_p2.x * line1_p1.y - line1_p1.x * line1_p2.y;

                double A2 = line2_p2.y - line2_p1.y;
                double B2 = line2_p1.x - line2_p2.x;
                double C2 = line2_p2.x * line2_p1.y - line2_p1.x * line2_p2.y;

                common_p.y = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);
                common_p.x = (C2 * B1 - C1 * B2) / (A1 * B2 - A2 * B1);
                //带入计算z
                //Ax+By+Cz+D=0;
                double[] vec1 = new double[3] { line1_p2.x - line1_p1.x, line1_p2.y - line1_p1.y, line1_p2.z - line1_p1.z };
                double x_ad = common_p.x - line1_p1.x;
                //common_p.y = x_ad / vec1[0] * vec1[1];
                common_p.z = x_ad / vec1[0] * vec1[2] + line1_p1.z;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "线交点警告");
                return false;
            }
        }
        //异面直线强行拟合
        private static bool common_xoy(point p11, point p12, point p21, point p22, ref point commonp)
        {
            try
            {
                //Ax+By+C=0直线在XoY面上的投影
                double A1 = p12.y - p11.y;
                double B1 = p11.x - p12.x;
                double C1 = p12.x * p11.y - p11.x * p12.y;

                double A2 = p22.y - p21.y;
                double B2 = p21.x - p22.x;
                double C2 = p22.x * p21.y - p21.x * p22.y;

                commonp.y = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);
                commonp.x = (C2 * B1 - C1 * B2) / (A1 * B2 - A2 * B1);

                double add_x1 = commonp.x - p11.x;
                double z1 = p11.z + add_x1 / (p12.x - p11.x) * (p12.z - p11.z);
                double add_x2 = commonp.x - p21.x;
                double z2 = p21.z + add_x2 / (p22.x - p21.x) * (p22.z - p21.z);
                Log.write_log("D:\\趾板Test.txt", "z1:" + z1.ToString() + "  " + "z2:" + z2.ToString() + "\r\n");
                commonp.z = (z1 + z2) / 2.0;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "面交线警告");
                return false;
            }

        }
        #endregion

        #region 线线交点
        //线线在XoY面上的投影交点,pid代表点在第几段直线上
        private static bool line_point(point line1_p1, point line1_p2, point line2_p1, point line2_p2, int pid, ref point common_p)
        {
            try
            {
                //Ax+By+C=0
                double A1 = line1_p2.y - line1_p1.y;
                double B1 = line1_p1.x - line1_p2.x;
                double C1 = line1_p2.x * line1_p1.y - line1_p1.x * line1_p2.y;

                double A2 = line2_p2.y - line2_p1.y;
                double B2 = line2_p1.x - line2_p2.x;
                double C2 = line2_p2.x * line2_p1.y - line2_p1.x * line2_p2.y;

                common_p.y = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);
                common_p.x = (C2 * B1 - C1 * B2) / (A1 * B2 - A2 * B1);
                //反算哪一个的z值
                if (pid == 1)
                {
                    line_z(line1_p1, line1_p2, ref common_p);
                }
                else
                {
                    line_z(line2_p1, line2_p2, ref common_p);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "线交点警告");
                return false;
            }
        }
        //线线交点反算Z值
        private static bool line_z(point line_p1, point line_p2, ref point target)
        {
            try
            {
                double[] dir = new double[3] { line_p2.x - line_p1.x, line_p2.y - line_p1.y, line_p2.z - line_p1.z };
                double diff_x = target.x - line_p1.x;
                target.z = diff_x / dir[0] * dir[2] + line_p1.z;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "获取z坐标警告");
                return false;
            }
        }
        #endregion

        #region 计算融合后的特异点（线线交点和面面交点）
        private static bool ad_and_bc(point[] sur1, point[] sur2, point vec1, point vec2, bool near_or_far, ref point[] res1, ref point[] res2)
        {
            //near:true;far:false;
            //sur:AAXX
            //vec:
            try
            {
                res1 = new point[2];
                res2 = new point[2];
                point A1 = new point(0, 0, 0);
                int pidA = 0;
                if (near_or_far)
                    surfance_point_near(sur1, sur2, ref A1, ref pidA);
                else
                    surfance_point_far(sur1, sur2, ref A1, ref pidA);
                if (pidA == 1)
                {
                    res1[0] = A1;
                    point D = new point(0, 0, 0);
                    Vector_In_Merge.vec_point(A1, vec1, ref D);
                    res1[1] = D;
                    pidA = 2;
                }
                else
                {
                    res2[0] = A1;
                    point D = new point(0, 0, 0);
                    Vector_In_Merge.vec_point(A1, vec2, ref D);
                    res2[1] = D;
                    pidA = 1;
                }
                point A2 = new point(0, 0, 0);
                line_point(sur1[0], sur1[1], sur2[0], sur2[1], pidA, ref A2);
                if (pidA == 1)
                {
                    res1[0] = A2;
                    point D = new point(0, 0, 0);
                    Vector_In_Merge.vec_point(A2, vec1, ref D);
                    res1[1] = D;
                }
                else
                {
                    res2[0] = A2;
                    point D = new point(0, 0, 0);
                    Vector_In_Merge.vec_point(A2, vec2, ref D);
                    res2[1] = D;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 精准拟合
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
        //l1和l2平行
        private static bool get_sur_precise(point l1p1, point l1p2, point l2p1, point l2p2, ref point sur, ref double D)
        {
            try
            {
                //Ax+By+Cz+D=0
                double[] l1 = new double[3] { l1p2.x - l1p1.x, l1p2.y - l1p1.y, l1p2.z - l1p1.z };
                double[] l2 = new double[3] { l2p1.x - l1p1.x, l2p1.y - l1p1.y, l2p1.z - l1p1.z };
                double[] paras = get_vec(l1, l2);
                sur.x = paras[0];
                sur.y = paras[1];
                sur.z = paras[2];
                D = -1.0 * (paras[0] * l1p1.x + paras[1] * l1p1.y + paras[2] * l1p1.z);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "线交点警告");
                return false;
            }
        }

        private static bool line_sur_precise(point sur1, double D, point p1, point p2, ref point common_p)
        {
            try
            {
                //Ax+By+C=0
                double A = sur1.x;
                double B = sur1.y;
                double C = sur1.z;

                double m = p2.x - p1.x;
                double n = p2.y - p1.y;
                double p = p2.z - p1.z;
                double t = -1.0 * (A * p1.x + B * p1.y + C * p1.z + D) / (A * m + B * n + C * p);
                common_p.x = p1.x + t * m;
                common_p.y = p1.y + t * n;
                common_p.z = p1.z + t * p;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "线交点警告");
                return false;
            }
        }

        private static bool ad_and_bc_precise(point[] sur1_1, point[] sur1_2, point[] sur2_1, point[] sur2_2, bool near_or_far, ref point[] res1, ref point[] res2)
        {
            //near:true;far:false;
            //sur:AAXX
            //vec:
            try
            {
                res1 = new point[2];
                res2 = new point[2];

                point A1 = new point(0, 0, 0);
                int pidA = 0;
                if (near_or_far)
                    surfance_point_near(sur1_1, sur1_2, ref A1, ref pidA);
                else
                    surfance_point_far(sur1_1, sur1_2, ref A1, ref pidA);
                if (pidA == 1)
                {
                    res1[0] = A1;
                    pidA = 2;
                }
                else
                {
                    res2[0] = A1;
                    pidA = 1;
                }
                point A2 = new point(0, 0, 0);
                line_point(sur1_1[0], sur1_1[1], sur1_2[0], sur1_2[1], pidA, ref A2);
                if (pidA == 1)
                {
                    res1[0] = A2;
                }
                else
                {
                    res2[0] = A2;
                }

                point D1 = new point(0, 0, 0);
                int pidD = 0;
                if (near_or_far)
                    surfance_point_near(sur2_1, sur2_2, ref D1, ref pidD);
                else
                    surfance_point_far(sur2_1, sur2_2, ref D1, ref pidD);
                if (pidD == 1)
                {
                    res1[1] = D1;
                    pidD = 2;
                }
                else
                {
                    res2[1] = D1;
                    pidD = 1;
                }
                point D2 = new point(0, 0, 0);
                line_point(sur2_1[0], sur2_1[1], sur2_2[0], sur2_2[1], pidD, ref D2);
                if (pidD == 1)
                {
                    res1[1] = D2;
                }
                else
                {
                    res2[1] = D2;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool merge_main_precise(point[] sur1_1, point[] sur1_2, point[] sur2_1, point[] sur2_2, double[] dam_dir, point base_point, ref point[] sur1, ref point[] sur2)
        {
            try
            {
                //ABCDTQX
                sur1 = new point[7];
                sur2 = new point[7];
                //QT延长线原则上相交
                point common_Q = new point(0, 0, 0), common_T = new point(0, 0, 0), common_X = new point(0, 0, 0);
                Log.write_log("D:\\趾板Test.txt", "CommonT:  ");
                common_xoy(sur1_1[4], sur1_2[4], sur2_1[4], sur2_2[4], ref common_Q);
                Log.write_log("D:\\趾板Test.txt", "CommonQ:  " + "\r\n");
                common_xoy(sur1_1[5], sur1_2[5], sur2_1[5], sur2_2[5], ref common_T);
                Log.write_log("D:\\趾板Test.txt", "CommonX:  " + "\r\n");
                common_xoy(sur1_1[6], sur1_2[6], sur2_1[6], sur2_2[6], ref common_X);
                //锁定向里或向外
                bool near_or_far = true;

                //方法1
                double[] dir1 = new double[2] { sur1_2[6].x - sur1_1[6].x, sur1_2[6].y - sur1_1[6].y };
                double[] dir2 = new double[2] { sur2_2[6].x - sur2_1[6].x, sur2_2[6].y - sur2_1[6].y };

                double angle1 = Vector_In_Merge.get_angle(dir1, dam_dir);
                double angle2 = Vector_In_Merge.get_angle(dir2, dam_dir);
                //MessageBox.Show(angle1.ToString() + "    " + angle2.ToString());
                if (angle2 > angle1)
                    near_or_far = false;

                #region 填写AD
                point[] res1 = new point[2], res2 = new point[2];
                //AX面交线
                point[] AX_list1 = new point[4] { sur1_1[0], sur1_2[0], sur1_2[6], sur1_1[6] };
                point[] AX_list2 = new point[4] { sur2_1[0], sur2_2[0], sur2_2[6], sur2_1[6] };
                point[] DQ_list1 = new point[4] { sur1_1[3], sur1_2[3], sur1_2[5], sur1_1[5] };
                point[] DQ_list2 = new point[4] { sur2_1[3], sur2_2[3], sur2_2[5], sur2_1[5] };
                ad_and_bc_precise(AX_list1, AX_list2, DQ_list1, DQ_list2, near_or_far, ref res1, ref res2);
                sur1[0] = res1[0]; sur1[3] = res1[1]; sur2[0] = res2[0]; sur2[3] = res2[1];
                #endregion
                #region 填写BC
                //BX面交线
                near_or_far = !near_or_far;
                point[] BX_list1 = new point[4] { sur1_1[1], sur1_2[1], sur1_2[6], sur1_1[6] };
                point[] BX_list2 = new point[4] { sur2_1[1], sur2_2[1], sur2_2[6], sur2_1[6] };
                point vec_bc1 = new point(sur1_2[2].x - sur1_2[1].x, sur1_2[2].y - sur1_2[1].y, sur1_2[2].z - sur1_2[1].z);
                point vec_bc2 = new point(sur2_1[2].x - sur2_1[1].x, sur2_1[2].y - sur2_1[1].y, sur2_1[2].z - sur2_1[1].z);
                res1 = new point[2]; res2 = new point[2];
                ad_and_bc(BX_list1, BX_list2, vec_bc1, vec_bc2, near_or_far, ref res1, ref res2);
                sur1[1] = res1[0]; sur1[2] = res1[1]; sur2[1] = res2[0]; sur2[2] = res2[1];
                #endregion


                //near_or_far = !near_or_far;

                sur1[5] = new point(common_T.x, common_T.y, common_T.z);
                sur2[5] = new point(common_T.x, common_T.y, common_T.z);
                sur1[4] = new point(common_Q.x, common_Q.y, common_Q.z);
                sur2[4] = new point(common_Q.x, common_Q.y, common_Q.z);
                sur1[6] = new point(common_X.x, common_X.y, common_X.z);
                sur2[6] = new point(common_X.x, common_X.y, common_X.z);


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "融合警告");
                return false;
            }
        }
        #endregion
    }
}
