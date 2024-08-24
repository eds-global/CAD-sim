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
    public partial class RoomDataPalette : UserControl
    {
        const double SquareFootToSquareMeter = 10.7639;
        const double SquareMeterToSquareFoot = 1.0 / SquareFootToSquareMeter;

        List<string> lpdValues = new List<string>();

        List<string> epdValues = new List<string>();

        List<string> occupValues = new List<string>();

        List<string> airValues = new List<string>();

        List<string> ceilValues = new List<string>();

        List<string> floorValues = new List<string>();

        List<string> spaceTypes = new List<string>();

        public RoomDataPalette()
        {
            InitializeComponent();

            lpdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "LPD");
            epdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "EPD");
            occupValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Occupancy");
            airValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "FreshAir");
            ceilValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Ceiling Finish");
            floorValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Floor Finish");
            spaceTypes = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Space Type");
            //allLayers = roomTag.GetAllLayers();

            lpdText1.Enabled = false;
            lpdText2.Enabled = false;

            epdText1.Enabled = false;
            epdText2.Enabled = false;

            occuText1.Enabled = false;
            occuText2.Enabled = false;
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

            foreach (var value in spaceTypes)
            {
                spaceComboBox.Items.Add(value);
            }

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

        private void matchButton_Click(object sender, EventArgs e)
        {
            EDSRoomTag wallCreation = new EDSRoomTag();
            wallCreation.MatchRoom(lpdCheck.Checked, epdCheck.Checked, occuCheck.Checked, freshAirCheck.Checked, floorCheck.Checked, ceilCheck.Checked);
            RefreshUI();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (spaceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the space type");
                return;
            }

            if (lpdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the LPD type");
                return;
            }

            if (epdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the EPD type");
                return;
            }

            if (occupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Occupany type");
                return;
            }

            if (freshAirComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Fresh Air type");
                return;
            }

            if (ceilFinishComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Ceiling Finish type");
                return;
            }

            if (floorFinishComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Floor Finish type");
                return;
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(epdText1.Text.ToString()) && string.IsNullOrWhiteSpace(epdText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the EPD");
                    return;
                }
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(epdText1.Text.ToString()) || string.IsNullOrWhiteSpace(epdText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the EPD");
                    return;
                }
            }

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(lpdText1.Text.ToString()) || string.IsNullOrWhiteSpace(lpdText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the LPD");
                    return;
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(occuText1.Text.ToString()) || string.IsNullOrWhiteSpace(occuText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the Occupancy");
                    return;
                }
            }

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(lpdText1.Text.Trim()))
                {
                    if (double.TryParse(lpdText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("LPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(lpdText2.Text.Trim()))
                {
                    if (double.TryParse(lpdText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("LPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(epdText1.Text.Trim()))
                {
                    if (double.TryParse(epdText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(epdText2.Text.Trim()))
                {
                    if (double.TryParse(epdText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(occuText1.Text.Trim()))
                {
                    if (double.TryParse(occuText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(occuText2.Text.Trim()))
                {
                    if (double.TryParse(occuText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            double valueForEPD = 0.0;

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForEPD = WattsPerSquareFootToWattsPerSquareMeter(double.Parse(epdText1.Text.Trim()));
                else
                    valueForEPD = double.Parse(epdText1.Text.Trim());
            }

            double valueForLPD = 0.0;

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForLPD = WattsPerSquareFootToWattsPerSquareMeter(double.Parse(lpdText1.Text.Trim()));
                else
                    valueForLPD = double.Parse(lpdText1.Text.Trim());
            }

            double valueForOccupany = 0.0;

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForOccupany = SquareFeetPerPersonToSquareMetersPerPerson(double.Parse(occuText1.Text.Trim()));
                else
                    valueForOccupany = double.Parse(occuText1.Text.Trim());
            }


            EDSRoomTag wallCreation = new EDSRoomTag();
            EDSRoomTag roomTag = new EDSRoomTag()
            {
                //roomLevel = levelComboBox.SelectedItem.ToString(),
                spaceType = spaceComboBox.SelectedItem.ToString(),
                epdType = epdComboBox.SelectedItem.ToString(),
                lpdType = lpdComboBox.SelectedItem.ToString(),
                occupancyType = occupComboBox.SelectedItem.ToString(),
                floorFinishType = floorFinishComboBox.SelectedItem.ToString(),
                freshAirType = freshAirComboBox.SelectedItem.ToString(),
                ceilingFinishType = ceilFinishComboBox.SelectedItem.ToString(),
                lpdText1 = valueForLPD.ToString(),
                lpdText2 = lpdText2.Text.ToString(),
                epdText1 = valueForEPD.ToString(),
                epdText2 = epdText2.Text.ToString(),
                occupancyText1 = valueForOccupany.ToString(),
                occupancyText2 = occuText2.Text.ToString()
            };
            wallCreation.UpdateRoom(roomTag, lpdCheck.Checked, epdCheck.Checked, occuCheck.Checked, freshAirCheck.Checked, floorCheck.Checked, ceilCheck.Checked);
            RefreshUI();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            EDSRoomTag wallCreation = new EDSRoomTag();
            var result = wallCreation.SelectRoom();

            if (result != null)
            {
                spaceComboBox.SelectedItem = result.spaceType.Split('-')[0];
                epdComboBox.SelectedItem = result.epdType;
                lpdComboBox.SelectedItem = result.lpdType;
                occupComboBox.SelectedItem = result.occupancyType;
                floorFinishComboBox.SelectedItem = result.floorFinishType;
                ceilFinishComboBox.SelectedItem = result.ceilingFinishType;
                freshAirComboBox.SelectedItem = result.freshAirType;
                lpdText1.Text = result.lpdText1;
                lpdText2.Text = result.lpdText2;
                epdText1.Text = result.epdText1;
                epdText2.Text = result.epdText2;
                occuText1.Text = result.occupancyText1;
                occuText2.Text = result.occupancyText2;
                //levelComboBox.SelectedItem = result.roomLevel;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (spaceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the space type");
                return;
            }

            if (lpdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the LPD type");
                return;
            }

            if (epdComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the EPD type");
                return;
            }

            if (occupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Occupany type");
                return;
            }

            if (freshAirComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Fresh Air type");
                return;
            }

            if (ceilFinishComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Ceiling Finish type");
                return;
            }

            if (floorFinishComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please provide the Floor Finish type");
                return;
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(epdText1.Text.ToString()) && string.IsNullOrWhiteSpace(epdText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the EPD");
                    return;
                }
            }

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(lpdText1.Text.ToString()) && string.IsNullOrWhiteSpace(lpdText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the LPD");
                    return;
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (string.IsNullOrWhiteSpace(occuText1.Text.ToString()) && string.IsNullOrWhiteSpace(occuText2.Text.ToString()))
                {
                    MessageBox.Show("Please provide input for the Occupancy");
                    return;
                }
            }


            int result = 0;
            if (int.TryParse(levelPreFix.Text.Trim(), out result))
            {

            }
            else
            {
                MessageBox.Show("Level should be an numeric value");
                return;
            }


            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(lpdText1.Text.Trim()))
                {
                    if (double.TryParse(lpdText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("LPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(lpdText2.Text.Trim()))
                {
                    if (double.TryParse(lpdText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("LPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(epdText1.Text.Trim()))
                {
                    if (double.TryParse(epdText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(epdText2.Text.Trim()))
                {
                    if (double.TryParse(epdText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(occuText1.Text.Trim()))
                {
                    if (double.TryParse(occuText1.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (!string.IsNullOrWhiteSpace(occuText2.Text.Trim()))
                {
                    if (double.TryParse(occuText2.Text, out _))
                    {

                    }
                    else
                    {
                        MessageBox.Show("EPD value should be an numeric value");
                        return;
                    }
                }
            }

            double valueForEPD = 0.0;

            if (epdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForEPD = WattsPerSquareFootToWattsPerSquareMeter(double.Parse(epdText1.Text.Trim()));
                else
                    valueForEPD = double.Parse(epdText1.Text.Trim());
            }

            double valueForLPD = 0.0;

            if (lpdComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForLPD = WattsPerSquareFootToWattsPerSquareMeter(double.Parse(lpdText1.Text.Trim()));
                else
                    valueForLPD = double.Parse(lpdText1.Text.Trim());
            }

            double valueForOccupany = 0.0;

            if (occupComboBox.SelectedItem.ToString().ToLower().Equals("custom"))
            {
                if (toggleSwitch1.Checked)
                    valueForOccupany = SquareFeetPerPersonToSquareMetersPerPerson(double.Parse(occuText1.Text.Trim()));
                else
                    valueForOccupany = double.Parse(occuText1.Text.Trim());
            }


            EDSRoomTag wallCreation = new EDSRoomTag();

            EDSRoomTag roomTag = new EDSRoomTag()
            {
                //roomLevel = levelComboBox.SelectedItem == null ? "" : levelComboBox.SelectedItem.ToString(),
                spaceType = spaceComboBox.SelectedItem == null ? "" : spaceComboBox.SelectedItem.ToString(),
                epdType = epdComboBox.SelectedItem == null ? "" : epdComboBox.SelectedItem.ToString(),
                lpdType = lpdComboBox.SelectedItem == null ? "" : lpdComboBox.SelectedItem.ToString(),
                occupancyType = occupComboBox.SelectedItem == null ? "" : occupComboBox.SelectedItem.ToString(),
                floorFinishType = floorFinishComboBox.SelectedItem == null ? "" : floorFinishComboBox.SelectedItem.ToString(),
                freshAirType = freshAirComboBox.SelectedItem == null ? "" : freshAirComboBox.SelectedItem.ToString(),
                ceilingFinishType = ceilFinishComboBox.SelectedItem == null ? "" : ceilFinishComboBox.SelectedItem.ToString(),
                lpdText1 = valueForLPD.ToString(),
                lpdText2 = lpdText2.Text.ToString(),
                epdText1 = valueForEPD.ToString(),
                epdText2 = epdText2.Text.ToString(),
                occupancyText1 = valueForOccupany.ToString(),
                occupancyText2 = occuText2.Text.ToString(),
                levelId = result
            };

            wallCreation.CreateRoom(roomTag);
            RefreshUI();
        }

        private void epdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (epdComboBox.SelectedItem != null)
            {
                var epdValue = epdComboBox.SelectedItem.ToString();
                if (epdValue.ToLower() == "custom")
                {
                    epdText1.Enabled = true;
                    epdText2.Enabled = true;
                }
                else
                {
                    epdText1.Enabled = false;
                    epdText2.Enabled = false;
                }
            }
        }

        private void lpdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lpdComboBox.SelectedItem != null)
            {
                var lpdValue = lpdComboBox.SelectedItem.ToString();
                if (lpdValue.ToLower() == "custom")
                {
                    lpdText1.Enabled = true;
                    lpdText2.Enabled = true;
                }
                else
                {
                    lpdText1.Enabled = false;
                    lpdText2.Enabled = false;
                }
            }
        }

        private void occupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (occupComboBox.SelectedItem != null)
            {
                var occpValue = occupComboBox.SelectedItem.ToString();
                if (occpValue.ToLower() == "custom")
                {
                    occuText1.Enabled = true;
                    occuText2.Enabled = true;
                }
                else
                {
                    occuText1.Enabled = false;
                    occuText2.Enabled = false;
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            spaceComboBox.SelectedItem = null;
            epdComboBox.SelectedItem = null;
            lpdComboBox.SelectedItem = null;
            occupComboBox.SelectedItem = null;
            floorFinishComboBox.SelectedItem = null;
            ceilFinishComboBox.SelectedItem = null;
            freshAirComboBox.SelectedItem = null;
            lpdText1.Text = "";
            lpdText2.Text = "";
            epdText1.Text = "";
            epdText2.Text = "";
            occuText1.Text = "";
            occuText2.Text = "";
            levelPreFix.Text = "";
            lpdCheck.Checked = false;
            epdCheck.Checked = false;
            occuCheck.Checked = false;
            ceilCheck.Checked = false;
            floorCheck.Checked = false;
            freshAirCheck.Checked = false;

        }

        // Convert from watts per square foot to watts per square meter
        double WattsPerSquareFootToWattsPerSquareMeter(double wattsPerSquareFoot)
        {
            return wattsPerSquareFoot * SquareFootToSquareMeter;
        }

        // Convert from watts per square meter to watts per square foot
        double WattsPerSquareMeterToWattsPerSquareFoot(double wattsPerSquareMeter)
        {
            return wattsPerSquareMeter * SquareMeterToSquareFoot;
        }

        double SquareFeetPerPersonToSquareMetersPerPerson(double sqftPerPerson)
        {
            return sqftPerPerson * SquareFootToSquareMeter;
        }

        private void lpdText1_KeyPress(object sender, KeyPressEventArgs e)
        {
            lpdText2.Text = "";
        }

        private void lpdText2_KeyPress(object sender, KeyPressEventArgs e)
        {
            lpdText1.Text = "";
        }

        private void epdText1_KeyPress(object sender, KeyPressEventArgs e)
        {
            epdText2.Text = "";
        }

        private void epdText2_KeyPress(object sender, KeyPressEventArgs e)
        {
            epdText1.Text = "";
        }

        private void occuText1_KeyPress(object sender, KeyPressEventArgs e)
        {
            occuText2.Text = "";
        }

        private void occuText2_KeyPress(object sender, KeyPressEventArgs e)
        {
            occuText1.Text = "";
        }
    }
}
