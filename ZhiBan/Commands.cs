using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    class Commands
    {
        public static void MainForm(string unparsed)
        {
            MainForm MF = new MainForm();
            MF.Show();
            //MessageBox.Show("Success");
        }

        public static void AutoForm(string unparsed)
        {
            Form智能设计 F = new Form智能设计();
            F.Show();
            //MessageBox.Show("Success");
        }

        public static void MainFormV3(string unparsed)
        {
            Form_V3 F = new Form_V3();
            F.Show();
            //MessageBox.Show("Success");
        }

        public static void LoginForm(string unparsed)
        {
            LoginForm MF = new LoginForm();
            MF.Show();
            //MessageBox.Show("Success");
        }
    }
}
