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

        }

        private void WallDataPalette_Load(object sender, EventArgs e)
        {
            LoadListValues();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {

        }

        private void UpdateAllButton_Click(object sender, EventArgs e)
        {

        }

        private void SelectButton_Click(object sender, EventArgs e)
        {

        }

        private void MatchAllButton_Click(object sender, EventArgs e)
        {

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
    }
}
