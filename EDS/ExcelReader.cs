using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;

namespace EDS
{
    public class ExcelReader
    {
        public static List<string> GetValuesFromExcel(string fileName, string sheetName)
        {
            string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            List<string> values = new List<string>();

            var filePath = Path.Combine(folderPath, "EDS_Database", fileName);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create an IExcelDataReader instance
                IExcelDataReader reader;

                // Detect the file format and create the reader
                if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (Path.GetExtension(filePath).Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    throw new NotSupportedException("Unsupported file format");
                }

                // Convert the reader to a dataset
                var dataset = reader.AsDataSet();

                // Find the DataTable with the specified sheet name
                DataTable table = null;
                foreach (DataTable dt in dataset.Tables)
                {
                    if (dt.TableName.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                    {
                        table = dt;
                        break;
                    }
                }

                if (table == null)
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");

                }

                // Iterate through the rows and columns of the DataTable
                foreach (DataRow row in table.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        values.Add(cell.ToString());
                    }
                }

                // Don't forget to close the reader
                reader.Close();
            }

            return values;
        }

    }
}
