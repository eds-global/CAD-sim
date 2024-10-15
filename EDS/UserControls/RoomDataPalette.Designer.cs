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
            this.matchButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.unitLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.levelPreFix = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.label21 = new System.Windows.Forms.Label();
            this.buildingType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(1, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 32);
            this.label6.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Room/Space/Zone Tag";
            // 
            // spaceComboBox
            // 
            this.spaceComboBox.DropDownHeight = 95;
            this.spaceComboBox.FormattingEnabled = true;
            this.spaceComboBox.IntegralHeight = false;
            this.spaceComboBox.ItemHeight = 13;
            this.spaceComboBox.Location = new System.Drawing.Point(93, 76);
            this.spaceComboBox.Name = "spaceComboBox";
            this.spaceComboBox.Size = new System.Drawing.Size(113, 21);
            this.spaceComboBox.TabIndex = 1;
            this.spaceComboBox.SelectedIndexChanged += new System.EventHandler(this.spaceComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "Space Type*";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "LPD*";
            // 
            // lpdComboBox
            // 
            this.lpdComboBox.DropDownHeight = 95;
            this.lpdComboBox.FormattingEnabled = true;
            this.lpdComboBox.IntegralHeight = false;
            this.lpdComboBox.ItemHeight = 13;
            this.lpdComboBox.Location = new System.Drawing.Point(93, 121);
            this.lpdComboBox.Name = "lpdComboBox";
            this.lpdComboBox.Size = new System.Drawing.Size(113, 21);
            this.lpdComboBox.TabIndex = 3;
            this.lpdComboBox.SelectedIndexChanged += new System.EventHandler(this.lpdComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 23);
            this.label4.TabIndex = 31;
            this.label4.Text = "EPD*";
            // 
            // epdComboBox
            // 
            this.epdComboBox.DropDownHeight = 95;
            this.epdComboBox.FormattingEnabled = true;
            this.epdComboBox.IntegralHeight = false;
            this.epdComboBox.ItemHeight = 13;
            this.epdComboBox.Location = new System.Drawing.Point(93, 185);
            this.epdComboBox.Name = "epdComboBox";
            this.epdComboBox.Size = new System.Drawing.Size(113, 21);
            this.epdComboBox.TabIndex = 3;
            this.epdComboBox.SelectedIndexChanged += new System.EventHandler(this.epdComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "Fresh Air*";
            // 
            // freshAirComboBox
            // 
            this.freshAirComboBox.DropDownHeight = 95;
            this.freshAirComboBox.FormattingEnabled = true;
            this.freshAirComboBox.IntegralHeight = false;
            this.freshAirComboBox.ItemHeight = 13;
            this.freshAirComboBox.Location = new System.Drawing.Point(94, 346);
            this.freshAirComboBox.Name = "freshAirComboBox";
            this.freshAirComboBox.Size = new System.Drawing.Size(111, 21);
            this.freshAirComboBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 23);
            this.label7.TabIndex = 33;
            this.label7.Text = "Occupancy*";
            // 
            // occupComboBox
            // 
            this.occupComboBox.DropDownHeight = 95;
            this.occupComboBox.FormattingEnabled = true;
            this.occupComboBox.IntegralHeight = false;
            this.occupComboBox.ItemHeight = 13;
            this.occupComboBox.Location = new System.Drawing.Point(93, 275);
            this.occupComboBox.Name = "occupComboBox";
            this.occupComboBox.Size = new System.Drawing.Size(113, 21);
            this.occupComboBox.TabIndex = 11;
            this.occupComboBox.SelectedIndexChanged += new System.EventHandler(this.occupComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 426);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 23);
            this.label8.TabIndex = 39;
            this.label8.Text = "Ceiling Finish*";
            // 
            // ceilFinishComboBox
            // 
            this.ceilFinishComboBox.DropDownHeight = 95;
            this.ceilFinishComboBox.FormattingEnabled = true;
            this.ceilFinishComboBox.IntegralHeight = false;
            this.ceilFinishComboBox.ItemHeight = 13;
            this.ceilFinishComboBox.Location = new System.Drawing.Point(94, 426);
            this.ceilFinishComboBox.Name = "ceilFinishComboBox";
            this.ceilFinishComboBox.Size = new System.Drawing.Size(111, 21);
            this.ceilFinishComboBox.TabIndex = 7;
            this.ceilFinishComboBox.SelectedIndexChanged += new System.EventHandler(this.ceilFinishComboBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 393);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 23);
            this.label9.TabIndex = 37;
            this.label9.Text = "Floor Finish*";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // floorFinishComboBox
            // 
            this.floorFinishComboBox.DropDownHeight = 95;
            this.floorFinishComboBox.FormattingEnabled = true;
            this.floorFinishComboBox.IntegralHeight = false;
            this.floorFinishComboBox.ItemHeight = 13;
            this.floorFinishComboBox.Location = new System.Drawing.Point(94, 393);
            this.floorFinishComboBox.Name = "floorFinishComboBox";
            this.floorFinishComboBox.Size = new System.Drawing.Size(111, 21);
            this.floorFinishComboBox.TabIndex = 6;
            this.floorFinishComboBox.SelectedIndexChanged += new System.EventHandler(this.floorFinishComboBox_SelectedIndexChanged);
            // 
            // lpdText1
            // 
            this.lpdText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lpdText1.Location = new System.Drawing.Point(93, 160);
            this.lpdText1.Name = "lpdText1";
            this.lpdText1.Size = new System.Drawing.Size(65, 20);
            this.lpdText1.TabIndex = 4;
            this.lpdText1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lpdText1_KeyPress);
            // 
            // lpdText2
            // 
            this.lpdText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lpdText2.Location = new System.Drawing.Point(163, 160);
            this.lpdText2.Name = "lpdText2";
            this.lpdText2.Size = new System.Drawing.Size(42, 20);
            this.lpdText2.TabIndex = 5;
            this.lpdText2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lpdText2_KeyPress);
            // 
            // epdText2
            // 
            this.epdText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.epdText2.Location = new System.Drawing.Point(163, 230);
            this.epdText2.Name = "epdText2";
            this.epdText2.Size = new System.Drawing.Size(40, 20);
            this.epdText2.TabIndex = 9;
            this.epdText2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.epdText2_KeyPress);
            // 
            // epdText1
            // 
            this.epdText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.epdText1.Location = new System.Drawing.Point(93, 230);
            this.epdText1.Name = "epdText1";
            this.epdText1.Size = new System.Drawing.Size(66, 20);
            this.epdText1.TabIndex = 8;
            this.epdText1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.epdText1_KeyPress);
            // 
            // occuText2
            // 
            this.occuText2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.occuText2.Location = new System.Drawing.Point(151, 318);
            this.occuText2.Name = "occuText2";
            this.occuText2.Size = new System.Drawing.Size(54, 20);
            this.occuText2.TabIndex = 13;
            this.occuText2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.occuText2_KeyPress);
            // 
            // occuText1
            // 
            this.occuText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.occuText1.Location = new System.Drawing.Point(93, 318);
            this.occuText1.Name = "occuText1";
            this.occuText1.Size = new System.Drawing.Size(53, 20);
            this.occuText1.TabIndex = 12;
            this.occuText1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.occuText1_KeyPress);
            // 
            // lpdCheck
            // 
            this.lpdCheck.AutoSize = true;
            this.lpdCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpdCheck.Location = new System.Drawing.Point(219, 160);
            this.lpdCheck.Name = "lpdCheck";
            this.lpdCheck.Size = new System.Drawing.Size(15, 14);
            this.lpdCheck.TabIndex = 6;
            this.lpdCheck.UseVisualStyleBackColor = true;
            // 
            // epdCheck
            // 
            this.epdCheck.AutoSize = true;
            this.epdCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.epdCheck.Location = new System.Drawing.Point(219, 230);
            this.epdCheck.Name = "epdCheck";
            this.epdCheck.Size = new System.Drawing.Size(15, 14);
            this.epdCheck.TabIndex = 10;
            this.epdCheck.UseVisualStyleBackColor = true;
            // 
            // occuCheck
            // 
            this.occuCheck.AutoSize = true;
            this.occuCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.occuCheck.Location = new System.Drawing.Point(222, 322);
            this.occuCheck.Name = "occuCheck";
            this.occuCheck.Size = new System.Drawing.Size(15, 14);
            this.occuCheck.TabIndex = 14;
            this.occuCheck.UseVisualStyleBackColor = true;
            // 
            // freshAirCheck
            // 
            this.freshAirCheck.AutoSize = true;
            this.freshAirCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freshAirCheck.Location = new System.Drawing.Point(222, 349);
            this.freshAirCheck.Name = "freshAirCheck";
            this.freshAirCheck.Size = new System.Drawing.Size(15, 14);
            this.freshAirCheck.TabIndex = 16;
            this.freshAirCheck.UseVisualStyleBackColor = true;
            // 
            // ceilCheck
            // 
            this.ceilCheck.AutoSize = true;
            this.ceilCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ceilCheck.Location = new System.Drawing.Point(222, 429);
            this.ceilCheck.Name = "ceilCheck";
            this.ceilCheck.Size = new System.Drawing.Size(15, 14);
            this.ceilCheck.TabIndex = 20;
            this.ceilCheck.UseVisualStyleBackColor = true;
            // 
            // floorCheck
            // 
            this.floorCheck.AutoSize = true;
            this.floorCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floorCheck.Location = new System.Drawing.Point(222, 395);
            this.floorCheck.Name = "floorCheck";
            this.floorCheck.Size = new System.Drawing.Size(15, 14);
            this.floorCheck.TabIndex = 18;
            this.floorCheck.UseVisualStyleBackColor = true;
            // 
            // matchButton
            // 
            this.matchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.matchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.matchButton.Location = new System.Drawing.Point(41, 515);
            this.matchButton.Name = "matchButton";
            this.matchButton.Size = new System.Drawing.Size(60, 32);
            this.matchButton.TabIndex = 24;
            this.matchButton.Text = "Match";
            this.matchButton.UseVisualStyleBackColor = false;
            this.matchButton.Click += new System.EventHandler(this.matchButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.updateButton.Location = new System.Drawing.Point(144, 476);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(60, 32);
            this.updateButton.TabIndex = 23;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.selectButton.Location = new System.Drawing.Point(78, 476);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(60, 32);
            this.selectButton.TabIndex = 22;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = false;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.addButton.Location = new System.Drawing.Point(5, 476);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(60, 32);
            this.addButton.TabIndex = 21;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(5, 259);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(207, 2);
            this.label14.TabIndex = 56;
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(7, 377);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(206, 2);
            this.label10.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(4, 462);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(210, 2);
            this.label11.TabIndex = 58;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(210, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(2, 434);
            this.label12.TabIndex = 59;
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.ForeColor = System.Drawing.Color.Red;
            this.unitLabel.Location = new System.Drawing.Point(161, 100);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(48, 13);
            this.unitLabel.TabIndex = 60;
            this.unitLabel.Text = "W/sqmk";
            this.unitLabel.Click += new System.EventHandler(this.unitLabel_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(90, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 15);
            this.label13.TabIndex = 61;
            this.label13.Text = "(W/sqm)";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(90, 298);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 14);
            this.label15.TabIndex = 62;
            this.label15.Text = "(sqm/person)";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(149, 298);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 14);
            this.label16.TabIndex = 63;
            this.label16.Text = "(No of person)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(176, 143);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 15);
            this.label17.TabIndex = 64;
            this.label17.Text = "(W)";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(4, 98);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 23);
            this.label18.TabIndex = 66;
            this.label18.Text = "Level*";
            // 
            // levelPreFix
            // 
            this.levelPreFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.levelPreFix.Location = new System.Drawing.Point(93, 99);
            this.levelPreFix.Name = "levelPreFix";
            this.levelPreFix.Size = new System.Drawing.Size(66, 20);
            this.levelPreFix.TabIndex = 2;
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.refreshButton.Location = new System.Drawing.Point(115, 515);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(60, 32);
            this.refreshButton.TabIndex = 25;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(90, 212);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 15);
            this.label19.TabIndex = 67;
            this.label19.Text = "(W/sqm)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(176, 212);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 15);
            this.label20.TabIndex = 68;
            this.label20.Text = "(W)";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toggleSwitch1.Location = new System.Drawing.Point(158, 10);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OffText = "SI";
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnText = "IP";
            this.toggleSwitch1.Size = new System.Drawing.Size(50, 19);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 20;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch1_CheckedChanged);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(0, 50);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 23);
            this.label21.TabIndex = 69;
            this.label21.Text = "Building Type*";
            // 
            // buildingType
            // 
            this.buildingType.DropDownHeight = 95;
            this.buildingType.FormattingEnabled = true;
            this.buildingType.IntegralHeight = false;
            this.buildingType.ItemHeight = 13;
            this.buildingType.Location = new System.Drawing.Point(93, 49);
            this.buildingType.Name = "buildingType";
            this.buildingType.Size = new System.Drawing.Size(113, 21);
            this.buildingType.TabIndex = 70;
            this.buildingType.SelectedIndexChanged += new System.EventHandler(this.buildingType_SelectedIndexChanged);
            // 
            // RoomDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.buildingType);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.levelPreFix);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.matchButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.addButton);
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
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Name = "RoomDataPalette";
            this.Size = new System.Drawing.Size(240, 550);
            this.Load += new System.EventHandler(this.RoomDataPalette_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private JCS.ToggleSwitch toggleSwitch1;
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
        private System.Windows.Forms.Button matchButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox lpdCheck;
        private System.Windows.Forms.CheckBox epdCheck;
        private System.Windows.Forms.CheckBox occuCheck;
        private System.Windows.Forms.CheckBox freshAirCheck;
        private System.Windows.Forms.CheckBox ceilCheck;
        private System.Windows.Forms.CheckBox floorCheck;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox levelPreFix;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox buildingType;
    }
}
