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
            setExternallWallSortByEvent();

            SelectionSet sset = CADUtilities.selectObjects("\n\nSelect drawing for export data ");

            if(chkValidate.Checked == true)
            {
                if (validateDrawing(sset))
                {
                    ExportAEC(sset);
                }
            }
            else
            {
                ExportAEC(sset);
            }                       
        }
        private void setExternallWallSortByEvent()
        {
            GenericModule.IsExtWllSrtBbyClkWs = clckWiseRdBttn.Checked;
        }
        private bool validateDrawing(SelectionSet sset)
        {
            return CheckForOpenBoundary(sset) ;
        }

        private void ExportAEC(SelectionSet sset)
        {
            string folderPath = Path.GetDirectoryName(CADUtilities.GetCurrentDrawingName());

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            AECExportManager expMan = new AECExportManager();
            AECData aecData = expMan.Export(folderPath, sset, progressBar1);

            progressBar1.Value = 95; progressBar1.Update();
            PopulateTreeView(aecData);
            progressBar1.Value = 100; progressBar1.Update();

            webBrowser1.Navigate(folderPath);
        }

        private void PopulateTreeView(AECData aecData)
        {
            treeView1.Nodes.Clear();
            foreach (Room room in aecData.Rooms)
            {
                TreeNode roomNode = new TreeNode(room.Name + " ( Area = " + room.Area + " )");
                roomNode.Tag = room.Handle;
                treeView1.Nodes.Add(roomNode);

                foreach (Wall wall in room.Walls)
                {
                    TreeNode wallNode = new TreeNode(wall.RoomName + "\\" + wall.Name);
                    wallNode.Tag = wall.Handle;
                    roomNode.Nodes.Add(wallNode);

                    foreach (Window window in wall.Windows)
                    {
                        TreeNode windowNode = new TreeNode(wall.RoomName + "\\" + wall.Name + "\\" + window.Name);
                        windowNode.Tag = window.Handle;
                        wallNode.Nodes.Add(windowNode);
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string handle = e.Node.Tag.ToString();

                Zoom.ZoomManager.Zoom2Handle(handle);
            }
            catch{}            
        }


        #region Open Boundary Finder
        double tolerance = 0.1;

        string line_Layer = "Wall";
        private static List<Wall> listOfLineData = new List<Wall>();
        private static List<ObjectId> listOfOthrObjIds = new List<ObjectId>();

        private bool IsAllBoundaryClosed = false;
        public bool CheckForOpenBoundary(SelectionSet sset)
        {
            IsAllBoundaryClosed = false;
            listOfOthrObjIds = new List<ObjectId>();
            listOfLineData = new List<Wall>();
            CADUtilities.SetUCSWorld();

            selectObjects(sset);

            if (listOfLineData.Count == 0)
            {
                string msg = "No of wall is zero and must be a line and layer name is " + line_Layer;
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            treeView1.Nodes.Clear();
            return mthdOfIdentifyOpnBndry(listOfLineData);
        }

        #region Set Input Data From User Selection
        private void selectObjects(SelectionSet sset)
        {
            // Get the current database
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock locDoc = acDoc.LockDocument())
            {
                Editor ed = acDoc.Editor;

                Database db = HostApplicationServices.WorkingDatabase;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    {
                        int count = 0;
                        foreach (SelectedObject so in sset)
                        {
                            count++;

                            ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n PreProcessing {0} of {1}", count, sset.Count);

                            ObjectId objId = so.ObjectId;

                            Entity ent = tr.GetObject(objId, OpenMode.ForWrite) as Entity;

                            if (setLineData(ent, objId) == false)
                            {
                                listOfOthrObjIds.Add(objId);
                            }
                        }
                    }

                    tr.Commit();
                }
            }
        }
        public bool setLineData(Entity ent, ObjectId objId)
        {
            bool flag = false;

            string layer = ent.Layer;

            if (layer == line_Layer)
            {
                flag = true;

                if (ent is Line)
                {
                    Line acLine = ent as Line;

                    double angle = (180 / Math.PI) * acLine.Angle;

                    Wall lineData = new Wall(acLine.StartPoint, acLine.EndPoint, acLine.Length, angle, ent.Handle.ToString());

                    listOfLineData.Add(lineData);
                }
            }

            return flag;
        }

        #endregion

        #region Function is identify to open boundary
        private bool mthdOfIdentifyOpnBndry(List<Wall> listOfLineData)
        {
            IsAllBoundaryClosed = true;

            List<CornerData> listOfCornerData = new List<CornerData>();

            for (int p = 0; p < listOfLineData.Count; p++)
            {
                ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n Validating {0} of {1}", p, listOfLineData.Count);

                Wall crrntLineData = listOfLineData[p];

                CornerData cornerData = new CornerData();

                cornerData.MainLineIndex = p;
                cornerData.MainLineData = crrntLineData;

                bool flag = AreCornerPointsValid(cornerData, crrntLineData);

                HighlightWrongCorner(cornerData, crrntLineData);

                listOfCornerData.Add(cornerData);
            }

            if (IsAllBoundaryClosed == false)
            {
                string msg = "Some of the corners are marked with circle due to not closed. Kindly review.";
                //MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return IsAllBoundaryClosed;
        }
        private bool AreCornerPointsValid(CornerData cornerData, Wall crrntLineData)
        {
            bool flag = false;

            List<Point3d> lstOfCornerPnt = new List<Point3d>();

            List<int> lstOfOtherLineIndex = new List<int>();

            List<Wall> lstOfOtherLineData = new List<Wall>();

            for (int q = 0; q < listOfLineData.Count; q++)
            {
                Wall othrLineData = listOfLineData[q];


                ObjectId crrntLineObjId = CADUtilities.HandleToObjectId(crrntLineData.Handle);

                ObjectId othrLineObjId = CADUtilities.HandleToObjectId(othrLineData.Handle);

                if (crrntLineObjId != othrLineObjId)
                {
                    Point3d cornerPoint = new Point3d();

                    if (IdentityAllCorners(cornerData, crrntLineData, othrLineData, ref cornerPoint))
                    {
                        lstOfOtherLineData.Add(othrLineData);

                        lstOfCornerPnt.Add(cornerPoint);

                        lstOfOtherLineIndex.Add(q);

                        flag = true;
                    }
                }
            }

            cornerData.ListOfCornerPoint = lstOfCornerPnt;
            cornerData.ListOfOtherLineIndex = lstOfOtherLineIndex;
            cornerData.ListOfOtherLineData = lstOfOtherLineData;

            return flag;
        }
        private bool IdentityAllCorners(CornerData cornerData, Wall crrntLineData, Wall othrLineData, ref Point3d cornerPoint)
        {
            bool flag = false;

            double strt_Strt_length = CADUtilities.GetLengthOfLine(crrntLineData.StartPoint, othrLineData.StartPoint);
            if (strt_Strt_length <= tolerance)
            {
                cornerPoint = crrntLineData.StartPoint;
                return true;
            }

            double strt_End_length = CADUtilities.GetLengthOfLine(crrntLineData.StartPoint, othrLineData.EndPoint);
            if (strt_End_length <= tolerance)
            {
                cornerPoint = crrntLineData.StartPoint;
                return true;
            }

            double end_Strt_length = CADUtilities.GetLengthOfLine(crrntLineData.EndPoint, othrLineData.StartPoint);
            if (end_Strt_length <= tolerance)
            {
                cornerPoint = crrntLineData.EndPoint;
                return true;
            }

            double end_End_length = CADUtilities.GetLengthOfLine(crrntLineData.EndPoint, othrLineData.EndPoint);
            if (end_End_length <= tolerance)
            {
                cornerPoint = crrntLineData.EndPoint;
                return true;
            }

            if (flag == false)
            {
                ObjectId crrntLineObjId = CADUtilities.HandleToObjectId(crrntLineData.Handle);

                ObjectId othrLineObjId = CADUtilities.HandleToObjectId(othrLineData.Handle);

                List<Point3d> listOfInsctPnt = CADUtilities.GetIntersectPointBtwTwoIds(crrntLineObjId, othrLineObjId);

                if (listOfInsctPnt.Count != 0)
                {
                    cornerPoint = listOfInsctPnt[0];
                    return true;
                }
            }

            return flag;
        }
        private int HighlightWrongCorner(CornerData cornerData, Wall crrntLineData)
        {
            List<Point3d> lstOfCornerPnt = cornerData.ListOfCornerPoint;

            bool IsStartPoint = false;
            bool IsEndPoint = false;

            if (lstOfCornerPnt?.Count > 0)
            {
                foreach (Point3d cornerPoint in lstOfCornerPnt)
                {
                    if (IsStartPoint == false)
                    {
                        double strt_Strt_length = CADUtilities.GetLengthOfLine(crrntLineData.StartPoint, cornerPoint);
                        if (strt_Strt_length <= tolerance)
                        {
                            IsStartPoint = true;
                        }
                    }

                    if (IsEndPoint == false)
                    {
                        double strt_End_length = CADUtilities.GetLengthOfLine(crrntLineData.EndPoint, cornerPoint);
                        if (strt_End_length <= tolerance)
                        {
                            IsEndPoint = true;
                        }
                    }

                    if (IsStartPoint == true && IsEndPoint == true)
                    {
                        break;
                    }
                }
            }

            List<Point3d> listOfPnt = new List<Point3d>();

            if (IsStartPoint == false)
            {
                listOfPnt.Add(crrntLineData.StartPoint);
            }

            if (IsEndPoint == false)
            {
                listOfPnt.Add(crrntLineData.EndPoint);
            }

            foreach (Point3d pnt in listOfPnt)
            {
                IsAllBoundaryClosed = false;
                string errCircleHandle = CADUtilities.CreateCircle(pnt, 100, "Error", 3);

                TreeNode errNode = new TreeNode("Error");
                errNode.Tag = errCircleHandle;
                treeView1.Nodes.Add(errNode);
            }

            return listOfPnt.Count;
        }

        #endregion
        #endregion

    }
}
