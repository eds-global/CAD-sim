using EDS.AEC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDS.Models;

namespace EDS.UserControls
{
    public partial class WallDataPalette : UserControl
    {
        List<string> externalWalls = new List<string>();

        List<string> internalWalls = new List<string>();

        List<string> finishTypes = new List<string>();
        public WallDataPalette()
        {
            InitializeComponent();

            externalWalls = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Ext Construction");
            internalWalls = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Internal Construction");
            finishTypes = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Finishes");
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

        private void WallDataPalette_Load(object sender, EventArgs e)
        {
            LoadListValues();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            double resultValue = 0.0;
            if (uValueCheck.Checked)
            {
                resultValue = double.Parse(uValue.Text);
            }
            else
            {
                //resultValue = double.Parse(uValue.Text);
            }

            EDSWall wall = new EDSWall()
            {
                extWallType = extWallCombo.SelectedItem == null ? "" : extWallCombo.SelectedItem.ToString(),
                intWallType = intWallCombo.SelectedItem == null ? "" : intWallCombo.SelectedItem.ToString(),
                uValue = resultValue.ToString(),
                eDS1Faces1 = f1Type1.SelectedItem == null ? "" : f1Type1.SelectedItem.ToString() + "-" + f1Type1CompText.Text,
                eDS1Faces2 = f1Type2.SelectedItem == null ? "" : f1Type2.SelectedItem.ToString() + "-" + f1Type2CompText.Text,
                eDS1Faces3 = f1Type3.SelectedItem == null ? "" : f1Type3.SelectedItem.ToString() + "-" + f1Type3CompText.Text,
                eDS2Faces1 = f2Type1.SelectedItem == null ? "" : f2Type1.SelectedItem.ToString() + "-" + f2Type1CompText.Text,
                eDS2Faces2 = f2Type2.SelectedItem == null ? "" : f2Type2.SelectedItem.ToString() + "-" + f2Type2CompText.Text,
                eDS2Faces3 = f2Type3.SelectedItem == null ? "" : f2Type3.SelectedItem.ToString() + "-" + f2Type3CompText.Text,
                uValueCheck = uValueCheck.Checked.ToString(),
            };
            wall.CreateWall(wall);

            RefreshUI();

        }

        private void UpdateAllButton_Click(object sender, EventArgs e)
        {
            EDSWall wall = new EDSWall()
            {
                extWallType = extWallCombo.SelectedItem == null ? "" : extWallCombo.SelectedItem.ToString(),
                intWallType = intWallCombo.SelectedItem == null ? "" : intWallCombo.SelectedItem.ToString(),
                uValue = uValue.Text,
                eDS1Faces1 = f1Type1.SelectedItem == null ? "" : f1Type1.SelectedItem.ToString() + "-" + f1Type1CompText.Text,
                eDS1Faces2 = f1Type2.SelectedItem == null ? "" : f1Type2.SelectedItem.ToString() + "-" + f1Type2CompText.Text,
                eDS1Faces3 = f1Type3.SelectedItem == null ? "" : f1Type3.SelectedItem.ToString() + "-" + f1Type3CompText.Text,
                eDS2Faces1 = f2Type1.SelectedItem == null ? "" : f2Type1.SelectedItem.ToString() + "-" + f2Type1CompText.Text,
                eDS2Faces2 = f2Type2.SelectedItem == null ? "" : f2Type2.SelectedItem.ToString() + "-" + f2Type2CompText.Text,
                eDS2Faces3 = f2Type3.SelectedItem == null ? "" : f2Type3.SelectedItem.ToString() + "-" + f2Type3CompText.Text,
                uValueCheck = uValueCheck.Checked.ToString(),
            };

            wall.UpdateLine(wall);

            RefreshUI();

        }

        private void RefreshUI()
        {
            extWallCombo.SelectedItem = null;
            intWallCombo.SelectedItem = null;
            uValue.Text = "";
            f1Type1.SelectedItem = f1Type2.SelectedItem = f1Type3.SelectedItem = null;
            f2Type1.SelectedItem = f2Type2.SelectedItem = f2Type3.SelectedItem = null;
            uValueCheck.Checked = false;
            progressBar1.Value = 0;
            treeView1.Nodes.Clear();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            EDSWall creation = new EDSWall();
            var edsValue = creation.GetWallLine();

            extWallCombo.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.extWallType).ToList()) ? edsValue.First().extWallType : "";
            intWallCombo.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.intWallType).ToList()) ? edsValue.First().intWallType : "";
            uValue.Text = CheckIfValueSame(edsValue.Select(x => x.uValue).ToList()) ? edsValue.First().uValue : "";
            f1Type1.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS1Faces1).ToList()) ? edsValue.First().eDS1Faces1 : "";
            f1Type2.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS1Faces2).ToList()) ? edsValue.First().eDS1Faces2 : "";
            f1Type3.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS1Faces3).ToList()) ? edsValue.First().eDS1Faces3 : "";
            f2Type1.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS2Faces1).ToList()) ? edsValue.First().eDS2Faces1 : "";
            f2Type2.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS2Faces2).ToList()) ? edsValue.First().eDS2Faces2 : "";
            f2Type3.SelectedItem = CheckIfValueSame(edsValue.Select(x => x.eDS2Faces3).ToList()) ? edsValue.First().eDS2Faces3 : "";
            uValueCheck.Checked = CheckIfValueSame(edsValue.Select(x => x.uValueCheck).ToList()) ? bool.Parse(edsValue.First().uValueCheck) == true ? true : false : false;
        }

        private bool CheckIfValueSame(List<string> values)
        {
            return values.Distinct().Count() == 1 ? true : false;
        }

        private void MatchAllButton_Click(object sender, EventArgs e)
        {
            EDSWall wallCreation = new EDSWall();
            wallCreation.MatchLine();
        }

        private void LoadListValues()
        {
            foreach (var extWall in externalWalls)
            {
                extWallCombo.Items.Add(extWall);
            }

            foreach (var intWall in internalWalls)
            {
                intWallCombo.Items.Add(intWall);
            }

            foreach (var fin in finishTypes)
            {
                f1Type1.Items.Add(fin);
                f1Type2.Items.Add(fin);
                f1Type3.Items.Add(fin);

                f2Type1.Items.Add(fin);
                f2Type2.Items.Add(fin);
                f2Type3.Items.Add(fin);
            }
        }

        private void extWallCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void intWallCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uValueCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (uValueCheck.Checked)
            {
                uValue.Enabled = true;
                extWallCombo.Enabled = false;
            }
            else
            {
                uValue.Enabled = false;
                extWallCombo.Enabled = true;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 25;
            EDSWall creation = new EDSWall();
            creation.FindClosedLoop();
            progressBar1.Value = 100;
        }

        static void UpdateProgressBar()
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string handle = e.Node.Tag.ToString();

                Zoom.ZoomManager.Zoom2Handle(handle);
            }
            catch { }
        }

        private void wallUpdateButton_Click(object sender, EventArgs e)
        {
            EDSWall wallCreation = new EDSWall();

            EDSWall wall = new EDSWall()
            {
                extWallType = extWallCombo.SelectedItem == null ? "" : extWallCombo.SelectedItem.ToString(),
                intWallType = intWallCombo.SelectedItem == null ? "" : intWallCombo.SelectedItem.ToString(),
                uValue = uValue.Text,
                eDS1Faces1 = f1Type1.SelectedItem == null ? "" : f1Type1.SelectedItem.ToString() + "-" + f1Type1CompText.Text,
                eDS1Faces2 = f1Type2.SelectedItem == null ? "" : f1Type2.SelectedItem.ToString() + "-" + f1Type2CompText.Text,
                eDS1Faces3 = f1Type3.SelectedItem == null ? "" : f1Type3.SelectedItem.ToString() + "-" + f1Type3CompText.Text,
                eDS2Faces1 = f2Type1.SelectedItem == null ? "" : f2Type1.SelectedItem.ToString() + "-" + f2Type1CompText.Text,
                eDS2Faces2 = f2Type2.SelectedItem == null ? "" : f2Type2.SelectedItem.ToString() + "-" + f2Type2CompText.Text,
                eDS2Faces3 = f2Type3.SelectedItem == null ? "" : f2Type3.SelectedItem.ToString() + "-" + f2Type3CompText.Text
            };

            wallCreation.UpdateLine(wall);

            RefreshUI();
        }

        private void wallMatchButton_Click(object sender, EventArgs e)
        {
            EDSWall wallCreation = new EDSWall();
            wallCreation.MatchLine();
        }
    }
}
