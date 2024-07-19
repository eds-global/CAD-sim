using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ResourceLib
{
    public partial class frmProjectInfo : Form
    {
        Dictionary<string, List<string>> buildings = new Dictionary<string, List<string>>();
        Dictionary<string, List<CityRecord>> cities = new Dictionary<string, List<CityRecord>>();
        public ProjectInformation projectInformation = new ProjectInformation();   
        public frmProjectInfo(Dictionary<string, List<string>> buildings, Dictionary<string, List<CityRecord>> cities)
        {
            InitializeComponent();

            this.buildings = buildings;
            this.cities = cities;            
        }
        private void ProjectInfo_Load(object sender, EventArgs e)
        {
            BindBuildings();
            BindCities();

#if DEBUG
            txtProjectName.Text = "MaxOffice";
            txtClientName.Text = "DLF";
            txtAddress.Text = "Vasant vihar, Delhi";

            cbBuildingCategory.Text = cbBuildingCategory.Items[0].ToString();
            cbBuildingTypes.Text = cbBuildingTypes.Items[0].ToString();

            cbStates.Text = cbStates.Items[0].ToString();
            cbCities.Text = cbCities.Items[0].ToString();
#endif
        }
        private void BindBuildings()
        {
            foreach (var building in buildings)
            {
                if (!cbBuildingCategory.Items.Contains(building.Key))
                {
                    cbBuildingCategory.Items.Add(building.Key);
                }
            }
        }
        private void BindCities()
        {
            foreach (var city in cities)
            {
                if (!cbStates.Items.Contains(city.Key))
                {
                    cbStates.Items.Add(city.Key);
                }
            }
        }
        private void cbBuildingCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buildingCategory = cbBuildingCategory.SelectedItem.ToString();

            List<string> buildingTypes =  buildings[buildingCategory];

            cbBuildingTypes.Items.Clear();

            foreach (string type in buildingTypes) 
            {
                cbBuildingTypes.Items.Add(type);
            }

#if DEBUG
            cbBuildingTypes.Text = cbBuildingTypes.Items[0].ToString();
#endif

        }
        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string state = cbStates.SelectedItem.ToString();

            List<CityRecord> cityRecords = cities[state];

            cbCities.Items.Clear();

            foreach (CityRecord cityRecord in cityRecords)
            {
                cbCities.Items.Add(cityRecord.Name);
            }

#if DEBUG
            cbCities.Text = cbCities.Items[0].ToString();
#endif
        }
        private void cbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string state = cbStates.SelectedItem.ToString();

            string city = cbCities.SelectedItem.ToString();

            List<CityRecord> cityRecords = cities[state];

            foreach (CityRecord cityRecord in cityRecords)
            {
                if(cityRecord.Name == city)
                {
                    lbClimateType.Text = "Climate Type: " + cityRecord.climateType;

                    string Weather_File_Name = cityRecord.Weather_File_Name;

                    string latitude = "";
                    string longitude = "";

                    DataReader.getLatLong(Weather_File_Name, ref latitude, ref longitude);

                    lbLatitude.Text = "Latitude: " + latitude + "° N";
                    lbLongitude.Text = "Longitud: " + longitude + "° E";
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            projectInformation.ProjectName = txtProjectName.Text;
            projectInformation.ClientName = txtClientName.Text;
            projectInformation.Address = txtAddress.Text;
            projectInformation.BuildingCategory = cbBuildingCategory.Text;
            projectInformation.BuildingType = cbBuildingTypes.Text;
            projectInformation.State = cbStates.Text;
            projectInformation.City = cbCities.Text;
            projectInformation.Latitude = lbLatitude.Text;
            projectInformation.Longitude = lbLongitude.Text;
            projectInformation.ClimateType = lbClimateType.Text;

            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
