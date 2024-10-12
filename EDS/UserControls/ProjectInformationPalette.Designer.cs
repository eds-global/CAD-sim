namespace EDS
{
    partial class ProjectInformationPalette
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            cbBuildingTypes = new System.Windows.Forms.ComboBox();
            cbBuildingCategory = new System.Windows.Forms.ComboBox();
            txtAddress = new System.Windows.Forms.TextBox();
            txtClientName = new System.Windows.Forms.TextBox();
            txtProjectName = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            cbRepCity = new System.Windows.Forms.ComboBox();
            cbCities = new System.Windows.Forms.ComboBox();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            txtLong = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            txtLat = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            cbStates = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            radCustomLocation = new System.Windows.Forms.RadioButton();
            radSelectLocation = new System.Windows.Forms.RadioButton();
            groupBox3 = new System.Windows.Forms.GroupBox();
            lbClimateType = new System.Windows.Forms.Label();
            lbLongitude = new System.Windows.Forms.Label();
            lbLatitude = new System.Windows.Forms.Label();
            btnOK = new System.Windows.Forms.Button();
            pbNorthArrow = new System.Windows.Forms.PictureBox();
            txtAngle = new System.Windows.Forms.TextBox();
            btnLoadProjectDetails = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pbNorthArrow)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(cbBuildingTypes);
            this.groupBox1.Controls.Add(cbBuildingCategory);
            this.groupBox1.Controls.Add(txtAddress);
            this.groupBox1.Controls.Add(txtClientName);
            this.groupBox1.Controls.Add(txtProjectName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(327, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Details";
            // 
            // cbBuildingTypes
            // 
            cbBuildingTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbBuildingTypes.FormattingEnabled = true;
            cbBuildingTypes.ItemHeight = 13;
            cbBuildingTypes.Location = new System.Drawing.Point(160, 150);
            cbBuildingTypes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            cbBuildingTypes.Name = "cbBuildingTypes";
            cbBuildingTypes.Size = new System.Drawing.Size(168, 21);
            cbBuildingTypes.TabIndex = 5;
            // 
            // cbBuildingCategory
            // 
            cbBuildingCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbBuildingCategory.FormattingEnabled = true;
            cbBuildingCategory.ItemHeight = 13;
            cbBuildingCategory.Location = new System.Drawing.Point(160, 115);
            cbBuildingCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            cbBuildingCategory.Name = "cbBuildingCategory";
            cbBuildingCategory.Size = new System.Drawing.Size(168, 21);
            cbBuildingCategory.TabIndex = 4;
            cbBuildingCategory.SelectedIndexChanged += new System.EventHandler(this.cbBuildingCategory_SelectedIndexChanged);
            // 
            // txtAddress
            // 
            txtAddress.Location = new System.Drawing.Point(160, 85);
            txtAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new System.Drawing.Size(168, 20);
            txtAddress.TabIndex = 3;
            // 
            // txtClientName
            // 
            txtClientName.Location = new System.Drawing.Point(160, 54);
            txtClientName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtClientName.Name = "txtClientName";
            txtClientName.Size = new System.Drawing.Size(168, 20);
            txtClientName.TabIndex = 2;
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new System.Drawing.Point(160, 22);
            txtProjectName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new System.Drawing.Size(168, 20);
            txtProjectName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 152);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Building Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Building Category";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Adddress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Client Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(cbRepCity);
            this.groupBox2.Controls.Add(cbCities);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(txtLong);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(txtLat);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(cbStates);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(radCustomLocation);
            this.groupBox2.Controls.Add(radSelectLocation);
            this.groupBox2.Location = new System.Drawing.Point(2, 197);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(197, 258);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Site Details";
            // 
            // cbRepCity
            // 
            cbRepCity.Enabled = false;
            cbRepCity.FormattingEnabled = true;
            cbRepCity.Location = new System.Drawing.Point(52, 219);
            cbRepCity.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            cbRepCity.Name = "cbRepCity";
            cbRepCity.Size = new System.Drawing.Size(135, 21);
            cbRepCity.TabIndex = 11;
            // 
            // cbCities
            // 
            cbCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbCities.FormattingEnabled = true;
            cbCities.Location = new System.Drawing.Point(52, 98);
            cbCities.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            cbCities.Name = "cbCities";
            cbCities.Size = new System.Drawing.Size(135, 21);
            cbCities.TabIndex = 7;
            cbCities.SelectedIndexChanged += new System.EventHandler(this.cbCities_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(2, 222);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Rep. City";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "City";
            // 
            // txtLong
            // 
            txtLong.Enabled = false;
            txtLong.Location = new System.Drawing.Point(141, 182);
            txtLong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtLong.Name = "txtLong";
            txtLong.Size = new System.Drawing.Size(46, 20);
            txtLong.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(103, 184);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Long.";
            // 
            // txtLat
            // 
            txtLat.Enabled = false;
            txtLat.Location = new System.Drawing.Point(52, 182);
            txtLat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtLat.Name = "txtLat";
            txtLat.Size = new System.Drawing.Size(48, 20);
            txtLat.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(2, 184);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Lat.";
            // 
            // cbStates
            // 
            cbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbStates.FormattingEnabled = true;
            cbStates.Location = new System.Drawing.Point(52, 59);
            cbStates.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            cbStates.Name = "cbStates";
            cbStates.Size = new System.Drawing.Size(135, 21);
            cbStates.TabIndex = 6;
            cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "State";
            // 
            // radCustomLocation
            // 
            radCustomLocation.AutoSize = true;
            radCustomLocation.Location = new System.Drawing.Point(52, 146);
            radCustomLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            radCustomLocation.Name = "radCustomLocation";
            radCustomLocation.Size = new System.Drawing.Size(104, 17);
            radCustomLocation.TabIndex = 8;
            radCustomLocation.Text = "Custom Location";
            radCustomLocation.UseVisualStyleBackColor = true;
            radCustomLocation.CheckedChanged += new System.EventHandler(this.radCustomLocation_CheckedChanged);
            // 
            // radSelectLocation
            // 
            radSelectLocation.AutoSize = true;
            radSelectLocation.Checked = true;
            radSelectLocation.Location = new System.Drawing.Point(52, 24);
            radSelectLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            radSelectLocation.Name = "radSelectLocation";
            radSelectLocation.Size = new System.Drawing.Size(99, 17);
            radSelectLocation.TabIndex = 0;
            radSelectLocation.TabStop = true;
            radSelectLocation.Text = "Select Location";
            radSelectLocation.UseVisualStyleBackColor = true;
            radSelectLocation.CheckedChanged += new System.EventHandler(this.radSelectLocation_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(lbClimateType);
            this.groupBox3.Controls.Add(lbLongitude);
            this.groupBox3.Controls.Add(lbLatitude);
            this.groupBox3.Location = new System.Drawing.Point(204, 204);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(125, 93);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Project Statistics";
            // 
            // lbClimateType
            // 
            lbClimateType.AutoSize = true;
            lbClimateType.Location = new System.Drawing.Point(4, 72);
            lbClimateType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbClimateType.Name = "lbClimateType";
            lbClimateType.Size = new System.Drawing.Size(71, 13);
            lbClimateType.TabIndex = 7;
            lbClimateType.Text = "Climate Type:";
            // 
            // lbLongitude
            // 
            lbLongitude.AutoSize = true;
            lbLongitude.Location = new System.Drawing.Point(4, 49);
            lbLongitude.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbLongitude.Name = "lbLongitude";
            lbLongitude.Size = new System.Drawing.Size(55, 13);
            lbLongitude.TabIndex = 6;
            lbLongitude.Text = "Longtude:";
            // 
            // lbLatitude
            // 
            lbLatitude.AutoSize = true;
            lbLatitude.Location = new System.Drawing.Point(4, 24);
            lbLatitude.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbLatitude.Name = "lbLatitude";
            lbLatitude.Size = new System.Drawing.Size(48, 13);
            lbLatitude.TabIndex = 5;
            lbLatitude.Text = "Latitude:";
            // 
            // btnOK
            // 
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Location = new System.Drawing.Point(187, 459);
            btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(142, 40);
            btnOK.TabIndex = 14;
            btnOK.Text = "Save Project Details";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pbNorthArrow
            // 
            pbNorthArrow.BackColor = System.Drawing.Color.Black;
            pbNorthArrow.Image = global::EDS.Properties.Resources.NorthArrow;
            pbNorthArrow.Location = new System.Drawing.Point(204, 302);
            pbNorthArrow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            pbNorthArrow.Name = "pbNorthArrow";
            pbNorthArrow.Size = new System.Drawing.Size(125, 136);
            pbNorthArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pbNorthArrow.TabIndex = 6;
            pbNorthArrow.TabStop = false;
            pbNorthArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbNorthArrow_MouseDown);
            pbNorthArrow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbNorthArrow_MouseMove);
            pbNorthArrow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbNorthArrow_MouseUp);
            // 
            // txtAngle
            // 
            txtAngle.Location = new System.Drawing.Point(205, 436);
            txtAngle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            txtAngle.Name = "txtAngle";
            txtAngle.Size = new System.Drawing.Size(126, 20);
            txtAngle.TabIndex = 12;
            txtAngle.Text = "0";
            txtAngle.TextChanged += new System.EventHandler(this.txtAngle_TextChanged);
            // 
            // btnLoadProjectDetails
            // 
            btnLoadProjectDetails.Location = new System.Drawing.Point(5, 459);
            btnLoadProjectDetails.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnLoadProjectDetails.Name = "btnLoadProjectDetails";
            btnLoadProjectDetails.Size = new System.Drawing.Size(144, 40);
            btnLoadProjectDetails.TabIndex = 13;
            btnLoadProjectDetails.Text = "Load Project Details";
            btnLoadProjectDetails.UseVisualStyleBackColor = true;
            btnLoadProjectDetails.Click += new System.EventHandler(this.btnLoadProjectDetails_Click);
            // 
            // ProjectInformationPalette
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(btnLoadProjectDetails);
            this.Controls.Add(txtAngle);
            this.Controls.Add(pbNorthArrow);
            this.Controls.Add(btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "ProjectInformationPalette";
            Size = new System.Drawing.Size(336, 505);
            this.Load += new System.EventHandler(this.ProjectInformationPalette_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pbNorthArrow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;

        public static System.Windows.Forms.Button btnOK;
        public static System.Windows.Forms.PictureBox pbNorthArrow;
        public static System.Windows.Forms.Button btnLoadProjectDetails;
        public static System.Windows.Forms.ComboBox cbBuildingTypes;
        public static System.Windows.Forms.ComboBox cbBuildingCategory;
        public static System.Windows.Forms.TextBox txtAddress;
        public static System.Windows.Forms.TextBox txtClientName;
        public static System.Windows.Forms.TextBox txtProjectName;
        public static System.Windows.Forms.ComboBox cbCities;
        public static System.Windows.Forms.ComboBox cbStates;
        public static System.Windows.Forms.Label lbClimateType;
        public static System.Windows.Forms.Label lbLongitude;
        public static System.Windows.Forms.Label lbLatitude;
        public static System.Windows.Forms.TextBox txtAngle;
        public static System.Windows.Forms.RadioButton radCustomLocation;
        public static System.Windows.Forms.RadioButton radSelectLocation;
        public static System.Windows.Forms.ComboBox cbRepCity;
        public static System.Windows.Forms.TextBox txtLat;
        public static System.Windows.Forms.TextBox txtLong;
    }
}
