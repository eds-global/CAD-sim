using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceLib
{
    public class CityRecord
    {
        public string Name;
        public string climateType;
        public string Weather_File_Name;
        public string Latitude;
        public string Longitude;

        public CityRecord(string Name, string climateType, string Weather_File_Name)
        {
            this.Name = Name;
            this.climateType = climateType;
            this.Weather_File_Name = Weather_File_Name;
        }
    }
}
