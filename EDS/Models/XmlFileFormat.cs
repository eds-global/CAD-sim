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
        public bool WriteToFile(GbXml gbXml,string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GbXml));

                // Serialize the object to an XML file
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, gbXml);
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
