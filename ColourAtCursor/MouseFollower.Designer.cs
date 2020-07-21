namespace SwatchThis
{
    partial class MouseFollower
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
            this.lblRGB = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.pbColour = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbColour)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRGB
            // 
            this.lblRGB.AutoSize = true;
            this.lblRGB.Location = new System.Drawing.Point(92, 6);
            this.lblRGB.Name = "lblRGB";
            this.lblRGB.Size = new System.Drawing.Size(36, 13);
            this.lblRGB.TabIndex = 1;
            this.lblRGB.Text = "(RGB)";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(27, 6);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(46, 13);
            this.lblCurrent.TabIndex = 0;
            this.lblCurrent.Text = "(current)";
            // 
            // pbColour
            // 
            this.pbColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColour.Location = new System.Drawing.Point(3, 3);
            this.pbColour.Name = "pbColour";
            this.pbColour.Size = new System.Drawing.Size(18, 18);
            this.pbColour.TabIndex = 3;
            this.pbColour.TabStop = false;
            // 
            // MouseFollower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(230, 24);
            this.Controls.Add(this.pbColour);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblRGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(250, 24);
            this.Name = "MouseFollower";
            this.ShowInTaskbar = false;
            this.Text = "MouseFollower";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbColour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRGB;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.PictureBox pbColour;
    }
}