using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceLib;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using static System.Windows.Forms.LinkLabel;

namespace EDS
{
    public partial class ProjectInformationPalette : UserControl
    {
        public static Dictionary<string, List<string>> buildings = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<CityRecord>> cities = new Dictionary<string, List<CityRecord>>();
        public static ProjectInformation projectInformation = new ProjectInformation();

        static double radian = 0;

        static float sx, sy;
        static float ex, ey;

        static bool isDragging = false;

        static bool customLocation = false;

        public ProjectInformationPalette()
        {
            InitializeComponent();

            buildings = DataReader.ReadBuildingData("Building.csv");
            cities = DataReader.ReadCityData("Cities.csv");
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
        private void ProjectInformationPalette_Load(object sender, EventArgs e)
        {
            BindBuildings();
            BindCities();

#if DEBUG
            //txtProjectName.Text = "MaxOffice";
            //txtClientName.Text = "DLF";
            //txtAddress.Text = "Vasant vihar, Delhi";

            //cbBuildingCategory.Text = cbBuildingCategory.Items[0].ToString();
            //cbBuildingTypes.Text = cbBuildingTypes.Items[0].ToString();

            //cbStates.Text = cbStates.Items[0].ToString();
            //cbCities.Text = cbCities.Items[0].ToString();
#endif

            LoadProjectInformation();
        }

        private Point getPolarPoint(Point point, double radian, int r)
        {
            float x = (float)(point.X + r * Math.Cos(radian));
            float y = (float)(point.X + r * Math.Sin(radian));

            return new Point((int)x, (int)y);
        }

        private void cbBuildingCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buildingCategory = cbBuildingCategory.SelectedItem.ToString();

            List<string> buildingTypes = buildings[buildingCategory];

            cbBuildingTypes.Items.Clear();

            foreach (string type in buildingTypes)
            {
                cbBuildingTypes.Items.Add(type);
            }

#if DEBUG
            //cbBuildingTypes.Text = cbBuildingTypes.Items[0].ToString();
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
            //cbCities.Text = cbCities.Items[0].ToString();
#endif
        }
        private void cbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string state = cbStates.SelectedItem.ToString();

            string city = cbCities.SelectedItem.ToString();

            List<CityRecord> cityRecords = cities[state];

            foreach (CityRecord cityRecord in cityRecords)
            {
                if (cityRecord.Name == city)
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
            projectInformation.customLocation = customLocation.ToString();
            projectInformation.State = cbStates.Text;

            if (customLocation == false)
            {
                projectInformation.City = cbCities.Text;
                projectInformation.Latitude = lbLatitude.Text;
                projectInformation.Longitude = lbLongitude.Text;
            }
            else
            {
                projectInformation.City = cbRepCity.Text;
                projectInformation.Latitude = txtLat.Text;
                projectInformation.Longitude = txtLong.Text;
            }

            projectInformation.ClimateType = lbClimateType.Text;
            projectInformation.Direction = txtAngle.Text;

            SaveProjectInformation(projectInformation);

            MessageBox.Show("Project Saved Successfully");
        }

        private void DrawNorthArrow()
        {
            Graphics graphics = pbNorthArrow.CreateGraphics();

            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, pbNorthArrow.Width, pbNorthArrow.Height);

            int w = pbNorthArrow.Width;
            int h = pbNorthArrow.Height;

            Pen pen = new Pen(Color.Red, 2);

            float cx = w / 2;
            float cy = w / 2;

            float x, y, r;

            x = 0;
            y = 0;

            r = 25;
            graphics.DrawEllipse(pen, cx - r, cy - r, 2 * r, 2 * r);

            r = 50;
            graphics.DrawEllipse(pen, cx - r, cy - r, 2 * r, 2 * r);

            float x1, y1, x2, y2;

            x1 = 0;
            y1 = 0;

            Point point = new Point((int)x1, (int)y1);

            Point newPoint = getPolarPoint(point, radian, 50);

            x2 = newPoint.X;
            y2 = newPoint.Y;

            x1 = cx + x1;
            y1 = cy - y1;

