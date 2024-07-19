using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;

namespace EDS.UserControls
{
    public partial class WindowsDataPalette : UserControl
    {
        public WindowsDataPalette()
        {
            InitializeComponent();
        }

        private void btnAssignWindowData_Click(object sender, EventArgs e)
        {
            SelectionSet sset = CADUtilities.selectObjects("\n\nSelect Windows");

            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock locDoc = acDoc.LockDocument())
            {
                Editor ed = acDoc.Editor;

                Database db = HostApplicationServices.WorkingDatabase;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    {
                        foreach (SelectedObject so in sset)
                        {
                            ObjectId objId = so.ObjectId;

                            SetWindowXData(objId);
                        }
                    }

                    tr.Commit();
                }
            }
        }

        private void SetWindowXData(ObjectId objId)
        {
            CADUtilities.SetXData(objId, "Sill Height", txtSillHeight.Text);
            CADUtilities.SetXData(objId, "Height of Vision Window", txtHeightOfVisionWindow.Text);
            CADUtilities.SetXData(objId, "Height of Daylight Window", txtHeightOfDaylightWindow.Text);
        }

        private void btnMatchWindowData_Click(object sender, EventArgs e)
        {
            ObjectId sourceId = CADUtilities.selectObject("\n\nSelect Source window");

            Dictionary<string, string> windowDataDict = GetWindowXDataDictionary(sourceId);

            SelectionSet destinationSet = CADUtilities.selectObjects("\n\nSelect Destination windows");

            SetWindowXDataToDestination(destinationSet, windowDataDict);
        }

        private static void SetWindowXDataToDestination(SelectionSet destinationSet, Dictionary<string, string> windowXDataDict)
        {
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock locDoc = acDoc.LockDocument())
            {
                Editor ed = acDoc.Editor;

                Database db = HostApplicationServices.WorkingDatabase;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    {
                        foreach (SelectedObject so in destinationSet)
                        {
                            ObjectId objId = so.ObjectId;

                            foreach (KeyValuePair<string, string> xData in windowXDataDict)
                            {
                                CADUtilities.SetXData(objId, xData.Key, xData.Value);
                            }
                        }
                    }

                    tr.Commit();
                }
            }
        }

        private static Dictionary<string, string> GetWindowXDataDictionary(ObjectId sourceId)
        {
            Dictionary<string, string> windowDataDict = new Dictionary<string, string>();
            AddXDataIntoDictionary(sourceId, windowDataDict, "Sill Height");
            AddXDataIntoDictionary(sourceId, windowDataDict, "Height of Vision Window");
            AddXDataIntoDictionary(sourceId, windowDataDict, "Height of Daylight Window");

            return windowDataDict;
        }

        private static void AddXDataIntoDictionary(ObjectId sourceId, Dictionary<string, string> windowDataDict, string appName)
        {
            windowDataDict.Add(appName, CADUtilities.GetXData(sourceId, appName));
        }

        private void btnAddWindow_Click(object sender, EventArgs e)
        {
            Point3d p1 = CADUtilities.GetPoint("Pick first point");

            Point3d p2 = CADUtilities.GetPoint("Pick second point", p1);

            string lineHandle = CADUtilities.CreateLine(p1, p2,"Windows", 2);
            ObjectId lineId = CADUtilities.HandleToObjectId(lineHandle);

            Point2d A = new Point2d(0, 0);
            Point2d B = new Point2d(0, 0);
            Point2d C = new Point2d(0, 0);
            Point2d D = new Point2d(0, 0);

            double width = Convert.ToDouble(txtWidth.Text);

            DBObjectCollection offsetObj1 = CADUtilities.Offset(lineId, width);            

            if(offsetObj1.Count > 0)
            {
                DBObject dbObj = offsetObj1[0];

                Line l = dbObj as Line;

                if(l != null)
                {
                    A = new Point2d(l.StartPoint.X, l.StartPoint.Y);
                    B = new Point2d(l.EndPoint.X, l.EndPoint.Y);
                }
            }

            DBObjectCollection offsetObj2 = CADUtilities.Offset(lineId, -width);

            CADUtilities.Erase(lineId);

            if (offsetObj2.Count > 0)
            {
                DBObject dbObj = offsetObj2[0];

                Line l = dbObj as Line;

                if (l != null)
                {
                    C = new Point2d(l.StartPoint.X, l.StartPoint.Y);
                    D = new Point2d(l.EndPoint.X, l.EndPoint.Y);
                }
            }

            ObjectId recId = CADUtilities.CreateRectangle(A, B, C, D, "Window", 2);

            SetWindowXData(recId);            
        }
    }
}
