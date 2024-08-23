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

        public string uValueCheck { get; set; }

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

        private void SetXDataForLine(EDSWall dSWall, Line line)
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

        public void FindClosedLoop()
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

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
                List<Line> lines = new List<Line>();
                List<EDSRoomTag> edsRooms = new List<EDSRoomTag>();
                if (res.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = res.Value;

                    // Collect all lines from the drawing
                    foreach (SelectedObject so in selectionSet)
                    {
                        ObjectId objId = so.ObjectId;

                        Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;
                        if (entity is Line)
                        {
                            if (entity.Layer == StringConstants.wallLayerName)
                                lines.Add(entity as Line);
                        }
                        if (entity is DBText)
                        {
                            if (entity.Layer == StringConstants.roomLayerName)
                            {
                                EDSRoomTag tag = new EDSRoomTag();
                                edsRooms.Add(tag.GetXDataForRoom(entity.ObjectId));
                            }
                        }
                    }

                    if (edsRooms.Count > 0)
                    {
                        CreateTreeNodes(edsRooms, lines);

                    }
                    else
                    {
                        CreateTreeNode(lines);
                    }

                    #region HiglightLogicForClosedLoop

                    //// Create an adjacency list
                    //Dictionary<Point3d, List<Line>> adjacencyList = new Dictionary<Point3d, List<Line>>();

                    //foreach (Line line in lines)
                    //{
                    //    if (!adjacencyList.ContainsKey(line.StartPoint))
                    //    {
                    //        adjacencyList[line.StartPoint] = new List<Line>();
                    //    }
                    //    if (!adjacencyList.ContainsKey(line.EndPoint))
                    //    {
                    //        adjacencyList[line.EndPoint] = new List<Line>();
                    //    }

                    //    adjacencyList[line.StartPoint].Add(line);
                    //    adjacencyList[line.EndPoint].Add(line);
                    //}

                    //// Find all possible loops using DFS
                    //List<List<Line>> loops = new List<List<Line>>();
                    //HashSet<Line> visitedLines = new HashSet<Line>();

                    //foreach (Line line in lines)
                    //{
                    //    if (!visitedLines.Contains(line))
                    //    {
                    //        List<Line> currentLoop = new List<Line>();
                    //        FindLoops(adjacencyList, line, line.StartPoint, line.StartPoint, currentLoop, loops, visitedLines);
                    //    }
                    //}

                    //// Calculate areas of the loops and find the best loop
                    //double maxArea = double.NegativeInfinity;
                    //List<Line> bestLoop = null;

                    //foreach (var loop in loops)
                    //{
                    //    double area = CalculateLoopArea(loop);
                    //    if (area > maxArea)
                    //    {
                    //        maxArea = area;
                    //        bestLoop = loop;
                    //    }
                    //}

                    //// Output the best loop
                    //if (bestLoop != null)
                    //{
                    //    foreach (Line loopLine in bestLoop)
                    //    {
                    //        loopLine.UpgradeOpen();
                    //        loopLine.Color = Color.FromColor(System.Drawing.Color.Yellow);
                    //        loopLine.DowngradeOpen();
                    //        doc.Editor.WriteMessage($"\nLine from {loopLine.StartPoint} to {loopLine.EndPoint}");
                    //    }
                    //    doc.Editor.WriteMessage($"\nMaximum Area: {maxArea}");
                    //}
                    //else
                    //{
                    //    doc.Editor.WriteMessage("\nNo closed loops found.");
                    //} 
                    #endregion

                }
                tr.Commit();
            }
        }

        private void CreateTreeNodes(List<EDSRoomTag> edsRooms, List<Line> lines)
        {
            foreach (var room in edsRooms)
            {
                room.allWalls = new List<Line>();
                var objectId = CADUtilities.HandleToObjectId(room.textHandleId);
                if (objectId != null)
                {
                    var roomLines = GetRoomLines(objectId, room);
                    if (roomLines != null)
                    {
                        foreach (var rmLine in roomLines)
                        {
                            var matchLines = lines.FindAll(x => x.Length.Equals(rmLine.Length));
                            foreach (var macLine in matchLines)
                            {
                                if (!room.allWalls.Any(x => x.ObjectId == macLine.ObjectId))
                                {
                                    if (rmLine.StartPoint.IsEqualTo(macLine.StartPoint) && rmLine.EndPoint.IsEqualTo(macLine.EndPoint))
                                    {
                                        room.allWalls.Add(macLine);
                                        break;
                                    }
                                    else if (rmLine.EndPoint.IsEqualTo(macLine.StartPoint) && rmLine.StartPoint.IsEqualTo(macLine.EndPoint))
                                    {
                                        room.allWalls.Add(macLine);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            TreeNode roomNodes = new TreeNode("Rooms");
            WallDataPalette.treeView1.Nodes.Clear();

            foreach (var eds in edsRooms)
            {
                int exCount = 1;
                int inCount = 1;

                TreeNode treeNode = new TreeNode("Walls");

                TreeNode externalNode = new TreeNode("External Walls");
                treeNode.Nodes.Add(externalNode);

                TreeNode internalNode = new TreeNode("Internal Walls");
                treeNode.Nodes.Add(internalNode);

                TreeNode roomNode = new TreeNode(eds.spaceType + " (" + eds.roomArea + ")");
                roomNode.Tag = eds.curveHandleId;
                roomNode.Nodes.Add(treeNode);

                roomNodes.Nodes.Add(roomNode);

                foreach (Line line in eds.allWalls)
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

            WallDataPalette.treeView1.Nodes.Add(roomNodes);
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
                DBObjectCollection dBObject = editor.TraceBoundary(dbText.Position, false);
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
                                polyline.Visible = false;
                                var id = blockTableRecord.AppendEntity(polyline);
                                transaction.AddNewlyCreatedDBObject(polyline, false);

                                roomTag.curveHandleId = id.Handle.ToString();
                                return lines;
                            }

                        }
                    }
                }
                transaction.Commit();
            }

            return null;
        }

        private void CreateTreeNode(List<Line> lines)
        {
            int exCount = 1;
            int inCount = 1;
            WallDataPalette.treeView1.Nodes.Clear();
            TreeNode treeNode = new TreeNode("Walls");

            TreeNode externalNode = new TreeNode("External Walls");
            treeNode.Nodes.Add(externalNode);

            TreeNode internalNode = new TreeNode("Internal Walls");
            treeNode.Nodes.Add(internalNode);


            WallDataPalette.treeView1.Nodes.Add(treeNode);
            foreach (Line line in lines)
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


    }
}
