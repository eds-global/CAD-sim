﻿using EDS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDS.UserControls
{
    public partial class WindowDataPalette : UserControl
    {
        public WindowDataPalette()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            if (insertComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the insertion mode");
                return;
            }
            if (windowComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the window type");
                return;
            }
            else
            {
                if (windowComboBox.SelectedItem.ToString() == "Openable")
                {
                    if (string.IsNullOrEmpty(openAble.Text.Trim()))
                    {
                        MessageBox.Show("Please provide the openable percentage");
                        return;
                    }
                }
            }


            EDSWindow window = new EDSWindow()
            {
                InsertionMode = insertComboBox.SelectedItem == null ? "" : insertComboBox.SelectedItem.ToString(),
                WindowType = windowComboBox.SelectedItem == null ? "" : windowComboBox.SelectedItem.ToString(),
                OpenAble = openAble.Text.Trim(),
                OverhangPF = overhangPF.Text.Trim(),
                VerticalPF = verticalPF.Text.Trim(),
                Height = height.Text.Trim(),
                Width = width.Text.Trim(),
                SillHeight = sillHeight.Text.Trim(),
                Spacing = spacing.Text.Trim(),
                WWR = wwr.Text.Trim(),
                DayLightWindow = dayLightWindow.Checked.ToString(),
                InteriorLightSelf = interiorLightSelf.Checked.ToString(),
                SpecifyOnDrawing = specifyOnDrawing.Checked.ToString()
            };

            window.CreateWindow(window);

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            EDSWindow window = new EDSWindow();
            EDSWindow result = window.SelectWindow();
            if (result != null)
            {
                insertComboBox.SelectedItem = result.InsertionMode;
                windowComboBox.SelectedItem = result.WindowType;
                openAble.Text = result.OpenAble;
                verticalPF.Text = result.VerticalPF;
                overhangPF.Text = result.OverhangPF;
                height.Text = result.Height;
                width.Text = result.Width;
                spacing.Text = result.Spacing;
                sillHeight.Text = result.SillHeight;
                wwr.Text = result.WWR;
                dayLightWindow.Checked = bool.Parse(result.DayLightWindow.ToString());
                interiorLightSelf.Checked = bool.Parse(result.InteriorLightSelf.ToString());
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (insertComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the insertion mode");
                return;
            }
            if (windowComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the window type");
                return;
            }
            else
            {
                if (windowComboBox.SelectedItem.ToString() == "Openable")
                {
                    if (string.IsNullOrEmpty(openAble.Text.Trim()))
                    {
                        MessageBox.Show("Please provide the openable percentage");
                        return;
                    }
                }
            }
            EDSWindow window = new EDSWindow()
            {
                InsertionMode = insertComboBox.SelectedItem == null ? "" : insertComboBox.SelectedItem.ToString(),
                WindowType = windowComboBox.SelectedItem == null ? "" : windowComboBox.SelectedItem.ToString(),
                OpenAble = openAble.Text.Trim(),
                OverhangPF = overhangPF.Text.Trim(),
                VerticalPF = verticalPF.Text.Trim(),
                Height = height.Text.Trim(),
                Width = width.Text.Trim(),
                SillHeight = sillHeight.Text.Trim(),
                Spacing = spacing.Text.Trim(),
                WWR = wwr.Text.Trim(),
                DayLightWindow = dayLightWindow.Checked.ToString(),
                InteriorLightSelf = interiorLightSelf.Checked.ToString(),
                SpecifyOnDrawing = specifyOnDrawing.Checked.ToString()
            };
            window.UpdateWindow(window);
        }

        private void matchButton_Click(object sender, EventArgs e)
        {
            EDSWindow window = new EDSWindow();
            window.MatchWindow();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void WindowDataPalette_Load(object sender, EventArgs e)
        {
            List<string> insertItems = new List<string>() { "Single Window", "Repeating Window", "Window To Wall Ratio" };
            insertComboBox.Items.AddRange(insertItems.ToArray());

            List<string> windowItems = new List<string>() { "Fixed", "Openable" };
            windowComboBox.Items.AddRange(windowItems.ToArray());

        }

        private void insertComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (insertComboBox.SelectedItem != null)
            {
                if (insertComboBox.SelectedItem.Equals("Single Window"))
                {
                    height.Enabled = true;
                    width.Enabled = true;
                    sillHeight.Enabled = true;
                    spacing.Enabled = false;
                    wwr.Enabled = false;
                }
                if (insertComboBox.SelectedItem.Equals("Repeating Window"))
                {
                    height.Enabled = true;
                    width.Enabled = true;
                    sillHeight.Enabled = true;
                    spacing.Enabled = true;
                    wwr.Enabled = false;
                }
                if (insertComboBox.SelectedItem.Equals("Window To Wall Ratio"))
                {
                    height.Enabled = false;
                    width.Enabled = false;
                    sillHeight.Enabled = true;
                    spacing.Enabled = false;
                    wwr.Enabled = true;
                }
            }
        }

        private void windowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (windowComboBox.SelectedItem != null)
            {
                if (windowComboBox.SelectedItem.Equals("Fixed"))
                {
                    openAble.Enabled = false;
                }
                else
                {
                    openAble.Enabled = true;
                }
            }
        }

        private void RefreshUI()
        {
            insertComboBox.SelectedItem = null;
            windowComboBox.SelectedItem = null;
            openAble.Text = "";
            overhangPF.Text = "";
            verticalPF.Text = "";
            height.Text = "";
            width.Text = "";
            sillHeight.Text = "";
            spacing.Text = "";
            wwr.Text = "";
            dayLightWindow.Checked = false;
            interiorLightSelf.Checked = false;
            specifyOnDrawing.Checked = false;
            height.Enabled = true;
            width.Enabled = true;
            sillHeight.Enabled = true;
            spacing.Enabled = true;
            wwr.Enabled = true;
        }

        private void toggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (!toggleSwitch1.Checked)
            {
                unitLabel.Text = "W/sqmk";
            }
            else
            {
                unitLabel.Text = "Btu/(h.ft2.F)";
            }
        }
    }
}