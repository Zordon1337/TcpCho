namespace TcpCho.Gui
{
    partial class Panel
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
            this.SSP1 = new System.Windows.Forms.Button();
            this.STD1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PacketID1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SEP1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SSP2 = new System.Windows.Forms.Button();
            this.SEP2 = new System.Windows.Forms.Button();
            this.Target1 = new System.Windows.Forms.TextBox();
            this.PacketID2 = new System.Windows.Forms.TextBox();
            this.PacketID3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Target2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.STD2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Separator = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CU = new System.Windows.Forms.Label();
            this.CUI = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SSP1
            // 
            this.SSP1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SSP1.Location = new System.Drawing.Point(9, 618);
            this.SSP1.Name = "SSP1";
            this.SSP1.Size = new System.Drawing.Size(395, 30);
            this.SSP1.TabIndex = 0;
            this.SSP1.Text = "Send string Packet to everyone";
            this.SSP1.UseVisualStyleBackColor = true;
            this.SSP1.Click += new System.EventHandler(this.SSP1_Click);
            // 
            // STD1
            // 
            this.STD1.Location = new System.Drawing.Point(9, 584);
            this.STD1.Name = "STD1";
            this.STD1.Size = new System.Drawing.Size(395, 29);
            this.STD1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 560);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "String to send";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 528);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(395, 29);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "String to send";
            // 
            // PacketID1
            // 
            this.PacketID1.Location = new System.Drawing.Point(8, 528);
            this.PacketID1.Name = "PacketID1";
            this.PacketID1.Size = new System.Drawing.Size(395, 29);
            this.PacketID1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 504);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "PacketID (In int)";
            // 
            // SEP1
            // 
            this.SEP1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SEP1.Location = new System.Drawing.Point(8, 453);
            this.SEP1.Name = "SEP1";
            this.SEP1.Size = new System.Drawing.Size(395, 30);
            this.SEP1.TabIndex = 0;
            this.SEP1.Text = "Send Empty Packet to everyone";
            this.SEP1.UseVisualStyleBackColor = true;
            this.SEP1.Click += new System.EventHandler(this.SEP1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(8, 418);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(395, 29);
            this.textBox3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "PacketID (in int)";
            // 
            // SSP2
            // 
            this.SSP2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SSP2.Location = new System.Drawing.Point(6, 345);
            this.SSP2.Name = "SSP2";
            this.SSP2.Size = new System.Drawing.Size(395, 30);
            this.SSP2.TabIndex = 0;
            this.SSP2.Text = "Send string Packet to user";
            this.SSP2.UseVisualStyleBackColor = true;
            this.SSP2.Click += new System.EventHandler(this.SSP2_Click);
            // 
            // SEP2
            // 
            this.SEP2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SEP2.Location = new System.Drawing.Point(8, 125);
            this.SEP2.Name = "SEP2";
            this.SEP2.Size = new System.Drawing.Size(395, 30);
            this.SEP2.TabIndex = 0;
            this.SEP2.Text = "Send Empty Packet to user";
            this.SEP2.UseVisualStyleBackColor = true;
            this.SEP2.Click += new System.EventHandler(this.SEP2_Click);
            // 
            // Target1
            // 
            this.Target1.Location = new System.Drawing.Point(6, 311);
            this.Target1.Name = "Target1";
            this.Target1.Size = new System.Drawing.Size(395, 29);
            this.Target1.TabIndex = 1;
            // 
            // PacketID2
            // 
            this.PacketID2.Location = new System.Drawing.Point(9, 199);
            this.PacketID2.Name = "PacketID2";
            this.PacketID2.Size = new System.Drawing.Size(395, 29);
            this.PacketID2.TabIndex = 1;
            // 
            // PacketID3
            // 
            this.PacketID3.Location = new System.Drawing.Point(9, 36);
            this.PacketID3.Name = "PacketID3";
            this.PacketID3.Size = new System.Drawing.Size(395, 29);
            this.PacketID3.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Target (id of target)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "PacketID (in int)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "PacketID (In int)";
            // 
            // Target2
            // 
            this.Target2.Location = new System.Drawing.Point(10, 90);
            this.Target2.Name = "Target2";
            this.Target2.Size = new System.Drawing.Size(395, 29);
            this.Target2.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 21);
            this.label9.TabIndex = 2;
            this.label9.Text = "Target (id of target)";
            // 
            // STD2
            // 
            this.STD2.Location = new System.Drawing.Point(5, 255);
            this.STD2.Name = "STD2";
            this.STD2.Size = new System.Drawing.Size(395, 29);
            this.STD2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "String to send";
            // 
            // Separator
            // 
            this.Separator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Separator.Location = new System.Drawing.Point(8, 162);
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(395, 10);
            this.Separator.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Location = new System.Drawing.Point(8, 381);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 10);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(9, 489);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 10);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(415, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(18, 641);
            this.panel3.TabIndex = 6;
            // 
            // CU
            // 
            this.CU.AutoSize = true;
            this.CU.Location = new System.Drawing.Point(440, 13);
            this.CU.Name = "CU";
            this.CU.Size = new System.Drawing.Size(148, 21);
            this.CU.TabIndex = 7;
            this.CU.Text = "Connected Users: ";
            this.CU.Click += new System.EventHandler(this.CU_Click);
            // 
            // CUI
            // 
            this.CUI.AutoSize = true;
            this.CUI.Location = new System.Drawing.Point(440, 44);
            this.CUI.Name = "CUI";
            this.CUI.Size = new System.Drawing.Size(170, 21);
            this.CUI.TabIndex = 7;
            this.CUI.Text = "Connected Users ID: ";
            // 
            // Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(851, 657);
            this.Controls.Add(this.CUI);
            this.Controls.Add(this.CU);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Separator);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PacketID1);
            this.Controls.Add(this.Target2);
            this.Controls.Add(this.PacketID3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.PacketID2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.STD2);
            this.Controls.Add(this.Target1);
            this.Controls.Add(this.STD1);
            this.Controls.Add(this.SEP2);
            this.Controls.Add(this.SEP1);
            this.Controls.Add(this.SSP2);
            this.Controls.Add(this.SSP1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Panel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel";
            this.Load += new System.EventHandler(this.Panel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SSP1;
        private System.Windows.Forms.TextBox STD1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PacketID1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SEP1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SSP2;
        private System.Windows.Forms.Button SEP2;
        private System.Windows.Forms.TextBox Target1;
        private System.Windows.Forms.TextBox PacketID2;
        private System.Windows.Forms.TextBox PacketID3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Target2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox STD2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel Separator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label CU;
        private System.Windows.Forms.Label CUI;
    }
}