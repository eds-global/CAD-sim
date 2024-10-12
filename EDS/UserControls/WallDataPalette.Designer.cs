namespace EDS.UserControls
{
    partial class WallDataPalette
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.uValue = new System.Windows.Forms.TextBox();
            this.uValueCheck = new System.Windows.Forms.CheckBox();
            this.extWallCombo = new System.Windows.Forms.ComboBox();
            this.intWallCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DrawButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.f1Type3CompText = new System.Windows.Forms.TextBox();
            this.f1Type2CompText = new System.Windows.Forms.TextBox();
            this.f1Type1CompText = new System.Windows.Forms.TextBox();
            this.f1Type3 = new System.Windows.Forms.ComboBox();
            this.f1Type2 = new System.Windows.Forms.ComboBox();
            this.f1Type1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.f2Type3CompText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.f2Type2CompText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.sameFaceCheck = new System.Windows.Forms.CheckBox();
            this.f2Type1CompText = new System.Windows.Forms.TextBox();
            this.f2Type2 = new System.Windows.Forms.ComboBox();
            this.f2Type1 = new System.Windows.Forms.ComboBox();
            this.f2Type3 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MatchAllButton = new System.Windows.Forms.Button();
            this.UpdateAllButton = new System.Windows.Forms.Button();
            this.wallMatchButton = new System.Windows.Forms.Button();
            this.wallUpdateButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.scanButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.unitLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draw Wall";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(145, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ext Wall Const.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "U-value";
            // 
            // uValue
            // 
            this.uValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uValue.Location = new System.Drawing.Point(119, 98);
            this.uValue.Name = "uValue";
            this.uValue.Size = new System.Drawing.Size(67, 20);
            this.uValue.TabIndex = 4;
            this.uValue.TextChanged += new System.EventHandler(this.uValue_TextChanged);
            // 
            // uValueCheck
            // 
            this.uValueCheck.AutoSize = true;
            this.uValueCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uValueCheck.Location = new System.Drawing.Point(17, 99);
            this.uValueCheck.Name = "uValueCheck";
            this.uValueCheck.Size = new System.Drawing.Size(15, 14);
            this.uValueCheck.TabIndex = 3;
            this.uValueCheck.UseVisualStyleBackColor = true;
            this.uValueCheck.CheckedChanged += new System.EventHandler(this.uValueCheck_CheckedChanged);
            // 
            // extWallCombo
            // 
            this.extWallCombo.DropDownHeight = 95;
            this.extWallCombo.FormattingEnabled = true;
            this.extWallCombo.IntegralHeight = false;
            this.extWallCombo.ItemHeight = 13;
            this.extWallCombo.Location = new System.Drawing.Point(119, 37);
            this.extWallCombo.Name = "extWallCombo";
            this.extWallCombo.Size = new System.Drawing.Size(121, 21);
            this.extWallCombo.TabIndex = 1;
            this.extWallCombo.SelectedIndexChanged += new System.EventHandler(this.extWallCombo_SelectedIndexChanged);
            // 
            // intWallCombo
            // 
            this.intWallCombo.DropDownHeight = 95;
            this.intWallCombo.FormattingEnabled = true;
            this.intWallCombo.IntegralHeight = false;
            this.intWallCombo.ItemHeight = 13;
            this.intWallCombo.Location = new System.Drawing.Point(119, 70);
            this.intWallCombo.Name = "intWallCombo";
            this.intWallCombo.Size = new System.Drawing.Size(121, 21);
            this.intWallCombo.TabIndex = 2;
            this.intWallCombo.SelectedIndexChanged += new System.EventHandler(this.intWallCombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Int Wall Const.*";
            // 
            // DrawButton
            // 
            this.DrawButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DrawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.DrawButton.Location = new System.Drawing.Point(14, 483);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(70, 28);
            this.DrawButton.TabIndex = 20;
            this.DrawButton.Text = "Draw";
            this.DrawButton.UseVisualStyleBackColor = false;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SelectButton.Location = new System.Drawing.Point(90, 483);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(64, 28);
            this.SelectButton.TabIndex = 21;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = false;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.f1Type3CompText);
            this.panel1.Controls.Add(this.f1Type2CompText);
            this.panel1.Controls.Add(this.f1Type1CompText);
            this.panel1.Controls.Add(this.f1Type3);
            this.panel1.Controls.Add(this.f1Type2);
            this.panel1.Controls.Add(this.f1Type1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(14, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 142);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(158, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(10, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 11);
            this.label10.TabIndex = 29;
            this.label10.Text = "Finish";
            // 
            // f1Type3CompText
            // 
            this.f1Type3CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type3CompText.Location = new System.Drawing.Point(136, 110);
            this.f1Type3CompText.Name = "f1Type3CompText";
            this.f1Type3CompText.Size = new System.Drawing.Size(62, 20);
            this.f1Type3CompText.TabIndex = 12;
            // 
            // f1Type2CompText
            // 
            this.f1Type2CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type2CompText.Location = new System.Drawing.Point(136, 83);
            this.f1Type2CompText.Name = "f1Type2CompText";
            this.f1Type2CompText.Size = new System.Drawing.Size(62, 20);
            this.f1Type2CompText.TabIndex = 10;
            // 
            // f1Type1CompText
            // 
            this.f1Type1CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type1CompText.Location = new System.Drawing.Point(136, 56);
            this.f1Type1CompText.Name = "f1Type1CompText";
            this.f1Type1CompText.Size = new System.Drawing.Size(62, 20);
            this.f1Type1CompText.TabIndex = 8;
            this.f1Type1CompText.TextChanged += new System.EventHandler(this.f1Type1CompText_TextChanged);
            // 
            // f1Type3
            // 
            this.f1Type3.DropDownHeight = 95;
            this.f1Type3.FormattingEnabled = true;
            this.f1Type3.IntegralHeight = false;
            this.f1Type3.ItemHeight = 13;
            this.f1Type3.Location = new System.Drawing.Point(12, 109);
            this.f1Type3.Name = "f1Type3";
            this.f1Type3.Size = new System.Drawing.Size(97, 21);
            this.f1Type3.TabIndex = 11;
            // 
            // f1Type2
            // 
            this.f1Type2.DropDownHeight = 95;
            this.f1Type2.FormattingEnabled = true;
            this.f1Type2.IntegralHeight = false;
            this.f1Type2.ItemHeight = 13;
            this.f1Type2.Location = new System.Drawing.Point(12, 82);
            this.f1Type2.Name = "f1Type2";
            this.f1Type2.Size = new System.Drawing.Size(97, 21);
            this.f1Type2.TabIndex = 9;
            // 
            // f1Type1
            // 
            this.f1Type1.DropDownHeight = 95;
            this.f1Type1.FormattingEnabled = true;
            this.f1Type1.IntegralHeight = false;
            this.f1Type1.ItemHeight = 13;
            this.f1Type1.Location = new System.Drawing.Point(12, 55);
            this.f1Type1.Name = "f1Type1";
            this.f1Type1.Size = new System.Drawing.Size(97, 21);
            this.f1Type1.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(226, 22);
            this.label8.TabIndex = 1;
            this.label8.Text = "Face 01";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.f2Type3CompText);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.f2Type2CompText);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.sameFaceCheck);
            this.panel2.Controls.Add(this.f2Type1CompText);
            this.panel2.Controls.Add(this.f2Type2);
            this.panel2.Controls.Add(this.f2Type1);
            this.panel2.Controls.Add(this.f2Type3);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(14, 319);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 142);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(158, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "%";
            // 
            // f2Type3CompText
            // 
            this.f2Type3CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type3CompText.Location = new System.Drawing.Point(136, 106);
            this.f2Type3CompText.Name = "f2Type3CompText";
            this.f2Type3CompText.Size = new System.Drawing.Size(62, 20);
            this.f2Type3CompText.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(119, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Same As Face 01";
            // 
            // f2Type2CompText
            // 
            this.f2Type2CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type2CompText.Location = new System.Drawing.Point(136, 78);
            this.f2Type2CompText.Name = "f2Type2CompText";
            this.f2Type2CompText.Size = new System.Drawing.Size(62, 20);
            this.f2Type2CompText.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 11);
            this.label13.TabIndex = 31;
            this.label13.Text = "Finish";
            // 
            // sameFaceCheck
            // 
            this.sameFaceCheck.AutoSize = true;
            this.sameFaceCheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.sameFaceCheck.Location = new System.Drawing.Point(55, 2);
            this.sameFaceCheck.Name = "sameFaceCheck";
            this.sameFaceCheck.Size = new System.Drawing.Size(15, 14);
            this.sameFaceCheck.TabIndex = 13;
            this.sameFaceCheck.UseVisualStyleBackColor = false;
            // 
            // f2Type1CompText
            // 
            this.f2Type1CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type1CompText.Location = new System.Drawing.Point(136, 54);
            this.f2Type1CompText.Name = "f2Type1CompText";
            this.f2Type1CompText.Size = new System.Drawing.Size(62, 20);
            this.f2Type1CompText.TabIndex = 15;
            // 
            // f2Type2
            // 
            this.f2Type2.DropDownHeight = 95;
            this.f2Type2.FormattingEnabled = true;
            this.f2Type2.IntegralHeight = false;
            this.f2Type2.ItemHeight = 13;
            this.f2Type2.Location = new System.Drawing.Point(11, 78);
            this.f2Type2.Name = "f2Type2";
            this.f2Type2.Size = new System.Drawing.Size(97, 21);
            this.f2Type2.TabIndex = 16;
            // 
            // f2Type1
            // 
            this.f2Type1.DropDownHeight = 95;
            this.f2Type1.FormattingEnabled = true;
            this.f2Type1.IntegralHeight = false;
            this.f2Type1.ItemHeight = 13;
            this.f2Type1.Location = new System.Drawing.Point(11, 52);
            this.f2Type1.Name = "f2Type1";
            this.f2Type1.Size = new System.Drawing.Size(97, 21);
            this.f2Type1.TabIndex = 14;
            // 
            // f2Type3
            // 
            this.f2Type3.DropDownHeight = 95;
            this.f2Type3.FormattingEnabled = true;
            this.f2Type3.IntegralHeight = false;
            this.f2Type3.ItemHeight = 13;
            this.f2Type3.Location = new System.Drawing.Point(11, 104);
            this.f2Type3.Name = "f2Type3";
            this.f2Type3.Size = new System.Drawing.Size(97, 21);
            this.f2Type3.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(224, 21);
            this.label9.TabIndex = 2;
            this.label9.Text = "Face 02";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(3, -2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 33);
            this.label6.TabIndex = 17;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // MatchAllButton
            // 
            this.MatchAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MatchAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchAllButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MatchAllButton.Location = new System.Drawing.Point(14, 518);
            this.MatchAllButton.Name = "MatchAllButton";
            this.MatchAllButton.Size = new System.Drawing.Size(68, 28);
            this.MatchAllButton.TabIndex = 23;
            this.MatchAllButton.Text = "Match All";
            this.MatchAllButton.UseVisualStyleBackColor = false;
            this.MatchAllButton.Click += new System.EventHandler(this.MatchAllButton_Click);
            // 
            // UpdateAllButton
            // 
            this.UpdateAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UpdateAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateAllButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UpdateAllButton.Location = new System.Drawing.Point(160, 483);
            this.UpdateAllButton.Name = "UpdateAllButton";
            this.UpdateAllButton.Size = new System.Drawing.Size(80, 28);
            this.UpdateAllButton.TabIndex = 22;
            this.UpdateAllButton.Text = "Update All";
            this.UpdateAllButton.UseVisualStyleBackColor = false;
            this.UpdateAllButton.Click += new System.EventHandler(this.UpdateAllButton_Click);
            // 
            // wallMatchButton
            // 
            this.wallMatchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.wallMatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wallMatchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.wallMatchButton.Location = new System.Drawing.Point(150, 122);
            this.wallMatchButton.Name = "wallMatchButton";
            this.wallMatchButton.Size = new System.Drawing.Size(88, 30);
            this.wallMatchButton.TabIndex = 6;
            this.wallMatchButton.Text = "Match";
            this.wallMatchButton.UseVisualStyleBackColor = false;
            this.wallMatchButton.Click += new System.EventHandler(this.wallMatchButton_Click);
            // 
            // wallUpdateButton
            // 
            this.wallUpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.wallUpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wallUpdateButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.wallUpdateButton.Location = new System.Drawing.Point(14, 122);
            this.wallUpdateButton.Name = "wallUpdateButton";
            this.wallUpdateButton.Size = new System.Drawing.Size(88, 30);
            this.wallUpdateButton.TabIndex = 5;
            this.wallUpdateButton.Text = "Update";
            this.wallUpdateButton.UseVisualStyleBackColor = false;
            this.wallUpdateButton.Click += new System.EventHandler(this.wallUpdateButton_Click);
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(14, 472);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(226, 1);
            this.label15.TabIndex = 23;
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.refreshButton.Location = new System.Drawing.Point(88, 518);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(64, 28);
            this.refreshButton.TabIndex = 24;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toggleSwitch1.Location = new System.Drawing.Point(182, 6);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OffText = "SI";
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnText = "IP";
            this.toggleSwitch1.Size = new System.Drawing.Size(50, 19);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 1;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch1_CheckedChanged);
            // 
            // scanButton
            // 
            this.scanButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.scanButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scanButton.Location = new System.Drawing.Point(160, 518);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(80, 28);
            this.scanButton.TabIndex = 25;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = false;
            this.scanButton.Visible = false;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 553);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(224, 23);
            this.progressBar1.TabIndex = 27;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(16, 583);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(227, 137);
            this.treeView1.TabIndex = 26;
            this.treeView1.Visible = false;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.ForeColor = System.Drawing.Color.Red;
            this.unitLabel.Location = new System.Drawing.Point(196, 99);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(48, 13);
            this.unitLabel.TabIndex = 30;
            this.unitLabel.Text = "W/sqmk";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(14, 160);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(222, 1);
            this.label16.TabIndex = 24;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // WallDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.wallMatchButton);
            this.Controls.Add(this.wallUpdateButton);
            this.Controls.Add(this.MatchAllButton);
            this.Controls.Add(this.UpdateAllButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.intWallCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.extWallCombo);
            this.Controls.Add(this.uValueCheck);
            this.Controls.Add(this.uValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Name = "WallDataPalette";
            this.Size = new System.Drawing.Size(247, 731);
            this.Load += new System.EventHandler(this.WallDataPalette_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox uValue;
        private System.Windows.Forms.CheckBox uValueCheck;
        private System.Windows.Forms.ComboBox extWallCombo;
        private System.Windows.Forms.ComboBox intWallCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox sameFaceCheck;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox f1Type3CompText;
        private System.Windows.Forms.TextBox f1Type2CompText;
        private System.Windows.Forms.TextBox f1Type1CompText;
        private System.Windows.Forms.ComboBox f1Type3;
        private System.Windows.Forms.ComboBox f1Type2;
        private System.Windows.Forms.ComboBox f1Type1;
        private System.Windows.Forms.TextBox f2Type3CompText;
        private System.Windows.Forms.TextBox f2Type2CompText;
        private System.Windows.Forms.TextBox f2Type1CompText;
        private System.Windows.Forms.ComboBox f2Type3;
        private System.Windows.Forms.ComboBox f2Type1;
        private System.Windows.Forms.ComboBox f2Type2;
        private System.Windows.Forms.Button MatchAllButton;
        private System.Windows.Forms.Button UpdateAllButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button wallMatchButton;
        private System.Windows.Forms.Button wallUpdateButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label unitLabel;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label16;
    }
}
