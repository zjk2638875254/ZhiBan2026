using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    //向量数学计算，融合部分使用
    class Vector_In_Merge
    {
        //向量单位化
        public static bool unit_vec(ref double[] v)
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

        //获取两个向量的法向量
        public static double[] vertical(double[] v1, double[] v2)
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

        //向量间夹角
        public static double get_angle(double[] v1, double[] v2)
        {
            double mode1 = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1]);
            double mode2 = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1]);
            double angle = Math.Acos((v1[0] * v2[0] + v1[1] * v2[1]) / (mode1 * mode2));
            //MessageBox.Show(angle.ToString());
            double y = v2[1] / mode2 - v1[1] / mode1;
            if (y > 0)
                angle = -1 * angle;
            return angle;

        }

        //在基点基础上移动向量获得新点
        public static bool vec_point(point base_point, point vec, ref point res)
        {
            try
            {
                res.x = base_point.x + vec.x;
                res.y = base_point.y + vec.y;
                res.z = base_point.z + vec.z;
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        //向量间叉乘,需要向量维度一样
        public static double vec_X_vec(point v1, point v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }
    }
}
