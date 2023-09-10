namespace GarmentZone.Screens
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewpass = new MetroFramework.Controls.MetroTextBox();
            this.txtOldpass = new MetroFramework.Controls.MetroTextBox();
            this.txtConfirmpass = new MetroFramework.Controls.MetroTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 40);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(324, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 40);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Password";
            // 
            // txtNewpass
            // 
            // 
            // 
            // 
            this.txtNewpass.CustomButton.Image = null;
            this.txtNewpass.CustomButton.Location = new System.Drawing.Point(277, 1);
            this.txtNewpass.CustomButton.Name = "";
            this.txtNewpass.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtNewpass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNewpass.CustomButton.TabIndex = 1;
            this.txtNewpass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNewpass.CustomButton.UseSelectable = true;
            this.txtNewpass.CustomButton.Visible = false;
            this.txtNewpass.DisplayIcon = true;
            this.txtNewpass.Icon = ((System.Drawing.Image)(resources.GetObject("txtNewpass.Icon")));
            this.txtNewpass.Lines = new string[0];
            this.txtNewpass.Location = new System.Drawing.Point(26, 90);
            this.txtNewpass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 6);
            this.txtNewpass.MaxLength = 32767;
            this.txtNewpass.Name = "txtNewpass";
            this.txtNewpass.PasswordChar = '*';
            this.txtNewpass.PromptText = "New Password";
            this.txtNewpass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNewpass.SelectedText = "";
            this.txtNewpass.SelectionLength = 0;
            this.txtNewpass.SelectionStart = 0;
            this.txtNewpass.ShortcutsEnabled = true;
            this.txtNewpass.Size = new System.Drawing.Size(301, 25);
            this.txtNewpass.TabIndex = 2;
            this.txtNewpass.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNewpass.UseSelectable = true;
            this.txtNewpass.WaterMark = "New Password";
            this.txtNewpass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNewpass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtOldpass
            // 
            // 
            // 
            // 
            this.txtOldpass.CustomButton.Image = null;
            this.txtOldpass.CustomButton.Location = new System.Drawing.Point(277, 1);
            this.txtOldpass.CustomButton.Name = "";
            this.txtOldpass.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtOldpass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOldpass.CustomButton.TabIndex = 1;
            this.txtOldpass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOldpass.CustomButton.UseSelectable = true;
            this.txtOldpass.CustomButton.Visible = false;
            this.txtOldpass.DisplayIcon = true;
            this.txtOldpass.Icon = ((System.Drawing.Image)(resources.GetObject("txtOldpass.Icon")));
            this.txtOldpass.Lines = new string[0];
            this.txtOldpass.Location = new System.Drawing.Point(26, 55);
            this.txtOldpass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 6);
            this.txtOldpass.MaxLength = 32767;
            this.txtOldpass.Name = "txtOldpass";
            this.txtOldpass.PasswordChar = '*';
            this.txtOldpass.PromptText = "Old Password";
            this.txtOldpass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOldpass.SelectedText = "";
            this.txtOldpass.SelectionLength = 0;
            this.txtOldpass.SelectionStart = 0;
            this.txtOldpass.ShortcutsEnabled = true;
            this.txtOldpass.Size = new System.Drawing.Size(301, 25);
            this.txtOldpass.TabIndex = 1;
            this.txtOldpass.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOldpass.UseSelectable = true;
            this.txtOldpass.WaterMark = "Old Password";
            this.txtOldpass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOldpass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtConfirmpass
            // 
            // 
            // 
            // 
            this.txtConfirmpass.CustomButton.Image = null;
            this.txtConfirmpass.CustomButton.Location = new System.Drawing.Point(277, 1);
            this.txtConfirmpass.CustomButton.Name = "";
            this.txtConfirmpass.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtConfirmpass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtConfirmpass.CustomButton.TabIndex = 1;
            this.txtConfirmpass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConfirmpass.CustomButton.UseSelectable = true;
            this.txtConfirmpass.CustomButton.Visible = false;
            this.txtConfirmpass.DisplayIcon = true;
            this.txtConfirmpass.Icon = ((System.Drawing.Image)(resources.GetObject("txtConfirmpass.Icon")));
            this.txtConfirmpass.Lines = new string[0];
            this.txtConfirmpass.Location = new System.Drawing.Point(26, 125);
            this.txtConfirmpass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 6);
            this.txtConfirmpass.MaxLength = 32767;
            this.txtConfirmpass.Name = "txtConfirmpass";
            this.txtConfirmpass.PasswordChar = '*';
            this.txtConfirmpass.PromptText = "Confirm Password";
            this.txtConfirmpass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConfirmpass.SelectedText = "";
            this.txtConfirmpass.SelectionLength = 0;
            this.txtConfirmpass.SelectionStart = 0;
            this.txtConfirmpass.ShortcutsEnabled = true;
            this.txtConfirmpass.Size = new System.Drawing.Size(301, 25);
            this.txtConfirmpass.TabIndex = 3;
            this.txtConfirmpass.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConfirmpass.UseSelectable = true;
            this.txtConfirmpass.WaterMark = "Confirm Password";
            this.txtConfirmpass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtConfirmpass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(26, 159);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(301, 39);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Change Password";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 208);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirmpass);
            this.Controls.Add(this.txtOldpass);
            this.Controls.Add(this.txtNewpass);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtNewpass;
        private MetroFramework.Controls.MetroTextBox txtOldpass;
        private MetroFramework.Controls.MetroTextBox txtConfirmpass;
        private System.Windows.Forms.Button btnSave;
    }
}