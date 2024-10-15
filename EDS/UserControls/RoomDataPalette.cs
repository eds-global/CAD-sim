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
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;
using OfficeOpenXml;
using System.IO;

namespace EDS.UserControls
{
    public partial class RoomDataPalette : UserControl
    {
        public static bool DrawRoom = false;
        public static string roomTag = string.Empty;

        string LastCommad = string.Empty;
        const double SquareFootToSquareMeter = 10.7639;
        const double SquareMeterToSquareFoot = 1.0 / SquareFootToSquareMeter;

        List<string> lpdValues = new List<string>();

        List<string> epdValues = new List<string>();

        List<string> occupValues = new List<string>();

        List<string> airValues = new List<string>();

        List<string> ceilValues = new List<string>();

        List<string> floorValues = new List<string>();

        List<string> spaceTypes = new List<string>();

        List<string> buildingTypes = new List<string>();

        public RoomDataPalette()
        {
            DrawRoom = false;
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            lpdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "LPD");
            epdValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "EPD");
            occupValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Occupancy");
            airValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "FreshAir");
            ceilValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Ceiling Finish");
            floorValues = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Floor Finish");
            //spaceTypes = ExcelReader.GetValuesFromExcel("Material Database.xlsx", "Space Type");
            buildingTypes = GetBuildingTypeData("Material Database.xlsx");
            //allLayers = roomTag.GetAllLayers();

            lpdText1.Enabled = false;
            lpdText2.Enabled = false;

            epdText1.Enabled = false;
            epdText2.Enabled = false;

            occuText1.Enabled = false;
            occuText2.Enabled = false;

            //spaceComboBox.SelectedIndex = 0;
            //epdComboBox.SelectedIndex = 0;
            //lpdComboBox.SelectedIndex = 0;
            //occupComboBox.SelectedIndex = 0;
            //floorFinishComboBox.SelectedIndex = 0;
            //ceilFinishComboBox.SelectedIndex = 0;
            //freshAirComboBox.SelectedIndex = 0;

        }

        private List<string> GetBuildingTypeData(string fileName)
        {
            string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var filePath = Path.Combine(folderPath, "EDS_Database", fileName);

            List<BuildingTypeData> buildingDataList = LoadBuildingDataFromExcel(filePath);

            return buildingDataList.Select(x => x.BuildingType).Distinct().ToList();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!string.IsNullOrEmpty(LastCommad))
                {
                    if (LastCommad == "D")
                        DrawCustomRoom();
                    else if (LastCommad == "U")
                        UpdateCustomRoom();
                    else if (LastCommad == "S")
                        SelectCustomRoom();
                    else if (LastCommad == "M")
                        MatchCustomRoom();
                }
            }
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
            foreach (var value in buildingTypes)
            {
                buildingType.Items.Add(value);
            }

            buildingType.SelectedIndex = 0;
            spaceComboBox.SelectedIndex = 0;
            epdComboBox.SelectedIndex = 0;
            lpdComboBox.SelectedIndex = 0;
            occupComboBox.SelectedIndex = 0;
            floorFinishComboBox.SelectedIndex = 0;
            ceilFinishComboBox.SelectedIndex = 0;
            freshAirComboBox.SelectedIndex = 0;
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
            MatchCustomRoom();
            LastCommad = "M";
        }

        private void MatchCustomRoom()
        {
            EDSRoomTag wallCreation = new EDSRoomTag();
            wallCreation.MatchRoom(lpdCheck.Checked, epdCheck.Checked, occuCheck.Checked, freshAirCheck.Checked, floorCheck.Checked, ceilCheck.Checked);
            RefreshUI();
            DrawRoom = false;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateCustomRoom();
            LastCommad = "U";
        }

        private void UpdateCustomRoom()
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
            DrawRoom = false;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            SelectCustomRoom();
            LastCommad = "S";
        }

        private void SelectCustomRoom()
        {
            EDSRoomTag wallCreation = new EDSRoomTag();
            var result = wallCreation.SelectRoom();

            if (result != null)
            {
                buildingType.SelectedItem = result.buildingType;
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

            DrawRoom = false;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DrawCustomRoom();
            LastCommad = "D";
        }

        private void DrawCustomRoom()
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
                buildingType = buildingType.SelectedItem.ToString(),
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
            DrawRoom = false;
            //RefreshUI();
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
            buildingType.SelectedIndex = 0;
            spaceComboBox.SelectedIndex = 0;
            epdComboBox.SelectedIndex = 0;
            lpdComboBox.SelectedIndex = 0;
            occupComboBox.SelectedIndex = 0;
            floorFinishComboBox.SelectedIndex = 0;
            ceilFinishComboBox.SelectedIndex = 0;
            freshAirComboBox.SelectedIndex = 0;
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

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void ceilFinishComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void unitLabel_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void floorFinishComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomTag = (sender as ComboBox).SelectedItem.ToString();
            Document doc = Application.DocumentManager.MdiActiveDocument;

            if (doc != null)
            {
                Editor ed = doc.Editor;

                // Use SendStringToExecute to cancel the command line (equivalent to ESC or Cancel command)
                doc.SendStringToExecute("\x1B", true, false, false); // "\x1B" represents the ESC key

                if (DrawRoom)
                    System.Windows.Forms.MessageBox.Show("Room Tag Selection Changed.\nPlease click on Add to place rooms.", "Room Placement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buildingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (buildingType.SelectedItem != null)
            {
                spaceTypes.Clear(); spaceComboBox.Items.Clear();
                var buildType = buildingType.SelectedItem.ToString();

                string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var filePath = Path.Combine(folderPath, "EDS_Database", "Material Database.xlsx");

                List<BuildingTypeData> buildingDataList = LoadBuildingDataFromExcel(filePath);

                spaceTypes = buildingDataList.FindAll(x => x.BuildingType.Equals(buildType)).Select(x => x.SpaceType).Distinct().ToList();

                foreach (var spaceType in spaceTypes)
                {
                    spaceComboBox.Items.Add(spaceType);
                }

                spaceComboBox.SelectedIndex = 0;
            }
        }

        List<BuildingTypeData> LoadBuildingDataFromExcel(string filePath)
        {
            List<BuildingTypeData> data = new List<BuildingTypeData>();

            // Load Excel file using EPPlus
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // EPPlus needs this for the free license

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets[9]; // Assuming the data is in the first sheet

                // Iterate through the rows, starting from row 2 to skip headers
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var buildingData = new BuildingTypeData
                    {
                        SerialNo = int.Parse(worksheet.Cells[row, 1].Text),
                        BuildingType = worksheet.Cells[row, 2].Text,
                        Code = worksheet.Cells[row, 3].Text,
                        SpaceType = worksheet.Cells[row, 4].Text,
                        Code2 = worksheet.Cells[row, 5].Text,
                        ActivityDescription = worksheet.Cells[row, 6].Text,
                        LightingPowerDensity = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 7].Text),
                        EquipmentPowerDensity = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 8].Text),
                        Occupancy = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 9].Text),
                        FreshAirPerPerson = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 10].Text),
                        FreshAirPerArea = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 11].Text),
                        FreshAirACH = string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? (double?)null : double.Parse(worksheet.Cells[row, 12].Text)
                    };

                    data.Add(buildingData);
                }
            }

            return data;
        }
    }

    public class BuildingTypeData
    {
        public int SerialNo { get; set; }
        public string BuildingType { get; set; }
        public string Code { get; set; }
        public string SpaceType { get; set; }
        public string Code2 { get; set; }
        public string ActivityDescription { get; set; }
        public double? LightingPowerDensity { get; set; }
        public double? EquipmentPowerDensity { get; set; }
        public double? Occupancy { get; set; }
        public double? FreshAirPerPerson { get; set; }
        public double? FreshAirPerArea { get; set; }
        public double? FreshAirACH { get; set; }
    }
}
