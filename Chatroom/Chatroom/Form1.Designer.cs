namespace Chatroom
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
            this.components = new System.ComponentModel.Container();
            this.user = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox = new System.Windows.Forms.Label();
            this.textWait = new System.Windows.Forms.Timer(this.components);
            this.textPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.textChange = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.user)).BeginInit();
            this.panel.SuspendLayout();
            this.textPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // user
            // 
            this.user.BackColor = System.Drawing.Color.Brown;
            this.user.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.user.Location = new System.Drawing.Point(304, 196);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(56, 53);
            this.user.TabIndex = 0;
            this.user.TabStop = false;
            this.user.Click += new System.EventHandler(this.Form1_Load);
            // 
            // timer
            // 
            this.timer.Interval = 60;
            this.timer.Tick += new System.EventHandler(this.mainTimerEvent);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel.Controls.Add(this.flowLayoutPanel1);
            this.panel.Controls.Add(this.textBox);
            this.panel.Controls.Add(this.user);
            this.panel.Location = new System.Drawing.Point(30, 30);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(728, 399);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-28, 405);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(782, 13);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // textBox
            // 
            this.textBox.AutoSize = true;
            this.textBox.BackColor = System.Drawing.Color.RosyBrown;
            this.textBox.Location = new System.Drawing.Point(371, 161);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(35, 13);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "label1";
            this.textBox.Visible = false;
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textPanel
            // 
            this.textPanel.Controls.Add(this.button1);
            this.textPanel.Controls.Add(this.textChange);
            this.textPanel.Location = new System.Drawing.Point(2, 285);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new System.Drawing.Size(785, 100);
            this.textPanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(359, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textChange
            // 
            this.textChange.Enabled = false;
            this.textChange.Location = new System.Drawing.Point(28, 13);
            this.textChange.Name = "textChange";
            this.textChange.Size = new System.Drawing.Size(304, 20);
            this.textChange.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(780, 450);
            this.Controls.Add(this.textPanel);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyisup);
            ((System.ComponentModel.ISupportInitialize)(this.user)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.textPanel.ResumeLayout(false);
            this.textPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox user;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label textBox;
        private System.Windows.Forms.Timer textWait;
        private System.Windows.Forms.Panel textPanel;
        private System.Windows.Forms.TextBox textChange;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

