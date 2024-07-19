namespace EDS
{
    partial class ExportPalette
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.antiClckWiseRdBttn = new System.Windows.Forms.RadioButton();
            this.clckWiseRdBttn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnScan = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProjectData = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabExportedFiles = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.chkValidate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabProjectData.SuspendLayout();
            this.tabExportedFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkValidate);
            this.splitContainer1.Panel1.Controls.Add(this.antiClckWiseRdBttn);
            this.splitContainer1.Panel1.Controls.Add(this.clckWiseRdBttn);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.btnScan);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(555, 762);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 0;
            // 
            // antiClckWiseRdBttn
            // 
            this.antiClckWiseRdBttn.AutoSize = true;
            this.antiClckWiseRdBttn.Location = new System.Drawing.Point(258, 53);
            this.antiClckWiseRdBttn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.antiClckWiseRdBttn.Name = "antiClckWiseRdBttn";
            this.antiClckWiseRdBttn.Size = new System.Drawing.Size(121, 20);
            this.antiClckWiseRdBttn.TabIndex = 7;
            this.antiClckWiseRdBttn.TabStop = true;
            this.antiClckWiseRdBttn.Text = "Anti Clock Wise";
            this.antiClckWiseRdBttn.UseVisualStyleBackColor = true;
            // 
            // clckWiseRdBttn
            // 
            this.clckWiseRdBttn.AutoSize = true;
            this.clckWiseRdBttn.Checked = true;
            this.clckWiseRdBttn.Location = new System.Drawing.Point(154, 53);
            this.clckWiseRdBttn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clckWiseRdBttn.Name = "clckWiseRdBttn";
            this.clckWiseRdBttn.Size = new System.Drawing.Size(96, 20);
            this.clckWiseRdBttn.TabIndex = 6;
            this.clckWiseRdBttn.TabStop = true;
            this.clckWiseRdBttn.Text = "Clock Wise";
            this.clckWiseRdBttn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "External Wall Sort By";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(137, 89);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(411, 46);
            this.progressBar1.TabIndex = 1;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(17, 89);
            this.btnScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(113, 46);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProjectData);
            this.tabControl1.Controls.Add(this.tabExportedFiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 617);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProjectData
            // 
            this.tabProjectData.Controls.Add(this.treeView1);
            this.tabProjectData.Location = new System.Drawing.Point(4, 25);
            this.tabProjectData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabProjectData.Name = "tabProjectData";
            this.tabProjectData.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabProjectData.Size = new System.Drawing.Size(547, 588);
            this.tabProjectData.TabIndex = 0;
            this.tabProjectData.Text = "Project Data";
            this.tabProjectData.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 2);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(541, 584);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabExportedFiles
            // 
            this.tabExportedFiles.Controls.Add(this.webBrowser1);
            this.tabExportedFiles.Location = new System.Drawing.Point(4, 25);
            this.tabExportedFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabExportedFiles.Name = "tabExportedFiles";
            this.tabExportedFiles.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabExportedFiles.Size = new System.Drawing.Size(547, 604);
            this.tabExportedFiles.TabIndex = 1;
            this.tabExportedFiles.Text = "Exported Files";
            this.tabExportedFiles.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 2);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(541, 600);
            this.webBrowser1.TabIndex = 0;
            // 
            // chkValidate
            // 
            this.chkValidate.AutoSize = true;
            this.chkValidate.Location = new System.Drawing.Point(18, 16);
            this.chkValidate.Name = "chkValidate";
            this.chkValidate.Size = new System.Drawing.Size(371, 20);
            this.chkValidate.TabIndex = 8;
            this.chkValidate.Text = "Validate Drawing before scanning ( may take longer time )";
            this.chkValidate.UseVisualStyleBackColor = true;
            // 
            // ExportPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ExportPalette";
            this.Size = new System.Drawing.Size(555, 762);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabProjectData.ResumeLayout(false);
            this.tabExportedFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProjectData;
        private System.Windows.Forms.TabPage tabExportedFiles;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.RadioButton antiClckWiseRdBttn;
        private System.Windows.Forms.RadioButton clckWiseRdBttn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkValidate;
    }
}
