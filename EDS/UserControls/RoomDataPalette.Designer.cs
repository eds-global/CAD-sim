namespace EDS.UserControls
{
    partial class RoomDataPalette
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
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.ExtMatchButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.spaceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lpdComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.epdComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.freshAirComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.occupComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ceilFinishComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.floorFinishComboBox = new System.Windows.Forms.ComboBox();
            this.lpdText1 = new System.Windows.Forms.TextBox();
            this.lpdText2 = new System.Windows.Forms.TextBox();
            this.epdText2 = new System.Windows.Forms.TextBox();
            this.epdText1 = new System.Windows.Forms.TextBox();
            this.occuText2 = new System.Windows.Forms.TextBox();
            this.occuText1 = new System.Windows.Forms.TextBox();
            this.lpdCheck = new System.Windows.Forms.CheckBox();
            this.epdCheck = new System.Windows.Forms.CheckBox();
            this.occuCheck = new System.Windows.Forms.CheckBox();
            this.freshAirCheck = new System.Windows.Forms.CheckBox();
            this.ceilCheck = new System.Windows.Forms.CheckBox();
            this.floorCheck = new System.Windows.Forms.CheckBox();
            this.MatchButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(1, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(395, 32);
            this.label6.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Room/Space/Zone Tag";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toggleSwitch1.Location = new System.Drawing.Point(336, 14);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OffText = "SI";
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnText = "IP";
            this.toggleSwitch1.Size = new System.Drawing.Size(50, 19);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 20;
            // 
            // ExtMatchButton
            // 
            this.ExtMatchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ExtMatchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExtMatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtMatchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ExtMatchButton.Location = new System.Drawing.Point(255, 12);
            this.ExtMatchButton.Name = "ExtMatchButton";
            this.ExtMatchButton.Size = new System.Drawing.Size(66, 25);
            this.ExtMatchButton.TabIndex = 24;
            this.ExtMatchButton.Text = "Next";
            this.ExtMatchButton.UseVisualStyleBackColor = false;
            this.ExtMatchButton.Click += new System.EventHandler(this.ExtMatchButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Location = new System.Drawing.Point(183, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 25);
            this.button1.TabIndex = 25;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // spaceComboBox
            // 
            this.spaceComboBox.DropDownHeight = 95;
            this.spaceComboBox.FormattingEnabled = true;
            this.spaceComboBox.IntegralHeight = false;
            this.spaceComboBox.ItemHeight = 13;
            this.spaceComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.spaceComboBox.Location = new System.Drawing.Point(97, 61);
            this.spaceComboBox.Name = "spaceComboBox";
            this.spaceComboBox.Size = new System.Drawing.Size(97, 21);
            this.spaceComboBox.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "Space Type";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "LPD";
            // 
            // lpdComboBox
            // 
            this.lpdComboBox.DropDownHeight = 95;
            this.lpdComboBox.FormattingEnabled = true;
            this.lpdComboBox.IntegralHeight = false;
            this.lpdComboBox.ItemHeight = 13;
            this.lpdComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.lpdComboBox.Location = new System.Drawing.Point(97, 103);
            this.lpdComboBox.Name = "lpdComboBox";
            this.lpdComboBox.Size = new System.Drawing.Size(97, 21);
            this.lpdComboBox.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 23);
            this.label4.TabIndex = 31;
            this.label4.Text = "EPD";
            // 
            // epdComboBox
            // 
            this.epdComboBox.DropDownHeight = 95;
            this.epdComboBox.FormattingEnabled = true;
            this.epdComboBox.IntegralHeight = false;
            this.epdComboBox.ItemHeight = 13;
            this.epdComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.epdComboBox.Location = new System.Drawing.Point(97, 130);
            this.epdComboBox.Name = "epdComboBox";
            this.epdComboBox.Size = new System.Drawing.Size(97, 21);
            this.epdComboBox.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "Fresh Air";
            // 
            // freshAirComboBox
            // 
            this.freshAirComboBox.DropDownHeight = 95;
            this.freshAirComboBox.FormattingEnabled = true;
            this.freshAirComboBox.IntegralHeight = false;
            this.freshAirComboBox.ItemHeight = 13;
            this.freshAirComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.freshAirComboBox.Location = new System.Drawing.Point(97, 202);
            this.freshAirComboBox.Name = "freshAirComboBox";
            this.freshAirComboBox.Size = new System.Drawing.Size(97, 21);
            this.freshAirComboBox.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 23);
            this.label7.TabIndex = 33;
            this.label7.Text = "Occupancy";
            // 
            // occupComboBox
            // 
            this.occupComboBox.DropDownHeight = 95;
            this.occupComboBox.FormattingEnabled = true;
            this.occupComboBox.IntegralHeight = false;
            this.occupComboBox.ItemHeight = 13;
            this.occupComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.occupComboBox.Location = new System.Drawing.Point(97, 175);
            this.occupComboBox.Name = "occupComboBox";
            this.occupComboBox.Size = new System.Drawing.Size(97, 21);
            this.occupComboBox.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 23);
            this.label8.TabIndex = 39;
            this.label8.Text = "Ceiling Finish";
            // 
            // ceilFinishComboBox
            // 
            this.ceilFinishComboBox.DropDownHeight = 95;
            this.ceilFinishComboBox.FormattingEnabled = true;
            this.ceilFinishComboBox.IntegralHeight = false;
            this.ceilFinishComboBox.ItemHeight = 13;
            this.ceilFinishComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.ceilFinishComboBox.Location = new System.Drawing.Point(97, 268);
            this.ceilFinishComboBox.Name = "ceilFinishComboBox";
            this.ceilFinishComboBox.Size = new System.Drawing.Size(97, 21);
            this.ceilFinishComboBox.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 23);
            this.label9.TabIndex = 37;
            this.label9.Text = "Floor Finish";
            // 
            // floorFinishComboBox
            // 
            this.floorFinishComboBox.DropDownHeight = 95;
            this.floorFinishComboBox.FormattingEnabled = true;
            this.floorFinishComboBox.IntegralHeight = false;
            this.floorFinishComboBox.ItemHeight = 13;
            this.floorFinishComboBox.Items.AddRange(new object[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.floorFinishComboBox.Location = new System.Drawing.Point(97, 241);
            this.floorFinishComboBox.Name = "floorFinishComboBox";
            this.floorFinishComboBox.Size = new System.Drawing.Size(97, 21);
            this.floorFinishComboBox.TabIndex = 36;
            // 
            // lpdText1
            // 
            this.lpdText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lpdText1.Location = new System.Drawing.Point(218, 106);
            this.lpdText1.Name = "lpdText1";
            this.lpdText1.Size = new System.Drawing.Size(50, 20);
            this.lpdText1.TabIndex = 40;
            // 
            // lpdText2
            // 
            this.lpdText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lpdText2.Location = new System.Drawing.Point(274, 106);
            this.lpdText2.Name = "lpdText2";
            this.lpdText2.Size = new System.Drawing.Size(50, 20);
            this.lpdText2.TabIndex = 41;
            // 
            // epdText2
            // 
            this.epdText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.epdText2.Location = new System.Drawing.Point(274, 133);
            this.epdText2.Name = "epdText2";
            this.epdText2.Size = new System.Drawing.Size(50, 20);
            this.epdText2.TabIndex = 43;
            // 
            // epdText1
            // 
            this.epdText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.epdText1.Location = new System.Drawing.Point(218, 133);
            this.epdText1.Name = "epdText1";
            this.epdText1.Size = new System.Drawing.Size(50, 20);
            this.epdText1.TabIndex = 42;
            // 
            // occuText2
            // 
            this.occuText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.occuText2.Location = new System.Drawing.Point(274, 178);
            this.occuText2.Name = "occuText2";
            this.occuText2.Size = new System.Drawing.Size(50, 20);
            this.occuText2.TabIndex = 45;
            // 
            // occuText1
            // 
            this.occuText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.occuText1.Location = new System.Drawing.Point(218, 178);
            this.occuText1.Name = "occuText1";
            this.occuText1.Size = new System.Drawing.Size(50, 20);
            this.occuText1.TabIndex = 44;
            // 
            // lpdCheck
            // 
            this.lpdCheck.AutoSize = true;
            this.lpdCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpdCheck.Location = new System.Drawing.Point(356, 110);
            this.lpdCheck.Name = "lpdCheck";
            this.lpdCheck.Size = new System.Drawing.Size(15, 14);
            this.lpdCheck.TabIndex = 46;
            this.lpdCheck.UseVisualStyleBackColor = true;
            // 
            // epdCheck
            // 
            this.epdCheck.AutoSize = true;
            this.epdCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.epdCheck.Location = new System.Drawing.Point(356, 137);
            this.epdCheck.Name = "epdCheck";
            this.epdCheck.Size = new System.Drawing.Size(15, 14);
            this.epdCheck.TabIndex = 47;
            this.epdCheck.UseVisualStyleBackColor = true;
            // 
            // occuCheck
            // 
            this.occuCheck.AutoSize = true;
            this.occuCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.occuCheck.Location = new System.Drawing.Point(356, 178);
            this.occuCheck.Name = "occuCheck";
            this.occuCheck.Size = new System.Drawing.Size(15, 14);
            this.occuCheck.TabIndex = 48;
            this.occuCheck.UseVisualStyleBackColor = true;
            // 
            // freshAirCheck
            // 
            this.freshAirCheck.AutoSize = true;
            this.freshAirCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freshAirCheck.Location = new System.Drawing.Point(356, 202);
            this.freshAirCheck.Name = "freshAirCheck";
            this.freshAirCheck.Size = new System.Drawing.Size(15, 14);
            this.freshAirCheck.TabIndex = 49;
            this.freshAirCheck.UseVisualStyleBackColor = true;
            // 
            // ceilCheck
            // 
            this.ceilCheck.AutoSize = true;
            this.ceilCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ceilCheck.Location = new System.Drawing.Point(356, 271);
            this.ceilCheck.Name = "ceilCheck";
            this.ceilCheck.Size = new System.Drawing.Size(15, 14);
            this.ceilCheck.TabIndex = 51;
            this.ceilCheck.UseVisualStyleBackColor = true;
            // 
            // floorCheck
            // 
            this.floorCheck.AutoSize = true;
            this.floorCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorCheck.Location = new System.Drawing.Point(356, 247);
            this.floorCheck.Name = "floorCheck";
            this.floorCheck.Size = new System.Drawing.Size(15, 14);
            this.floorCheck.TabIndex = 50;
            this.floorCheck.UseVisualStyleBackColor = true;
            // 
            // MatchButton
            // 
            this.MatchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MatchButton.Location = new System.Drawing.Point(294, 329);
            this.MatchButton.Name = "MatchButton";
            this.MatchButton.Size = new System.Drawing.Size(95, 34);
            this.MatchButton.TabIndex = 55;
            this.MatchButton.Text = "Match";
            this.MatchButton.UseVisualStyleBackColor = false;
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UpdateButton.Location = new System.Drawing.Point(202, 328);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(88, 34);
            this.UpdateButton.TabIndex = 54;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = false;
            // 
            // SelectButton
            // 
            this.SelectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SelectButton.Location = new System.Drawing.Point(97, 329);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(95, 34);
            this.SelectButton.TabIndex = 53;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = false;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.AddButton.Location = new System.Drawing.Point(5, 328);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(88, 34);
            this.AddButton.TabIndex = 52;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(5, 160);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(381, 2);
            this.label14.TabIndex = 56;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(5, 230);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(381, 2);
            this.label10.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(5, 312);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(381, 2);
            this.label11.TabIndex = 58;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(345, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(2, 210);
            this.label12.TabIndex = 59;
            // 
            // RoomDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.MatchButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ceilCheck);
            this.Controls.Add(this.floorCheck);
            this.Controls.Add(this.freshAirCheck);
            this.Controls.Add(this.occuCheck);
            this.Controls.Add(this.epdCheck);
            this.Controls.Add(this.lpdCheck);
            this.Controls.Add(this.occuText2);
            this.Controls.Add(this.occuText1);
            this.Controls.Add(this.epdText2);
            this.Controls.Add(this.epdText1);
            this.Controls.Add(this.lpdText2);
            this.Controls.Add(this.lpdText1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ceilFinishComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.floorFinishComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.freshAirComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.occupComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.epdComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lpdComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spaceComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ExtMatchButton);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Name = "RoomDataPalette";
            this.Size = new System.Drawing.Size(399, 378);
            this.Load += new System.EventHandler(this.RoomDataPalette_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Button ExtMatchButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox spaceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox lpdComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox epdComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox freshAirComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox occupComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ceilFinishComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox floorFinishComboBox;
        private System.Windows.Forms.TextBox lpdText1;
        private System.Windows.Forms.TextBox lpdText2;
        private System.Windows.Forms.TextBox epdText2;
        private System.Windows.Forms.TextBox epdText1;
        private System.Windows.Forms.TextBox occuText2;
        private System.Windows.Forms.TextBox occuText1;
        private System.Windows.Forms.CheckBox lpdCheck;
        private System.Windows.Forms.CheckBox epdCheck;
        private System.Windows.Forms.CheckBox occuCheck;
        private System.Windows.Forms.CheckBox freshAirCheck;
        private System.Windows.Forms.CheckBox ceilCheck;
        private System.Windows.Forms.CheckBox floorCheck;
        private System.Windows.Forms.Button MatchButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}
