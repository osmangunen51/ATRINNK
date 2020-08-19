namespace MakinaTurkiye.ImageToWebP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnDizinSec = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PnlEkAyar = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlstrpbtnTemizle = new System.Windows.Forms.ToolStripButton();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDurdur = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PnlEkAyar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.btnDizinSec);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 30);
            this.panel1.TabIndex = 0;
            // 
            // txtPath
            // 
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPath.Location = new System.Drawing.Point(59, 0);
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(639, 30);
            this.txtPath.TabIndex = 5;
            // 
            // btnDizinSec
            // 
            this.btnDizinSec.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDizinSec.Location = new System.Drawing.Point(698, 0);
            this.btnDizinSec.Name = "btnDizinSec";
            this.btnDizinSec.Size = new System.Drawing.Size(75, 30);
            this.btnDizinSec.TabIndex = 4;
            this.btnDizinSec.Text = "Seç...";
            this.btnDizinSec.UseVisualStyleBackColor = true;
            this.btnDizinSec.Click += new System.EventHandler(this.btnDizinSec_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Klasör";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.PnlEkAyar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(773, 676);
            this.panel2.TabIndex = 1;
            // 
            // PnlEkAyar
            // 
            this.PnlEkAyar.Controls.Add(this.btnDurdur);
            this.PnlEkAyar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEkAyar.Location = new System.Drawing.Point(0, 0);
            this.PnlEkAyar.Name = "PnlEkAyar";
            this.PnlEkAyar.Size = new System.Drawing.Size(773, 114);
            this.PnlEkAyar.TabIndex = 0;
            this.PnlEkAyar.TabStop = false;
            this.PnlEkAyar.Text = "Ek Ayarlar";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpbtnTemizle});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(767, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlstrpbtnTemizle
            // 
            this.tlstrpbtnTemizle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlstrpbtnTemizle.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnTemizle.Image")));
            this.tlstrpbtnTemizle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnTemizle.Name = "tlstrpbtnTemizle";
            this.tlstrpbtnTemizle.Size = new System.Drawing.Size(66, 22);
            this.tlstrpbtnTemizle.Text = "Temizle";
            this.tlstrpbtnTemizle.Click += new System.EventHandler(this.tlstrpbtnTemizle_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.ForeColor = System.Drawing.Color.Bisque;
            this.txtLog.Location = new System.Drawing.Point(3, 41);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(767, 518);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "dasdas";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 562);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "İşlem Logları";
            // 
            // btnDurdur
            // 
            this.btnDurdur.Location = new System.Drawing.Point(323, 50);
            this.btnDurdur.Name = "btnDurdur";
            this.btnDurdur.Size = new System.Drawing.Size(136, 43);
            this.btnDurdur.TabIndex = 5;
            this.btnDurdur.Text = "Başlat";
            this.btnDurdur.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 706);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "Image To WebP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.PnlEkAyar.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnDizinSec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox PnlEkAyar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlstrpbtnTemizle;
        private System.Windows.Forms.Button btnDurdur;
    }
}

