using EDS.AEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;
using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;

namespace EDS.Models
{
    public class EDSWindow
    {
        public string InsertionMode { get; set; }
        public string WindowType { get; set; }
        public string OpenAble { get; set; }
        public string OverhangPF { get; set; }
        public string VerticalPF { get; set; }
        public string DayLightWindow { get; set; }
        public string InteriorLightSelf { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string SillHeight { get; set; }
        public string Spacing { get; set; }
        public string WWR { get; set; }
        public string SpecifyOnDrawing { get; set; }
        public string WindHandleId { set; get; }
        public string UValue { set; get; }
        public string VLT { set; get; }
        public string SHGC { set; get; }

        public EDSWindow()
        {

        }

        public EDSWindow(string insertionMode, string windowType, string openAble, string overhangPF, string verticalPF, string dayLightWindow, string interiorLightSelf, string height, string width, string sillHeight, string spacing, string wWR, string specifyOnDrawing)
        {
            InsertionMode = insertionMode;
            WindowType = windowType;
            OpenAble = openAble;
            OverhangPF = overhangPF;
            VerticalPF = verticalPF;
            DayLightWindow = dayLightWindow;
            InteriorLightSelf = interiorLightSelf;
            Height = height;
            Width = width;
            SillHeight = sillHeight;
            Spacing = spacing;
            WWR = wWR;
            SpecifyOnDrawing = specifyOnDrawing;
        }

        public void CreateWindow(EDSWindow window)
        {
            EDSCreation.CreateLayer(StringConstants.windowLayerName, 3);

            if (window.InsertionMode.ToString() == "Single Window")
            {
                CreateRectangleWithInsertionPoint(window);
            }
            else if (window.InsertionMode.ToString() == "Repeating Window")
            {
                CreateRectanglesAlongLine(window);
            }
            else
            {
                CreateFullWallRectangle(window);
            }
        }

        public EDSWindow SelectWindow()
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.LockDocument();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for both lightweight and 3D polylines
                TypedValue[] filter = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.Start, "LWPOLYLINE"),
                    new TypedValue((int)DxfCode.Start, "POLYLINE"),
                    new TypedValue((int)DxfCode.Start, "POLYLINE2D"),
                    new TypedValue((int)DxfCode.Start, "POLYLINE3D")
                };

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                PromptEntityOptions targetOptions = new PromptEntityOptions("\nSelect the window from the document:");
                PromptEntityResult targetResult = editor.GetEntity(targetOptions);

                if (targetResult.Status != PromptStatus.OK)
                {
                    System.Windows.Forms.MessageBox.Show("Please select the window for extract data");
                    return null;
                }

