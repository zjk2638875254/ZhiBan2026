using Bentley.UI.Controls.WinForms.PanelFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiBan
{
    public partial class Form_V3: Form
    {
        Form_堆石坝 f堆石坝 = new Form_堆石坝();
        Form_绘图 f绘图 = new Form_绘图();
        Form_面板 f面板 = new Form_面板();
        Form_趾板 f趾板 = new Form_趾板();

        #region 必要的参数
        public static DataTable data_x;
        public static point point_start = new point(0, 0, 0);
        public static point point_end = new point(0, 0, 0);
        public static double dam_rate = 0.0;

        public static ArrayList 面板_location = new ArrayList();
        #endregion

        public Form_V3()
        {
            InitializeComponent();
            x = 1000;
            y = 450;
            setTag(this);

            f堆石坝.TopLevel = false;
            f绘图.TopLevel = false;
            f面板.TopLevel = false;
            f趾板.TopLevel = false;
        }

        #region 自动调整窗体
        private float x; // 当前窗体的宽度
        private float y; // 当前窗体的高度
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //MessageBox.Show(newx.ToString() + "   " + newy.ToString());
            foreach (Control con in cons.Controls)
            {
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(';');
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx);
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy);
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx);
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy);
                    float currentSize = Convert.ToSingle(mytag[4]) * newy;
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }

        private void FormPanel_Resize(object sender, EventArgs e)
        {
            /*
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
            MessageBox.Show("resize panel");
            */
        }

        #endregion

        private void 面板堆石坝设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tool_item in BaseMenu.Items)
                tool_item.ForeColor = Color.Black;
            面板堆石坝设计ToolStripMenuItem.ForeColor = Color.Red;

            f堆石坝.Dock = DockStyle.Fill;
            f堆石坝.Show();
            BasePanel.Controls.Clear();
            BasePanel.Controls.Add(f堆石坝);
        }

        private void 面板设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tool_item in BaseMenu.Items)
                tool_item.ForeColor = Color.Black;
            面板设计ToolStripMenuItem.ForeColor = Color.Red;

            f面板.Dock = DockStyle.Fill;
            f面板.Show();
            BasePanel.Controls.Clear();
            BasePanel.Controls.Add(f面板);
        }

        private void 趾板设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tool_item in BaseMenu.Items)
                tool_item.ForeColor = Color.Black;
            趾板设计ToolStripMenuItem.ForeColor = Color.Red;

            f趾板.Dock = DockStyle.Fill;
            f趾板.Show();
            BasePanel.Controls.Clear();
            BasePanel.Controls.Add(f趾板);
        }

        private void 成果输出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tool_item in BaseMenu.Items)
                tool_item.ForeColor = Color.Black;
            成果输出ToolStripMenuItem.ForeColor = Color.Red;

            f绘图.Dock = DockStyle.Fill;
            f绘图.Show();
            BasePanel.Controls.Clear();
            BasePanel.Controls.Add(f绘图);
        }

        private void 更新数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tool_item in BaseMenu.Items)
                tool_item.ForeColor = Color.Black;
            更新数据库ToolStripMenuItem.ForeColor = Color.Red;
        }

        public static void refresh_para(string name, object value)
        {
            switch(name)
            {
                case "TableX":
                    data_x = (DataTable)value;
                    break;

                case "PointStart":
                    point_start = (point)value;
                    break;

                case "PointEnd":
                    point_end = (point)value;
                    break;

                case "DamRate":
                    dam_rate = (double)value;
                    break;

                case "MianBanArray":
                    面板_location = (ArrayList)value;
                    break;
            }
        }
    }
}
