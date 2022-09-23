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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TlStBrlLblDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlstrpbtnTemizle = new System.Windows.Forms.ToolStripButton();
            this.PnlEkAyar = new System.Windows.Forms.GroupBox();
            this.txtBaseDizin = new System.Windows.Forms.TextBox();
            this.lblDizin = new System.Windows.Forms.Label();
            this.btnDurdur = new System.Windows.Forms.Button();
            this.btnBaslatDurdur = new System.Windows.Forms.Button();
            this.ChLogDurum = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1031, 869);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.statusStrip1);
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 157);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1031, 712);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "İşlem Logları";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TlStBrlLblDurum,
            this.lblDurum});
            this.statusStrip1.Location = new System.Drawing.Point(4, 682);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1023, 26);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TlStBrlLblDurum
            // 
            this.TlStBrlLblDurum.Name = "TlStBrlLblDurum";
            this.TlStBrlLblDurum.Size = new System.Drawing.Size(0, 20);
            // 
            // lblDurum
            // 
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(151, 20);
            this.lblDurum.Text = "toolStripStatusLabel1";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.ForeColor = System.Drawing.Color.Bisque;
            this.txtLog.Location = new System.Drawing.Point(4, 46);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1023, 662);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpbtnTemizle});
            this.toolStrip1.Location = new System.Drawing.Point(4, 19);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1023, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlstrpbtnTemizle
            // 
            this.tlstrpbtnTemizle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlstrpbtnTemizle.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnTemizle.Image")));
            this.tlstrpbtnTemizle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnTemizle.Name = "tlstrpbtnTemizle";
            this.tlstrpbtnTemizle.Size = new System.Drawing.Size(84, 24);
            this.tlstrpbtnTemizle.Text = "Temizle";
            this.tlstrpbtnTemizle.Click += new System.EventHandler(this.tlstrpbtnTemizle_Click);
            // 
            // PnlEkAyar
            // 
            this.PnlEkAyar.Controls.Add(this.ChLogDurum);
            this.PnlEkAyar.Controls.Add(this.txtBaseDizin);
            this.PnlEkAyar.Controls.Add(this.lblDizin);
            this.PnlEkAyar.Controls.Add(this.btnDurdur);
            this.PnlEkAyar.Controls.Add(this.btnBaslatDurdur);
            this.PnlEkAyar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEkAyar.Location = new System.Drawing.Point(0, 0);
            this.PnlEkAyar.Margin = new System.Windows.Forms.Padding(4);
            this.PnlEkAyar.Name = "PnlEkAyar";
            this.PnlEkAyar.Padding = new System.Windows.Forms.Padding(4);
            this.PnlEkAyar.Size = new System.Drawing.Size(1031, 157);
            this.PnlEkAyar.TabIndex = 0;
            this.PnlEkAyar.TabStop = false;
            this.PnlEkAyar.Text = "Ek Ayarlar";
            // 
            // txtBaseDizin
            // 
            this.txtBaseDizin.Location = new System.Drawing.Point(87, 34);
            this.txtBaseDizin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBaseDizin.Name = "txtBaseDizin";
            this.txtBaseDizin.Size = new System.Drawing.Size(934, 22);
            this.txtBaseDizin.TabIndex = 8;
            // 
            // lblDizin
            // 
            this.lblDizin.AutoSize = true;
            this.lblDizin.Location = new System.Drawing.Point(12, 36);
            this.lblDizin.Name = "lblDizin";
            this.lblDizin.Size = new System.Drawing.Size(71, 16);
            this.lblDizin.TabIndex = 7;
            this.lblDizin.Text = "Base Dizin";
            // 
            // btnDurdur
            // 
            this.btnDurdur.Location = new System.Drawing.Point(200, 81);
            this.btnDurdur.Margin = new System.Windows.Forms.Padding(4);
            this.btnDurdur.Name = "btnDurdur";
            this.btnDurdur.Size = new System.Drawing.Size(181, 53);
            this.btnDurdur.TabIndex = 6;
            this.btnDurdur.Text = "Durdur";
            this.btnDurdur.UseVisualStyleBackColor = true;
            this.btnDurdur.Click += new System.EventHandler(this.btnDurdur_Click);
            // 
            // btnBaslatDurdur
            // 
            this.btnBaslatDurdur.Location = new System.Drawing.Point(12, 81);
            this.btnBaslatDurdur.Margin = new System.Windows.Forms.Padding(4);
            this.btnBaslatDurdur.Name = "btnBaslatDurdur";
            this.btnBaslatDurdur.Size = new System.Drawing.Size(181, 53);
            this.btnBaslatDurdur.TabIndex = 5;
            this.btnBaslatDurdur.Text = "Başlat";
            this.btnBaslatDurdur.UseVisualStyleBackColor = true;
            this.btnBaslatDurdur.Click += new System.EventHandler(this.btnBaslatDurdur_Click);
            // 
            // ChLogDurum
            // 
            this.ChLogDurum.AutoSize = true;
            this.ChLogDurum.Location = new System.Drawing.Point(388, 98);
            this.ChLogDurum.Name = "ChLogDurum";
            this.ChLogDurum.Size = new System.Drawing.Size(130, 20);
            this.ChLogDurum.TabIndex = 9;
            this.ChLogDurum.Text = "Log Aktif / Pasif ?";
            this.ChLogDurum.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 869);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.Text = "Image Fixer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TlStBrlLblDurum;
        private System.Windows.Forms.ToolStripStatusLabel lblDurum;
        private System.Windows.Forms.CheckBox ChLogDurum;
    }
}

