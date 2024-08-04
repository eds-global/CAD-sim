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
    public partial class RoomDataPalette : UserControl
    {
        List<string> lpdValues = new List<string>();

        List<string> epdValues = new List<string>();

        List<string> occupValues = new List<string>();

        List<string> airValues = new List<string>();

        List<string> ceilValues = new List<string>();

        List<string> floorValues = new List<string>();

        public RoomDataPalette()
        {
            InitializeComponent();

            lpdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "LPD");
            epdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "EPD");
            occupValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Occupancy");
            airValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "FreshAir");
            ceilValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Ceiling Finish");
            floorValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Floor Finish");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RoomDataPalette_Load(object sender, EventArgs e)
        {
            LoadListValues();
        }

        private void LoadListValues()
        {
            foreach (var value in lpdValues)
            {
                lpdComboBox.Items.Add(value);
            }

            foreach (var value in epdValues)
            {
                epdComboBox.Items.Add(value);
            }

            foreach (var value in occupValues)
            {
                occupComboBox.Items.Add(value);
            }

            foreach (var value in ceilValues)
            {
                ceilFinishComboBox.Items.Add(value);
            }

            foreach (var value in floorValues)
            {
                floorFinishComboBox.Items.Add(value);
            }

            foreach (var value in airValues)
            {
                freshAirComboBox.Items.Add(value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ExtMatchButton_Click(object sender, EventArgs e)
        {

        }
    }
}
