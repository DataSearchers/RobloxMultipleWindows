namespace MultiBloX
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
            this.vTheme1 = new VTheme();
            this.label1 = new System.Windows.Forms.Label();
            this.vButton2 = new VButton();
            this.vButton1 = new VButton();
            this.vTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vTheme1
            // 
            this.vTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.vTheme1.Colors = new Bloom[0];
            this.vTheme1.Controls.Add(this.label1);
            this.vTheme1.Controls.Add(this.vButton2);
            this.vTheme1.Controls.Add(this.vButton1);
            this.vTheme1.Customization = "";
            this.vTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.vTheme1.Image = null;
            this.vTheme1.Location = new System.Drawing.Point(0, 0);
            this.vTheme1.Movable = true;
            this.vTheme1.Name = "vTheme1";
            this.vTheme1.NoRounding = false;
            this.vTheme1.Sizable = false;
            this.vTheme1.Size = new System.Drawing.Size(249, 118);
            this.vTheme1.SmartBounds = true;
            this.vTheme1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.vTheme1.TabIndex = 0;
            this.vTheme1.Text = "MultiBloX Instances";
            this.vTheme1.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.vTheme1.Transparent = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "                    IMPORTANT !\r\nClick the button before launching Roblox.";
            // 
            // vButton2
            // 
            this.vButton2.BackColor = System.Drawing.Color.White;
            this.vButton2.Colors = new Bloom[0];
            this.vButton2.Customization = "";
            this.vButton2.Font = new System.Drawing.Font("Verdana", 8F);
            this.vButton2.Image = null;
            this.vButton2.Location = new System.Drawing.Point(177, 3);
            this.vButton2.Name = "vButton2";
            this.vButton2.NoRounding = false;
            this.vButton2.Size = new System.Drawing.Size(69, 29);
            this.vButton2.TabIndex = 1;
            this.vButton2.Text = "X";
            this.vButton2.Transparent = false;
            this.vButton2.Click += new System.EventHandler(this.vButton2_Click);
            // 
            // vButton1
            // 
            this.vButton1.Colors = new Bloom[0];
            this.vButton1.Customization = "";
            this.vButton1.Font = new System.Drawing.Font("Verdana", 8F);
            this.vButton1.Image = null;
            this.vButton1.Location = new System.Drawing.Point(3, 71);
            this.vButton1.Name = "vButton1";
            this.vButton1.NoRounding = false;
            this.vButton1.Size = new System.Drawing.Size(243, 40);
            this.vButton1.TabIndex = 0;
            this.vButton1.Text = "START";
            this.vButton1.Transparent = false;
            this.vButton1.Click += new System.EventHandler(this.vButton1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(249, 118);
            this.Controls.Add(this.vTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.vTheme1.ResumeLayout(false);
            this.vTheme1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private VTheme vTheme1;
        private VButton vButton1;
        private VButton vButton2;
        private System.Windows.Forms.Label label1;
    }
}

