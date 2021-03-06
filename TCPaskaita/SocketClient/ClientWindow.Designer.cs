namespace PacketClient
{
    partial class Form1
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
            this.clientPacketTB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.serverPacketTB = new System.Windows.Forms.TextBox();
            this.hostnameTB = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PortTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientP4Btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "CLIENT_P1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Packet1Click);
            // 
            // clientPacketTB
            // 
            this.clientPacketTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientPacketTB.Location = new System.Drawing.Point(6, 19);
            this.clientPacketTB.Multiline = true;
            this.clientPacketTB.Name = "clientPacketTB";
            this.clientPacketTB.ReadOnly = true;
            this.clientPacketTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clientPacketTB.Size = new System.Drawing.Size(564, 231);
            this.clientPacketTB.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClientP4Btn);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(13, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 167);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CLIENT packets";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "CLIENT_P3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Packet3Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "CLIENT_P2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Packet2Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientPacketTB);
            this.groupBox2.Location = new System.Drawing.Point(303, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(576, 256);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CLIENT packet info";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.serverPacketTB);
            this.groupBox3.Location = new System.Drawing.Point(13, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(866, 258);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SERVER packet received";
            // 
            // serverPacketTB
            // 
            this.serverPacketTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverPacketTB.Location = new System.Drawing.Point(7, 20);
            this.serverPacketTB.Multiline = true;
            this.serverPacketTB.Name = "serverPacketTB";
            this.serverPacketTB.ReadOnly = true;
            this.serverPacketTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.serverPacketTB.Size = new System.Drawing.Size(853, 232);
            this.serverPacketTB.TabIndex = 0;
            // 
            // hostnameTB
            // 
            this.hostnameTB.Location = new System.Drawing.Point(8, 19);
            this.hostnameTB.Name = "hostnameTB";
            this.hostnameTB.Size = new System.Drawing.Size(270, 20);
            this.hostnameTB.TabIndex = 12;
            this.hostnameTB.Text = "127.0.0.1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PortTB);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.hostnameTB);
            this.groupBox4.Location = new System.Drawing.Point(13, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 83);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SERVER IP address";
            // 
            // PortTB
            // 
            this.PortTB.Location = new System.Drawing.Point(9, 57);
            this.PortTB.Name = "PortTB";
            this.PortTB.Size = new System.Drawing.Size(269, 20);
            this.PortTB.TabIndex = 14;
            this.PortTB.Text = "7777";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Server TCP port";
            // 
            // ClientP4Btn
            // 
            this.ClientP4Btn.Location = new System.Drawing.Point(7, 107);
            this.ClientP4Btn.Name = "ClientP4Btn";
            this.ClientP4Btn.Size = new System.Drawing.Size(82, 23);
            this.ClientP4Btn.TabIndex = 3;
            this.ClientP4Btn.Text = "CLIENT_P4";
            this.ClientP4Btn.UseVisualStyleBackColor = true;
            this.ClientP4Btn.Click += new System.EventHandler(this.ClientP4Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 545);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "SOCKET CLIENT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox clientPacketTB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox serverPacketTB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox hostnameTB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox PortTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClientP4Btn;
    }
}

