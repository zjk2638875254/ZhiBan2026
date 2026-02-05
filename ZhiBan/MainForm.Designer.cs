
namespace ZhiBan
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.RadioPanel = new System.Windows.Forms.Panel();
            this.DirABTrue = new System.Windows.Forms.RadioButton();
            this.DirABFalse = new System.Windows.Forms.RadioButton();
            this.end_z = new System.Windows.Forms.NumericUpDown();
            this.end_y = new System.Windows.Forms.NumericUpDown();
            this.end_x = new System.Windows.Forms.NumericUpDown();
            this.start_z = new System.Windows.Forms.NumericUpDown();
            this.start_y = new System.Windows.Forms.NumericUpDown();
            this.start_x = new System.Windows.Forms.NumericUpDown();
            this.data__paras = new System.Windows.Forms.DataGridView();
            this.data_xys = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_show = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rate_dam_input = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.存储在本地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传到服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入X线坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入体型参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.三维展示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除文本框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放置参数表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.规则块建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.整体建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分块自由建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.趾板分块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分块调整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按板长建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试按板长建模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入坝轴线坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxMain.SuspendLayout();
            this.RadioPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.end_z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data__paras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_xys)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.RadioPanel);
            this.groupBoxMain.Controls.Add(this.end_z);
            this.groupBoxMain.Controls.Add(this.end_y);
            this.groupBoxMain.Controls.Add(this.end_x);
            this.groupBoxMain.Controls.Add(this.start_z);
            this.groupBoxMain.Controls.Add(this.start_y);
            this.groupBoxMain.Controls.Add(this.start_x);
            this.groupBoxMain.Controls.Add(this.data__paras);
            this.groupBoxMain.Controls.Add(this.data_xys);
            this.groupBoxMain.Controls.Add(this.label2);
            this.groupBoxMain.Controls.Add(this.label1);
            this.groupBoxMain.Controls.Add(this.txt_show);
            this.groupBoxMain.Controls.Add(this.label12);
            this.groupBoxMain.Controls.Add(this.label13);
            this.groupBoxMain.Controls.Add(this.label14);
            this.groupBoxMain.Controls.Add(this.label11);
            this.groupBoxMain.Controls.Add(this.label10);
            this.groupBoxMain.Controls.Add(this.label8);
            this.groupBoxMain.Controls.Add(this.rate_dam_input);
            this.groupBoxMain.Controls.Add(this.label9);
            this.groupBoxMain.Location = new System.Drawing.Point(9, 24);
            this.groupBoxMain.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(1162, 599);
            this.groupBoxMain.TabIndex = 24;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "参数化趾板建模-数学计算系统-V1.2";
            // 
            // RadioPanel
            // 
            this.RadioPanel.Controls.Add(this.DirABTrue);
            this.RadioPanel.Controls.Add(this.DirABFalse);
            this.RadioPanel.Location = new System.Drawing.Point(10, 274);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(293, 35);
            this.RadioPanel.TabIndex = 46;
            // 
            // DirABTrue
            // 
            this.DirABTrue.AutoSize = true;
            this.DirABTrue.Location = new System.Drawing.Point(3, 4);
            this.DirABTrue.Name = "DirABTrue";
            this.DirABTrue.Size = new System.Drawing.Size(112, 25);
            this.DirABTrue.TabIndex = 44;
            this.DirABTrue.TabStop = true;
            this.DirABTrue.Text = "坝轴正向";
            this.DirABTrue.UseVisualStyleBackColor = true;
            // 
            // DirABFalse
            // 
            this.DirABFalse.AutoSize = true;
            this.DirABFalse.Location = new System.Drawing.Point(173, 4);
            this.DirABFalse.Name = "DirABFalse";
            this.DirABFalse.Size = new System.Drawing.Size(112, 25);
            this.DirABFalse.TabIndex = 45;
            this.DirABFalse.TabStop = true;
            this.DirABFalse.Text = "坝轴反向";
            this.DirABFalse.UseVisualStyleBackColor = true;
            // 
            // end_z
            // 
            this.end_z.DecimalPlaces = 4;
            this.end_z.Location = new System.Drawing.Point(183, 203);
            this.end_z.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.end_z.Name = "end_z";
            this.end_z.Size = new System.Drawing.Size(120, 31);
            this.end_z.TabIndex = 35;
            // 
            // end_y
            // 
            this.end_y.DecimalPlaces = 4;
            this.end_y.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.end_y.Location = new System.Drawing.Point(183, 168);
            this.end_y.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.end_y.Name = "end_y";
            this.end_y.Size = new System.Drawing.Size(120, 31);
            this.end_y.TabIndex = 33;
            // 
            // end_x
            // 
            this.end_x.DecimalPlaces = 4;
            this.end_x.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.end_x.Location = new System.Drawing.Point(183, 133);
            this.end_x.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.end_x.Name = "end_x";
            this.end_x.Size = new System.Drawing.Size(120, 31);
            this.end_x.TabIndex = 31;
            // 
            // start_z
            // 
            this.start_z.DecimalPlaces = 4;
            this.start_z.Location = new System.Drawing.Point(183, 94);
            this.start_z.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.start_z.Name = "start_z";
            this.start_z.Size = new System.Drawing.Size(120, 31);
            this.start_z.TabIndex = 29;
            // 
            // start_y
            // 
            this.start_y.DecimalPlaces = 4;
            this.start_y.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.start_y.Location = new System.Drawing.Point(183, 59);
            this.start_y.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.start_y.Name = "start_y";
            this.start_y.Size = new System.Drawing.Size(120, 31);
            this.start_y.TabIndex = 27;
            // 
            // start_x
            // 
            this.start_x.DecimalPlaces = 4;
            this.start_x.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.start_x.Location = new System.Drawing.Point(183, 25);
            this.start_x.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.start_x.Name = "start_x";
            this.start_x.Size = new System.Drawing.Size(120, 31);
            this.start_x.TabIndex = 11;
            // 
            // data__paras
            // 
            this.data__paras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.data__paras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data__paras.Location = new System.Drawing.Point(628, 24);
            this.data__paras.Name = "data__paras";
            this.data__paras.RowTemplate.Height = 23;
            this.data__paras.Size = new System.Drawing.Size(528, 279);
            this.data__paras.TabIndex = 38;
            // 
            // data_xys
            // 
            this.data_xys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.data_xys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_xys.Location = new System.Drawing.Point(311, 24);
            this.data_xys.Name = "data_xys";
            this.data_xys.RowTemplate.Height = 23;
            this.data_xys.Size = new System.Drawing.Size(311, 279);
            this.data_xys.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(10, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(632, 2);
            this.label2.TabIndex = 43;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(632, 2);
            this.label1.TabIndex = 42;
            this.label1.Text = "label1";
            // 
            // txt_show
            // 
            this.txt_show.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_show.Location = new System.Drawing.Point(10, 309);
            this.txt_show.Name = "txt_show";
            this.txt_show.Size = new System.Drawing.Size(1146, 284);
            this.txt_show.TabIndex = 37;
            this.txt_show.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 21);
            this.label12.TabIndex = 36;
            this.label12.Text = "坝轴线终点Z坐标：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(189, 21);
            this.label13.TabIndex = 34;
            this.label13.Text = "坝轴线终点Y坐标：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(189, 21);
            this.label14.TabIndex = 32;
            this.label14.Text = "坝轴线终点X坐标：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(189, 21);
            this.label11.TabIndex = 30;
            this.label11.Text = "坝轴线起点Z坐标：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(189, 21);
            this.label10.TabIndex = 28;
            this.label10.Text = "坝轴线起点Y坐标：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 21);
            this.label8.TabIndex = 26;
            this.label8.Text = "坝轴线起点X坐标：";
            // 
            // rate_dam_input
            // 
            this.rate_dam_input.FormattingEnabled = true;
            this.rate_dam_input.Location = new System.Drawing.Point(132, 239);
            this.rate_dam_input.Name = "rate_dam_input";
            this.rate_dam_input.Size = new System.Drawing.Size(173, 29);
            this.rate_dam_input.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 21);
            this.label9.TabIndex = 23;
            this.label9.Text = "面板坝坡比：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目ToolStripMenuItem,
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.建模ToolStripMenuItem,
            this.趾板分块ToolStripMenuItem,
            this.按板长建模ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.测试按板长建模ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1180, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 项目ToolStripMenuItem
            // 
            this.项目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.存储在本地ToolStripMenuItem,
            this.上传到服务器ToolStripMenuItem});
            this.项目ToolStripMenuItem.Font = new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem";
            this.项目ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.项目ToolStripMenuItem.Text = "项目";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.打开ToolStripMenuItem.Text = "打开本地文件";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.ReadFile_Click);
            // 
            // 存储在本地ToolStripMenuItem
            // 
            this.存储在本地ToolStripMenuItem.Name = "存储在本地ToolStripMenuItem";
            this.存储在本地ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.存储在本地ToolStripMenuItem.Text = "存储在本地";
            this.存储在本地ToolStripMenuItem.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // 上传到服务器ToolStripMenuItem
            // 
            this.上传到服务器ToolStripMenuItem.Name = "上传到服务器ToolStripMenuItem";
            this.上传到服务器ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.上传到服务器ToolStripMenuItem.Text = "上传到服务器";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入X线坐标ToolStripMenuItem,
            this.导入体型参数ToolStripMenuItem,
            this.导入坝轴线坐标ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 导入X线坐标ToolStripMenuItem
            // 
            this.导入X线坐标ToolStripMenuItem.Name = "导入X线坐标ToolStripMenuItem";
            this.导入X线坐标ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.导入X线坐标ToolStripMenuItem.Text = "导入X线坐标";
            this.导入X线坐标ToolStripMenuItem.Click += new System.EventHandler(this.导入X线坐标ToolStripMenuItem_Click);
            // 
            // 导入体型参数ToolStripMenuItem
            // 
            this.导入体型参数ToolStripMenuItem.Name = "导入体型参数ToolStripMenuItem";
            this.导入体型参数ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.导入体型参数ToolStripMenuItem.Text = "导入体型参数";
            this.导入体型参数ToolStripMenuItem.Click += new System.EventHandler(this.导入体型参数ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.三维展示ToolStripMenuItem,
            this.清除文本框ToolStripMenuItem,
            this.放置参数表ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 三维展示ToolStripMenuItem
            // 
            this.三维展示ToolStripMenuItem.Name = "三维展示ToolStripMenuItem";
            this.三维展示ToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.三维展示ToolStripMenuItem.Text = "三维展示";
            // 
            // 清除文本框ToolStripMenuItem
            // 
            this.清除文本框ToolStripMenuItem.Name = "清除文本框ToolStripMenuItem";
            this.清除文本框ToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.清除文本框ToolStripMenuItem.Text = "清除文本框";
            this.清除文本框ToolStripMenuItem.Click += new System.EventHandler(this.清除文本框ToolStripMenuItem_Click);
            // 
            // 放置参数表ToolStripMenuItem
            // 
            this.放置参数表ToolStripMenuItem.Name = "放置参数表ToolStripMenuItem";
            this.放置参数表ToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.放置参数表ToolStripMenuItem.Text = "放置参数表";
            // 
            // 建模ToolStripMenuItem
            // 
            this.建模ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.规则块建模ToolStripMenuItem,
            this.整体建模ToolStripMenuItem,
            this.分块自由建模ToolStripMenuItem});
            this.建模ToolStripMenuItem.Name = "建模ToolStripMenuItem";
            this.建模ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.建模ToolStripMenuItem.Text = "建模";
            // 
            // 规则块建模ToolStripMenuItem
            // 
            this.规则块建模ToolStripMenuItem.Name = "规则块建模ToolStripMenuItem";
            this.规则块建模ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.规则块建模ToolStripMenuItem.Text = "规则块建模-边缘需要手动检查调整";
            this.规则块建模ToolStripMenuItem.Click += new System.EventHandler(this.规则块建模ToolStripMenuItem_Click);
            // 
            // 整体建模ToolStripMenuItem
            // 
            this.整体建模ToolStripMenuItem.Name = "整体建模ToolStripMenuItem";
            this.整体建模ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.整体建模ToolStripMenuItem.Text = "整体建模-手动分割";
            this.整体建模ToolStripMenuItem.Click += new System.EventHandler(this.整体建模ToolStripMenuItem_Click);
            // 
            // 分块自由建模ToolStripMenuItem
            // 
            this.分块自由建模ToolStripMenuItem.Name = "分块自由建模ToolStripMenuItem";
            this.分块自由建模ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.分块自由建模ToolStripMenuItem.Text = "分块自由建模";
            this.分块自由建模ToolStripMenuItem.Click += new System.EventHandler(this.分块自由建模ToolStripMenuItem_Click);
            // 
            // 趾板分块ToolStripMenuItem
            // 
            this.趾板分块ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分块调整ToolStripMenuItem});
            this.趾板分块ToolStripMenuItem.Name = "趾板分块ToolStripMenuItem";
            this.趾板分块ToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.趾板分块ToolStripMenuItem.Text = "趾板分块";
            // 
            // 分块调整ToolStripMenuItem
            // 
            this.分块调整ToolStripMenuItem.Name = "分块调整ToolStripMenuItem";
            this.分块调整ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.分块调整ToolStripMenuItem.Text = "分块调整";
            this.分块调整ToolStripMenuItem.Click += new System.EventHandler(this.分块调整ToolStripMenuItem_Click);
            // 
            // 按板长建模ToolStripMenuItem
            // 
            this.按板长建模ToolStripMenuItem.Name = "按板长建模ToolStripMenuItem";
            this.按板长建模ToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.按板长建模ToolStripMenuItem.Text = "按板长建模";
            this.按板长建模ToolStripMenuItem.Click += new System.EventHandler(this.分块自由建模ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 测试按板长建模ToolStripMenuItem
            // 
            this.测试按板长建模ToolStripMenuItem.Name = "测试按板长建模ToolStripMenuItem";
            this.测试按板长建模ToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.测试按板长建模ToolStripMenuItem.Text = "测试-按板长建模";
            this.测试按板长建模ToolStripMenuItem.Click += new System.EventHandler(this.测试按板长建模ToolStripMenuItem_Click);
            // 
            // 导入坝轴线坐标ToolStripMenuItem
            // 
            this.导入坝轴线坐标ToolStripMenuItem.Name = "导入坝轴线坐标ToolStripMenuItem";
            this.导入坝轴线坐标ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.导入坝轴线坐标ToolStripMenuItem.Text = "导入坝轴线坐标";
            this.导入坝轴线坐标ToolStripMenuItem.Click += new System.EventHandler(this.导入坝轴线坐标ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 632);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBoxMain);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Show_Load);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.end_z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data__paras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_xys)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.NumericUpDown end_z;
        private System.Windows.Forms.NumericUpDown end_y;
        private System.Windows.Forms.NumericUpDown end_x;
        private System.Windows.Forms.NumericUpDown start_z;
        private System.Windows.Forms.NumericUpDown start_y;
        private System.Windows.Forms.NumericUpDown start_x;
        private System.Windows.Forms.DataGridView data__paras;
        private System.Windows.Forms.DataGridView data_xys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txt_show;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox rate_dam_input;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 存储在本地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传到服务器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 三维展示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除文本框ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 规则块建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 整体建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放置参数表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分块自由建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 趾板分块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分块调整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入X线坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入体型参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按板长建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.RadioButton DirABFalse;
        private System.Windows.Forms.RadioButton DirABTrue;
        private System.Windows.Forms.Panel RadioPanel;
        private System.Windows.Forms.ToolStripMenuItem 测试按板长建模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入坝轴线坐标ToolStripMenuItem;
    }
}