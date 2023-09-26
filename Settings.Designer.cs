namespace YTMusicDesktop
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.check_discordrpc = new Guna.UI2.WinForms.Guna2CheckBox();
            this.check_websettings = new Guna.UI2.WinForms.Guna2CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(107, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "YT Music Desktop";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(142, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(117, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version 1.1 Pre-release 1";
            // 
            // check_discordrpc
            // 
            this.check_discordrpc.AutoSize = true;
            this.check_discordrpc.Checked = true;
            this.check_discordrpc.CheckedState.BorderRadius = 0;
            this.check_discordrpc.CheckedState.BorderThickness = 0;
            this.check_discordrpc.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.check_discordrpc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_discordrpc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_discordrpc.ForeColor = System.Drawing.Color.White;
            this.check_discordrpc.Location = new System.Drawing.Point(103, 186);
            this.check_discordrpc.Name = "check_discordrpc";
            this.check_discordrpc.Size = new System.Drawing.Size(145, 25);
            this.check_discordrpc.TabIndex = 4;
            this.check_discordrpc.Text = "Use Discord RPC";
            this.check_discordrpc.UncheckedState.BorderRadius = 0;
            this.check_discordrpc.UncheckedState.BorderThickness = 0;
            this.check_discordrpc.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.check_discordrpc.CheckStateChanged += new System.EventHandler(this.check_discordrpc_CheckStateChanged);
            // 
            // check_websettings
            // 
            this.check_websettings.AutoSize = true;
            this.check_websettings.CheckedState.BorderRadius = 0;
            this.check_websettings.CheckedState.BorderThickness = 0;
            this.check_websettings.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.check_websettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_websettings.ForeColor = System.Drawing.Color.White;
            this.check_websettings.Location = new System.Drawing.Point(103, 217);
            this.check_websettings.Name = "check_websettings";
            this.check_websettings.Size = new System.Drawing.Size(255, 25);
            this.check_websettings.TabIndex = 5;
            this.check_websettings.Text = "Experiment: Web Settings Button";
            this.toolTip1.SetToolTip(this.check_websettings, "If this is disabled, the settings can be accessed from the menu bar.");
            this.check_websettings.UncheckedState.BorderRadius = 0;
            this.check_websettings.UncheckedState.BorderThickness = 0;
            this.check_websettings.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.check_websettings.CheckStateChanged += new System.EventHandler(this.check_websettings_CheckStateChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(399, 303);
            this.Controls.Add(this.check_websettings);
            this.Controls.Add(this.check_discordrpc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2CheckBox check_discordrpc;
        private Guna.UI2.WinForms.Guna2CheckBox check_websettings;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}