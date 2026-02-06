namespace ZhiBan
{
    partial class Form_绘图
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.趾板建模 = new System.Windows.Forms.Button();
            this.二维出图 = new System.Windows.Forms.Button();
            this.输出Excel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "趾板分块";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 172);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(576, 216);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(134, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(453, 154);
            this.dataGridView1.TabIndex = 2;
            // 
            // 趾板建模
            // 
            this.趾板建模.Location = new System.Drawing.Point(13, 52);
            this.趾板建模.Name = "趾板建模";
            this.趾板建模.Size = new System.Drawing.Size(115, 34);
            this.趾板建模.TabIndex = 3;
            this.趾板建模.Text = "趾板建模";
            this.趾板建模.UseVisualStyleBackColor = true;
            // 
            // 二维出图
            // 
            this.二维出图.Location = new System.Drawing.Point(13, 92);
            this.二维出图.Name = "二维出图";
            this.二维出图.Size = new System.Drawing.Size(115, 34);
            this.二维出图.TabIndex = 4;
            this.二维出图.Text = "二维出图";
            this.二维出图.UseVisualStyleBackColor = true;
            // 
            // 输出Excel
            // 
            this.输出Excel.Location = new System.Drawing.Point(12, 132);
            this.输出Excel.Name = "输出Excel";
            this.输出Excel.Size = new System.Drawing.Size(115, 34);
            this.输出Excel.TabIndex = 5;
            this.输出Excel.Text = "输出Excel";
            this.输出Excel.UseVisualStyleBackColor = true;
            // 
            // Form_绘图
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.输出Excel);
            this.Controls.Add(this.二维出图);
            this.Controls.Add(this.趾板建模);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_绘图";
            this.Text = "Form_绘图";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button 趾板建模;
        private System.Windows.Forms.Button 二维出图;
        private System.Windows.Forms.Button 输出Excel;
    }
}