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
            this.label1.Location = new System.Drawing.Point(17, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draw Wall";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(193, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ext Wall Const.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "U-value";
            // 
            // uValue
            // 
            this.uValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uValue.Location = new System.Drawing.Point(159, 120);
            this.uValue.Margin = new System.Windows.Forms.Padding(4);
            this.uValue.Name = "uValue";
            this.uValue.Size = new System.Drawing.Size(89, 22);
            this.uValue.TabIndex = 3;
            this.uValue.TextChanged += new System.EventHandler(this.uValue_TextChanged);
            // 
            // uValueCheck
            // 
            this.uValueCheck.AutoSize = true;
            this.uValueCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uValueCheck.Location = new System.Drawing.Point(23, 122);
            this.uValueCheck.Margin = new System.Windows.Forms.Padding(4);
            this.uValueCheck.Name = "uValueCheck";
            this.uValueCheck.Size = new System.Drawing.Size(18, 17);
            this.uValueCheck.TabIndex = 2;
            this.uValueCheck.UseVisualStyleBackColor = true;
            this.uValueCheck.CheckedChanged += new System.EventHandler(this.uValueCheck_CheckedChanged);
            // 
            // extWallCombo
            // 
            this.extWallCombo.DropDownHeight = 95;
            this.extWallCombo.FormattingEnabled = true;
            this.extWallCombo.IntegralHeight = false;
            this.extWallCombo.ItemHeight = 16;
            this.extWallCombo.Location = new System.Drawing.Point(159, 46);
            this.extWallCombo.Margin = new System.Windows.Forms.Padding(4);
            this.extWallCombo.Name = "extWallCombo";
            this.extWallCombo.Size = new System.Drawing.Size(160, 24);
            this.extWallCombo.TabIndex = 0;
            this.extWallCombo.SelectedIndexChanged += new System.EventHandler(this.extWallCombo_SelectedIndexChanged);
            // 
            // intWallCombo
            // 
            this.intWallCombo.DropDownHeight = 95;
            this.intWallCombo.FormattingEnabled = true;
            this.intWallCombo.IntegralHeight = false;
            this.intWallCombo.ItemHeight = 16;
            this.intWallCombo.Location = new System.Drawing.Point(159, 86);
            this.intWallCombo.Margin = new System.Windows.Forms.Padding(4);
            this.intWallCombo.Name = "intWallCombo";
            this.intWallCombo.Size = new System.Drawing.Size(160, 24);
            this.intWallCombo.TabIndex = 1;
            this.intWallCombo.SelectedIndexChanged += new System.EventHandler(this.intWallCombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 86);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Int Wall Const.*";
            // 
            // DrawButton
            // 
            this.DrawButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DrawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.DrawButton.Location = new System.Drawing.Point(18, 595);
            this.DrawButton.Margin = new System.Windows.Forms.Padding(4);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(94, 35);
            this.DrawButton.TabIndex = 19;
            this.DrawButton.Text = "Draw";
            this.DrawButton.UseVisualStyleBackColor = false;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SelectButton.Location = new System.Drawing.Point(120, 595);
            this.SelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(85, 35);
            this.SelectButton.TabIndex = 20;
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
            this.panel1.Location = new System.Drawing.Point(18, 211);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 174);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(210, 49);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(13, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 14);
            this.label10.TabIndex = 29;
            this.label10.Text = "Finish";
            // 
            // f1Type3CompText
            // 
            this.f1Type3CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type3CompText.Location = new System.Drawing.Point(181, 135);
            this.f1Type3CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type3CompText.Name = "f1Type3CompText";
            this.f1Type3CompText.Size = new System.Drawing.Size(82, 22);
            this.f1Type3CompText.TabIndex = 11;
            // 
            // f1Type2CompText
            // 
            this.f1Type2CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type2CompText.Location = new System.Drawing.Point(181, 102);
            this.f1Type2CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type2CompText.Name = "f1Type2CompText";
            this.f1Type2CompText.Size = new System.Drawing.Size(82, 22);
            this.f1Type2CompText.TabIndex = 9;
            // 
            // f1Type1CompText
            // 
            this.f1Type1CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f1Type1CompText.Location = new System.Drawing.Point(181, 69);
            this.f1Type1CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type1CompText.Name = "f1Type1CompText";
            this.f1Type1CompText.Size = new System.Drawing.Size(82, 22);
            this.f1Type1CompText.TabIndex = 7;
            this.f1Type1CompText.TextChanged += new System.EventHandler(this.f1Type1CompText_TextChanged);
            // 
            // f1Type3
            // 
            this.f1Type3.DropDownHeight = 95;
            this.f1Type3.FormattingEnabled = true;
            this.f1Type3.IntegralHeight = false;
            this.f1Type3.ItemHeight = 16;
            this.f1Type3.Location = new System.Drawing.Point(16, 134);
            this.f1Type3.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type3.Name = "f1Type3";
            this.f1Type3.Size = new System.Drawing.Size(128, 24);
            this.f1Type3.TabIndex = 10;
            // 
            // f1Type2
            // 
            this.f1Type2.DropDownHeight = 95;
            this.f1Type2.FormattingEnabled = true;
            this.f1Type2.IntegralHeight = false;
            this.f1Type2.ItemHeight = 16;
            this.f1Type2.Location = new System.Drawing.Point(16, 101);
            this.f1Type2.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type2.Name = "f1Type2";
            this.f1Type2.Size = new System.Drawing.Size(128, 24);
            this.f1Type2.TabIndex = 8;
            // 
            // f1Type1
            // 
            this.f1Type1.DropDownHeight = 95;
            this.f1Type1.FormattingEnabled = true;
            this.f1Type1.IntegralHeight = false;
            this.f1Type1.ItemHeight = 16;
            this.f1Type1.Location = new System.Drawing.Point(16, 68);
            this.f1Type1.Margin = new System.Windows.Forms.Padding(4);
            this.f1Type1.Name = "f1Type1";
            this.f1Type1.Size = new System.Drawing.Size(128, 24);
            this.f1Type1.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(301, 27);
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
            this.panel2.Location = new System.Drawing.Point(18, 393);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 174);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(210, 46);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 16);
            this.label12.TabIndex = 32;
            this.label12.Text = "%";
            // 
            // f2Type3CompText
            // 
            this.f2Type3CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type3CompText.Location = new System.Drawing.Point(181, 130);
            this.f2Type3CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type3CompText.Name = "f2Type3CompText";
            this.f2Type3CompText.Size = new System.Drawing.Size(82, 22);
            this.f2Type3CompText.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(159, -1);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Same As Face 01";
            // 
            // f2Type2CompText
            // 
            this.f2Type2CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type2CompText.Location = new System.Drawing.Point(181, 96);
            this.f2Type2CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type2CompText.Name = "f2Type2CompText";
            this.f2Type2CompText.Size = new System.Drawing.Size(82, 22);
            this.f2Type2CompText.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 46);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 14);
            this.label13.TabIndex = 31;
            this.label13.Text = "Finish";
            // 
            // sameFaceCheck
            // 
            this.sameFaceCheck.AutoSize = true;
            this.sameFaceCheck.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.sameFaceCheck.Location = new System.Drawing.Point(73, 3);
            this.sameFaceCheck.Margin = new System.Windows.Forms.Padding(4);
            this.sameFaceCheck.Name = "sameFaceCheck";
            this.sameFaceCheck.Size = new System.Drawing.Size(18, 17);
            this.sameFaceCheck.TabIndex = 12;
            this.sameFaceCheck.UseVisualStyleBackColor = false;
            // 
            // f2Type1CompText
            // 
            this.f2Type1CompText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.f2Type1CompText.Location = new System.Drawing.Point(181, 66);
            this.f2Type1CompText.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type1CompText.Name = "f2Type1CompText";
            this.f2Type1CompText.Size = new System.Drawing.Size(82, 22);
            this.f2Type1CompText.TabIndex = 14;
            // 
            // f2Type2
            // 
            this.f2Type2.DropDownHeight = 95;
            this.f2Type2.FormattingEnabled = true;
            this.f2Type2.IntegralHeight = false;
            this.f2Type2.ItemHeight = 16;
            this.f2Type2.Location = new System.Drawing.Point(15, 96);
            this.f2Type2.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type2.Name = "f2Type2";
            this.f2Type2.Size = new System.Drawing.Size(128, 24);
            this.f2Type2.TabIndex = 15;
            // 
            // f2Type1
            // 
            this.f2Type1.DropDownHeight = 95;
            this.f2Type1.FormattingEnabled = true;
            this.f2Type1.IntegralHeight = false;
            this.f2Type1.ItemHeight = 16;
            this.f2Type1.Location = new System.Drawing.Point(15, 64);
            this.f2Type1.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type1.Name = "f2Type1";
            this.f2Type1.Size = new System.Drawing.Size(128, 24);
            this.f2Type1.TabIndex = 13;
            // 
            // f2Type3
            // 
            this.f2Type3.DropDownHeight = 95;
            this.f2Type3.FormattingEnabled = true;
            this.f2Type3.IntegralHeight = false;
            this.f2Type3.ItemHeight = 16;
            this.f2Type3.Location = new System.Drawing.Point(15, 128);
            this.f2Type3.Margin = new System.Windows.Forms.Padding(4);
            this.f2Type3.Name = "f2Type3";
            this.f2Type3.Size = new System.Drawing.Size(128, 24);
            this.f2Type3.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-1, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(299, 26);
            this.label9.TabIndex = 2;
            this.label9.Text = "Face 02";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(4, -2);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(315, 41);
            this.label6.TabIndex = 17;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // MatchAllButton
            // 
            this.MatchAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MatchAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchAllButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MatchAllButton.Location = new System.Drawing.Point(19, 638);
            this.MatchAllButton.Margin = new System.Windows.Forms.Padding(4);
            this.MatchAllButton.Name = "MatchAllButton";
            this.MatchAllButton.Size = new System.Drawing.Size(91, 35);
            this.MatchAllButton.TabIndex = 22;
            this.MatchAllButton.Text = "Match All";
            this.MatchAllButton.UseVisualStyleBackColor = false;
            this.MatchAllButton.Click += new System.EventHandler(this.MatchAllButton_Click);
            // 
            // UpdateAllButton
            // 
            this.UpdateAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UpdateAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateAllButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UpdateAllButton.Location = new System.Drawing.Point(213, 595);
            this.UpdateAllButton.Margin = new System.Windows.Forms.Padding(4);
            this.UpdateAllButton.Name = "UpdateAllButton";
            this.UpdateAllButton.Size = new System.Drawing.Size(106, 35);
            this.UpdateAllButton.TabIndex = 21;
            this.UpdateAllButton.Text = "Update All";
            this.UpdateAllButton.UseVisualStyleBackColor = false;
            this.UpdateAllButton.Click += new System.EventHandler(this.UpdateAllButton_Click);
            // 
            // wallMatchButton
            // 
            this.wallMatchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.wallMatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wallMatchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.wallMatchButton.Location = new System.Drawing.Point(200, 150);
            this.wallMatchButton.Margin = new System.Windows.Forms.Padding(4);
            this.wallMatchButton.Name = "wallMatchButton";
            this.wallMatchButton.Size = new System.Drawing.Size(117, 37);
            this.wallMatchButton.TabIndex = 5;
            this.wallMatchButton.Text = "Match";
            this.wallMatchButton.UseVisualStyleBackColor = false;
            this.wallMatchButton.Click += new System.EventHandler(this.wallMatchButton_Click);
            // 
            // wallUpdateButton
            // 
            this.wallUpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.wallUpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wallUpdateButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.wallUpdateButton.Location = new System.Drawing.Point(18, 150);
            this.wallUpdateButton.Margin = new System.Windows.Forms.Padding(4);
            this.wallUpdateButton.Name = "wallUpdateButton";
            this.wallUpdateButton.Size = new System.Drawing.Size(117, 37);
            this.wallUpdateButton.TabIndex = 4;
            this.wallUpdateButton.Text = "Update";
            this.wallUpdateButton.UseVisualStyleBackColor = false;
            this.wallUpdateButton.Click += new System.EventHandler(this.wallUpdateButton_Click);
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(18, 581);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(301, 1);
            this.label15.TabIndex = 23;
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.refreshButton.Location = new System.Drawing.Point(118, 638);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(85, 35);
            this.refreshButton.TabIndex = 23;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toggleSwitch1.Location = new System.Drawing.Point(242, 7);
            this.toggleSwitch1.Margin = new System.Windows.Forms.Padding(4);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OffText = "SI";
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnText = "IP";
            this.toggleSwitch1.Size = new System.Drawing.Size(67, 23);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 1;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch1_CheckedChanged);
            // 
            // scanButton
            // 
            this.scanButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.scanButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scanButton.Location = new System.Drawing.Point(213, 638);
            this.scanButton.Margin = new System.Windows.Forms.Padding(4);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(107, 35);
            this.scanButton.TabIndex = 26;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = false;
            this.scanButton.Visible = false;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 681);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(298, 28);
            this.progressBar1.TabIndex = 27;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(21, 717);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(301, 168);
            this.treeView1.TabIndex = 28;
            this.treeView1.Visible = false;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.ForeColor = System.Drawing.Color.Red;
            this.unitLabel.Location = new System.Drawing.Point(262, 122);
            this.unitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(57, 16);
            this.unitLabel.TabIndex = 30;
            this.unitLabel.Text = "W/sqmk";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(18, 197);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(296, 1);
            this.label16.TabIndex = 24;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // WallDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WallDataPalette";
            this.Size = new System.Drawing.Size(329, 900);
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
