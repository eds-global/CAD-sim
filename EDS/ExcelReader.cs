using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS
{
    internal class ExcelReader
    {
        string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static List<string> GetValuesFromExcel(string fileName, string sheetName)
        {
            List<string> values = new List<string>();

            using (SpreadsheetLight.SLDocument document = new SpreadsheetLight.SLDocument(fileName))
            {
                // Check if the sheet name exists in the workbook
                var sheetNames = document.GetWorksheetNames();
                if (!sheetNames.Contains(sheetName))
                {
                    Console.WriteLine($"Sheet '{sheetName}' does not exist.");
                    return new List<string>();
                }

                // Select the worksheet by name
                document.SelectWorksheet(sheetName);

                // Get the number of rows in the worksheet
                int rowCount = document.GetWorksheetStatistics().NumberOfRows;

                // Read the first column (column index 1) for all rows
                for (int row = 1; row <= rowCount; row++)
                {
                    // Read the cell value from the first column
                    string cellValue = document.GetCellValueAsString(row, 1);
                    values.Add(cellValue);
                }
            }

            return values;
        }

    }
}
