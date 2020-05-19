namespace ColourAtCursor
{
    partial class MainDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDisplay));
            this.label1 = new System.Windows.Forms.Label();
            this.lblColour = new System.Windows.Forms.Label();
            this.pbColour = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbColour)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last colour:";
            // 
            // lblColour
            // 
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(80, 9);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(37, 13);
            this.lblColour.TabIndex = 1;
            this.lblColour.Text = "(none)";
            // 
            // pbColour
            // 
            this.pbColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColour.Location = new System.Drawing.Point(285, 9);
            this.pbColour.Name = "pbColour";
            this.pbColour.Size = new System.Drawing.Size(26, 26);
            this.pbColour.TabIndex = 2;
            this.pbColour.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hotkey:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ctrl+Alt+C";
            // 
            // MainDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 43);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbColour);
            this.Controls.Add(this.lblColour);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainDisplay";
            this.Text = "Colour At Cursor";
            ((System.ComponentModel.ISupportInitialize)(this.pbColour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblColour;
        private System.Windows.Forms.PictureBox pbColour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

