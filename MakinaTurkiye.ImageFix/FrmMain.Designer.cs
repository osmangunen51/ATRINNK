namespace MakinaTurkiye.ImageFix
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlstrpbtnTemizle = new System.Windows.Forms.ToolStripButton();
            this.PnlEkAyar = new System.Windows.Forms.GroupBox();
            this.btnDurdur = new System.Windows.Forms.Button();
            this.btnBaslatDurdur = new System.Windows.Forms.Button();
            this.lblDizin = new System.Windows.Forms.Label();
            this.txtBaseDizin = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.PnlEkAyar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.PnlEkAyar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 1086);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 196);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1160, 890);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "İşlem Logları";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.ForeColor = System.Drawing.Color.Bisque;
            this.txtLog.Location = new System.Drawing.Point(4, 58);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1152, 827);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpbtnTemizle});
            this.toolStrip1.Location = new System.Drawing.Point(4, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1152, 34);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlstrpbtnTemizle
            // 
            this.tlstrpbtnTemizle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlstrpbtnTemizle.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnTemizle.Image")));
            this.tlstrpbtnTemizle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnTemizle.Name = "tlstrpbtnTemizle";
            this.tlstrpbtnTemizle.Size = new System.Drawing.Size(93, 29);
            this.tlstrpbtnTemizle.Text = "Temizle";
            this.tlstrpbtnTemizle.Click += new System.EventHandler(this.tlstrpbtnTemizle_Click);
            // 
            // PnlEkAyar
            // 
            this.PnlEkAyar.Controls.Add(this.txtBaseDizin);
            this.PnlEkAyar.Controls.Add(this.lblDizin);
            this.PnlEkAyar.Controls.Add(this.btnDurdur);
            this.PnlEkAyar.Controls.Add(this.btnBaslatDurdur);
            this.PnlEkAyar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEkAyar.Location = new System.Drawing.Point(0, 0);
            this.PnlEkAyar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PnlEkAyar.Name = "PnlEkAyar";
            this.PnlEkAyar.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PnlEkAyar.Size = new System.Drawing.Size(1160, 196);
            this.PnlEkAyar.TabIndex = 0;
            this.PnlEkAyar.TabStop = false;
            this.PnlEkAyar.Text = "Ek Ayarlar";
            // 
            // btnDurdur
            // 
            this.btnDurdur.Location = new System.Drawing.Point(225, 101);
            this.btnDurdur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDurdur.Name = "btnDurdur";
            this.btnDurdur.Size = new System.Drawing.Size(204, 66);
            this.btnDurdur.TabIndex = 6;
            this.btnDurdur.Text = "Durdur";
            this.btnDurdur.UseVisualStyleBackColor = true;
            this.btnDurdur.Click += new System.EventHandler(this.btnDurdur_Click);
            // 
            // btnBaslatDurdur
            // 
            this.btnBaslatDurdur.Location = new System.Drawing.Point(13, 101);
            this.btnBaslatDurdur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBaslatDurdur.Name = "btnBaslatDurdur";
            this.btnBaslatDurdur.Size = new System.Drawing.Size(204, 66);
            this.btnBaslatDurdur.TabIndex = 5;
            this.btnBaslatDurdur.Text = "Başlat";
            this.btnBaslatDurdur.UseVisualStyleBackColor = true;
            this.btnBaslatDurdur.Click += new System.EventHandler(this.btnBaslatDurdur_Click);
            // 
            // lblDizin
            // 
            this.lblDizin.AutoSize = true;
            this.lblDizin.Location = new System.Drawing.Point(13, 45);
            this.lblDizin.Name = "lblDizin";
            this.lblDizin.Size = new System.Drawing.Size(85, 20);
            this.lblDizin.TabIndex = 7;
            this.lblDizin.Text = "Base Dizin";
            // 
            // txtBaseDizin
            // 
            this.txtBaseDizin.Location = new System.Drawing.Point(98, 42);
            this.txtBaseDizin.Name = "txtBaseDizin";
            this.txtBaseDizin.Size = new System.Drawing.Size(1050, 26);
            this.txtBaseDizin.TabIndex = 8;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 1086);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.Text = "Image Fixer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PnlEkAyar.ResumeLayout(false);
            this.PnlEkAyar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox PnlEkAyar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlstrpbtnTemizle;
        private System.Windows.Forms.Button btnBaslatDurdur;
        private System.Windows.Forms.Button btnDurdur;
        private System.Windows.Forms.TextBox txtBaseDizin;
        private System.Windows.Forms.Label lblDizin;
    }
}

