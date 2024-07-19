namespace EDS.UserControls
{
    partial class WindowsDataPalette
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
            this.btnAssignWindowData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSillHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHeightOfVisionWindow = new System.Windows.Forms.TextBox();
            this.txtHeightOfDaylightWindow = new System.Windows.Forms.TextBox();
            this.btnMatchWindowData = new System.Windows.Forms.Button();
            this.btnAddWindow = new System.Windows.Forms.Button();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAssignWindowData
            // 
            this.btnAssignWindowData.Location = new System.Drawing.Point(12, 126);
            this.btnAssignWindowData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAssignWindowData.Name = "btnAssignWindowData";
            this.btnAssignWindowData.Size = new System.Drawing.Size(83, 41);
            this.btnAssignWindowData.TabIndex = 0;
            this.btnAssignWindowData.Text = "Assign Window Data";
            this.btnAssignWindowData.UseVisualStyleBackColor = true;
            this.btnAssignWindowData.Click += new System.EventHandler(this.btnAssignWindowData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sill Height";
            // 
            // txtSillHeight
            // 
            this.txtSillHeight.Location = new System.Drawing.Point(142, 13);
            this.txtSillHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSillHeight.Name = "txtSillHeight";
            this.txtSillHeight.Size = new System.Drawing.Size(29, 20);
            this.txtSillHeight.TabIndex = 2;
            this.txtSillHeight.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height of Vision Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height of Daylight Window";
            // 
            // txtHeightOfVisionWindow
            // 
            this.txtHeightOfVisionWindow.Location = new System.Drawing.Point(142, 50);
            this.txtHeightOfVisionWindow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHeightOfVisionWindow.Name = "txtHeightOfVisionWindow";
            this.txtHeightOfVisionWindow.Size = new System.Drawing.Size(29, 20);
            this.txtHeightOfVisionWindow.TabIndex = 5;
            this.txtHeightOfVisionWindow.Text = "10";
            // 
            // txtHeightOfDaylightWindow
            // 
            this.txtHeightOfDaylightWindow.Location = new System.Drawing.Point(142, 93);
            this.txtHeightOfDaylightWindow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHeightOfDaylightWindow.Name = "txtHeightOfDaylightWindow";
            this.txtHeightOfDaylightWindow.Size = new System.Drawing.Size(29, 20);
            this.txtHeightOfDaylightWindow.TabIndex = 6;
            this.txtHeightOfDaylightWindow.Text = "15";
            // 
            // btnMatchWindowData
            // 
            this.btnMatchWindowData.Location = new System.Drawing.Point(100, 126);
            this.btnMatchWindowData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMatchWindowData.Name = "btnMatchWindowData";
            this.btnMatchWindowData.Size = new System.Drawing.Size(83, 41);
            this.btnMatchWindowData.TabIndex = 7;
            this.btnMatchWindowData.Text = "Match Window Data";
            this.btnMatchWindowData.UseVisualStyleBackColor = true;
            this.btnMatchWindowData.Click += new System.EventHandler(this.btnMatchWindowData_Click);
            // 
            // btnAddWindow
            // 
            this.btnAddWindow.Location = new System.Drawing.Point(12, 172);
            this.btnAddWindow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddWindow.Name = "btnAddWindow";
            this.btnAddWindow.Size = new System.Drawing.Size(83, 41);
            this.btnAddWindow.TabIndex = 8;
            this.btnAddWindow.Text = "Add Window";
            this.btnAddWindow.UseVisualStyleBackColor = true;
            this.btnAddWindow.Click += new System.EventHandler(this.btnAddWindow_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(142, 186);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(29, 20);
            this.txtWidth.TabIndex = 9;
            this.txtWidth.Text = "200";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 188);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Width";
            // 
            // WindowsDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.btnAddWindow);
            this.Controls.Add(this.btnMatchWindowData);
            this.Controls.Add(this.txtHeightOfDaylightWindow);
            this.Controls.Add(this.txtHeightOfVisionWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSillHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAssignWindowData);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "WindowsDataPalette";
            this.Size = new System.Drawing.Size(185, 222);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAssignWindowData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSillHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHeightOfVisionWindow;
        private System.Windows.Forms.TextBox txtHeightOfDaylightWindow;
        private System.Windows.Forms.Button btnMatchWindowData;
        private System.Windows.Forms.Button btnAddWindow;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label4;
    }
}
