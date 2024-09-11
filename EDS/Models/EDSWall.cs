using EDS.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using EDS.AEC;
using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;
using System.Windows.Shapes;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;
using System.Reflection;
using System.IO;
using OfficeOpenXml;

namespace EDS.Models
{
    public class EDSWall
    {
        public string extWallType { get; set; }
        public string intWallType { get; set; }
        public string uValue { get; set; }
        public string eDS1Faces1 { get; set; }
        public string eDS1Faces2 { get; set; }
        public string eDS1Faces3 { get; set; }
        public string eDS2Faces1 { get; set; }
        public string eDS2Faces2 { get; set; }
        public string eDS2Faces3 { get; set; }
        public string wallHandleId { get; set; }

        List<EDSExcelRoom> rooms = new List<EDSExcelRoom>();
        public string uValueCheck { get; set; }

        List<ObjectId> erasedPolylineIds = new List<ObjectId>();

        public EDSWall()
        {

        }

        public EDSWall(string extWallType, string intWallType, string uValue, string eDS1Faces1, string eDS1Faces2, string eDS1Faces3, string eDS2Faces1, string eDS2Faces2, string eDS2Faces3, string uValueCheck)
        {
            this.extWallType = extWallType;
            this.intWallType = intWallType;
            this.uValue = uValue;
            this.eDS1Faces1 = eDS1Faces1;
            this.eDS1Faces2 = eDS1Faces2;
            this.eDS1Faces3 = eDS1Faces3;
            this.eDS2Faces1 = eDS2Faces1;
            this.eDS2Faces2 = eDS2Faces2;
            this.eDS2Faces3 = eDS2Faces3;
            this.uValueCheck = uValueCheck;
        }

        public void CreateWall(EDSWall wall)
        {
            try
            {
                EDSCreation.CreateLayer(StringConstants.wallLayerName, 1);
                CreateWallLine(wall);

            }
            catch (System.Exception ex)
            {

            }
        }

        public void CreateWallLine(EDSWall dSWall)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            List<Point3d> points = new List<Point3d>();
            //List<Point3d> points = new List<Point3d>();
            PromptPointOptions ppo = new PromptPointOptions("\nSpecify first point or [Exit]: ");
            PromptPointResult ppr = doc.Editor.GetPoint(ppo);

            if (ppr.Status == PromptStatus.OK)
            {
                points.Add(ppr.Value);
            }


            while (ppr.Status == PromptStatus.OK)
            {
                ppo.Message = "\nSpecify next point or [Exit]: ";
                ppo.UseBasePoint = true;
                ppo.BasePoint = ppr.Value;
                ppr = doc.Editor.GetPoint(ppo);

                if (ppr.Status == PromptStatus.OK)
                {
                    points.Add(ppr.Value);

                    using (doc.LockDocument())
                    {
                        if (points.Count > 1)
                        {
                            using (Transaction tr = db.TransactionManager.StartTransaction())
                            {
                                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


                                Line line = new Line();
                                line.StartPoint = new Point3d(points[0].X, points[0].Y, 0);
                                line.EndPoint = new Point3d(points[0 + 1].X, points[0 + 1].Y, 0);

                                line.Layer = StringConstants.wallLayerName;

                                btr.AppendEntity(line);
                                tr.AddNewlyCreatedDBObject(line, true);

                                SetXDataForLine(dSWall, line);

                                tr.Commit();
                            }
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nNot enough points to create a line.");
                        }
                    }

                    points.RemoveAt(0);
                }
            }


        }