            x2 = cx + x2;
            y2 = cy - y2;

            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public void SaveProjectInformation(ProjectInformation projectInformation)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            {
                Editor ed = doc.Editor;
                Transaction trans = ed.Document.Database.TransactionManager.StartTransaction();
                try
                {
                    DBDictionary nod = (DBDictionary)trans.GetObject(ed.Document.Database.NamedObjectsDictionaryId, OpenMode.ForWrite);
                    if (nod.Contains("EDS"))
                    {
                        ObjectId entryId = nod.GetAt("EDS");
                        Xrecord myXrecord = null;
                        myXrecord = (Xrecord)trans.GetObject(entryId, OpenMode.ForWrite);

                        nod.UpgradeOpen();
                        nod.Remove("EDS");

                        ResultBuffer data = new ResultBuffer(new TypedValue((int)DxfCode.Int16, 1),
                            new TypedValue((int)DxfCode.Text, "Project Information"),
                            new TypedValue((int)DxfCode.Text, "Project Name" + " : " + projectInformation.ProjectName),
                            new TypedValue((int)DxfCode.Text, "Client Name" + " : " + projectInformation.ClientName),
                            new TypedValue((int)DxfCode.Text, "Address" + " : " + projectInformation.Address),
                            new TypedValue((int)DxfCode.Text, "Building Category" + " : " + projectInformation.BuildingCategory),
                            new TypedValue((int)DxfCode.Text, "Building Type" + " : " + projectInformation.BuildingType),
                            new TypedValue((int)DxfCode.Text, "Custom Location" + " : " + projectInformation.customLocation),
                            new TypedValue((int)DxfCode.Text, "State" + " : " + projectInformation.State),
                            new TypedValue((int)DxfCode.Text, "City" + " : " + projectInformation.City),
                            new TypedValue((int)DxfCode.Text, "Latitude" + " : " + projectInformation.Latitude),
                            new TypedValue((int)DxfCode.Text, "Longitude" + " : " + projectInformation.Longitude),
                            new TypedValue((int)DxfCode.Text, "Climate Type" + " : " + projectInformation.ClimateType),
                            new TypedValue((int)DxfCode.Text, "Direction" + " : " + projectInformation.Direction)
                            );

                        myXrecord.Data = data;
                        nod.SetAt("EDS", myXrecord);
                    }
                    else
                    {
                        nod.UpgradeOpen();
                        Xrecord myXrecord = new Xrecord();
                        ResultBuffer data = new ResultBuffer(new TypedValue((int)DxfCode.Int16, 1),
                            new TypedValue((int)DxfCode.Text, "Project Information"),
                            new TypedValue((int)DxfCode.Text, "Project Name" + " : " + projectInformation.ProjectName),
                            new TypedValue((int)DxfCode.Text, "Client Name" + " : " + projectInformation.ClientName),
                            new TypedValue((int)DxfCode.Text, "Address" + " : " + projectInformation.Address),
                            new TypedValue((int)DxfCode.Text, "Building Category" + " : " + projectInformation.BuildingCategory),
                            new TypedValue((int)DxfCode.Text, "Building Type" + " : " + projectInformation.BuildingType),
                            new TypedValue((int)DxfCode.Text, "Custom Location" + " : " + projectInformation.customLocation),
                            new TypedValue((int)DxfCode.Text, "State" + " : " + projectInformation.State),
                            new TypedValue((int)DxfCode.Text, "City" + " : " + projectInformation.City),
                            new TypedValue((int)DxfCode.Text, "Latitude" + " : " + projectInformation.Latitude),
                            new TypedValue((int)DxfCode.Text, "Longitude" + " : " + projectInformation.Longitude),
                            new TypedValue((int)DxfCode.Text, "Climate Type" + " : " + projectInformation.ClimateType),
                            new TypedValue((int)DxfCode.Text, "Direction" + " : " + projectInformation.Direction)
                            );

                        myXrecord.Data = data;
                        nod.SetAt("EDS", myXrecord);
                        trans.AddNewlyCreatedDBObject(myXrecord, true);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    ed.WriteMessage("a problem occurred because " + ex.Message);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public static bool LoadProjectInformation()
        {
            bool isEDSDrawing = false;

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            {
                Editor ed = doc.Editor;
                Transaction trans = ed.Document.Database.TransactionManager.StartTransaction();
                try
                {
                    DBDictionary nod = (DBDictionary)trans.GetObject(ed.Document.Database.NamedObjectsDictionaryId, OpenMode.ForRead);
                    if (nod.Contains("EDS"))
                    {
                        isEDSDrawing = true;

                        ObjectId entryId = nod.GetAt("EDS");
                        Xrecord myXrecord = null;
                        myXrecord = (Xrecord)trans.GetObject(entryId, OpenMode.ForRead);

                        bool projectInfoFound = false;
                        foreach (TypedValue value in myXrecord.Data)
                        {
                            string s = value.Value.ToString();

                            if (s == "Project Information")
                            {
                                projectInfoFound = true;
                                projectInformation = new ProjectInformation();
                            }
                            else
                            {
                                if (projectInfoFound == true)
                                {
                                    string[] array = s.Split(':');

                                    //ed.WriteMessage("\n" + array[0] + " = " + array[1] + "\n");

                                    string key = array[0].Trim();

                                    switch (key)
                                    {
                                        case "Project Name":
                                            projectInformation.ProjectName = array[1].Trim();
                                            break;
                                        case "Client Name":
                                            projectInformation.ClientName = array[1].Trim();
                                            break;
                                        case "Address":
                                            projectInformation.Address = array[1].Trim();
                                            break;
                                        case "Building Category":
                                            projectInformation.BuildingCategory = array[1].Trim();
                                            break;
                                        case "Building Type":
                                            projectInformation.BuildingType = array[1].Trim();
                                            break;
                                        case "Custom Location":
                                            projectInformation.customLocation = array[1].Trim();
                                            break;
                                        case "State":
                                            projectInformation.State = array[1].Trim();
                                            break;
                                        case "City":
                                            projectInformation.City = array[1].Trim();
                                            break;
                                        case "Latitude":
                                            if ("True" == projectInformation.customLocation)
                                            {
                                                projectInformation.Latitude = array[1].Trim() /*+ " : " + array[2].Trim()*/;
                                            }
                                            else
                                            {
                                                projectInformation.Latitude = array[1].Trim() + " : " + array[2].Trim();
                                            }
                                            break;
                                        case "Longitude":
                                            if ("True" == projectInformation.customLocation)
                                            {
                                                projectInformation.Longitude = array[1].Trim() /*+ " : " + array[2].Trim()*/;
                                            }
                                            else
                                            {
                                                projectInformation.Longitude = array[1].Trim() + " : " + array[2].Trim();
                                            }
                                            break;
                                        case "Climate Type":
                                            if ("True" == projectInformation.customLocation)
                                            {

                                            }
                                            else
                                            {
                                                if (array.Length >= 3)
                                                {
                                                    projectInformation.ClimateType = array[1].Trim() + " : " + array[2].Trim();
                                                }
                                            }
                                            break;
                                        case "Direction":
                                            projectInformation.Direction = array[1].Trim();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    ed.WriteMessage("a problem occurred because " + ex.Message);
                }
                finally
                {
                    trans.Dispose();
                }
            }

            PopulatePalette(isEDSDrawing);

            return isEDSDrawing;
        }
        private void btnLoadProjectDetails_Click(object sender, EventArgs e)
        {
            LoadProjectInformation();
        }

        private static void PopulatePalette(bool isEDSDrawing)
        {
            //if (projectInformation.ProjectName == null)
            if (isEDSDrawing == false)
            {
                txtProjectName.Text = "";
                txtClientName.Text = "";
                txtAddress.Text = "";
                cbBuildingCategory.Text = "";
                cbBuildingTypes.Text = "";
                cbStates.Text = "";

                //if (customLocation == false)
                //{
                //    cbCities.Text = "";
                //    lbLatitude.Text = "";
                //    lbLongitude.Text = "";
                //}
                //else
                //{
                cbRepCity.Text = "";
                txtLat.Text = "";
                txtLong.Text = "";
                //}  

                lbClimateType.Text = "";

                txtAngle.Text = "";
            }
            else
            {
                txtProjectName.Text = projectInformation.ProjectName;
                txtClientName.Text = projectInformation.ClientName;
                txtAddress.Text = projectInformation.Address;
                cbBuildingCategory.Text = projectInformation.BuildingCategory;
                cbBuildingTypes.Text = projectInformation.BuildingType;
                cbStates.Text = projectInformation.State;

                if ("True" == projectInformation.customLocation)
                {
                    radCustomLocation.Checked = true;
                    customLocation = true;
                }
                else
                {
                    radCustomLocation.Checked = false;
                    customLocation = false;
                }

                if (customLocation == false)
                {
                    cbCities.Text = projectInformation.City;
                    lbLatitude.Text = projectInformation.Latitude;
                    lbLongitude.Text = projectInformation.Longitude;
                    lbClimateType.Text = projectInformation.ClimateType;
                }
                else
                {
                    cbRepCity.Text = projectInformation.City;
                    txtLat.Text = projectInformation.Latitude;
                    txtLong.Text = projectInformation.Longitude;
                }
                ;
                if (projectInformation.Direction != "")
                {
                    double degree = Convert.ToDouble(projectInformation.Direction);
                    radian = (Math.PI / 180.0) * degree;
                    txtAngle.Text = degree.ToString();
                }
            }
        }

        private void txtAngle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double degree = Convert.ToDouble(txtAngle.Text);
                degree *= -1;
                degree = 90 + degree;
                radian = (Math.PI / 180.0) * degree;
                DrawNorthArrow();
            }
            catch
            {

            }
        }

        private void radCustomLocation_CheckedChanged(object sender, EventArgs e)
        {
            onLocationChanged();
        }

        private void radSelectLocation_CheckedChanged(object sender, EventArgs e)
        {
            onLocationChanged();
        }

        void onLocationChanged()
        {
            if (radSelectLocation.Checked == true)
            {
                customLocation = false;

                txtLat.Enabled = false;
                txtLong.Enabled = false;
                cbRepCity.Enabled = false;

                cbStates.Enabled = true;
                cbCities.Enabled = true;


            }
            else
            {
                customLocation = true;

                txtLat.Enabled = true;
                txtLong.Enabled = true;
                cbRepCity.Enabled = true;

                cbStates.Enabled = false;
                cbCities.Enabled = false;
            }
        }

        //private void pbNorthArrow_MouseDown(object sender, MouseEventArgs e)
        //{

        //}

        //private void pbNorthArrow_MouseMove(object sender, MouseEventArgs e)
        //{

        //}

        //private void pbNorthArrow_MouseUp(object sender, MouseEventArgs e)
        //{

        //}

        private void pbNorthArrow_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            int w = pbNorthArrow.Width;
            int h = pbNorthArrow.Height;

            float cx = w / 2;
            float cy = w / 2;

            sx = 0;
            sy = 0;
        }

        //private void pbNorthArrow_MouseHover(object sender, EventArgs e)
        //{
        //    Draw();
        //}

        //private void pbNorthArrow_Paint(object sender, PaintEventArgs e)
        //{
        //    Draw();
        //}

        //private void pbNorthArrow_ControlAdded(object sender, ControlEventArgs e)
        //{
        //    Draw();
        //}

        //private void pbNorthArrow_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    Draw();
        //}

        private void pbNorthArrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {

                int w = pbNorthArrow.Width;
                int h = pbNorthArrow.Height;

                float cx = w / 2;
                float cy = w / 2;

                ex = e.X - cx;
                ey = -1 * (e.Y - cy);

                float x1, y1, x2, y2;

                x1 = sx;
                y1 = sy;

                x2 = ex;
                y2 = ey;

                float deltaX = x2 - x1;
                float deltaY = y2 - y1;

                radian = Math.Atan2(deltaY, deltaX);

                double degree = (180.0 / Math.PI) * radian;

                degree = 90 - degree;

                txtAngle.Text = degree.ToString("0.#");

                //DrawNorthArrow();
            }
        }

        private void pbNorthArrow_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
