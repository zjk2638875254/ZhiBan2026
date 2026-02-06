namespace ZhiBan
{
    partial class Form_堆石坝
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
            this.end_z = new System.Windows.Forms.NumericUpDown();
            this.end_y = new System.Windows.Forms.NumericUpDown();
            this.end_x = new System.Windows.Forms.NumericUpDown();
            this.start_z = new System.Windows.Forms.NumericUpDown();
            this.start_y = new System.Windows.Forms.NumericUpDown();
            this.start_x = new System.Windows.Forms.NumericUpDown();
            this.data_xys = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rate_dam_input = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.导入X线坐标表 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.end_z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_xys)).BeginInit();
            this.SuspendLayout();
            // 
            // end_z
            // 
            this.end_z.DecimalPlaces = 4;
            this.end_z.Location = new System.Drawing.Point(188, 190);
            this.end_z.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.end_z.Name = "end_z";
            this.end_z.Size = new System.Drawing.Size(120, 29);
            this.end_z.TabIndex = 79;
            this.end_z.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // end_y
            // 
            this.end_y.DecimalPlaces = 4;
            this.end_y.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.end_y.Location = new System.Drawing.Point(188, 155);
            this.end_y.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.end_y.Name = "end_y";
            this.end_y.Size = new System.Drawing.Size(120, 29);
            this.end_y.TabIndex = 77;
            this.end_y.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // end_x
            // 
            this.end_x.DecimalPlaces = 4;
            this.end_x.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.end_x.Location = new System.Drawing.Point(188, 120);
            this.end_x.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.end_x.Name = "end_x";
            this.end_x.Size = new System.Drawing.Size(120, 29);
            this.end_x.TabIndex = 75;
            this.end_x.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // start_z
            // 
            this.start_z.DecimalPlaces = 4;
            this.start_z.Location = new System.Drawing.Point(188, 81);
            this.start_z.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.start_z.Name = "start_z";
            this.start_z.Size = new System.Drawing.Size(120, 29);
            this.start_z.TabIndex = 73;
            this.start_z.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // start_y
            // 
            this.start_y.DecimalPlaces = 4;
            this.start_y.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.start_y.Location = new System.Drawing.Point(188, 46);
            this.start_y.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.start_y.Name = "start_y";
            this.start_y.Size = new System.Drawing.Size(120, 29);
            this.start_y.TabIndex = 71;
            this.start_y.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // start_x
            // 
            this.start_x.DecimalPlaces = 4;
            this.start_x.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.start_x.Location = new System.Drawing.Point(188, 12);
            this.start_x.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.start_x.Name = "start_x";
            this.start_x.Size = new System.Drawing.Size(120, 29);
            this.start_x.TabIndex = 67;
            this.start_x.ValueChanged += new System.EventHandler(this.point_ValueChanged);
            // 
            // data_xys
            // 
            this.data_xys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.data_xys.BackgroundColor = System.Drawing.SystemColors.Control;
            this.data_xys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_xys.Location = new System.Drawing.Point(314, 11);
            this.data_xys.Name = "data_xys";
            this.data_xys.RowTemplate.Height = 23;
            this.data_xys.Size = new System.Drawing.Size(274, 377);
            this.data_xys.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(15, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 2);
            this.label2.TabIndex = 82;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 2);
            this.label1.TabIndex = 81;
            this.label1.Text = "label1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(171, 19);
            this.label12.TabIndex = 80;
            this.label12.Text = "坝轴线终点Z坐标：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(171, 19);
            this.label13.TabIndex = 78;
            this.label13.Text = "坝轴线终点Y坐标：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(171, 19);
            this.label14.TabIndex = 76;
            this.label14.Text = "坝轴线终点X坐标：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(171, 19);
            this.label11.TabIndex = 74;
            this.label11.Text = "坝轴线起点Z坐标：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(171, 19);
            this.label10.TabIndex = 72;
            this.label10.Text = "坝轴线起点Y坐标：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(171, 19);
            this.label8.TabIndex = 70;
            this.label8.Text = "坝轴线起点X坐标：";
            // 
            // rate_dam_input
            // 
            this.rate_dam_input.FormattingEnabled = true;
            this.rate_dam_input.Location = new System.Drawing.Point(141, 229);
            this.rate_dam_input.Name = "rate_dam_input";
            this.rate_dam_input.Size = new System.Drawing.Size(173, 27);
            this.rate_dam_input.TabIndex = 69;
            this.rate_dam_input.SelectedIndexChanged += new System.EventHandler(this.rate_dam_input_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 19);
            this.label9.TabIndex = 68;
            this.label9.Text = "面板坝坡比：";
            // 
            // 导入X线坐标表
            // 
            this.导入X线坐标表.Location = new System.Drawing.Point(16, 266);
            this.导入X线坐标表.Name = "导入X线坐标表";
            this.导入X线坐标表.Size = new System.Drawing.Size(292, 32);
            this.导入X线坐标表.TabIndex = 83;
            this.导入X线坐标表.Text = "导入X线坐标表";
            this.导入X线坐标表.UseVisualStyleBackColor = true;
            this.导入X线坐标表.Click += new System.EventHandler(this.导入X线坐标表_Click);
            // 
            // Form_堆石坝
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.导入X线坐标表);
            this.Controls.Add(this.end_z);
            this.Controls.Add(this.end_y);
            this.Controls.Add(this.end_x);
            this.Controls.Add(this.start_z);
            this.Controls.Add(this.start_y);
            this.Controls.Add(this.start_x);
            this.Controls.Add(this.data_xys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rate_dam_input);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_堆石坝";
            this.Text = "Form_堆石坝";
            ((System.ComponentModel.ISupportInitialize)(this.end_z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.end_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_xys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown end_z;
        private System.Windows.Forms.NumericUpDown end_y;
        private System.Windows.Forms.NumericUpDown end_x;
        private System.Windows.Forms.NumericUpDown start_z;
        private System.Windows.Forms.NumericUpDown start_y;
        private System.Windows.Forms.NumericUpDown start_x;
        private System.Windows.Forms.DataGridView data_xys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox rate_dam_input;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button 导入X线坐标表;
    }
}