        public void SetXDataForLine(EDSWall dSWall, Line line)
        {
            CADUtilities.SetXData(line.ObjectId, StringConstants.extWallType, dSWall.extWallType);
            CADUtilities.SetXData(line.ObjectId, StringConstants.intWallType, dSWall.intWallType);
            CADUtilities.SetXData(line.ObjectId, StringConstants.uValue, dSWall.uValue);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS1Faces1, dSWall.eDS1Faces1);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS1Faces2, dSWall.eDS1Faces2);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS1Faces3, dSWall.eDS1Faces3);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS2Faces1, dSWall.eDS2Faces1);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS2Faces2, dSWall.eDS2Faces2);
            CADUtilities.SetXData(line.ObjectId, StringConstants.eDS2Faces3, dSWall.eDS2Faces3);
            CADUtilities.SetXData(line.ObjectId, StringConstants.uValueCheck, dSWall.uValueCheck);
            CADUtilities.SetXData(line.ObjectId, StringConstants.wallHandleId, line.ObjectId.Handle.ToString());
        }

        public void UpdateLine(EDSWall wall)
        {
            EDSCreation.CreateLayer(StringConstants.wallLayerName, 1);

            List<string> linesLayer = new List<string>();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            // Define the selection filter for lines
            TypedValue[] filter = new TypedValue[1];
            filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

            // Create the selection filter
            SelectionFilter selectionFilter = new SelectionFilter(filter);

            PromptSelectionResult res = ed.SelectImplied(); ;

            if (res.Value == null)
            {
                System.Windows.Forms.MessageBox.Show("No items selected for update" + "\n" + "Please select the walls for update");
                PromptSelectionOptions targetOptions = new PromptSelectionOptions();
                targetOptions.MessageForAdding = "\nSelect target objects: ";
                PromptSelectionResult targetResult = ed.GetSelection(targetOptions, selectionFilter);

                doc.LockDocument();

                if (targetResult.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = targetResult.Value;
                    using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                    {
                        foreach (SelectedObject selObj in selectionSet)
                        {
                            if (selObj != null)
                            {
                                Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForRead);
                                Line line = entity as Line;
                                if (line != null)
                                {
                                    linesLayer.Add(line.Layer);
                                }
                            }
                        }
                        tr.Commit();
                    }

                    if (!linesLayer.All(x => x.Equals(StringConstants.wallLayerName)))
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("We have found lines in the different layer." + "\n" + "Do you want to move lines to ZWall layer?", "Warning", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                            {
                                foreach (SelectedObject selObj in selectionSet)
                                {
                                    if (selObj != null)
                                    {
                                        Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                        Line line = entity as Line;
                                        if (line != null)
                                        {
                                            line.Layer = StringConstants.wallLayerName;
                                            SetXDataForLine(wall, line);
                                        }
                                    }
                                }
                                tr.Commit();
                            }
                        }
                        else
                        {
                            using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                            {
                                foreach (SelectedObject selObj in selectionSet)
                                {
                                    if (selObj != null)
                                    {
                                        Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                        Line line = entity as Line;
                                        if (line != null)
                                        {
                                            if (line.Layer == StringConstants.wallLayerName)
                                            {
                                                SetXDataForLine(wall, line);
                                            }
                                        }
                                    }
                                }
                                tr.Commit();
                            }
                        }
                    }
                    else
                    {
                        using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                        {
                            foreach (SelectedObject selObj in selectionSet)
                            {
                                if (selObj != null)
                                {
                                    Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                    Line line = entity as Line;
                                    if (line != null)
                                    {
                                        if (line.Layer == StringConstants.wallLayerName)
                                        {
                                            SetXDataForLine(wall, line);
                                        }
                                    }
                                }
                            }
                            tr.Commit();
                        }
                    }
                    ed.SetImpliedSelection(new ObjectId[0]);
                }
            }
            else
            {
                doc.LockDocument();

                if (res.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = res.Value;
                    using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                    {
                        foreach (SelectedObject selObj in selectionSet)
                        {
                            if (selObj != null)
                            {
                                Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForRead);
                                Line line = entity as Line;
                                if (line != null)
                                {
                                    linesLayer.Add(line.Layer);
                                }
                            }
                        }
                        tr.Commit();
                    }

                    if (!linesLayer.All(x => x.Equals(StringConstants.wallLayerName)))
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("We have found lines in the different layer." + "\n" + "Do you want to move lines to ZWall layer?", "Warning", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                            {
                                foreach (SelectedObject selObj in selectionSet)
                                {
                                    if (selObj != null)
                                    {
                                        Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                        Line line = entity as Line;
                                        if (line != null)
                                        {
                                            line.Layer = StringConstants.wallLayerName;
                                            SetXDataForLine(wall, line);
                                        }
                                    }
                                }
                                tr.Commit();
                            }
                        }
                        else
                        {
                            using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                            {
                                foreach (SelectedObject selObj in selectionSet)
                                {
                                    if (selObj != null)
                                    {
                                        Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                        Line line = entity as Line;
                                        if (line != null)
                                        {
                                            if (line.Layer == StringConstants.wallLayerName)
                                            {
                                                SetXDataForLine(wall, line);
                                            }
                                        }
                                    }
                                }
                                tr.Commit();
                            }
                        }
                    }
                    else
                    {
                        using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                        {
                            foreach (SelectedObject selObj in selectionSet)
                            {
                                if (selObj != null)
                                {
                                    Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForWrite);
                                    Line line = entity as Line;
                                    if (line != null)
                                    {
                                        if (line.Layer == StringConstants.wallLayerName)
                                        {
                                            SetXDataForLine(wall, line);
                                        }
                                    }
                                }
                            }
                            tr.Commit();
                        }
                    }
                    ed.SetImpliedSelection(new ObjectId[0]);
                }
            }
        }

        public List<EDSWall> GetWallLine()
        {
            List<EDSWall> walls = new List<EDSWall>();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            // Define the selection filter for lines
            TypedValue[] filter = new TypedValue[1];
            filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

            // Create the selection filter
            SelectionFilter selectionFilter = new SelectionFilter(filter);

            // Prompt for the selection
            PromptSelectionOptions opts = new PromptSelectionOptions();
            opts.MessageForAdding = "Select lines: ";
            PromptSelectionResult res = ed.GetSelection(opts, selectionFilter);

            // Check the result status
            if (res.Status == PromptStatus.OK)
            {
                SelectionSet selectionSet = res.Value;

                // Iterate through the selected lines
                using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                {
                    foreach (SelectedObject selObj in selectionSet)
                    {
                        if (selObj != null)
                        {
                            Entity entity = (Entity)tr.GetObject(selObj.ObjectId, OpenMode.ForRead);
                            Line line = entity as Line;
                            if (line != null)
                            {
                                //ed.WriteMessage($"\nLine: Start Point({line.StartPoint}), End Point({line.EndPoint})");
                                if (line.Layer == StringConstants.wallLayerName)
                                    walls.Add(GetXDataForLine(line));
                            }
                        }
                    }
                    tr.Commit();
                }
            }
            else
            {
                ed.WriteMessage("\nSelection canceled or no lines selected.");
            }

            ed.SetImpliedSelection(new ObjectId[0]);

            return walls;
        }

        public EDSWall GetXDataForLine(Line line)
        {
            EDSWall xData = new EDSWall();

            xData.extWallType = CADUtilities.GetXData(line.ObjectId, StringConstants.extWallType);
            xData.intWallType = CADUtilities.GetXData(line.ObjectId, StringConstants.intWallType);
            xData.uValue = CADUtilities.GetXData(line.ObjectId, StringConstants.uValue);
            xData.eDS1Faces1 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS1Faces1);
            xData.eDS1Faces2 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS1Faces2);
            xData.eDS1Faces3 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS1Faces3);
            xData.eDS2Faces1 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS2Faces1);
            xData.eDS2Faces2 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS2Faces2);
            xData.eDS2Faces3 = CADUtilities.GetXData(line.ObjectId, StringConstants.eDS2Faces3);
            xData.uValueCheck = CADUtilities.GetXData(line.ObjectId, StringConstants.uValueCheck);
            xData.wallHandleId = CADUtilities.GetXData(line.ObjectId, StringConstants.wallHandleId);

            return xData;
        }

        public void FindClosedLoop(System.Windows.Forms.TreeView treeView)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            doc.LockDocument();
            #region GenericLogicFromClosedLoop

            //doc.LockDocument();
            //using (Transaction transaction = doc.TransactionManager.StartTransaction())
            //{
            //    BlockTable blockTable = (BlockTable)transaction.GetObject(db.BlockTableId, OpenMode.ForRead);
            //    BlockTableRecord modelSpace = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


            //    foreach (ObjectId objId in modelSpace)
            //    {
            //        Entity entity = transaction.GetObject(objId, OpenMode.ForRead) as Entity;
            //        if (entity is Line)
            //        {
            //            lines.Add(entity as Line);
            //        }
            //    }
            //}

            //foreach (Line line in lines)
            //{
            //    var midPoint = GetLineMidpoint(line);
            //    DetectClosedLoops(new Point3d(midPoint.X + 5, midPoint.Y, midPoint.Z));
            //    DetectClosedLoops(new Point3d(midPoint.X - 5, midPoint.Y, midPoint.Z));
            //    DetectClosedLoops(new Point3d(midPoint.X, midPoint.Y + 5, midPoint.Z));
            //    DetectClosedLoops(new Point3d(midPoint.X, midPoint.Y - 5, midPoint.Z));
            //}
            #endregion
            PromptSelectionResult res;
            if (ed.SelectImplied().Value == null)
            {
                // Prompt for the selection
                PromptSelectionOptions opts = new PromptSelectionOptions();
                opts.MessageForAdding = "Select lines: ";
                res = ed.GetSelection(opts);
            }
            else
            {
                res = ed.SelectImplied();
            }

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                List<Line> wallLines = new List<Line>();
                List<EDSRoomTag> edsRooms = new List<EDSRoomTag>();
                List<Polyline> windowLines = new List<Polyline>();
                if (res.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = res.Value;

                    ExportValidation exportValidation = new ExportValidation();

                    foreach (SelectedObject so in selectionSet)
                    {
                        ObjectId objId = so.ObjectId;

                        Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;
                        if (entity is Line)
                        {
                            if (entity.Layer == StringConstants.wallLayerName)
                                wallLines.Add(entity as Line);
                        }
                    }
                    Point3dCollection intersections = exportValidation.FindIntersections(wallLines, tr);

                    if (intersections.Count == 0)
                    {
                        ed.WriteMessage("\nNo intersection points found.");
                        return;
                    }

                    ed.WriteMessage($"\nIntersection point count: {intersections.Count}");

                    // Break lines at each intersection point
                    int i = 0;
                    foreach (Point3d intersection in intersections)
                    {
                        exportValidation.BreakLinesAtPoint(wallLines, intersection, tr);
                        i++;

                        // Progress reporting
                        double progress = (double)i / intersections.Count * 100;
                        ed.WriteMessage($"\n{progress:0.00}% processed...");
                    }

                    exportValidation.IdentifyOpenLoopLinesWithCircles(db, selectionSet, treeView);


                    if (treeView.Nodes.Count == 0)
                    {

                        // Collect all lines from the drawing
                        foreach (SelectedObject so in selectionSet)
                        {
                            ObjectId objId = so.ObjectId;

                            Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;
                            //if (entity is Line)
                            //{
                            //    if (entity.Layer == StringConstants.wallLayerName)
                            //        wallLines.Add(entity as Line);
                            //}
                            if (entity is DBText)
                            {
                                if (entity.Layer == StringConstants.roomLayerName)
                                {
                                    EDSRoomTag tag = new EDSRoomTag();
                                    edsRooms.Add(tag.GetXDataForRoom(entity.ObjectId));
                                }
                            }
                            if (entity is Polyline)
                            {
                                if (entity.Layer == StringConstants.windowLayerName)
                                {
                                    windowLines.Add(entity as Polyline);
                                }
                            }
                        }

                        if (edsRooms.Count > 0)
                        {
                            CreateTreeNodes(edsRooms, wallLines, windowLines, treeView);

                        }
                        else
                        {
                            System.Windows.MessageBox.Show("No rooms found");
                            //CreateTreeNode(wallLines, windowLines,treeView);
                        }



                        if (rooms.Count > 0)
                        {
                            string dllPath = Assembly.GetExecutingAssembly().Location;
                            string directoryName = System.IO.Path.GetDirectoryName(dllPath);

                            string fileName = System.IO.Path.Combine(directoryName, "CAD Output Template.xlsx");

                            var name = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name;

                            if (!System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), "CAD Output Template.xlsx")))
                                System.IO.File.Copy(fileName, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), "CAD Output Template.xlsx"));
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var cadOutputFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), "CAD Output Template.xlsx");

                            using (ExcelPackage package = new ExcelPackage(cadOutputFile))
                            {
                                //get the first worksheet in the workbook
                                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                                worksheet.Cells[1, 4].Value = "Project1";
                                worksheet.Cells[2, 4].Value = "Text";
                                worksheet.Cells[3, 4].Value = "Text";

                                int rowCount = 8;

                                foreach (var room in rooms)
                                {
                                    for (int iNo = 0; iNo < room.walls.Count; iNo++)
                                    {
                                        worksheet.Cells[rowCount, 3].Value = room.walls[iNo].wall.wallHandleId;
                                        worksheet.Cells[rowCount, 4].Value = room.room.spaceType.ToString() + "_" + room.walls[iNo].wall.wallHandleId;
                                    }
                                }

                                package.Save();
                            }
                        }
                    }
                }
                tr.Commit();
            }
        }

        private void CreateTreeNodes(List<EDSRoomTag> edsRooms, List<Line> wallLines, List<Polyline> windowLines, TreeView treeView)
        {
            foreach (var room in edsRooms)
            {
                Dictionary<ObjectId, List<Polyline>> wallWindows = new Dictionary<ObjectId, List<Polyline>>();
                room.allWalls = new List<Line>();
                var objectId = CADUtilities.HandleToObjectId(room.textHandleId);
                if (objectId != null)
                {
                    var roomLines = GetRoomLines(objectId, room);

                    if (roomLines != null)
                    {
                        foreach (var rmLine in roomLines)
                        {
                            var point = GetMidpoint(rmLine);
                            //var matchLines = wallLines.FindAll(x => x.Length.Equals(rmLine.Length));
                            foreach (var macLine in wallLines)
                            {
                                if (/*LinesOverlap(rmLine, macLine) && AreLinesParallel(rmLine, macLine)*/IsPointOnLine(macLine, point))
                                {
                                    if (!room.allWalls.Any(x => x.ObjectId == macLine.ObjectId))
                                    {
                                        room.allWalls.Add(macLine);
                                        FindWindowsForWall(macLine.Id, windowLines, ref wallWindows);
                                        break;
                                    }

                                }
                            }
                        }
                        room.allWindows = wallWindows;
                    }
                }
            }


            TreeNode roomNodes = new TreeNode("Rooms");
            treeView.Nodes.Clear();

            rooms = new List<EDSExcelRoom>();
            foreach (var eds in edsRooms)
            {
                EDSExcelRoom excelFormat = new EDSExcelRoom();
                excelFormat.walls = new List<EDSExcelWall>();
                excelFormat.room = eds;

                int exCount = 1;
                int inCount = 1;
                int windowCount = 1;

                EDSWall wall1 = new EDSWall();

                TreeNode treeNode = new TreeNode("Walls");

                TreeNode externalNode = new TreeNode("External Walls");
                treeNode.Nodes.Add(externalNode);

                TreeNode internalNode = new TreeNode("Internal Walls");
                treeNode.Nodes.Add(internalNode);

                TreeNode roomNode = new TreeNode(eds.spaceType + " (" + Math.Round(double.Parse(eds.roomArea)).ToString() + ")");
                roomNode.Tag = eds.curveHandleId;
                roomNode.Nodes.Add(treeNode);

                roomNodes.Nodes.Add(roomNode);

                foreach (Line line in eds.allWalls)
                {
                    EDSExcelWall eDSExcelWall = new EDSExcelWall();
                    var wall = GetXDataForLine(line);
                    eDSExcelWall.wall = wall;

                    if (bool.Parse(wall.uValueCheck) == false)
                    {
                        TreeNode child = new TreeNode("Line " + exCount.ToString());
                        child.Tag = line.Handle.ToString();

                        if (eds.allWindows.ContainsKey(line.ObjectId))
                        {
                            EDSWindow window = new EDSWindow();

                            eDSExcelWall.windows = new List<EDSWindow>();
                            foreach (var win in eds.allWindows[line.ObjectId])
                            {
                                TreeNode windowNode = new TreeNode("Window " + windowCount.ToString());
                                windowNode.Tag = win.Handle.ToString();
                                child.Nodes.Add(windowNode);
                                eDSExcelWall.windows.Add(window.GetXDataForWindow(win.ObjectId));
                                windowCount++;
                            }
                        }
                        excelFormat.walls.Add(eDSExcelWall);
                        externalNode.Nodes.Add(child);

                        exCount++;
                    }
                    else
                    {
                        TreeNode child = new TreeNode("Line " + inCount.ToString());
                        child.Tag = line.Handle.ToString();

                        if (eds.allWindows.ContainsKey(line.ObjectId))
                        {
                            foreach (var win in eds.allWindows[line.ObjectId])
                            {
                                TreeNode windowNode = new TreeNode("Window " + windowCount.ToString());
                                windowNode.Tag = win.Handle.ToString();
                                child.Nodes.Add(windowNode);

                                windowCount++;
                            }
                        }
                        excelFormat.walls.Add(eDSExcelWall);
                        internalNode.Nodes.Add(child);

                        inCount++;
                    }
                }
                rooms.Add(excelFormat);
            }

            treeView.Nodes.Add(roomNodes);

        }

        private void ManageWindowVisiblity(List<Polyline> windowLines, bool visible)
        {
            foreach (Polyline windowData in windowLines)
            {
                CADUtilities.ChangeVisibility(CADUtilities.HandleToObjectId(windowData.Id.Handle.ToString()), visible);
            }
        }

        void FindWindowsForWall(ObjectId objectId, List<Polyline> windowLines, ref Dictionary<ObjectId, List<Polyline>> wallWindows)
        {
            List<Polyline> objectIds = new List<Polyline>();
            foreach (var line in windowLines)
            {
                if (CADUtilities.GetIntersectPointBtwTwoIds(objectId, line.Id).Count != 0)
                {
                    objectIds.Add(line);
                }
            }

            if (!wallWindows.ContainsKey(objectId))
                wallWindows.Add(objectId, objectIds);
            else
                wallWindows[objectId].AddRange(objectIds);
        }

        private List<Line> GetRoomLines(ObjectId objectId, EDSRoomTag roomTag)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;

            doc.LockDocument();
            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = transaction.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


                var dbText = transaction.GetObject(objectId, OpenMode.ForRead) as DBText;
                DBObjectCollection dBObject = editor.TraceBoundary(dbText.Position, true);
                if (dBObject.Count == 0)
                {
                    string msg = "Selected walls are not close properly. Kindly review it and run the tool again.";

                    System.Windows.MessageBox.Show(msg);
                }
                else
                {
                    foreach (DBObject obj in dBObject)
                    {
                        if (obj is Polyline)
                        {
                            var polyline = obj as Polyline;
                            roomTag.roomArea = polyline.Area.ToString();
                            var lines = new List<Line>();
                            DBObjectCollection objectCollection = new DBObjectCollection();
                            polyline.Explode(objectCollection);
                            foreach (Entity acEnt in objectCollection)
                            {
                                if (acEnt is Line)
                                {
                                    Line acLine = acEnt as Line;

                                    lines.Add(acLine);
                                }
                            }

                            if (lines.Count > 0)
                            {
                                polyline.Layer = StringConstants.windowLayerName;
                                //polyline.Visible = false;
                                //var id = blockTableRecord.AppendEntity(polyline);
                                //transaction.AddNewlyCreatedDBObject(polyline, false);

                                //roomTag.curveHandleId = id.Handle.ToString();
                                return lines;
                            }

                        }
                    }
                }
                transaction.Commit();
            }

            return null;
        }

        private void CreateTreeNode(List<Line> wallLines, List<Polyline> windowLines, TreeView treeView)
        {
            int exCount = 1;
            int inCount = 1;
            int windowCount = 1;

            treeView.Nodes.Clear();
            TreeNode treeNode = new TreeNode("Walls");

            TreeNode externalNode = new TreeNode("External Walls");
            treeNode.Nodes.Add(externalNode);

            TreeNode internalNode = new TreeNode("Internal Walls");
            treeNode.Nodes.Add(internalNode);


            treeView.Nodes.Add(treeNode);
            foreach (Line line in wallLines)
            {
                var wall = GetXDataForLine(line);
                if (bool.Parse(wall.uValueCheck) == false)
                {
                    TreeNode child = new TreeNode("Line " + exCount.ToString());
                    child.Tag = line.Handle.ToString();
                    externalNode.Nodes.Add(child);

                    exCount++;
                }
                else
                {
                    TreeNode child = new TreeNode("Line " + inCount.ToString());
                    child.Tag = line.Handle.ToString();
                    internalNode.Nodes.Add(child);

                    inCount++;
                }
            }
        }

        public void MatchLine()
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            doc.LockDocument();
            using (Transaction transaction = doc.Database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for lines
                TypedValue[] filter = new TypedValue[1];
                filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                PromptEntityOptions sourceOptions = new PromptEntityOptions("\nSelect source object: ");
                PromptEntityResult sourceResult = ed.GetEntity(sourceOptions);

                if (sourceResult.Status != PromptStatus.OK)
                    return;

                // Retrieve the source object
                Entity sourceEntity = (Entity)transaction.GetObject(sourceResult.ObjectId, OpenMode.ForRead);

                // Get properties of the source entity
                string layerName = sourceEntity.Layer;
                var sourceData = GetXDataForLine(sourceEntity as Line);
                // Add more properties as needed

                // Prompt user to select target objects
                PromptSelectionOptions targetOptions = new PromptSelectionOptions();
                targetOptions.MessageForAdding = "\nSelect target objects: ";
                PromptSelectionResult targetResult = ed.GetSelection(targetOptions, selectionFilter);

                if (targetResult.Status != PromptStatus.OK)
                    return;

                // Apply properties to each target object
                foreach (ObjectId id in targetResult.Value.GetObjectIds())
                {
                    Entity targetEntity = (Entity)transaction.GetObject(id, OpenMode.ForWrite);
                    targetEntity.UpgradeOpen();
                    // Set properties from source entity
                    targetEntity.Layer = layerName;
                    SetXDataForLine(sourceData, targetEntity as Line);
                    targetEntity.DowngradeOpen();
                    // Apply more properties as needed
                }

                transaction.Commit();
            }
        }

        private bool LinesOverlap(Line line1, Line line2)
        {
            Point3d intersection;

            Point3dCollection intersectionCollection = new Point3dCollection();

            line1.IntersectWith(line2, Intersect.OnBothOperands, intersectionCollection, IntPtr.Zero, IntPtr.Zero);

            if (intersectionCollection.Count == 0)
                return false;
            else
                intersection = intersectionCollection[0];

            // Check if the intersection point is within the bounds of both line segments
            bool IsPointOnLineSegment(Line line, Point3d point)
            {
                double minX = Math.Min(line.StartPoint.X, line.EndPoint.X);
                double maxX = Math.Max(line.StartPoint.X, line.EndPoint.X);
                double minY = Math.Min(line.StartPoint.Y, line.EndPoint.Y);
                double maxY = Math.Max(line.StartPoint.Y, line.EndPoint.Y);

                return point.X >= minX && point.X <= maxX &&
                       point.Y >= minY && point.Y <= maxY;
            }

            return IsPointOnLineSegment(line1, intersection) && IsPointOnLineSegment(line2, intersection);
        }

        private bool AreLinesParallel(Line line1, Line line2)
        {
            Vector2d vector1 = new Vector2d(line1.EndPoint.X - line1.StartPoint.X, line1.EndPoint.Y - line1.StartPoint.Y);
            Vector2d vector2 = new Vector2d(line2.EndPoint.X - line2.StartPoint.X, line2.EndPoint.Y - line2.StartPoint.Y);

            // Check if the cross product is zero
            double crossProduct = vector1.X * vector2.Y - vector1.Y * vector2.X;
            return Math.Abs(crossProduct) < 1e-10; // Small epsilon for floating point comparison
        }

        public Point3d GetMidpoint(Line line)
        {
            Point3d startPoint = line.StartPoint;
            Point3d endPoint = line.EndPoint;

            // Calculate the midpoint
            Point3d midpoint = new Point3d(
                (startPoint.X + endPoint.X) / 2.0,
                (startPoint.Y + endPoint.Y) / 2.0,
                (startPoint.Z + endPoint.Z) / 2.0
            );

            return midpoint;
        }

        // Method to check if a point lies on a given line
        public bool IsPointOnLine(Line line, Point3d point)
        {
            Point3d startPoint = line.StartPoint;
            Point3d endPoint = line.EndPoint;

            // Check if the point is collinear with the line
            Vector3d lineVector = endPoint - startPoint;
            Vector3d pointVector = point - startPoint;

            double crossProduct = lineVector.CrossProduct(pointVector).Length;
            if (Math.Abs(crossProduct) > Tolerance.Global.EqualPoint)
                return false;

            // Check if the point lies within the line segment
            double dotProduct = pointVector.DotProduct(lineVector);
            if (dotProduct < 0 || dotProduct > lineVector.LengthSqrd)
                return false;

            return true;
        }
    }
}
