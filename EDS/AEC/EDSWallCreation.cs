using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;

namespace EDS.AEC
{
    public class EDSWallCreation
    {
        public static string layerName = "ZWall";

        public static string extWallType = "ExtWall";
        public static string intWallType = "IntWall";
        public static string uValue = "UValue";
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
            PromptPointOptions ppoStart = new PromptPointOptions("\nEnter the start point of the line: ");
            PromptPointResult pprStart = doc.Editor.GetPoint(ppoStart);
            if (pprStart.Status != PromptStatus.OK)
                return;
            else if (pprStart.Status == PromptStatus.OK)
                points.Add(pprStart.Value);

            // Prompt for the end point
            PromptPointOptions ppoEnd = new PromptPointOptions("\nEnter the end point of the line: ");
            ppoEnd.BasePoint = pprStart.Value;
            ppoEnd.UseBasePoint = true;
            PromptPointResult pprEnd = doc.Editor.GetPoint(ppoEnd);
            if (pprEnd.Status != PromptStatus.OK)
                return;
            else if (pprEnd.Status == PromptStatus.OK)
                points.Add(pprEnd.Value);

            using (doc.LockDocument())
            {
                if (points.Count > 1)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


                        for (int i = 0; i < points.Count; i++)
                        {
                            if (i == points.Count - 1)
                            {
                                if (!(points[0].IsEqualTo(points[i])))
                                {
                                    Line line = new Line();
                                    line.StartPoint = new Point3d(points[0].X, points[0].Y, 0);
                                    line.EndPoint = new Point3d(points[i].X, points[i].Y, 0);

                                    line.Layer = layerName;

                                    btr.AppendEntity(line);
                                    tr.AddNewlyCreatedDBObject(line, true);

                                    SetXDataForLine(dSWall, line);
                                }
                            }
                            else
                            {
                                Line line = new Line();
                                line.StartPoint = new Point3d(points[i].X, points[i].Y, 0);
                                line.EndPoint = new Point3d(points[i + 1].X, points[i + 1].Y, 0);

                                line.Layer = layerName;

                                btr.AppendEntity(line);
                                tr.AddNewlyCreatedDBObject(line, true);

                                SetXDataForLine(dSWall, line);
                            }
                        }

                        tr.Commit();
                    }
                }
                else
                {
                    doc.Editor.WriteMessage("\nNot enough points to create a line.");
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
                ed.SetImpliedSelection(new ObjectId[0]);
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

            return xData;
        }

        public void FindClosedLoop()
        {

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            doc.LockDocument();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                List<Line> lines = new List<Line>();

                // Collect all lines from the drawing
                foreach (ObjectId objId in btr)
                {
                    Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;
                    if (entity is Line)
                    {
                        if (entity.Layer == layerName)
                            lines.Add(entity as Line);
                    }
                }

                // Create an adjacency list
                Dictionary<Point3d, List<Line>> adjacencyList = new Dictionary<Point3d, List<Line>>();

                foreach (Line line in lines)
                {
                    if (!adjacencyList.ContainsKey(line.StartPoint))
                    {
                        adjacencyList[line.StartPoint] = new List<Line>();
                    }
                    if (!adjacencyList.ContainsKey(line.EndPoint))
                    {
                        adjacencyList[line.EndPoint] = new List<Line>();
                    }

                    adjacencyList[line.StartPoint].Add(line);
                    adjacencyList[line.EndPoint].Add(line);
                }

                // Find all possible loops using DFS
                List<List<Line>> loops = new List<List<Line>>();
                HashSet<Line> visitedLines = new HashSet<Line>();

                foreach (Line line in lines)
                {
                    if (!visitedLines.Contains(line))
                    {
                        List<Line> currentLoop = new List<Line>();
                        FindLoops(adjacencyList, line, line.StartPoint, line.StartPoint, currentLoop, loops, visitedLines);
                    }
                }

                // Calculate areas of the loops and find the best loop
                double maxArea = double.NegativeInfinity;
                List<Line> bestLoop = null;

                foreach (var loop in loops)
                {
                    double area = CalculateLoopArea(loop);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        bestLoop = loop;
                    }
                }

                // Output the best loop
                if (bestLoop != null)
                {
                    foreach (Line loopLine in bestLoop)
                    {
                        loopLine.UpgradeOpen();
                        loopLine.Color = Color.FromColor(System.Drawing.Color.Yellow);
                        loopLine.DowngradeOpen();
                        doc.Editor.WriteMessage($"\nLine from {loopLine.StartPoint} to {loopLine.EndPoint}");
                    }
                    doc.Editor.WriteMessage($"\nMaximum Area: {maxArea}");
                }
                else
                {
                    doc.Editor.WriteMessage("\nNo closed loops found.");
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
    }

    public class ClosedLoopFinder
    {
        public List<Line> GetLines(Database db)
        {
            List<Line> lines = new List<Line>();

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                foreach (ObjectId objId in btr)
                {
                    Entity entity = tr.GetObject(objId, OpenMode.ForRead) as Entity;
                    if (entity is Line)
                    {
                        lines.Add(entity as Line);
                    }
                }

                tr.Commit();
            }

            return lines;
        }

        public bool DoLinesIntersect(Line line1, Line line2)
        {
            Point3dCollection points = new Point3dCollection();
            line1.IntersectWith(line2, Intersect.OnBothOperands, points, IntPtr.Zero, IntPtr.Zero);
            return points.Count > 0;
        }

        public Dictionary<Line, List<Line>> BuildGraph(List<Line> lines)
        {
            Dictionary<Line, List<Line>> graph = new Dictionary<Line, List<Line>>();

            foreach (Line line in lines)
            {
                graph[line] = new List<Line>();
            }

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    if (DoLinesIntersect(lines[i], lines[j]))
                    {
                        graph[lines[i]].Add(lines[j]);
                        graph[lines[j]].Add(lines[i]);
                    }
                }
            }

            return graph;
        }

        public List<List<Line>> FindClosedLoops(Dictionary<Line, List<Line>> graph)
        {
            List<List<Line>> closedLoops = new List<List<Line>>();
            HashSet<Line> visited = new HashSet<Line>();

            foreach (Line line in graph.Keys)
            {
                List<Line> currentPath = new List<Line>();
                FindLoopsRecursive(line, line, visited, currentPath, closedLoops, graph);
            }

            return closedLoops;
        }

        private void FindLoopsRecursive(Line startLine, Line currentLine, HashSet<Line> visited, List<Line> currentPath, List<List<Line>> closedLoops, Dictionary<Line, List<Line>> graph)
        {
            visited.Add(currentLine);
            currentPath.Add(currentLine);

            foreach (Line neighbor in graph[currentLine])
            {
                if (neighbor == startLine && currentPath.Count > 2)
                {
                    closedLoops.Add(new List<Line>(currentPath));
                }
                else if (!visited.Contains(neighbor))
                {
                    FindLoopsRecursive(startLine, neighbor, visited, currentPath, closedLoops, graph);
                }
            }

            currentPath.Remove(currentLine);
            visited.Remove(currentLine);
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

        public EDSWall()
        {

        }

        public EDSWall(string extWallType, string intWallType, string uValue, string eDS1Faces1, string eDS1Faces2, string eDS1Faces3, string eDS2Faces1, string eDS2Faces2, string eDS2Faces3)
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
        }
    }

}
