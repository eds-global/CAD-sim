using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceLib;

namespace ResourceLib
{
    public class DataReader
    {
        static string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public void setServerPath(string serverPath)
        {
            folderPath = serverPath;
        }
        public static Dictionary<string, List<string>> ReadBuildingData(string csvFileName)
        {
            Dictionary<string, List<string>> buildingData = new Dictionary<string, List<string>>();
            
            string dataFilePath = Path.Combine(folderPath, "EDS_Database\\" + csvFileName);

            if (File.Exists(dataFilePath))
            {
                try
                {
                    StreamReader sr = new StreamReader(dataFilePath);
                    String line;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        line = sr.ReadLine();

                        string[] array = line.Split(',');                        

                        if (buildingData.Keys.Contains(array[0]))
                        {
                            List<string> value = buildingData[array[0]];

                            value.Add(array[1]);

                            buildingData[array[0]] = value;
                        }
                        else
                        {
                            List<string> value = new List<string>();

                            value.Add(array[1]);

                            buildingData.Add(array[0], value);
                        }
                    }
                    sr.Close();
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                }
                finally
                {
                }
            }
            else
            {
                MessageBox.Show(csvFileName + " not present in " + folderPath);
            }

            return buildingData;
        }

        public static Dictionary<string, List<CityRecord>> ReadCityData(string csvFileName)
        {
            Dictionary<string, List<CityRecord>> cityData = new Dictionary<string, List<CityRecord>>();

            string dataFilePath = Path.Combine(folderPath, "EDS_Database\\" + csvFileName);

            if (File.Exists(dataFilePath))
            {
                try
                {
                    StreamReader sr = new StreamReader(dataFilePath);
                    String line;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        line = sr.ReadLine();

                        if(line != null)
                        {
                            string[] array = line.Split(',');

                            if (cityData.Keys.Contains(array[0]))
                            {
                                List<CityRecord> value = cityData[array[0]];

                                CityRecord cityRecord = new CityRecord(array[1], array[2], array[3]);
                                value.Add(cityRecord);

                                cityData[array[0]] = value;
                            }
                            else
                            {
                                List<CityRecord> value = new List<CityRecord>();

                                string Weather_File_Name = array[3];

                                //string latitude = "";
                                //string longitude = "";

                                //getLatLong(Weather_File_Name, ref latitude, ref longitude);

                                CityRecord cityRecord = new CityRecord(array[1], array[2], Weather_File_Name);
                                value.Add(cityRecord);

                                cityData.Add(array[0], value);
                            }
                        }                        
                    }
                    sr.Close();
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    //Console.WriteLine("Executing finally block.");
                }
            }
            else
            {
                MessageBox.Show(csvFileName + " not present in " + folderPath);
            }

            return cityData;
        }

        public static void getLatLong(string weather_File_Name, ref string latitude, ref string longitude)
        {
            string dataFilePath = Path.Combine(folderPath, "Database\\Weather_Files\\" + weather_File_Name);

            if (File.Exists(dataFilePath))
            {
                try
                {
                    StreamReader sr = new StreamReader(dataFilePath);
                    String line;
                    line = sr.ReadLine();

                    string[] array = line.Split(',');

                    latitude  = array[6]; 
                    longitude = array[7];  
                  
                    sr.Close();
                    Console.ReadLine();
                }
                catch(Exception e) 
                {
                    MessageBox.Show(weather_File_Name + " not present in " + folderPath);
                }
            }
        }
    }
}
