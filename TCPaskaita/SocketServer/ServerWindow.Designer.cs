namespace MySocketServer
{
    partial class ServerWindow
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
            this.clientPacketTB = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.serverPacketTB = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.portTB = new System.Windows.Forms.TextBox();
            this.Listen = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientPacketTB
            // 
            this.clientPacketTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientPacketTB.Location = new System.Drawing.Point(6, 19);
            this.clientPacketTB.Multiline = true;
            this.clientPacketTB.Name = "clientPacketTB";
            this.clientPacketTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clientPacketTB.Size = new System.Drawing.Size(561, 151);
            this.clientPacketTB.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientPacketTB);
            this.groupBox2.Location = new System.Drawing.Point(13, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 176);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CLIENT packet received";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.serverPacketTB);
            this.groupBox3.Location = new System.Drawing.Point(13, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(573, 180);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SERVER packet sent";
            // 
            // serverPacketTB
            // 
            this.serverPacketTB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverPacketTB.Location = new System.Drawing.Point(7, 20);
            this.serverPacketTB.Multiline = true;
            this.serverPacketTB.Name = "serverPacketTB";
            this.serverPacketTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.serverPacketTB.Size = new System.Drawing.Size(560, 148);
            this.serverPacketTB.TabIndex = 0;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(95, 12);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(176, 14);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(140, 20);
            this.textBoxIP.TabIndex = 7;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // portTB
            // 
            this.portTB.Location = new System.Drawing.Point(322, 14);
            this.portTB.Name = "portTB";
            this.portTB.Size = new System.Drawing.Size(100, 20);
            this.portTB.TabIndex = 8;
            // 
            // Listen
            // 
            this.Listen.Location = new System.Drawing.Point(20, 12);
            this.Listen.Name = "Listen";
            this.Listen.Size = new System.Drawing.Size(69, 22);
            this.Listen.TabIndex = 9;
            this.Listen.Text = "Listen";
            this.Listen.UseVisualStyleBackColor = true;
            this.Listen.Click += new System.EventHandler(this.Listen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 420);
            this.Controls.Add(this.Listen);
            this.Controls.Add(this.portTB);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "SOCKET SERVER";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox clientPacketTB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox serverPacketTB;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox portTB;
        private System.Windows.Forms.Button Listen;
    }
}

