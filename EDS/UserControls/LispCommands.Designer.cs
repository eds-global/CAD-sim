namespace EDS.UserControls
{
    partial class LispCommands
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBreakLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBreakLine
            // 
            this.btnBreakLine.Location = new System.Drawing.Point(17, 15);
            this.btnBreakLine.Name = "btnBreakLine";
            this.btnBreakLine.Size = new System.Drawing.Size(152, 52);
            this.btnBreakLine.TabIndex = 0;
            this.btnBreakLine.Text = "Break Line";
            this.btnBreakLine.UseVisualStyleBackColor = true;
            this.btnBreakLine.Click += new System.EventHandler(this.btnBreakLine_Click);
            // 
            // LispCommands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBreakLine);
            this.Name = "LispCommands";
            this.Size = new System.Drawing.Size(455, 812);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBreakLine;
    }
}
