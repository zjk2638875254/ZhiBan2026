namespace ZhiBan
{
    partial class Form_V3
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
            this.BasePanel = new System.Windows.Forms.Panel();
            this.BaseMenu = new System.Windows.Forms.MenuStrip();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.面板堆石坝设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.面板设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.趾板设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.成果输出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BaseMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.BasePanel.Location = new System.Drawing.Point(0, 27);
            this.BasePanel.Name = "BasePanel";
            this.BasePanel.Size = new System.Drawing.Size(600, 400);
            this.BasePanel.TabIndex = 0;
            this.BasePanel.Resize += new System.EventHandler(this.FormPanel_Resize);
            // 
            // BaseMenu
            // 
            this.BaseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.面板堆石坝设计ToolStripMenuItem,
            this.面板设计ToolStripMenuItem,
            this.趾板设计ToolStripMenuItem,
            this.成果输出ToolStripMenuItem,
            this.更新数据库ToolStripMenuItem,
            this.说明ToolStripMenuItem});
            this.BaseMenu.Location = new System.Drawing.Point(0, 0);
            this.BaseMenu.Name = "BaseMenu";
            this.BaseMenu.Size = new System.Drawing.Size(880, 25);
            this.BaseMenu.TabIndex = 1;
            this.BaseMenu.Text = "menuStrip1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(607, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(273, 399);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // 面板堆石坝设计ToolStripMenuItem
            // 
            this.面板堆石坝设计ToolStripMenuItem.Name = "面板堆石坝设计ToolStripMenuItem";
            this.面板堆石坝设计ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.面板堆石坝设计ToolStripMenuItem.Text = "堆石坝设计";
            this.面板堆石坝设计ToolStripMenuItem.Click += new System.EventHandler(this.面板堆石坝设计ToolStripMenuItem_Click);
            // 
            // 面板设计ToolStripMenuItem
            // 
            this.面板设计ToolStripMenuItem.Name = "面板设计ToolStripMenuItem";
            this.面板设计ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.面板设计ToolStripMenuItem.Text = "面板设计";
            this.面板设计ToolStripMenuItem.Click += new System.EventHandler(this.面板设计ToolStripMenuItem_Click);
            // 
            // 趾板设计ToolStripMenuItem
            // 
            this.趾板设计ToolStripMenuItem.Name = "趾板设计ToolStripMenuItem";
            this.趾板设计ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.趾板设计ToolStripMenuItem.Text = "趾板设计";
            this.趾板设计ToolStripMenuItem.Click += new System.EventHandler(this.趾板设计ToolStripMenuItem_Click);
            // 
            // 成果输出ToolStripMenuItem
            // 
            this.成果输出ToolStripMenuItem.Name = "成果输出ToolStripMenuItem";
            this.成果输出ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.成果输出ToolStripMenuItem.Text = "成果输出";
            this.成果输出ToolStripMenuItem.Click += new System.EventHandler(this.成果输出ToolStripMenuItem_Click);
            // 
            // 更新数据库ToolStripMenuItem
            // 
            this.更新数据库ToolStripMenuItem.Name = "更新数据库ToolStripMenuItem";
            this.更新数据库ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.更新数据库ToolStripMenuItem.Text = "更新数据库";
            this.更新数据库ToolStripMenuItem.Click += new System.EventHandler(this.更新数据库ToolStripMenuItem_Click);
            // 
            // 说明ToolStripMenuItem
            // 
            this.说明ToolStripMenuItem.Name = "说明ToolStripMenuItem";
            this.说明ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.说明ToolStripMenuItem.Text = "说明";
            // 
            // Form_V3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 428);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.BasePanel);
            this.Controls.Add(this.BaseMenu);
            this.Font = new System.Drawing.Font("华文仿宋", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.BaseMenu;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_V3";
            this.Text = "混凝土面板堆石坝设计系统-V2.0";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.BaseMenu.ResumeLayout(false);
            this.BaseMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BasePanel;
        private System.Windows.Forms.MenuStrip BaseMenu;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem 面板堆石坝设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 面板设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 趾板设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 成果输出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 说明ToolStripMenuItem;
    }
}