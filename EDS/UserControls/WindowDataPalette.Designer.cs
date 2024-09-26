namespace EDS.UserControls
{
    partial class WindowDataPalette
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
            this.unitLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.insertComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.windowComboBox = new System.Windows.Forms.ComboBox();
            this.openAble = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.overhangPF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.verticalPF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dayLightWindow = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.interiorLightSelf = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.sillHeight = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.spacing = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.wwr = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.matchButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.specifyOnDrawing = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.ForeColor = System.Drawing.Color.Red;
            this.unitLabel.Location = new System.Drawing.Point(174, 222);
            this.unitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(57, 16);
            this.unitLabel.TabIndex = 64;
            this.unitLabel.Text = "W/sqmk";
            this.unitLabel.Click += new System.EventHandler(this.unitLabel_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 28);
            this.label1.TabIndex = 62;
            this.label1.Text = "Place Window";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(-4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(287, 39);
            this.label6.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-1, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 28);
            this.label2.TabIndex = 66;
            this.label2.Text = "Insertion Mode*";
            // 
            // insertComboBox
            // 
            this.insertComboBox.DropDownHeight = 95;
            this.insertComboBox.FormattingEnabled = true;
            this.insertComboBox.IntegralHeight = false;
            this.insertComboBox.ItemHeight = 16;
            this.insertComboBox.Location = new System.Drawing.Point(143, 55);
            this.insertComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.insertComboBox.Name = "insertComboBox";
            this.insertComboBox.Size = new System.Drawing.Size(128, 24);
            this.insertComboBox.TabIndex = 0;
            this.insertComboBox.SelectedIndexChanged += new System.EventHandler(this.insertComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 28);
            this.label3.TabIndex = 68;
            this.label3.Text = "Window Type*";
            // 
            // windowComboBox
            // 
            this.windowComboBox.DropDownHeight = 95;
            this.windowComboBox.FormattingEnabled = true;
            this.windowComboBox.IntegralHeight = false;
            this.windowComboBox.ItemHeight = 16;
            this.windowComboBox.Location = new System.Drawing.Point(143, 89);
            this.windowComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.windowComboBox.Name = "windowComboBox";
            this.windowComboBox.Size = new System.Drawing.Size(128, 24);
            this.windowComboBox.TabIndex = 1;
            this.windowComboBox.SelectedIndexChanged += new System.EventHandler(this.windowComboBox_SelectedIndexChanged);
            // 
            // openAble
            // 
            this.openAble.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.openAble.Location = new System.Drawing.Point(143, 122);
            this.openAble.Margin = new System.Windows.Forms.Padding(4);
            this.openAble.Name = "openAble";
            this.openAble.Size = new System.Drawing.Size(129, 22);
            this.openAble.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(4, 122);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 28);
            this.label18.TabIndex = 69;
            this.label18.Text = "% Openable";
            // 
            // overhangPF
            // 
            this.overhangPF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.overhangPF.Location = new System.Drawing.Point(143, 154);
            this.overhangPF.Margin = new System.Windows.Forms.Padding(4);
            this.overhangPF.Name = "overhangPF";
            this.overhangPF.Size = new System.Drawing.Size(129, 22);
            this.overhangPF.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 154);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 28);
            this.label4.TabIndex = 71;
            this.label4.Text = "Overhang PF";
            // 
            // verticalPF
            // 
            this.verticalPF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.verticalPF.Location = new System.Drawing.Point(143, 186);
            this.verticalPF.Margin = new System.Windows.Forms.Padding(4);
            this.verticalPF.Name = "verticalPF";
            this.verticalPF.Size = new System.Drawing.Size(129, 22);
            this.verticalPF.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 28);
            this.label5.TabIndex = 73;
            this.label5.Text = "Vertical PF";
            // 
            // dayLightWindow
            // 
            this.dayLightWindow.AutoSize = true;
            this.dayLightWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayLightWindow.Location = new System.Drawing.Point(12, 241);
            this.dayLightWindow.Margin = new System.Windows.Forms.Padding(4);
            this.dayLightWindow.Name = "dayLightWindow";
            this.dayLightWindow.Size = new System.Drawing.Size(18, 17);
            this.dayLightWindow.TabIndex = 5;
            this.dayLightWindow.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(145, 238);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 20);
            this.label7.TabIndex = 75;
            this.label7.Text = "Daylight Window";
            // 
            // interiorLightSelf
            // 
            this.interiorLightSelf.AutoSize = true;
            this.interiorLightSelf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interiorLightSelf.Location = new System.Drawing.Point(12, 272);
            this.interiorLightSelf.Margin = new System.Windows.Forms.Padding(4);
            this.interiorLightSelf.Name = "interiorLightSelf";
            this.interiorLightSelf.Size = new System.Drawing.Size(18, 17);
            this.interiorLightSelf.TabIndex = 6;
            this.interiorLightSelf.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(145, 269);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 20);
            this.label8.TabIndex = 77;
            this.label8.Text = "Interior LightSelf";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(4, 293);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(275, 2);
            this.label10.TabIndex = 79;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // height
            // 
            this.height.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.height.Location = new System.Drawing.Point(142, 326);
            this.height.Margin = new System.Windows.Forms.Padding(4);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(129, 22);
            this.height.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 326);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 28);
            this.label9.TabIndex = 80;
            this.label9.Text = "Height";
            // 
            // width
            // 
            this.width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.width.Location = new System.Drawing.Point(142, 356);
            this.width.Margin = new System.Windows.Forms.Padding(4);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(129, 22);
            this.width.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 354);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 28);
            this.label11.TabIndex = 82;
            this.label11.Text = "Width";
            // 
            // sillHeight
            // 
            this.sillHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sillHeight.Location = new System.Drawing.Point(143, 426);
            this.sillHeight.Margin = new System.Windows.Forms.Padding(4);
            this.sillHeight.Name = "sillHeight";
            this.sillHeight.Size = new System.Drawing.Size(129, 22);
            this.sillHeight.TabIndex = 9;
            this.sillHeight.TextChanged += new System.EventHandler(this.sillHeight_TextChanged);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 426);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 28);
            this.label12.TabIndex = 84;
            this.label12.Text = "Sill Height";
            // 
            // spacing
            // 
            this.spacing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spacing.Location = new System.Drawing.Point(142, 456);
            this.spacing.Margin = new System.Windows.Forms.Padding(4);
            this.spacing.Name = "spacing";
            this.spacing.Size = new System.Drawing.Size(129, 22);
            this.spacing.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 456);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 28);
            this.label13.TabIndex = 86;
            this.label13.Text = "Spacing";
            // 
            // wwr
            // 
            this.wwr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wwr.Location = new System.Drawing.Point(143, 486);
            this.wwr.Margin = new System.Windows.Forms.Padding(4);
            this.wwr.Name = "wwr";
            this.wwr.Size = new System.Drawing.Size(129, 22);
            this.wwr.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 486);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 28);
            this.label14.TabIndex = 88;
            this.label14.Text = "WWR";
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.refreshButton.Location = new System.Drawing.Point(147, 579);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(91, 42);
            this.refreshButton.TabIndex = 16;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(1, 523);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(275, 2);
            this.label15.TabIndex = 94;
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // matchButton
            // 
            this.matchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.matchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.matchButton.Location = new System.Drawing.Point(36, 579);
            this.matchButton.Margin = new System.Windows.Forms.Padding(4);
            this.matchButton.Name = "matchButton";
            this.matchButton.Size = new System.Drawing.Size(103, 42);
            this.matchButton.TabIndex = 15;
            this.matchButton.Text = "Match All";
            this.matchButton.UseVisualStyleBackColor = false;
            this.matchButton.Click += new System.EventHandler(this.matchButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.updateButton.Location = new System.Drawing.Point(178, 529);
            this.updateButton.Margin = new System.Windows.Forms.Padding(4);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(94, 42);
            this.updateButton.TabIndex = 14;
            this.updateButton.Text = "Update All";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.selectButton.Location = new System.Drawing.Point(92, 529);
            this.selectButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(81, 42);
            this.selectButton.TabIndex = 13;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = false;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.drawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.drawButton.Location = new System.Drawing.Point(8, 529);
            this.drawButton.Margin = new System.Windows.Forms.Padding(4);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(76, 42);
            this.drawButton.TabIndex = 12;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = false;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // specifyOnDrawing
            // 
            this.specifyOnDrawing.AutoSize = true;
            this.specifyOnDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.specifyOnDrawing.Location = new System.Drawing.Point(8, 395);
            this.specifyOnDrawing.Margin = new System.Windows.Forms.Padding(4);
            this.specifyOnDrawing.Name = "specifyOnDrawing";
            this.specifyOnDrawing.Size = new System.Drawing.Size(18, 17);
            this.specifyOnDrawing.TabIndex = 97;
            this.specifyOnDrawing.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(138, 392);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(145, 20);
            this.label16.TabIndex = 96;
            this.label16.Text = "Specify on Drawing";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(174, 306);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 16);
            this.label17.TabIndex = 98;
            this.label17.Text = "mm or ft";
            // 
            // WindowDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label17);
            this.Controls.Add(this.specifyOnDrawing);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.matchButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.wwr);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.spacing);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.sillHeight);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.width);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.height);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.interiorLightSelf);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dayLightWindow);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.verticalPF);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.overhangPF);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.openAble);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.windowComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.insertComboBox);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WindowDataPalette";
            this.Size = new System.Drawing.Size(286, 637);
            this.Load += new System.EventHandler(this.WindowDataPalette_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label unitLabel;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox insertComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox windowComboBox;
        private System.Windows.Forms.TextBox openAble;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox overhangPF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox verticalPF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox dayLightWindow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox interiorLightSelf;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox height;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox sillHeight;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox spacing;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox wwr;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button matchButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.CheckBox specifyOnDrawing;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}
