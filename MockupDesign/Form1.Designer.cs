namespace MockupDesign
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSX = new System.Windows.Forms.TextBox();
            this.txtSY = new System.Windows.Forms.TextBox();
            this.txtEY = new System.Windows.Forms.TextBox();
            this.txtEX = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(355, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 22);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 36);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSX
            // 
            this.txtSX.Location = new System.Drawing.Point(355, 105);
            this.txtSX.Name = "txtSX";
            this.txtSX.Size = new System.Drawing.Size(70, 22);
            this.txtSX.TabIndex = 10;
            this.txtSX.Text = "0";
            // 
            // txtSY
            // 
            this.txtSY.Location = new System.Drawing.Point(452, 105);
            this.txtSY.Name = "txtSY";
            this.txtSY.Size = new System.Drawing.Size(70, 22);
            this.txtSY.TabIndex = 11;
            this.txtSY.Text = "0";
            // 
            // txtEY
            // 
            this.txtEY.Location = new System.Drawing.Point(452, 147);
            this.txtEY.Name = "txtEY";
            this.txtEY.Size = new System.Drawing.Size(70, 22);
            this.txtEY.TabIndex = 13;
            this.txtEY.Text = "0";
            // 
            // txtEX
            // 
            this.txtEX.Location = new System.Drawing.Point(355, 147);
            this.txtEX.Name = "txtEX";
            this.txtEX.Size = new System.Drawing.Size(70, 22);
            this.txtEX.TabIndex = 12;
            this.txtEX.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 575);
            this.Controls.Add(this.txtEY);
            this.Controls.Add(this.txtEX);
            this.Controls.Add(this.txtSY);
            this.Controls.Add(this.txtSX);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSX;
        private System.Windows.Forms.TextBox txtSY;
        private System.Windows.Forms.TextBox txtEY;
        private System.Windows.Forms.TextBox txtEX;
    }
}