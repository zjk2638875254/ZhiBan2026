using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiBan
{
    [Serializable]
    class DamData
    {
        public DataTable data_xy;
        public DataTable data_para;
        public double rate;

        public DamData(DataTable xy, DataTable para, double r)
        {
            data_xy = xy;
            data_para = para;
            rate = r;
        }

        public DamData()
        {
            data_xy = null;
            data_para = null;
            rate = 1.0;
        }
    }

    class point
    {
        public double x;
        public double y;
        public double z;
        public point(double X, double Y, double Z)
        {
            x = X; y = Y; z = Z;
        }
    }

    class section_para
    {
        public double Lax;
        public double Lbx;
        public double Lad;
        public double Lbc;
        public double T;
        public double rate_qt;
        public double Len;
        public section_para(double ax, double bx, double ad, double bc, double t, double r_qt, double len)
        {
            Lax = ax; Lbx = bx; Lad = ad; Lbc = bc; T = t; rate_qt = r_qt;Len = len;
        }
    }
}
