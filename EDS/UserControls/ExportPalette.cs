using EDS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EDS
{
    public partial class ExportPalette : UserControl
    {
        public ExportPalette()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            progressBar1.Value = 25;
            EDSWall creation = new EDSWall();
            creation.FindClosedLoop(treeView1);
            progressBar1.Value = 100;


        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string handle = e.Node.Tag.ToString();

                Zoom.ZoomManager.Zoom2Handle(handle);
            }
            catch { }
        }

        private void clearErrors_Click(object sender, EventArgs e)
        {
            EDSWall creation = new EDSWall();
            creation.DeleteElementsFromLayer(StringConstants.errorLayerName);
        }
    }
}
