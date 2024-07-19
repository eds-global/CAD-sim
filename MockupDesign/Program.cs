using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceLib;

namespace MockupDesign
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //frmProjectInfo frm = new frmProjectInfo(DataReader.ReadBuildingData("Building.csv"), DataReader.ReadCityData("Cities.csv"));
            //Application.Run(frm);
            //ProjectInformation projectInformation = new ResourceLib.ProjectInformation();
            //projectInformation = frm.projectInformation;


        }
    }
}