                Entity entity = transaction.GetObject(targetResult.ObjectId, OpenMode.ForRead) as Entity;
                if (entity is Polyline)
                    return GetXDataForWindow(targetResult.ObjectId);
                else
                    return null;
            }
        }
        public void UpdateWindow(EDSWindow window)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.LockDocument();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for both lightweight and 3D polylines
                TypedValue[] filter = new TypedValue[]
                {
                    //new TypedValue((int)DxfCode.Start, "LWPOLYLINE"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE2D"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE3D")
                };

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                PromptSelectionOptions targetOptions = new PromptSelectionOptions();
                targetOptions.MessageForAdding = "\nSelect the window from the document:";
                PromptSelectionResult targetResult = editor.GetSelection(targetOptions, selectionFilter);

                if (targetResult.Status != PromptStatus.OK)
                {
                    System.Windows.Forms.MessageBox.Show("Please select the window for extract data");
                    return;
                }
                foreach (ObjectId objectId in targetResult.Value.GetObjectIds())
                {
                    SetXDataForWindow(objectId, window);
                }

                transaction.Commit();
            }
        }
        public void MatchWindow()
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.LockDocument();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for both lightweight and 3D polylines
                TypedValue[] filter = new TypedValue[]
                {
                    //new TypedValue((int)DxfCode.Start, "LWPOLYLINE"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE2D"),
                    //new TypedValue((int)DxfCode.Start, "POLYLINE3D")
                };

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                PromptEntityOptions targetOptions = new PromptEntityOptions("\nSelect the source window from the document:");
                PromptEntityResult targetResult = editor.GetEntity(targetOptions);

                if (targetResult.Status != PromptStatus.OK)
                {
                    System.Windows.Forms.MessageBox.Show("Please select the source window for document:");
                    return;
                }

                Entity entity = transaction.GetObject(targetResult.ObjectId, OpenMode.ForRead) as Entity;
                if (entity is Polyline)
                {
                    var window = GetXDataForWindow(targetResult.ObjectId);

                    PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
                    promptSelectionOptions.MessageForAdding = "\nSelect the target window from the document:";
                    PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

                    if (targetResult.Status != PromptStatus.OK)
                    {
                        System.Windows.Forms.MessageBox.Show("Please select the window for match");
                        return;
                    }
                    foreach (ObjectId objectId in promptSelectionResult.Value.GetObjectIds())
                    {
                        SetXDataForWindow(objectId, window);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Select the window from the document");
                    return;
                }

                transaction.Commit();
            }
        }

        public void SetXDataForWindow(ObjectId objectId, EDSWindow window)
        {
            CADUtilities.SetXData(objectId, StringConstants.InsertionMode, window.InsertionMode.ToString());
            CADUtilities.SetXData(objectId, StringConstants.WindowType, window.WindowType.ToString());
            CADUtilities.SetXData(objectId, StringConstants.OpenAble, window.OpenAble.ToString());
            CADUtilities.SetXData(objectId, StringConstants.OverhangPF, window.OverhangPF.ToString());
            CADUtilities.SetXData(objectId, StringConstants.VerticalPF, window.VerticalPF.ToString());
            CADUtilities.SetXData(objectId, StringConstants.Height, window.Height.ToString());
            CADUtilities.SetXData(objectId, StringConstants.Width, window.Width.ToString());
            CADUtilities.SetXData(objectId, StringConstants.SillHeight, window.SillHeight.ToString());
            CADUtilities.SetXData(objectId, StringConstants.Spacing, window.Spacing.ToString());
            CADUtilities.SetXData(objectId, StringConstants.WWR, window.WWR.ToString());
            CADUtilities.SetXData(objectId, StringConstants.InteriorLightSelf, window.InteriorLightSelf.ToString());
            CADUtilities.SetXData(objectId, StringConstants.DayLightWindow, window.DayLightWindow.ToString());
            CADUtilities.SetXData(objectId, StringConstants.WindHandleId, objectId.Handle.ToString());
            CADUtilities.SetXData(objectId, StringConstants.UValue, window.UValue.ToString());
            CADUtilities.SetXData(objectId, StringConstants.VLT, window.VLT.ToString());
            CADUtilities.SetXData(objectId, StringConstants.SHGC, window.SHGC.ToString());

        }

        public EDSWindow GetXDataForWindow(ObjectId objectId)
        {
            EDSWindow window = new EDSWindow();

            window.InsertionMode = CADUtilities.GetXData(objectId, StringConstants.InsertionMode);
            window.WindowType = CADUtilities.GetXData(objectId, StringConstants.WindowType);
            window.OpenAble = CADUtilities.GetXData(objectId, StringConstants.OpenAble);
            window.OverhangPF = CADUtilities.GetXData(objectId, StringConstants.OverhangPF);
            window.VerticalPF = CADUtilities.GetXData(objectId, StringConstants.VerticalPF);
            window.DayLightWindow = CADUtilities.GetXData(objectId, StringConstants.DayLightWindow);
            window.InteriorLightSelf = CADUtilities.GetXData(objectId, StringConstants.InteriorLightSelf);
            window.Height = CADUtilities.GetXData(objectId, StringConstants.Height);
            window.Width = CADUtilities.GetXData(objectId, StringConstants.Width);
            window.SillHeight = CADUtilities.GetXData(objectId, StringConstants.SillHeight);
            window.Spacing = CADUtilities.GetXData(objectId, StringConstants.Spacing);
            window.WWR = CADUtilities.GetXData(objectId, StringConstants.WWR);
            window.WindHandleId = CADUtilities.GetXData(objectId, StringConstants.WindHandleId);
            window.VLT = CADUtilities.GetXData(objectId, StringConstants.VLT);
            window.SHGC = CADUtilities.GetXData(objectId, StringConstants.SHGC);
            window.UValue = CADUtilities.GetXData(objectId, StringConstants.UValue);
            return window;
        }


        public void CreateFullWallRectangle(EDSWindow window)
        {
            // Get the active document and database
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.LockDocument();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for lines
                TypedValue[] filter = new TypedValue[1];
                filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                // Prompt the user to select a line
                PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
                promptSelectionOptions.MessageForAdding = "\nSelect a wall for window placement:";
                PromptSelectionResult promptSelectionResult = editor.GetSelection(promptSelectionOptions, selectionFilter);

                if (promptSelectionResult.Status != PromptStatus.OK) return;

                foreach (ObjectId objectId in promptSelectionResult.Value.GetObjectIds())
                {
                    Line line = transaction.GetObject(objectId, OpenMode.ForRead) as Line;

                    Point3d startPoint = line.StartPoint;
                    Point3d endPoint = line.EndPoint;

                    // Normalize the direction vector of the wall line
                    Vector3d lineDirection = (endPoint - startPoint).GetNormal();

                    // Define the safe distance (100mm) from both endpoints
                    double safeDistance = 100.0;

                    // Calculate the adjusted start and end points considering the safe distance
                    Point3d adjustedStartPoint = startPoint + lineDirection * safeDistance;
                    Point3d adjustedEndPoint = endPoint - lineDirection * safeDistance;

                    double wallHeight = StringConstants.wallHeight;

                    // Calculate the total length of the rectangle along the line
                    double wallLength = adjustedStartPoint.DistanceTo(adjustedEndPoint);

                    // Calculate the midpoint of the adjusted line for placing the rectangle
                    Point3d midPoint = adjustedStartPoint + (lineDirection * (wallLength / 2));

                    // Calculate the perpendicular vector for the rectangle's height
                    Vector3d heightVector = lineDirection.CrossProduct(Vector3d.ZAxis).GetNormal() * (wallHeight / 2);

                    // Calculate the half-width vector along the wall's length direction
                    Vector3d halfWidthVector = lineDirection * (wallLength / 2);

                    // Define the rectangle's four corners by offsetting from the midpoint
                    Point3d corner1 = midPoint - halfWidthVector + heightVector;
                    Point3d corner2 = midPoint + halfWidthVector + heightVector;
                    Point3d corner3 = midPoint + halfWidthVector - heightVector;
                    Point3d corner4 = midPoint - halfWidthVector - heightVector;

                    // Create the rectangle by defining a Polyline

                    BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(Application.DocumentManager.MdiActiveDocument.Database.CurrentSpaceId, OpenMode.ForWrite);

                    Polyline rectangle = new Polyline();
                    rectangle.AddVertexAt(0, new Point2d(corner1.X, corner1.Y), 0, 0, 0);
                    rectangle.AddVertexAt(1, new Point2d(corner2.X, corner2.Y), 0, 0, 0);
                    rectangle.AddVertexAt(2, new Point2d(corner3.X, corner3.Y), 0, 0, 0);
                    rectangle.AddVertexAt(3, new Point2d(corner4.X, corner4.Y), 0, 0, 0);
                    rectangle.Closed = true;

                    rectangle.Layer = StringConstants.windowLayerName;

                    // Add the rectangle to the drawing
                    ObjectId objectId1 = blockTableRecord.AppendEntity(rectangle);
                    transaction.AddNewlyCreatedDBObject(rectangle, true);

                    SetXDataForWindow(objectId1, window);
                }

                transaction.Commit();
            }
        }

        public void CreateRectangleWithInsertionPoint(EDSWindow window)
        {
            // Get the active document and database
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.LockDocument();

            // Create the rectangle by defining a Polyline
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // Prompt the user to select a line
                PromptEntityOptions entityOptions = new PromptEntityOptions("\nSelect a wall for window placement:");
                entityOptions.SetRejectMessage("\nSelected object must be a wall.");
                entityOptions.AddAllowedClass(typeof(Line), true);
                PromptEntityResult entityResult = editor.GetEntity(entityOptions);

                if (entityResult.Status != PromptStatus.OK) return;

                // Get the line object
                Line line = transaction.GetObject(entityResult.ObjectId, OpenMode.ForRead) as Line;

                Point3d startPoint = line.StartPoint;
                Point3d endPoint = line.EndPoint;

                // Normalize the direction vector of the line
                Vector3d lineDirection = (endPoint - startPoint).GetNormal();

                // Define a variable to store the width
                double width;

                // If the width is provided by the user, use it
                if (!bool.Parse(window.SpecifyOnDrawing))
                {
                    width = double.Parse(window.Width);

                    if (width > Math.Round(line.Length, 2))
                    {
                        System.Windows.Forms.MessageBox.Show($"\nError: Window width ({width}) is greater than the wall length ({Math.Round(line.Length, 2)}).", "Window Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    PromptPointOptions promptPointOptions = new PromptPointOptions("\nSpecify the insertion point on the wall: ");
                    promptPointOptions.AllowArbitraryInput = false;
                    promptPointOptions.BasePoint = startPoint;
                    promptPointOptions.UseBasePoint = true;

                    PromptPointResult insertionPointResult = editor.GetPoint(promptPointOptions);

                    // Check if the user canceled the input
                    if (insertionPointResult.Status != PromptStatus.OK) return;

                    // Get the insertion point provided by the user
                    Point3d insertionPoint = insertionPointResult.Value;

                    // Ensure that the insertion point lies between startPoint and endPoint on the line
                    if (!IsPointOnLine(insertionPoint, startPoint, endPoint))
                    {
                        editor.WriteMessage("\nThe insertion point must be on the line.");
                        return;
                    }
                    double height = StringConstants.wallHeight;
                    // Calculate the perpendicular vector for the rectangle's height
                    Vector3d heightVector = lineDirection.CrossProduct(Vector3d.ZAxis).GetNormal() * (height / 2);

                    // Calculate the half-width vector along the line's direction
                    Vector3d halfWidthVector = lineDirection * (width / 2);

                    // Define the rectangle's four corners by offsetting from the insertion point
                    Point3d corner1 = insertionPoint - halfWidthVector + heightVector;
                    Point3d corner2 = insertionPoint + halfWidthVector + heightVector;
                    Point3d corner3 = insertionPoint + halfWidthVector - heightVector;
                    Point3d corner4 = insertionPoint - halfWidthVector - heightVector;


                    BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(Application.DocumentManager.MdiActiveDocument.Database.CurrentSpaceId, OpenMode.ForWrite);

                    Polyline rectangle = new Polyline();
                    rectangle.AddVertexAt(0, new Point2d(corner1.X, corner1.Y), 0, 0, 0);
                    rectangle.AddVertexAt(1, new Point2d(corner2.X, corner2.Y), 0, 0, 0);
                    rectangle.AddVertexAt(2, new Point2d(corner3.X, corner3.Y), 0, 0, 0);
                    rectangle.AddVertexAt(3, new Point2d(corner4.X, corner4.Y), 0, 0, 0);
                    rectangle.Closed = true;

                    rectangle.Layer = StringConstants.windowLayerName;

                    // Add the rectangle to the drawing
                    ObjectId objectId = blockTableRecord.AppendEntity(rectangle);
                    transaction.AddNewlyCreatedDBObject(rectangle, true);

                    SetXDataForWindow(objectId, window);

                }
                else
                {
                    // Ask the user to input two points to calculate the width
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    PromptPointResult pointResult1 = ed.GetPoint("\nSpecify first point for rectangle width: ");
                    if (pointResult1.Status != PromptStatus.OK) return;

                    PromptPointResult pointResult2 = ed.GetPoint("\nSpecify second point for rectangle width: ");
                    if (pointResult2.Status != PromptStatus.OK) return;

                    // Calculate the width based on the distance between the two points
                    width = pointResult1.Value.DistanceTo(pointResult2.Value);
                    window.Width = width.ToString();

                    CreateRectangleFromTwoPoints(pointResult1.Value, pointResult2.Value, StringConstants.wallHeight, window);

                }

                transaction.Commit();
            }
        }

        public void CreateRectangleFromTwoPoints(Point3d insertionPoint, Point3d secondPoint, double height, EDSWindow window)
        {
            // Calculate the direction from the insertion point to the second point
            Vector3d direction = (secondPoint - insertionPoint).GetNormal();

            // Calculate the width as the distance between the two points
            double width = insertionPoint.DistanceTo(secondPoint);

            // Calculate the perpendicular vector for half of the rectangle's height
            Vector3d halfHeightVector = direction.CrossProduct(Vector3d.ZAxis).GetNormal() * (height / 2);

            // Define the rectangle's four corners
            Point3d corner1 = insertionPoint - halfHeightVector;               // Bottom-left corner
            Point3d corner2 = insertionPoint + direction * width - halfHeightVector; // Bottom-right corner
            Point3d corner3 = corner2 + halfHeightVector * 2;                  // Top-right corner
            Point3d corner4 = corner1 + halfHeightVector * 2;                  // Top-left corner

            // Create the rectangle by defining a Polyline
            using (Transaction tr = Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
            {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(Application.DocumentManager.MdiActiveDocument.Database.CurrentSpaceId, OpenMode.ForWrite);

                Polyline rectangle = new Polyline();
                rectangle.AddVertexAt(0, new Point2d(corner1.X, corner1.Y), 0, 0, 0);
                rectangle.AddVertexAt(1, new Point2d(corner2.X, corner2.Y), 0, 0, 0);
                rectangle.AddVertexAt(2, new Point2d(corner3.X, corner3.Y), 0, 0, 0);
                rectangle.AddVertexAt(3, new Point2d(corner4.X, corner4.Y), 0, 0, 0);
                rectangle.Closed = true;

                rectangle.Layer = StringConstants.windowLayerName;

                // Add the rectangle to the drawing
                var objectId = btr.AppendEntity(rectangle);
                tr.AddNewlyCreatedDBObject(rectangle, true);

                tr.Commit();

                SetXDataForWindow(objectId, window);
            }
        }

        // Helper function to check if a point lies on the line segment between start and end
        private bool IsPointOnLine(Point3d point, Point3d startPoint, Point3d endPoint)
        {
            Vector3d lineDirection = (endPoint - startPoint).GetNormal();
            Vector3d toPoint = (point - startPoint).GetNormal();

            double dotProduct = lineDirection.DotProduct(toPoint);

            return Math.Abs(dotProduct - 1.0) < 1e-6 &&
                   point.DistanceTo(startPoint) + point.DistanceTo(endPoint) - startPoint.DistanceTo(endPoint) < 1e-6;
        }

        public void CreateRectanglesAlongLine(EDSWindow window)
        {
            // Get the active document and database
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            document.LockDocument();
            // Start a transaction
            using (Transaction tr = database.TransactionManager.StartTransaction())
            {
                // Define the selection filter for lines
                TypedValue[] filter = new TypedValue[1];
                filter[0] = new TypedValue((int)DxfCode.Start, "LINE");

                // Create the selection filter
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                // Prompt the user to select a line
                PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
                promptSelectionOptions.MessageForAdding = "\nSelect a wall for window placement:";
                PromptSelectionResult promptSelectionResult = document.Editor.GetSelection(promptSelectionOptions, selectionFilter);

                if (promptSelectionResult.Status != PromptStatus.OK) return;

                foreach (ObjectId objectId in promptSelectionResult.Value.GetObjectIds())
                {
                    // Get the line object
                    Line line = tr.GetObject(objectId, OpenMode.ForRead) as Line;

                    // Define parameters for the rectangle creation
                    #region OldWindowCode
                    //double width = double.Parse(window.Width);  // Width of the rectangle
                    //double height = StringConstants.wallHeight;  // Height of the rectangle
                    //double spacing = double.Parse(window.Spacing); // Spacing between rectangles

                    //// Get the line's midpoint
                    //Point3d midPoint = line.GetPointAtParameter(line.EndParam / 2);

                    //// Get the direction vector of the line
                    //Vector3d lineDirection = (line.EndPoint - line.StartPoint).GetNormal();

                    //// Call the function to create rectangles along the line
                    //CreateRectanglesAlongLine(midPoint, width, height, spacing, lineDirection, line.StartPoint, line.EndPoint, window);

                    #endregion

                    double width = double.Parse(window.Width);  // Width of the rectangle
                    double height = StringConstants.wallHeight;  // Height of the rectangle
                    double spacing = double.Parse(window.Spacing); // Spacing between rectangles
                    double halfWindowHeight = StringConstants.wallHeight / 2.0;

                    if (width > Math.Round(line.Length, 2))
                    {
                        System.Windows.Forms.MessageBox.Show($"\nError: Window width ({width}) is greater than the wall length ({Math.Round(line.Length, 2)}).", "Window Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }


                    //double wallLength = line.Length;

                    double reservedGap = 100;  // 100mm gap at both ends

                    // Calculate the wall's direction as a unit vector from realStartPoint to realEndPoint
                    Point3d realStartPoint, realEndPoint;
                    if (line.StartPoint.X < line.EndPoint.X || (line.StartPoint.X == line.EndPoint.X && line.StartPoint.Y < line.EndPoint.Y))
                    {
                        realStartPoint = line.StartPoint;
                        realEndPoint = line.EndPoint;
                    }
                    else
                    {
                        realStartPoint = line.EndPoint;
                        realEndPoint = line.StartPoint;
                    }

                    Vector3d wallDirection = (realEndPoint - realStartPoint).GetNormal();
                    double wallLength = (realEndPoint - realStartPoint).Length;
                    double adjustedWallLength = wallLength - 2 * reservedGap; // Ensure the 100mm gap is on both sides

                    // Step 2: Calculate how many windows fit within wall length with space for both width and spacing
                    int fullWindowCount = (int)(adjustedWallLength / (width + spacing));

                    // Calculate the remaining space after placing full windows (this could still fit one more window without spacing)
                    double remainingSpace = adjustedWallLength - fullWindowCount * (width + spacing);

                    // If the remaining space can fit one more window (even without extra spacing), add it
                    if (remainingSpace >= width)
                    {
                        fullWindowCount++;
                    }

                    // Ensure that windows can be placed, adjusting to fit within the available length
                    if (fullWindowCount > 0)
                    {
                        double offset = reservedGap; // Start exactly 100mm from the start point
                        Vector3d windowHeightDirection = wallDirection.RotateBy(Math.PI / 2, Vector3d.ZAxis);

                        BlockTable bt = tr.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        if (fullWindowCount % 2 == 0)
                        {
                            Point3d midPoint = realStartPoint + wallDirection * (wallLength / 2);
                            Point3d windowBasePoint = midPoint - (((spacing / 2) + width) * wallDirection);
                            var lastBasePoint = windowBasePoint;

                            while (lastBasePoint.DistanceTo(realStartPoint) >= (width + reservedGap))
                            {

                                Point3d bottomLeft = windowBasePoint - halfWindowHeight * windowHeightDirection;
                                Point3d bottomRight = bottomLeft + width * wallDirection;
                                Point3d topLeft = windowBasePoint + halfWindowHeight * windowHeightDirection;
                                Point3d topRight = topLeft + width * wallDirection;

                                // Ensure that the window fits entirely within the line (both points within the line's start and end)
                                if (bottomLeft.DistanceTo(realStartPoint) >= reservedGap && bottomRight.DistanceTo(realEndPoint) <= adjustedWallLength)
                                {
                                    // Create the polyline (rectangle) representing the window
                                    Polyline polyWindow = new Polyline();
                                    polyWindow.AddVertexAt(0, new Point2d(bottomLeft.X, bottomLeft.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(1, new Point2d(bottomRight.X, bottomRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(2, new Point2d(topRight.X, topRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(3, new Point2d(topLeft.X, topLeft.Y), 0, 0, 0);
                                    polyWindow.Closed = true;

                                    polyWindow.Layer = StringConstants.windowLayerName;

                                    // Add the window to the drawing
                                    ObjectId objectId1 = btr.AppendEntity(polyWindow);
                                    tr.AddNewlyCreatedDBObject(polyWindow, true);

                                    SetXDataForWindow(objectId1, window);
                                }
                                lastBasePoint = windowBasePoint;
                                windowBasePoint = windowBasePoint - ((spacing + width) * wallDirection);
                            }

                            windowBasePoint = midPoint + (((spacing / 2) + width) * wallDirection);
                            lastBasePoint = windowBasePoint;
                            while (lastBasePoint.DistanceTo(realEndPoint) >= (width + reservedGap))
                            {

                                Point3d bottomLeft = windowBasePoint - halfWindowHeight * windowHeightDirection;
                                Point3d bottomRight = bottomLeft - width * wallDirection;
                                Point3d topLeft = windowBasePoint + halfWindowHeight * windowHeightDirection;
                                Point3d topRight = topLeft - width * wallDirection;

                                // Ensure that the window fits entirely within the line (both points within the line's start and end)
                                if (bottomLeft.DistanceTo(realStartPoint) >= reservedGap && bottomRight.DistanceTo(realEndPoint) <= adjustedWallLength)
                                {
                                    // Create the polyline (rectangle) representing the window
                                    Polyline polyWindow = new Polyline();
                                    polyWindow.AddVertexAt(0, new Point2d(bottomLeft.X, bottomLeft.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(1, new Point2d(bottomRight.X, bottomRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(2, new Point2d(topRight.X, topRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(3, new Point2d(topLeft.X, topLeft.Y), 0, 0, 0);
                                    polyWindow.Closed = true;

                                    polyWindow.Layer = StringConstants.windowLayerName;

                                    // Add the window to the drawing
                                    ObjectId objectId1 = btr.AppendEntity(polyWindow);
                                    tr.AddNewlyCreatedDBObject(polyWindow, true);

                                    SetXDataForWindow(objectId1, window);
                                }

                                lastBasePoint = windowBasePoint;
                                windowBasePoint = windowBasePoint + ((spacing + width) * wallDirection);
                            }

                        }
                        else
                        {
                            Point3d midPoint = realStartPoint + wallDirection * (wallLength / 2);
                            Point3d windowBasePoint = midPoint - (((width / 2) + 0) * wallDirection);
                            var lastBasePoint = windowBasePoint;

                            while (lastBasePoint.DistanceTo(realStartPoint) >= (width + reservedGap))
                            {

                                Point3d bottomLeft = windowBasePoint - halfWindowHeight * windowHeightDirection;
                                Point3d bottomRight = bottomLeft + width * wallDirection;
                                Point3d topLeft = windowBasePoint + halfWindowHeight * windowHeightDirection;
                                Point3d topRight = topLeft + width * wallDirection;

                                // Ensure that the window fits entirely within the line (both points within the line's start and end)
                                if (bottomLeft.DistanceTo(realStartPoint) >= reservedGap && bottomRight.DistanceTo(realEndPoint) <= adjustedWallLength)
                                {
                                    // Create the polyline (rectangle) representing the window
                                    Polyline polyWindow = new Polyline();
                                    polyWindow.AddVertexAt(0, new Point2d(bottomLeft.X, bottomLeft.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(1, new Point2d(bottomRight.X, bottomRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(2, new Point2d(topRight.X, topRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(3, new Point2d(topLeft.X, topLeft.Y), 0, 0, 0);
                                    polyWindow.Closed = true;

                                    polyWindow.Layer = StringConstants.windowLayerName;

                                    // Add the window to the drawing
                                    ObjectId objectId1 = btr.AppendEntity(polyWindow);
                                    tr.AddNewlyCreatedDBObject(polyWindow, true);

                                    SetXDataForWindow(objectId1, window);
                                }
                                lastBasePoint = windowBasePoint;
                                windowBasePoint = windowBasePoint - ((spacing + width) * wallDirection);
                            }

                            windowBasePoint = midPoint + (((width / 2) + 0) * wallDirection);
                            lastBasePoint = windowBasePoint;
                            while (lastBasePoint.DistanceTo(realEndPoint) >= (width + reservedGap))
                            {

                                Point3d bottomLeft = windowBasePoint - halfWindowHeight * windowHeightDirection;
                                Point3d bottomRight = bottomLeft - width * wallDirection;
                                Point3d topLeft = windowBasePoint + halfWindowHeight * windowHeightDirection;
                                Point3d topRight = topLeft - width * wallDirection;

                                // Ensure that the window fits entirely within the line (both points within the line's start and end)
                                if (bottomLeft.DistanceTo(realStartPoint) >= reservedGap && bottomRight.DistanceTo(realEndPoint) <= adjustedWallLength)
                                {
                                    // Create the polyline (rectangle) representing the window
                                    Polyline polyWindow = new Polyline();
                                    polyWindow.AddVertexAt(0, new Point2d(bottomLeft.X, bottomLeft.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(1, new Point2d(bottomRight.X, bottomRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(2, new Point2d(topRight.X, topRight.Y), 0, 0, 0);
                                    polyWindow.AddVertexAt(3, new Point2d(topLeft.X, topLeft.Y), 0, 0, 0);
                                    polyWindow.Closed = true;

                                    polyWindow.Layer = StringConstants.windowLayerName;

                                    // Add the window to the drawing
                                    ObjectId objectId1 = btr.AppendEntity(polyWindow);
                                    tr.AddNewlyCreatedDBObject(polyWindow, true);

                                    SetXDataForWindow(objectId1, window);
                                }

                                lastBasePoint = windowBasePoint;
                                windowBasePoint = windowBasePoint + ((spacing + width) * wallDirection);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The width and spacing exceed the wall length.", "Invalid Placement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                // Commit the transaction
                tr.Commit();
            }
        }

        private void CreateRectanglesAlongLine(Point3d midPoint, double width, double height, double spacing, Vector3d direction, Point3d startPoint, Point3d endPoint, EDSWindow window)
        {
            // Normalize the direction vector of the line
            Vector3d lineDirection = direction.GetNormal();

            // Calculate the perpendicular vector for the rectangle's height
            Vector3d heightVector = lineDirection.CrossProduct(Vector3d.ZAxis).GetNormal() * (height / 2);

            // Define the safe distance from the endpoints
            double safeDistance = 50.0;  // 50mm buffer from each endpoint

            // Calculate the half-width vector along the line's direction
            Vector3d halfWidthVector = lineDirection * (width / 2);

            // Calculate the initial offset for the first rectangle (spacing / 2.0)
            double initialOffset = spacing / 2.0;

            // Place the first rectangle at the midpoint with spacing/2.0
            //PlaceRectangle(midPoint, width, height, lineDirection, heightVector);

            // Now, place rectangles on the left side of the midpoint
            Point3d currentLeftPoint = midPoint - lineDirection * (initialOffset + width / 2);
            while (CheckIfWithinBounds(currentLeftPoint, lineDirection, startPoint, endPoint, safeDistance))  // Ensure rectangles stay within bounds
            {
                PlaceRectangle(currentLeftPoint, width, height, lineDirection, heightVector, window);
                currentLeftPoint -= lineDirection * (spacing + width);  // Move to the next left position
            }

            // Place rectangles on the right side of the midpoint
            Point3d currentRightPoint = midPoint + lineDirection * (initialOffset + width / 2);
            while (CheckIfWithinBounds(currentRightPoint, lineDirection, startPoint, endPoint, safeDistance))  // Ensure rectangles stay within bounds
            {
                PlaceRectangle(currentRightPoint, width, height, lineDirection, heightVector, window);
                currentRightPoint += lineDirection * (spacing + width);  // Move to the next right position
            }
        }

        private void PlaceRectangle(Point3d centerPoint, double width, double height, Vector3d direction, Vector3d heightVector, EDSWindow window)
        {
            // Calculate the four corners of the rectangle based on the center point, width, and height
            Vector3d halfWidthVector = direction.GetNormal() * (width / 2);

            Point3d bottomLeft = centerPoint - halfWidthVector - heightVector;
            Point3d bottomRight = centerPoint + halfWidthVector - heightVector;
            Point3d topLeft = centerPoint - halfWidthVector + heightVector;
            Point3d topRight = centerPoint + halfWidthVector + heightVector;

            // Create the rectangle as a polyline
            using (Polyline rect = new Polyline(4))
            {
                // Add vertices for the four corners of the rectangle
                rect.AddVertexAt(0, new Point2d(bottomLeft.X, bottomLeft.Y), 0, 0, 0);
                rect.AddVertexAt(1, new Point2d(bottomRight.X, bottomRight.Y), 0, 0, 0);
                rect.AddVertexAt(2, new Point2d(topRight.X, topRight.Y), 0, 0, 0);
                rect.AddVertexAt(3, new Point2d(topLeft.X, topLeft.Y), 0, 0, 0);
                rect.Closed = true;

                rect.Layer = StringConstants.windowLayerName;

                // Add the rectangle to the drawing
                BlockTableRecord btr = Application.DocumentManager.MdiActiveDocument.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                ObjectId objectId = btr.AppendEntity(rect);
                Application.DocumentManager.MdiActiveDocument.TransactionManager.TopTransaction.AddNewlyCreatedDBObject(rect, true);

                SetXDataForWindow(objectId, window);
            }
        }

        private bool CheckIfWithinBounds(Point3d currentPoint, Vector3d direction, Point3d startPoint, Point3d endPoint, double safeDistance)
        {
            // Offset the start and end points by the safe distance
            Point3d adjustedStartPoint = startPoint + direction.GetNormal() * safeDistance;
            Point3d adjustedEndPoint = endPoint - direction.GetNormal() * safeDistance;

            // Project the current point onto the adjusted line using the dot product
            Vector3d toAdjustedStart = currentPoint - adjustedStartPoint;
            Vector3d toAdjustedEnd = currentPoint - adjustedEndPoint;

            // Check if the current point is between the adjusted start and end points
            bool isWithinStart = direction.DotProduct(toAdjustedStart) >= 0;  // Check if the point is past the adjusted start
            bool isWithinEnd = direction.DotProduct(toAdjustedEnd) <= 0;      // Check if the point is before the adjusted end

            return isWithinStart && isWithinEnd;
        }
    }

    public class FeetInchConverter
    {
        // Conversion constants
        const double feetToMeters = 0.3048;
        const double inchToMeters = 0.0254;

        public double ConvertFeetInchToMeters(string feetInchStr)
        {
            // Combined regex pattern to support both feet-and-inches and feet-only formats
            string pattern = @"^\s*(\d+)'(\d*)\" + "\"" + @"?\s*$";

            // Match the input against the pattern
            Match match = Regex.Match(feetInchStr, pattern);

            // If the match is successful, parse feet and inches
            if (match.Success)
            {
                int feet = int.Parse(match.Groups[1].Value);

                // Inches may be optional, so we check if it is present
                int inches = 0;
                if (!string.IsNullOrEmpty(match.Groups[2].Value))
                {
                    inches = int.Parse(match.Groups[2].Value);
                }

                // Convert to meters
                double meters = (feet * feetToMeters) + (inches * inchToMeters);
                return meters;
            }

            // If no match, throw an error
            throw new ArgumentException("Invalid format. Please use the format X'Y\" (e.g., 5'8\") or X' (e.g., 5').");
        }
    }
}
