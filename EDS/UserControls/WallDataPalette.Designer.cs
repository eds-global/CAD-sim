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
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.uValue = new System.Windows.Forms.TextBox();
            this.uValueCheck = new System.Windows.Forms.CheckBox();
            this.extWallCombo = new System.Windows.Forms.ComboBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.MatchButton = new System.Windows.Forms.Button();
            this.uChange = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draw Wall";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(348, 6);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OffText = "SP";
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnText = "IP";
            this.toggleSwitch1.Size = new System.Drawing.Size(50, 19);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 1;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(312, 6);
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
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ext Wall Const.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(252, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "U-Value";
            // 
            // uValue
            // 
            this.uValue.Location = new System.Drawing.Point(314, 37);
            this.uValue.Name = "uValue";
            this.uValue.Size = new System.Drawing.Size(50, 20);
            this.uValue.TabIndex = 5;
            // 
            // uValueCheck
            // 
            this.uValueCheck.AutoSize = true;
            this.uValueCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uValueCheck.Location = new System.Drawing.Point(231, 40);
            this.uValueCheck.Name = "uValueCheck";
            this.uValueCheck.Size = new System.Drawing.Size(15, 14);
            this.uValueCheck.TabIndex = 6;
            this.uValueCheck.UseVisualStyleBackColor = true;
            // 
            // extWallCombo
            // 
            this.extWallCombo.DropDownHeight = 95;
            this.extWallCombo.FormattingEnabled = true;
            this.extWallCombo.IntegralHeight = false;
            this.extWallCombo.ItemHeight = 13;
            this.extWallCombo.Items.AddRange(new string[] {
            "Wood Wall",
            "Metal Wall",
            "Brick Wall",
            "Concrete Wall"});
            this.extWallCombo.Location = new System.Drawing.Point(114, 37);
            this.extWallCombo.Name = "extWallCombo";
            this.extWallCombo.Size = new System.Drawing.Size(95, 21);
            this.extWallCombo.TabIndex = 7;
            // 
            // updateButton
            // 
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.Location = new System.Drawing.Point(114, 64);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(88, 34);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // MatchButton
            // 
            this.MatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatchButton.Location = new System.Drawing.Point(255, 63);
            this.MatchButton.Name = "MatchButton";
            this.MatchButton.Size = new System.Drawing.Size(95, 34);
            this.MatchButton.TabIndex = 9;
            this.MatchButton.Text = "Match";
            this.MatchButton.UseVisualStyleBackColor = true;
            // 
            // uChange
            // 
            this.uChange.AutoSize = true;
            this.uChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uChange.ForeColor = System.Drawing.Color.Red;
            this.uChange.Location = new System.Drawing.Point(369, 39);
            this.uChange.Name = "uChange";
            this.uChange.Size = new System.Drawing.Size(49, 16);
            this.uChange.TabIndex = 10;
            this.uChange.Text = "(m -> ft)";
            // 
            // WallDataPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uChange);
            this.Controls.Add(this.MatchButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.extWallCombo);
            this.Controls.Add(this.uValueCheck);
            this.Controls.Add(this.uValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.label1);
            this.Name = "WallDataPalette";
            this.Size = new System.Drawing.Size(431, 336);
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
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button MatchButton;
        private System.Windows.Forms.Label uChange;
    }
}
