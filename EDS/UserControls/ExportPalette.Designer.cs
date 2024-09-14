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
            this.chkValidate = new System.Windows.Forms.CheckBox();
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
            this.clearErrors = new System.Windows.Forms.Button();
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
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.clearErrors);
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
            this.splitContainer1.Size = new System.Drawing.Size(416, 619);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // chkValidate
            // 
            this.chkValidate.AutoSize = true;
            this.chkValidate.Location = new System.Drawing.Point(14, 13);
            this.chkValidate.Margin = new System.Windows.Forms.Padding(2);
            this.chkValidate.Name = "chkValidate";
            this.chkValidate.Size = new System.Drawing.Size(297, 17);
            this.chkValidate.TabIndex = 8;
            this.chkValidate.Text = "Validate Drawing before scanning ( may take longer time )";
            this.chkValidate.UseVisualStyleBackColor = true;
            // 
            // antiClckWiseRdBttn
            // 
            this.antiClckWiseRdBttn.AutoSize = true;
            this.antiClckWiseRdBttn.Location = new System.Drawing.Point(194, 43);
            this.antiClckWiseRdBttn.Name = "antiClckWiseRdBttn";
            this.antiClckWiseRdBttn.Size = new System.Drawing.Size(100, 17);
            this.antiClckWiseRdBttn.TabIndex = 7;
            this.antiClckWiseRdBttn.TabStop = true;
            this.antiClckWiseRdBttn.Text = "Anti Clock Wise";
            this.antiClckWiseRdBttn.UseVisualStyleBackColor = true;
            // 
            // clckWiseRdBttn
            // 
            this.clckWiseRdBttn.AutoSize = true;
            this.clckWiseRdBttn.Checked = true;
            this.clckWiseRdBttn.Location = new System.Drawing.Point(116, 43);
            this.clckWiseRdBttn.Name = "clckWiseRdBttn";
            this.clckWiseRdBttn.Size = new System.Drawing.Size(79, 17);
            this.clckWiseRdBttn.TabIndex = 6;
            this.clckWiseRdBttn.TabStop = true;
            this.clckWiseRdBttn.Text = "Clock Wise";
            this.clckWiseRdBttn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "External Wall Sort By";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(103, 72);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(308, 37);
            this.progressBar1.TabIndex = 1;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(13, 72);
            this.btnScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(85, 37);
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
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 491);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProjectData
            // 
            this.tabProjectData.Controls.Add(this.treeView1);
            this.tabProjectData.Location = new System.Drawing.Point(4, 22);
            this.tabProjectData.Margin = new System.Windows.Forms.Padding(2);
            this.tabProjectData.Name = "tabProjectData";
            this.tabProjectData.Padding = new System.Windows.Forms.Padding(2);
            this.tabProjectData.Size = new System.Drawing.Size(408, 465);
            this.tabProjectData.TabIndex = 0;
            this.tabProjectData.Text = "Project Data";
            this.tabProjectData.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(2, 2);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(404, 461);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabExportedFiles
            // 
            this.tabExportedFiles.Controls.Add(this.webBrowser1);
            this.tabExportedFiles.Location = new System.Drawing.Point(4, 22);
            this.tabExportedFiles.Margin = new System.Windows.Forms.Padding(2);
            this.tabExportedFiles.Name = "tabExportedFiles";
            this.tabExportedFiles.Padding = new System.Windows.Forms.Padding(2);
            this.tabExportedFiles.Size = new System.Drawing.Size(408, 464);
            this.tabExportedFiles.TabIndex = 1;
            this.tabExportedFiles.Text = "Exported Files";
            this.tabExportedFiles.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(2, 2);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(404, 460);
            this.webBrowser1.TabIndex = 0;
            // 
            // clearErrors
            // 
            this.clearErrors.BackColor = System.Drawing.Color.IndianRed;
            this.clearErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearErrors.Location = new System.Drawing.Point(316, 13);
            this.clearErrors.Name = "clearErrors";
            this.clearErrors.Size = new System.Drawing.Size(94, 30);
            this.clearErrors.TabIndex = 9;
            this.clearErrors.Text = "Clear Errors";
            this.clearErrors.UseVisualStyleBackColor = false;
            this.clearErrors.Click += new System.EventHandler(this.clearErrors_Click);
            // 
            // ExportPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ExportPalette";
            this.Size = new System.Drawing.Size(416, 619);
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
        private System.Windows.Forms.Button clearErrors;
    }
}
