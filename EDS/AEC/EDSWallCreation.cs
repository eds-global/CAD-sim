using EDS.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;
using static System.Windows.Forms.LinkLabel;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;

namespace EDS.AEC
{
    public class EDSWallCreation
    {
        public static string layerName = "ZWall";

        public static string extWallType = "ExtWall";
        public static string intWallType = "IntWall";
        public static string uValue = "UValue";
        public static string uValueCheck = "UValueCheck";
        public static string eDS1Faces1 = "Face1Type1";
        public static string eDS1Faces2 = "Face1Type2";
        public static string eDS1Faces3 = "Face1Type3";
        public static string eDS2Faces1 = "Face2Type1";
        public static string eDS2Faces2 = "Face2Type2";
        public static string eDS2Faces3 = "Face2Type3";

        public EDSWallCreation() { }

        public void CreateWall(EDSWall wall)
        {
            try
            {
                CreateLayer();

                CreateWallLine(wall);
            }
            catch (System.Exception ex)
            {

            }
        }

        public bool CreateLayer()
        {
            try
            {
                Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database db = doc.Database;
                // Start a transaction
                using (doc.LockDocument())
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        // Get the Layer table from the current database
                        LayerTable layerTable = (LayerTable)trans.GetObject(HostApplicationServices.WorkingDatabase.LayerTableId, OpenMode.ForRead);

                        // Check if the layer already exists
                        if (!layerTable.Has(layerName))
                        {
                            // Open the layer table for write
                            layerTable.UpgradeOpen();

                            // Create a new layer table record
                            LayerTableRecord newLayer = new LayerTableRecord
                            {
                                Name = layerName,
                                Color = Color.FromColorIndex(ColorMethod.ByAci, 1) // 1 = red color
                            };

                            // Add the new layer to the layer table
                            layerTable.Add(newLayer);

                            // Add the new layer table record to the transaction
                            trans.AddNewlyCreatedDBObject(newLayer, true);
                        }

                        // Commit the transaction
                        trans.Commit();
                    }
                }

                Console.WriteLine("Layer created successfully.");

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false;
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

                                line.Layer = layerName;

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

        private static void SetXDataForLine(EDSWall dSWall, Line line)
        {
            CADUtilities.SetXData(line.ObjectId, extWallType, dSWall.extWallType);
            CADUtilities.SetXData(line.ObjectId, intWallType, dSWall.intWallType);
            CADUtilities.SetXData(line.ObjectId, uValue, dSWall.uValue);
            CADUtilities.SetXData(line.ObjectId, eDS1Faces1, dSWall.eDS1Faces1);
            CADUtilities.SetXData(line.ObjectId, eDS1Faces2, dSWall.eDS1Faces2);
            CADUtilities.SetXData(line.ObjectId, eDS1Faces3, dSWall.eDS1Faces3);
            CADUtilities.SetXData(line.ObjectId, eDS2Faces1, dSWall.eDS2Faces1);
            CADUtilities.SetXData(line.ObjectId, eDS2Faces2, dSWall.eDS2Faces2);
            CADUtilities.SetXData(line.ObjectId, eDS2Faces3, dSWall.eDS2Faces3);
            CADUtilities.SetXData(line.ObjectId, uValueCheck, dSWall.uValueCheck);
        }

