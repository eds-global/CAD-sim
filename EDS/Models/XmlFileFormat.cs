using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace EDS.Models
{
    internal class XmlFileFormat
    {
        public bool WriteToFile(Campus campus,string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Campus));

                // Serialize the object to an XML file
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, campus);
                }

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error while creating gbxmlfile");
                return false;
            }
        }
    }
}
