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
using static System.Windows.Forms.LinkLabel;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;
using System.Globalization;

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

        List<Polyline> windowLines = new List<Polyline>();

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
            // Get start and end points of the line
            Point3d startPoint = line.StartPoint;
            Point3d endPoint = line.EndPoint;

            // Vector from start to end point
            Vector3d lineVector = endPoint - startPoint;

            // Vector from start point to the given point
            Vector3d pointVector = point - startPoint;

            // Check if the cross product of these two vectors is zero (parallel vectors)
            // Also check if the point lies within the bounds of the line segment
            if (lineVector.CrossProduct(pointVector).Length == 0)
            {
                // The point is collinear, now check if it lies on the line segment
                double dotProduct = pointVector.DotProduct(lineVector);
                return (dotProduct >= 0 && dotProduct <= lineVector.LengthSqrd);
            }

            return false; // Point is not on the line
        }

        public void DeleteElementsFromLayer(string layerName)
        {
            // Get the current document and its database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            doc.LockDocument();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Get the LayerTable from the database
                LayerTable layerTable = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);

                // Check if the layer exists
                if (!layerTable.Has(layerName))
                {
                    doc.Editor.WriteMessage($"\nLayer '{layerName}' does not exist.");
                    return;
                }

                // Open the BlockTable for reading
                BlockTable blockTable = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);

                // Open the ModelSpace block table record for writing
                BlockTableRecord modelSpace = (BlockTableRecord)trans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                // Iterate through all entities in the ModelSpace
                foreach (ObjectId objId in modelSpace)
                {
                    Entity ent = (Entity)trans.GetObject(objId, OpenMode.ForRead);

                    // Check if the entity is on the target layer
                    if (ent.Layer == layerName)
                    {
                        // Upgrade the entity to write mode to delete it
                        ent.UpgradeOpen();
                        ent.Erase();
                    }
                }

                // Commit the transaction
                trans.Commit();
            }
        }
    }

    public class LineSelectCount
    {
        public LineSegment lineSegment;
        public int iCount = 0;
        public List<string> spaces = new List<string>();
    }

    public class LineSegment
    {
        public Point3d StartPoint { get; set; }
        public Point3d EndPoint { get; set; }

        public LineSegment(Point3d start, Point3d end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        // Equality members (used to compare segments for equality)
        public override bool Equals(dynamic obj)
        {

            if (obj is LineSegment other1)
            {
                return (StartPoint.Equals(other1.StartPoint) && EndPoint.Equals(other1.EndPoint)) ||
                       (StartPoint.Equals(other1.EndPoint) && EndPoint.Equals(other1.StartPoint));
            }
            else if (obj.Key is LineSegment other)
            {
                return (StartPoint.Equals(other.StartPoint) && EndPoint.Equals(other.EndPoint)) ||
                       (StartPoint.Equals(other.EndPoint) && EndPoint.Equals(other.StartPoint));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StartPoint.GetHashCode() ^ EndPoint.GetHashCode();
        }

        public void WriteToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string startPointStr = FormatPoint(StartPoint);
                string endPointStr = FormatPoint(EndPoint);

                writer.WriteLine($"StartPoint: {startPointStr}, EndPoint: {endPointStr}");
            }
        }

        private string FormatPoint(Point3d point)
        {
            return string.Format(CultureInfo.InvariantCulture, "({0}, {1}, {2})", point.X, point.Y, point.Z);
        }
    }
}