        public void UpdateLine(EDSWall wall)
        {
            CreateLayer();

            List<string> linesLayer = new List<string>();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            // Define the selection filter for lines
            TypedValue[] filter = new TypedValue[1];
            filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

            // Create the selection filter
            SelectionFilter selectionFilter = new SelectionFilter(filter);

            PromptSelectionResult res = ed.SelectImplied(); ;

            if (res.Value==null)
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

                    if (!linesLayer.All(x => x.Equals(layerName)))
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
                                            line.Layer = layerName;
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
                                            if (line.Layer == layerName)
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
                                        if (line.Layer == layerName)
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

                    if (!linesLayer.All(x => x.Equals(layerName)))
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
                                            line.Layer = layerName;
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
                                            if (line.Layer == layerName)
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
                                        if (line.Layer == layerName)
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
                                if (line.Layer == layerName)
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

        EDSWall GetXDataForLine(Line line)
        {
            EDSWall xData = new EDSWall();

            xData.extWallType = CADUtilities.GetXData(line.ObjectId, extWallType);
            xData.intWallType = CADUtilities.GetXData(line.ObjectId, intWallType);
            xData.uValue = CADUtilities.GetXData(line.ObjectId, uValue);
            xData.eDS1Faces1 = CADUtilities.GetXData(line.ObjectId, eDS1Faces1);
            xData.eDS1Faces2 = CADUtilities.GetXData(line.ObjectId, eDS1Faces2);
            xData.eDS1Faces3 = CADUtilities.GetXData(line.ObjectId, eDS1Faces3);
            xData.eDS2Faces1 = CADUtilities.GetXData(line.ObjectId, eDS2Faces1);
            xData.eDS2Faces2 = CADUtilities.GetXData(line.ObjectId, eDS2Faces2);
            xData.eDS2Faces3 = CADUtilities.GetXData(line.ObjectId, eDS2Faces3);
            xData.uValueCheck = CADUtilities.GetXData(line.ObjectId, uValueCheck);

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


            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                List<Line> lines = new List<Line>();
                // Define the selection filter for lines
                TypedValue[] filter = new TypedValue[1];
                filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                // Prompt for the selection
                PromptSelectionOptions opts = new PromptSelectionOptions();
                opts.MessageForAdding = "Select lines: ";
                PromptSelectionResult res = ed.GetSelection(opts, selectionFilter);


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
                            if (entity.Layer == layerName)
                                lines.Add(entity as Line);
                        }
                    }

                    CreateTreeNode(lines);

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

        private static void FindLoops(Dictionary<Point3d, List<Line>> adjacencyList, Line currentLine, Point3d startPoint, Point3d currentPoint, List<Line> currentLoop, List<List<Line>> loops, HashSet<Line> visitedLines)
        {
            if (currentLoop.Count > 0 && currentPoint == startPoint)
            {
                loops.Add(new List<Line>(currentLoop));
                return;
            }

            foreach (Line nextLine in adjacencyList[currentPoint])
            {
                if (!visitedLines.Contains(nextLine))
                {
                    visitedLines.Add(nextLine);
                    currentLoop.Add(nextLine);

                    Point3d nextPoint = nextLine.StartPoint == currentPoint ? nextLine.EndPoint : nextLine.StartPoint;
                    FindLoops(adjacencyList, nextLine, startPoint, nextPoint, currentLoop, loops, visitedLines);

                    currentLoop.Remove(nextLine);
                    visitedLines.Remove(nextLine);
                }
            }
        }

        private static double CalculateLoopArea(List<Line> loop)
        {
            // Implement the Shoelace formula to calculate the area of the loop
            double area = 0;
            for (int i = 0; i < loop.Count; i++)
            {
                Point3d p1 = loop[i].StartPoint;
                Point3d p2 = loop[(i + 1) % loop.Count].StartPoint;
                area += p1.X * p2.Y - p2.X * p1.Y;
            }
            return Math.Abs(area) / 2;
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
            //treeNode.Nodes.Add(treeNode);
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

        public Point3d GetLineMidpoint(Line line)
        {
            // Ensure the line object is not null
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            // Get the start and end points of the line
            Point3d startPoint = line.StartPoint;
            Point3d endPoint = line.EndPoint;

            // Calculate the midpoint
            Point3d midpoint = new Point3d(
                (startPoint.X + endPoint.X) / 2,
                (startPoint.Y + endPoint.Y) / 2,
                (startPoint.Z + endPoint.Z) / 2
            );

            return midpoint;
        }

        public void DetectClosedLoops(Point3d seedPoint)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;
            doc.LockDocument();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord modelSpace = (BlockTableRecord)tr.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                try
                {

                    DBObjectCollection boundaries = ed.TraceBoundary(seedPoint, false);

                    if (boundaries.Count > 0)
                    {
                        foreach (DBObject obj in boundaries)
                        {
                            if (obj is ZwSoft.ZwCAD.DatabaseServices.Polyline polyline)
                            {
                                modelSpace.AppendEntity(polyline);
                                tr.AddNewlyCreatedDBObject(polyline, true);
                                // Process the closed loop polyline here
                                ed.WriteMessage($"\nClosed loop found with {polyline.NumberOfVertices} vertices.");
                            }
                            else if (obj is Region region)
                            {
                                // Process the closed region here
                                ed.WriteMessage($"\nClosed region found.");
                            }
                        }
                    }
                    else
                    {
                        ed.WriteMessage("\nNo closed loop found at the specified point.");
                    }
                }
                catch (System.Exception)
                {

                }

                tr.Commit();
            }
        }
    }
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
    }

}
