using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;

namespace EDS.Models
{
    public class ExportValidation
    {
        public void IdentifyOpenLoopLinesWithCircles(Database db, SelectionSet selectionSet, System.Windows.Forms.TreeView treeView)
        {
            List<Line> lines = new List<Line>();

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Add all lines to the list
                foreach (SelectedObject selObj in selectionSet)
                {
                    try
                    {
                        Line line = trans.GetObject(selObj.ObjectId, OpenMode.ForRead) as Line;
                        if (line != null)
                        {
                            if (line.Layer == StringConstants.wallLayerName)
                                lines.Add(line);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // Dictionary to store connection status of points
                Dictionary<Point3d, int> pointConnections = new Dictionary<Point3d, int>();

                // Loop through all lines to check point connections
                foreach (var line in lines)
                {
                    // Increment connection count for start and end points
                    if (pointConnections.ContainsKey(line.StartPoint))
                        pointConnections[line.StartPoint]++;
                    else
                        pointConnections[line.StartPoint] = 1;

                    if (pointConnections.ContainsKey(line.EndPoint))
                        pointConnections[line.EndPoint]++;
                    else
                        pointConnections[line.EndPoint] = 1;
                }


                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                // Loop through the pointConnections dictionary to find open points
                foreach (var connection in pointConnections)
                {
                    // If the connection count is 1, it means the point is open
                    if (connection.Value == 1)
                    {

                        // Create a circle at the open connection
                        Circle openCircle = new Circle(connection.Key, Vector3d.ZAxis, 10); // Radius of 10 units
                        openCircle.ColorIndex = 2;
                        var objectid = btr.AppendEntity(openCircle);
                        trans.AddNewlyCreatedDBObject(openCircle, true);

                        TreeNode treeNode = new TreeNode("Errors");
                        treeNode.Tag = objectid.Handle.ToString();
                        treeView.Nodes.Add(treeNode);
                    }
                }

                trans.Commit();
            }
        }

        public void HighlightLineWithoutEndpointIntersection(Database db, SelectionSet selectionSet, System.Windows.Forms.TreeView treeView)
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                List<Line> lines = new List<Line>();

                // Add all lines to the list
                foreach (SelectedObject selObj in selectionSet)
                {
                    Line line = tr.GetObject(selObj.ObjectId, OpenMode.ForRead) as Line;
                    if (line != null)
                    {
                        if (line.Layer == StringConstants.wallLayerName)
                            lines.Add(line);
                    }
                }

                // Compare each line with every other line
                for (int i = 0; i < lines.Count; i++)
                {
                    for (int j = i + 1; j < lines.Count; j++)
                    {
                        Line line1 = lines[i];
                        Line line2 = lines[j];

                        Point3d? intersection = GetIntersection(line1, line2);

                        // If there is an intersection and it's at the endpoints of one line
                        if (intersection.HasValue)
                        {
                            if (IsEndpointIntersection(line1, intersection.Value) && !IsEndpointIntersection(line2, intersection.Value))
                            {
                                HighlightLine(line2);  // Highlight the second line (not intersecting at endpoint)

                                TreeNode treeNode = new TreeNode();
                                treeNode.Text = "Error";
                                treeNode.Tag = line2.ObjectId.Handle.ToString();
                                treeView.Nodes.Add(treeNode);
                            }
                            else if (!IsEndpointIntersection(line1, intersection.Value) && IsEndpointIntersection(line2, intersection.Value))
                            {
                                HighlightLine(line1);  // Highlight the first line (not intersecting at endpoint)

                                TreeNode treeNode = new TreeNode();
                                treeNode.Text = "Error";
                                treeNode.Tag = line1.ObjectId.Handle.ToString();
                                treeView.Nodes.Add(treeNode);
                            }
                        }
                    }
                }

                tr.Commit();
            }
        }

        private Point3d? GetIntersection(Line line1, Line line2)
        {
            Point3dCollection intersectionPoints = new Point3dCollection();
            line1.IntersectWith(line2, Intersect.OnBothOperands, intersectionPoints, IntPtr.Zero, IntPtr.Zero);

            if (intersectionPoints.Count > 0)
            {
                return intersectionPoints[0];  // Return the first intersection point
            }

            return null;
        }

        private bool IsEndpointIntersection(Line line, Point3d intersection)
        {
            // Check if the intersection point is at either endpoint of the line
            return line.StartPoint.IsEqualTo(intersection) || line.EndPoint.IsEqualTo(intersection);
        }

        private void HighlightLine(Line line)
        {
            using (Transaction tr = line.Database.TransactionManager.StartTransaction())
            {
                Line highlightLine = tr.GetObject(line.ObjectId, OpenMode.ForWrite) as Line;
                highlightLine.ColorIndex = 3;  // Set line color to red (or any color you want)
                tr.Commit();
            }
        }
    }

}