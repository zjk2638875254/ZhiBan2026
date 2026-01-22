using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class FuncMerge
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
                common_xoy(sur1_1[4], sur1_2[4], sur2_1[4], sur2_2[4], ref common_Q);
                common_xoy(sur1_1[5], sur1_2[5], sur2_1[5], sur2_2[5], ref common_T);
                common_xoy(sur1_1[6], sur1_2[6], sur2_1[6], sur2_2[6], ref common_X);

                //锁定向里或向外
                bool near_or_far = true;
                
                //方法1
                double[] dir1 = new double[2] { sur1_2[6].x - sur1_1[6].x, sur1_2[6].y - sur1_1[6].y };
                double[] dir2 = new double[2] { sur2_2[6].x - sur2_1[6].x, sur2_2[6].y - sur2_1[6].y };

                double angle1 = Vector_In_Merge.get_angle(dir1, dam_dir);
                double angle2 = Vector_In_Merge.get_angle(dir2, dam_dir);
                //MessageBox.Show(angle1.ToString() + "    " + angle2.ToString());
                if (angle2 < angle1)
                    near_or_far = false;
                #region 不用了
                //方法2
                /*
                double len1 = Math.Sqrt((sur1_1[6].x - base_point.x) * (sur1_1[6].x - base_point.x) + (sur1_1[6].y - base_point.y) * (sur1_1[6].y - base_point.y) + (sur1_1[6].z - base_point.z) * (sur1_1[6].z - base_point.z));
                double len2 = Math.Sqrt((sur2_2[6].x - base_point.x) * (sur2_2[6].x - base_point.x) + (sur2_2[6].y - base_point.y) * (sur2_2[6].y - base_point.y) + (sur2_2[6].z - base_point.z) * (sur2_2[6].z - base_point.z));
                double len_middle = Math.Sqrt(((sur1_2[6].x + sur2_1[6].x)/2.0 - base_point.x) * ((sur1_2[6].x + sur2_1[6].x) / 2.0 - base_point.x) + ((sur1_2[6].y + sur2_1[6].y) / 2.0 - base_point.y) * ((sur1_2[6].y + sur2_1[6].y) / 2.0 - base_point.y) + ((sur1_2[6].z + sur2_1[6].z) / 2.0 - base_point.z) * ((sur1_2[6].z + sur2_1[6].z) / 2.0 - base_point.z));
                if (len1 < len_middle && len2 < len_middle)
                    near_or_far = false;
                */
                //方法3
                /*
                double A = dam_dir[1];
                double B = dam_dir[0];
                double C = (base_point.x + dam_dir[0]) * base_point.y - base_point.x * (base_point.y + dam_dir[1]);
                point visual_p = new point((sur1_2[6].x + sur2_1[6].x) / 2.0, (sur1_2[6].y + sur2_1[6].y) / 2.0 , (sur1_2[6].z + sur2_1[6].z) / 2.0);
                double len1 = Math.Abs(sur1_1[6].x * A + sur1_1[6].y * B + C);
                double len2 = Math.Abs(sur2_2[6].x * A + sur2_2[6].y * B + C);
                double len_middle = Math.Abs(visual_p.x * A + visual_p.y * B + C);
                if (len1 >= len_middle && len2 >= len_middle)
                    near_or_far = false;
                */
                #endregion
                
                #region 填写AD
                //AX面交线
                point[] AX_list1 = new point[4] { sur1_1[0], sur1_2[0], sur1_2[6], sur1_1[6] };
                point[] AX_list2 = new point[4] { sur2_1[0], sur2_2[0], sur2_2[6], sur2_1[6] };
                point vec_ad1 = new point(sur1_2[3].x - sur1_2[0].x, sur1_2[3].y - sur1_2[0].y, sur1_2[3].z - sur1_2[0].z);
                point vec_ad2 = new point(sur2_1[3].x - sur2_1[0].x, sur2_1[3].y - sur2_1[0].y, sur2_1[3].z - sur2_1[0].z);
                point[] res1 = new point[2], res2 = new point[2];
                ad_and_bc(AX_list1, AX_list2, vec_ad1, vec_ad2, near_or_far, ref res1, ref res2);

                #region 最好不用
                //如果超出界限，重新计算边界点并裁剪
                //根据向量间夹角判断
                /*
                point v_a1_first = new point(sur1_2[0].x - sur1_1[0].x, sur1_2[0].y - sur1_1[0].y, sur1_2[0].z - sur1_1[0].z);
                point v_a2_first = new point(res1[0].x - sur1_2[0].x, res1[0].y - sur1_2[0].y, res1[0].z - sur1_2[0].z);
                point v_d1_first = new point(sur1_2[3].x - sur1_1[3].x, sur1_2[3].y - sur1_1[3].y, sur1_2[3].z - sur1_1[3].z);
                point v_d2_first = new point(res1[3].x - sur1_2[3].x, res1[3].y - sur1_2[3].y, res1[3].z - sur1_2[3].z);
                point v_a1_second = new point(sur2_2[0].x - sur2_1[0].x, sur2_2[0].y - sur2_1[0].y, sur2_2[0].z - sur2_1[0].z);
                point v_a2_second = new point(res2[0].x - sur2_1[0].x, res2[0].y - sur2_1[0].y, res2[0].z - sur2_1[0].z);
                point v_d1_second = new point(sur2_2[3].x - sur2_1[3].x, sur2_2[3].y - sur2_1[3].y, sur2_2[3].z - sur2_1[3].z);
                point v_d2_second = new point(res2[3].x - sur2_1[3].x, res2[3].y - sur2_1[3].y, res2[3].z - sur2_1[3].z);
                if (Vector_In_Merge.vec_X_vec(v_a1_first, v_a2_first) > 0)
                {
                    //拟合后的点出界了
                }
                if (Vector_In_Merge.vec_X_vec(v_d1_first, v_d2_first) > 0)
                {
                    //拟合后的点出界了
                }
                if (Vector_In_Merge.vec_X_vec(v_a1_second, v_a2_second) > 0)
                {
                    //拟合后的点出界了
                }
                if (Vector_In_Merge.vec_X_vec(v_d1_second, v_d2_second) > 0)
                {
                    //拟合后的点出界了
                }
                */
                #endregion
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
    }
}
