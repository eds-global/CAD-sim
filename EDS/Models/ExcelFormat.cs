using OfficeOpenXml;
using ResourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using static System.Windows.Forms.LinkLabel;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;
using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;

namespace EDS.Models
{
    public class EDSExcel
    {
        public ProjectInformation projectInformation { get; set; }
        public List<EDSExcelRoom> roomList { get; set; }
    }
    public class EDSExcelRoom
    {
        public EDSRoomTag room { get; set; }
        public List<EDSExcelWall> walls { get; set; }
    }
    public class EDSExcelWall
    {
        public EDSWall wall { get; set; }
        public List<EDSWindow> windows { get; set; }
    }

    public class EDSScan
    {
        List<Polyline> windowLines = new List<Polyline>();
        List<EDSExcelRoom> rooms = new List<EDSExcelRoom>();
        List<LineSelectCount> segmentCount = new List<LineSelectCount>();
        public void FindClosedLoop(System.Windows.Forms.TreeView treeView)
        {
            try
            {
                Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database db = doc.Database;
                Editor ed = doc.Editor;
                doc.LockDocument();

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

                HideWindows(db, res, false);

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    List<Line> wallLines = new List<Line>();
                    List<EDSRoomTag> edsRooms = new List<EDSRoomTag>();
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
                                CreateTreeNodes(edsRooms, wallLines, windowLines, treeView);
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("No rooms found");
                            }

                            UnHideWindows(db, true);

                            Thread.Sleep(1000);

                            if (!string.IsNullOrEmpty(ProjectInformationPalette.projectInformation.Direction))
                            {

                                AddExcelData(db);

                                CreateXMLFile(db);

                            }
                            else
                            {
                                MessageBox.Show("Not able to load process the project parameters\nPlease check the parameters for excel and xml data");
                            }
                        }
                        else
                        {
                            rooms = new List<EDSExcelRoom>();
                            UnHideWindows(db, true);
                        }
                    }
                    tr.Commit();
                }
            }
            catch (Exception ex)
            {
                Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database db = doc.Database;
                Editor ed = doc.Editor;
                doc.LockDocument();
                UnHideWindows(db, true);
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateXMLFile(Database database)
        {
            if (rooms.Count > 0)
            {
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    ProjectInformationPalette.LoadProjectInformation();

                    string dllPath = Assembly.GetExecutingAssembly().Location;
                    string directoryName = System.IO.Path.GetDirectoryName(dllPath);

                    var name = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name;

                    if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".xml")))
                        System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".xml"));

                    var xmlOutputFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".xml");
                    XmlFileFormat format = new XmlFileFormat();
                    format.WriteToFile(GetXMLData(rooms, database), xmlOutputFile);

                }
            }
        }

        GbXml GetXMLData(List<EDSExcelRoom> rooms, Database database)
        {
            var campus = new Campus();
            GbXml gbxml = new GbXml
            {
                UseSIUnitsForResults = true,
                LengthUnit = "Meters",
                VolumeUnit = "CubicMeters",
                Version = "6.01",
                SurfaceReferenceLocation = "Centerline",
                AreaUnit = "SquareMeters",
                TemperatureUnit = "C",
                Campus = campus,
                DocumentHistory = new DocumentHistory
                {
                    CreatedBy = new CreatedBy
                    {
                        ProgramId = "openstudio",
                        Date = DateTime.Now,
                        PersonId = "unknown"
                    },
                    ProgramInfo = new ProgramInfo
                    {
                        Id = "openstudio",
                        ProductName = "OpenStudio",
                        Version = "2.8.0",
                        Platform = "Windows",
                        ProjectEntity = string.Empty // Adjust as needed
                    },
                    PersonInfo = new PersonInfo
                    {
                        Id = "unknown",
                        FirstName = "Unknown",
                        LastName = "Unknown"
                    }
                }
            };

            var allHeight = 3000.0;

            campus.Id = "Facility";
            campus.Name = "Facility";
            Building building = new Building();
            building.Area = 2222;
            building.Id = "Building_1";
            building.BuildingType = "Unknown";
            building.Spaces = new List<Space>();
            foreach (EDSExcelRoom room in rooms)
            {
                Space space = new Space();
                space.Id = room.room.textHandleId;
                space.Name = room.room.spaceType;
                space.Area = Convert.ToDouble(room.room.roomArea);
                space.Volume = space.Area * allHeight;
                space.PeopleNumber = new PeopleNumber() { Unit = "SquareMPerPerson", Value = 17.695817 };
                space.EquipPowerPerArea = new EquipPowerPerArea() { Unit = "WattPerSquareMeter", Value = 10.656271 };
                space.LightPowerPerArea = new LightPowerPerArea() { Unit = "WattPerSquareMeter", Value = 7.642376 };
                space.BuildingStoreyIdRef = "Building Room";

                building.Spaces.Add(space);
            }

            campus.Building = building;
            campus.Surfaces = new List<Surface>();

            foreach (EDSExcelRoom room in rooms)
            {
                Surface surface = new Surface();
                surface.ConstructionIdRef = "ExtSlabCarpet_4in_ClimateZone_1-8";
                surface.SurfaceType = "SlabOnGrade";
                surface.Id = room.room.curveHandleId;
                surface.AdjacentSpaceId = new List<AdjacentSpaceId>() { new AdjacentSpaceId() { SpaceIdRef = room.room.textHandleId } };
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    var polyline = transaction.GetObject(CADUtilities.HandleToObjectId(room.room.curveHandleId), OpenMode.ForRead) as Polyline;
                    Point3d point3D = polyline.GetPoint3dAt(0);
                    RectangularGeometry rectangularGeometry = new RectangularGeometry();
                    rectangularGeometry.Azimuth = 90.0;
                    rectangularGeometry.CartesianPoint = new CartesianPoint();
                    rectangularGeometry.CartesianPoint.Coordinates = new List<double>();
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.X / 1000.0);
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.Y / 1000.0);
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.Z / 1000.0);

                    rectangularGeometry.Tilt = 180;
                    rectangularGeometry.Height = (polyline.GeometricExtents.MaxPoint.Y - polyline.GeometricExtents.MinPoint.Y) / 1000.0;
                    rectangularGeometry.Width = (polyline.GeometricExtents.MaxPoint.X - polyline.GeometricExtents.MinPoint.X) / 1000.0;

                    surface.RectangularGeometry = rectangularGeometry;

                    PlanarGeometry planarGeometry = new PlanarGeometry();
                    planarGeometry.PolyLoop = new PolyLoop();
                    planarGeometry.PolyLoop.CartesianPoints = new List<CartesianPoint>();

                    for (int i = 0; i < polyline.NumberOfVertices; i++)
                    {
                        Point3d vertex = polyline.GetPoint3dAt(i);
                        CartesianPoint point = new CartesianPoint();
                        point.Coordinates = new List<double>() { (vertex.X / 1000.0), (vertex.Y / 1000.0), (vertex.Z / 1000.0) };
                        planarGeometry.PolyLoop.CartesianPoints.Add(point);
                    }

                    if (!IsClockwise(planarGeometry.PolyLoop.CartesianPoints))
                        planarGeometry.PolyLoop.CartesianPoints.Reverse();

                    //planarGeometry.PolyLoop.CartesianPoints = SortPointsClockwise(planarGeometry.PolyLoop.CartesianPoints);
                    surface.PlanarGeometry = planarGeometry;
                }

                campus.Surfaces.Add(surface);
            }

            foreach (EDSExcelRoom room in rooms)
            {
                Surface surface = new Surface();
                surface.ConstructionIdRef = "SHRAE_189.1-2009_ExtRoof_IEAD_ClimateZone_2-5";
                surface.SurfaceType = "Roof";
                surface.Id = room.room.curveHandleId;
                surface.AdjacentSpaceId = new List<AdjacentSpaceId>() { new AdjacentSpaceId() { SpaceIdRef = room.room.textHandleId } };
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    var polyline = transaction.GetObject(CADUtilities.HandleToObjectId(room.room.curveHandleId), OpenMode.ForRead) as Polyline;
                    Point3d point3D = polyline.GetPoint3dAt(0);
                    RectangularGeometry rectangularGeometry = new RectangularGeometry();
                    rectangularGeometry.Azimuth = 90.0;
                    rectangularGeometry.CartesianPoint = new CartesianPoint();
                    rectangularGeometry.CartesianPoint.Coordinates = new List<double>();
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.X / 1000.0);
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.Y / 1000.0);
                    rectangularGeometry.CartesianPoint.Coordinates.Add(point3D.Z + allHeight / 1000.0);

                    rectangularGeometry.Tilt = 0;
                    rectangularGeometry.Height = (polyline.GeometricExtents.MaxPoint.Y - polyline.GeometricExtents.MinPoint.Y) / 1000.0;
                    rectangularGeometry.Width = (polyline.GeometricExtents.MaxPoint.X - polyline.GeometricExtents.MinPoint.X) / 1000.0;

                    surface.RectangularGeometry = rectangularGeometry;

                    PlanarGeometry planarGeometry = new PlanarGeometry();
                    planarGeometry.PolyLoop = new PolyLoop();
                    planarGeometry.PolyLoop.CartesianPoints = new List<CartesianPoint>();
                    for (int i = 0; i < polyline.NumberOfVertices; i++)
                    {
                        Point3d vertex = polyline.GetPoint3dAt(i);
                        CartesianPoint point = new CartesianPoint();
                        point.Coordinates = new List<double>() { (vertex.X / 1000.0), (vertex.Y / 1000.0), (vertex.Z + allHeight / 1000.0) };
                        planarGeometry.PolyLoop.CartesianPoints.Add(point);
                    }

                    if (IsClockwise(planarGeometry.PolyLoop.CartesianPoints))
                        planarGeometry.PolyLoop.CartesianPoints.Reverse();

                    //planarGeometry.PolyLoop.CartesianPoints.Reverse();//SortPointsAnticlockwise();
                    surface.PlanarGeometry = planarGeometry;
                }

                campus.Surfaces.Add(surface);
            }


            foreach (EDSExcelRoom room in rooms)
            {
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    foreach (EDSExcelWall excelWall in room.walls)
                    {
                        Line line = transaction.GetObject(CADUtilities.HandleToObjectId(excelWall.wall.wallHandleId), OpenMode.ForWrite) as Line;
                        //EDSExport eDSExport = new EDSExport();
                        //eDSExport.mthdOfSortExternalWall(new List<Line>() { line });
                        if (segmentCount.Find(x => (x.lineSegment.StartPoint.Equals(line.StartPoint) && x.lineSegment.EndPoint.Equals(line.EndPoint)) || (x.lineSegment.EndPoint.Equals(line.StartPoint) && x.lineSegment.StartPoint.Equals(line.EndPoint))).iCount == 1)
                        {
                            Surface surface = new Surface();
                            surface.ConstructionIdRef = "ASHRAE_189.1-2009_ExtWall_Mass_ClimateZone_5";
                            surface.SurfaceType = "Exterior Wall";
                            surface.Id = line.Handle.ToString();
                            surface.AdjacentSpaceId = new List<AdjacentSpaceId>() { new AdjacentSpaceId() { SpaceIdRef = room.room.textHandleId } };

                            RectangularGeometry rectangularGeometry = new RectangularGeometry();
                            rectangularGeometry.Azimuth = GetAzimuthAngle(line, double.Parse(ProjectInformationPalette.projectInformation.Direction));
                            rectangularGeometry.CartesianPoint = new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } };
                            rectangularGeometry.Tilt = 90;
                            rectangularGeometry.Height = (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0;
                            rectangularGeometry.Width = /*(line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0 == 0 ? (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0 :*/ (line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0;
                            surface.RectangularGeometry = rectangularGeometry;

                            PlanarGeometry planarGeometry = new PlanarGeometry();
                            planarGeometry.PolyLoop = new PolyLoop();
                            planarGeometry.PolyLoop.CartesianPoints = new List<CartesianPoint>();

                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z + allHeight) / 1000.0 } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z + allHeight) / 1000.0 } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z / 1000.0) } });

                            //if (!IsClockwise(planarGeometry.PolyLoop.CartesianPoints))
                            //    planarGeometry.PolyLoop.CartesianPoints.Reverse();

                            //planarGeometry.PolyLoop.CartesianPoints = SortPointsClockwise(planarGeometry.PolyLoop.CartesianPoints);

                            surface.PlanarGeometry = planarGeometry;


                            if (excelWall.windows.Count > 0)
                            {
                                surface.Openings = new List<Opening>();
                                foreach (var excelWindow in excelWall.windows)
                                {
                                    Polyline window = transaction.GetObject(CADUtilities.HandleToObjectId(excelWindow.WindHandleId), OpenMode.ForWrite) as Polyline;
                                    Opening opening = new Opening();
                                    opening.OpeningType = "Fixed Window";
                                    opening.WindowTypeIdRef = "ASHRAE_189.1-2009_ExtWindow_ClimateZone_4-5";
                                    opening.Id = window.Id.Handle.ToString();

                                    RectangularGeometry rectangularGeometry1 = new RectangularGeometry();
                                    rectangularGeometry1.Azimuth = 180;
                                    rectangularGeometry1.Tilt = 90;
                                    rectangularGeometry1.Height = (window.GeometricExtents.MaxPoint.Y - window.GeometricExtents.MinPoint.Y) / 1000.0;
                                    rectangularGeometry1.Width = (window.GeometricExtents.MaxPoint.X - window.GeometricExtents.MinPoint.X) / 1000.0;

                                    PlanarGeometry planarGeometry1 = new PlanarGeometry();
                                    planarGeometry1.PolyLoop = new PolyLoop();
                                    planarGeometry1.PolyLoop.CartesianPoints = new List<CartesianPoint>();

                                    for (int i = 0; i <= window.NumberOfVertices - 1; i++)
                                    {
                                        if (i == 3)
                                        {
                                            // Get start and end points of the segment
                                            Point3d startPoint = window.GetPoint3dAt(i);
                                            Point3d endPoint = window.GetPoint3dAt(0);

                                            // Create a line segment for the polyline segment
                                            Line segment = new Line(startPoint, endPoint);

                                            // Check for intersection
                                            Point3dCollection intersectionCollection = new Point3dCollection();
                                            line.IntersectWith(segment, Intersect.OnBothOperands, intersectionCollection, IntPtr.Zero, IntPtr.Zero);
                                            if (intersectionCollection.Count > 0)
                                            {
                                                foreach (Point3d pt in intersectionCollection)
                                                {
                                                    //CartesianPoint point1 = new CartesianPoint();
                                                    //point1.Coordinates = new List<double>() { (pt.X / 1000.0), (pt.Y / 1000.0), ((pt.Z + (double.Parse(excelWindow.SillHeight)) + (double.Parse(string.IsNullOrEmpty(excelWindow.Height) ? allHeight.ToString() : excelWindow.Height))) / 1000.0) };
                                                    //planarGeometry1.PolyLoop.CartesianPoints.Add(point1);

                                                    CartesianPoint point = new CartesianPoint();
                                                    point.Coordinates = new List<double>() { (pt.X / 1000.0), (pt.Y / 1000.0), ((pt.Z + (double.Parse(excelWindow.SillHeight))) / 1000.0) };
                                                    planarGeometry1.PolyLoop.CartesianPoints.Add(point);


                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Get start and end points of the segment
                                            Point3d startPoint = window.GetPoint3dAt(i);
                                            Point3d endPoint = window.GetPoint3dAt(i + 1);

                                            // Create a line segment for the polyline segment
                                            Line segment = new Line(startPoint, endPoint);

                                            // Check for intersection
                                            Point3dCollection intersectionCollection = new Point3dCollection();
                                            line.IntersectWith(segment, Intersect.OnBothOperands, intersectionCollection, IntPtr.Zero, IntPtr.Zero);
                                            if (intersectionCollection.Count > 0)
                                            {
                                                foreach (Point3d pt in intersectionCollection)
                                                {
                                                    CartesianPoint point = new CartesianPoint();
                                                    point.Coordinates = new List<double>() { (pt.X / 1000.0), (pt.Y / 1000.0), ((pt.Z + (double.Parse(excelWindow.SillHeight))) / 1000.0) };
                                                    planarGeometry1.PolyLoop.CartesianPoints.Add(point);

                                                    //CartesianPoint point1 = new CartesianPoint();
                                                    //point1.Coordinates = new List<double>() { (pt.X / 1000.0), (pt.Y / 1000.0), ((pt.Z + (double.Parse(excelWindow.SillHeight)) + (double.Parse(string.IsNullOrEmpty(excelWindow.Height) ? allHeight.ToString() : excelWindow.Height))) / 1000.0) };
                                                    //planarGeometry1.PolyLoop.CartesianPoints.Add(point1);
                                                }
                                            }
                                        }

                                    }

                                    //if (IsClockwise(planarGeometry1.PolyLoop.CartesianPoints))
                                    //    planarGeometry1.PolyLoop.CartesianPoints.Reverse();

                                    var points = SortPointsAlongLine(new CartesianPoint(line.StartPoint.X, line.StartPoint.Y, line.StartPoint.Z), new CartesianPoint(line.EndPoint.X, line.EndPoint.Y, line.EndPoint.Z), planarGeometry1.PolyLoop.CartesianPoints);
                                    planarGeometry1.PolyLoop.CartesianPoints = new List<CartesianPoint>();
                                    for (int iNo = 0; iNo < points.Count; iNo++)
                                    {
                                        if (iNo == 0)
                                        {
                                            CartesianPoint point = points[iNo];
                                            planarGeometry1.PolyLoop.CartesianPoints.Add(point);

                                            CartesianPoint point1 = new CartesianPoint();
                                            point1.Coordinates = new List<double>() { points[iNo].Coordinates[0], points[iNo].Coordinates[1], points[iNo].Coordinates[2] + (double.Parse(string.IsNullOrEmpty(excelWindow.Height) ? allHeight.ToString() : excelWindow.Height) / 1000.0) };
                                            planarGeometry1.PolyLoop.CartesianPoints.Add(point1);
                                        }
                                        else
                                        {
                                            CartesianPoint point1 = new CartesianPoint();
                                            point1.Coordinates = new List<double>() { points[iNo].Coordinates[0], points[iNo].Coordinates[1], points[iNo].Coordinates[2] + (double.Parse(string.IsNullOrEmpty(excelWindow.Height) ? allHeight.ToString() : excelWindow.Height) / 1000.0) };
                                            planarGeometry1.PolyLoop.CartesianPoints.Add(point1);

                                            CartesianPoint point = points[iNo];
                                            planarGeometry1.PolyLoop.CartesianPoints.Add(point);
                                        }
                                    }


                                    opening.PlanarGeometry = planarGeometry1;
                                    rectangularGeometry1.CartesianPoint = planarGeometry1.PolyLoop.CartesianPoints[0];

                                    opening.RectangularGeometry = rectangularGeometry1;
                                    surface.Openings.Add(opening);
                                }
                            }

                            campus.Surfaces.Add(surface);

                        }
                        //else
                        //{
                        //    Surface surface = new Surface();
                        //    surface.ConstructionIdRef = "ASHRAE_189.1-2009_IntWall_Mass_ClimateZone_5";
                        //    surface.SurfaceType = "Interior Wall";
                        //    surface.Id = line.Handle.ToString();

                        //    List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                        //    foreach (var space in segmentCount.Find(x => (x.lineSegment.StartPoint.Equals(line.StartPoint) && x.lineSegment.EndPoint.Equals(line.EndPoint)) || (x.lineSegment.EndPoint.Equals(line.StartPoint) && x.lineSegment.StartPoint.Equals(line.EndPoint))).spaces.Distinct())
                        //    {
                        //        if (space != room.room.textHandleId)
                        //            adjacentSpaceIds.Add(new AdjacentSpaceId() { SpaceIdRef = space });
                        //    }

                        //    surface.AdjacentSpaceId = adjacentSpaceIds;
                        //    //surface.AdjacentSpaceId = new List<AdjacentSpaceId>() { new AdjacentSpaceId() { SpaceIdRef = room.room.textHandleId } };

                        //    RectangularGeometry rectangularGeometry = new RectangularGeometry();
                        //    rectangularGeometry.Azimuth = 90;
                        //    rectangularGeometry.CartesianPoint = new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } };
                        //    rectangularGeometry.Tilt = 90;
                        //    rectangularGeometry.Height = (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0;
                        //    rectangularGeometry.Width = /*(line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0 == 0 ? (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0 :*/ (line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0;
                        //    surface.RectangularGeometry = rectangularGeometry;

                        //    PlanarGeometry planarGeometry = new PlanarGeometry();
                        //    planarGeometry.PolyLoop = new PolyLoop();
                        //    planarGeometry.PolyLoop.CartesianPoints = new List<CartesianPoint>();

                        //    planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } });
                        //    planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z + allHeight) / 1000.0 } });
                        //    planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z + allHeight) / 1000.0 } });
                        //    planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z / 1000.0) } });

                        //    planarGeometry.PolyLoop.CartesianPoints = SortPointsClockwise(planarGeometry.PolyLoop.CartesianPoints);

                        //    surface.PlanarGeometry = planarGeometry;

                        //    campus.Surfaces.Add(surface);

                        //}
                    }

                }
            }

            List<string> visitedWall = new List<string>();

            foreach (EDSExcelRoom room in rooms)
            {
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    foreach (EDSExcelWall excelWall in room.walls)
                    {
                        Line line = transaction.GetObject(CADUtilities.HandleToObjectId(excelWall.wall.wallHandleId), OpenMode.ForWrite) as Line;
                        if (segmentCount.Find(x => (x.lineSegment.StartPoint.Equals(line.StartPoint) && x.lineSegment.EndPoint.Equals(line.EndPoint)) || (x.lineSegment.EndPoint.Equals(line.StartPoint) && x.lineSegment.StartPoint.Equals(line.EndPoint))).iCount == 1)
                        {

                        }
                        else
                        {
                            Surface surface = new Surface();
                            surface.ConstructionIdRef = "ASHRAE_189.1-2009_IntWall_Mass_ClimateZone_5";
                            surface.SurfaceType = "Interior Wall";
                            surface.Id = line.Handle.ToString();

                            List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                            var data = segmentCount.Find(x => (x.lineSegment.StartPoint.Equals(line.StartPoint) && x.lineSegment.EndPoint.Equals(line.EndPoint)) || (x.lineSegment.EndPoint.Equals(line.StartPoint) && x.lineSegment.StartPoint.Equals(line.EndPoint))).spaces.Distinct();
                            foreach (var space in data)
                            {
                                adjacentSpaceIds.Add(new AdjacentSpaceId() { SpaceIdRef = space });
                            }

                            surface.AdjacentSpaceId = adjacentSpaceIds;
                            //surface.AdjacentSpaceId = new List<AdjacentSpaceId>() { new AdjacentSpaceId() { SpaceIdRef = room.room.textHandleId } };

                            RectangularGeometry rectangularGeometry = new RectangularGeometry();
                            rectangularGeometry.Azimuth = 90;
                            rectangularGeometry.CartesianPoint = new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } };
                            rectangularGeometry.Tilt = 90;
                            rectangularGeometry.Height = (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0;
                            rectangularGeometry.Width = /*(line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0 == 0 ? (line.GeometricExtents.MaxPoint.Y - line.GeometricExtents.MinPoint.Y) / 1000.0 :*/ (line.GeometricExtents.MaxPoint.X - line.GeometricExtents.MinPoint.X) / 1000.0;
                            surface.RectangularGeometry = rectangularGeometry;

                            PlanarGeometry planarGeometry = new PlanarGeometry();
                            planarGeometry.PolyLoop = new PolyLoop();
                            planarGeometry.PolyLoop.CartesianPoints = new List<CartesianPoint>();

                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z / 1000.0) } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.StartPoint.X / 1000.0), (line.StartPoint.Y / 1000.0), (line.StartPoint.Z + allHeight) / 1000.0 } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z + allHeight) / 1000.0 } });
                            planarGeometry.PolyLoop.CartesianPoints.Add(new CartesianPoint() { Coordinates = new List<double> { (line.EndPoint.X / 1000.0), (line.EndPoint.Y / 1000.0), (line.EndPoint.Z / 1000.0) } });

                            if (!IsClockwise(planarGeometry.PolyLoop.CartesianPoints))
                                planarGeometry.PolyLoop.CartesianPoints.Reverse();

                            //planarGeometry.PolyLoop.CartesianPoints = SortPointsClockwise(planarGeometry.PolyLoop.CartesianPoints);

                            surface.PlanarGeometry = planarGeometry;

                            if (!visitedWall.Contains(line.Handle.ToString()))
                            {
                                visitedWall.Add(line.Handle.ToString());
                                campus.Surfaces.Add(surface);
                            }

                        }
                    }

                }
            }

            return gbxml;
        }

        private void AddExcelData(Database db)
        {
            if (rooms.Count > 0)
            {
                using (Transaction transaction = db.TransactionManager.StartTransaction())
                {
                    ProjectInformationPalette.LoadProjectInformation();

                    string dllPath = Assembly.GetExecutingAssembly().Location;
                    string directoryName = System.IO.Path.GetDirectoryName(dllPath);

                    string fileName = System.IO.Path.Combine(directoryName, "CAD Output Template.xlsx");

                    var name = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name;

                    string moveFileName = System.IO.Path.GetFileNameWithoutExtension(name);


                    if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), moveFileName + ".xlsx")))
                        System.IO.File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), moveFileName + ".xlsx"));

                    System.IO.File.Copy(fileName, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), moveFileName + ".xlsx"));

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var cadOutputFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), moveFileName + ".xlsx");

                    using (ExcelPackage package = new ExcelPackage(cadOutputFile))
                    {
                        //get the first worksheet in the workbook
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        worksheet.Cells["F2"].Value = ProjectInformationPalette.projectInformation.City;
                        worksheet.Cells["D1"].Value = ProjectInformationPalette.projectInformation.ProjectName;
                        worksheet.Cells["D2"].Value = ProjectInformationPalette.projectInformation.State;
                        worksheet.Cells["D3"].Value = ProjectInformationPalette.projectInformation.BuildingCategory;

                        int rowCount = 8;

                        foreach (var room in rooms)
                        {
                            for (int iNo = 0; iNo < room.walls.Count; iNo++)
                            {
                                worksheet.Cells[rowCount, 3].Value = room.walls[iNo].wall.wallHandleId;
                                worksheet.Cells[rowCount, 4].Value = room.room.spaceType.ToString() + "_" + room.walls[iNo].wall.wallHandleId;
                                Entity entity = transaction.GetObject(CADUtilities.HandleToObjectId(room.walls[iNo].wall.wallHandleId), OpenMode.ForRead) as Entity;
                                if (entity is Line)
                                {
                                    Line line = entity as Line;

                                    worksheet.Cells[rowCount, 5].Value = GetAzimuthAngle(line, double.Parse(ProjectInformationPalette.projectInformation.Direction));

                                    worksheet.Cells[rowCount, 6].Value = Math.Round(line.Length);
                                    worksheet.Cells[rowCount, 7].Value = StringConstants.TopHeight;
                                }

                                double totalArea = 0.0;
                                if (room.walls[iNo].windows != null)
                                {
                                    if (room.walls[iNo].windows.Count > 0)
                                    {
                                        for (int iNo1 = 0; iNo1 < room.walls[iNo].windows.Count(); iNo1++)
                                        {
                                            Entity entity1 = transaction.GetObject(CADUtilities.HandleToObjectId(room.walls[iNo].windows[iNo1].WindHandleId), OpenMode.ForRead) as Entity;
                                            if (entity1 is Polyline)
                                            {
                                                Polyline line = entity1 as Polyline;
                                                totalArea = totalArea + (line.Area / 1_000_000);
                                            }

                                        }
                                    }
                                }

                                worksheet.Cells[rowCount, 9].Value = Math.Round(totalArea, 2);
                                worksheet.Cells[rowCount, 11].Value = room.room.spaceType;
                                worksheet.Cells[rowCount, 12].Value = room.walls[iNo].wall.extWallType;
                                rowCount++;
                            }
                        }

                        ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];
                        excelWorksheet.Cells["F2"].Value = ProjectInformationPalette.projectInformation.City;
                        excelWorksheet.Cells["D1"].Value = ProjectInformationPalette.projectInformation.ProjectName;
                        excelWorksheet.Cells["D2"].Value = ProjectInformationPalette.projectInformation.State;
                        excelWorksheet.Cells["D3"].Value = ProjectInformationPalette.projectInformation.BuildingCategory;

                        rowCount = 8;

                        foreach (var room in rooms)
                        {
                            excelWorksheet.Cells[rowCount, 3].Value = room.room.spaceType.ToString();
                            excelWorksheet.Cells[rowCount, 4].Value = room.room.spaceType.ToString().Split('-')[0];
                            excelWorksheet.Cells[rowCount, 5].Value = double.Parse(room.room.roomArea) / 1_000_000;

                            double totalArea = 0.0;
                            double windowGlaze = 0.0;
                            for (int iNo = 0; iNo < room.walls.Count; iNo++)
                            {
                                if (room.walls[iNo].windows != null)
                                {
                                    if (room.walls[iNo].windows.Count > 0)
                                    {

                                        for (int iNo1 = 0; iNo1 < room.walls[iNo].windows.Count(); iNo1++)
                                        {
                                            Entity entity1 = transaction.GetObject(CADUtilities.HandleToObjectId(room.walls[iNo].windows[iNo1].WindHandleId), OpenMode.ForRead) as Entity;
                                            if (entity1 is Polyline)
                                            {
                                                Polyline line = entity1 as Polyline;
                                                totalArea = totalArea + (line.Area / 1_000_000);
                                                windowGlaze = windowGlaze + (string.IsNullOrEmpty(room.walls[iNo].windows[iNo1].VLT) ? 0 : double.
                                                    Parse(room.walls[iNo].windows[iNo1].VLT));
                                            }

                                        }
                                    }
                                }
                            }

                            excelWorksheet.Cells[rowCount, 6].Value = Math.Round(totalArea, 2);
                            excelWorksheet.Cells[rowCount, 7].Value = windowGlaze;

                            rowCount++;
                        }

                        ExcelWorksheet workSheetVentilation = package.Workbook.Worksheets[2];
                        workSheetVentilation.Cells["F2"].Value = ProjectInformationPalette.projectInformation.City;
                        workSheetVentilation.Cells["D1"].Value = ProjectInformationPalette.projectInformation.ProjectName;
                        workSheetVentilation.Cells["D2"].Value = ProjectInformationPalette.projectInformation.State;
                        workSheetVentilation.Cells["D3"].Value = ProjectInformationPalette.projectInformation.BuildingCategory;

                        rowCount = 8;

                        foreach (var room in rooms)
                        {
                            workSheetVentilation.Cells[rowCount, 3].Value = room.room.spaceType.ToString();
                            workSheetVentilation.Cells[rowCount, 4].Value = room.room.spaceType.ToString().Split('-')[0];
                            workSheetVentilation.Cells[rowCount, 5].Value = double.Parse(room.room.roomArea) / 1_000_000;

                            double totalArea = 0.0;
                            double windowsArea = 0.0;
                            for (int iNo = 0; iNo < room.walls.Count; iNo++)
                            {
                                double windowArea = 0.0;

                                if (room.walls[iNo].windows != null)
                                {
                                    if (room.walls[iNo].windows.Count > 0)
                                    {
                                        for (int iNo1 = 0; iNo1 < room.walls[iNo].windows.Count(); iNo1++)
                                        {
                                            Entity entity1 = transaction.GetObject(CADUtilities.HandleToObjectId(room.walls[iNo].windows[iNo1].WindHandleId), OpenMode.ForRead) as Entity;
                                            if (entity1 is Polyline)
                                            {
                                                Polyline line = entity1 as Polyline;
                                                windowArea = (line.Area / 1_000_000);

                                                if (room.walls[iNo].windows[iNo1].WindowType.Equals("Openable"))
                                                {
                                                    windowsArea = windowsArea + (line.Area / 1_000_000);
                                                    totalArea = totalArea + (windowArea * (double.Parse(room.walls[iNo].windows[iNo1].OpenAble) / 100.0));
                                                }
                                            }

                                        }
                                    }
                                }

                            }
                            workSheetVentilation.Cells[rowCount, 6].Value = Math.Round(windowsArea, 2);
                            workSheetVentilation.Cells[rowCount, 7].Value = Math.Round(totalArea, 2);
                            rowCount++;
                        }

                        package.Save();
                    }
                }
            }
        }

        public List<CartesianPoint> SortPointsAlongLine(CartesianPoint lineStart, CartesianPoint lineEnd, List<CartesianPoint> points)
        {
            // Calculate the direction vector of the line and normalize it
            CartesianPoint lineDirection = lineEnd.Subtract(lineStart).Normalize();

            // Sort points by their projection onto the line
            var sortedPoints = points.OrderBy(point =>
            {
                CartesianPoint vecToPoint = point.Subtract(lineStart);
                double projectionLength = vecToPoint.DotProduct(lineDirection);
                return projectionLength;
            }).ToList();

            return sortedPoints;
        }
        static bool IsClockwise(List<CartesianPoint> points)
        {
            double sum = 0.0;

            // Ignore Z-coordinate and compute using only X and Y
            for (int i = 0; i < points.Count; i++)
            {
                CartesianPoint current = points[i];
                CartesianPoint next = points[(i + 1) % points.Count]; // Wrap around to the first point
                sum += (next.Coordinates[0] - current.Coordinates[0]) * (next.Coordinates[1] + current.Coordinates[1]);
            }

            // If sum is negative, the points are clockwise
            return sum > 0;
        }

        public static List<CartesianPoint> SortPointsClockwise(List<CartesianPoint> points)
        {
            // Calculate the centroid or use the first point as reference
            CartesianPoint referencePoint = CalculateCentroid(points);

            points.Sort((a, b) =>
            {
                // Get the angle of point a and point b relative to the reference point
                double angleA = Math.Atan2(a.Coordinates[1] - referencePoint.Coordinates[1], a.Coordinates[0] - referencePoint.Coordinates[0]);
                double angleB = Math.Atan2(b.Coordinates[1] - referencePoint.Coordinates[1], b.Coordinates[0] - referencePoint.Coordinates[0]);

                // Sort by angle (clockwise)
                return angleA.CompareTo(angleB);
            });

            return points;
        }

        // Method to calculate the centroid (average of all points)
        private static CartesianPoint CalculateCentroid(List<CartesianPoint> points)
        {
            double sumX = 0, sumY = 0;
            int count = points.Count;

            foreach (var point in points)
            {
                sumX += point.Coordinates[0];
                sumY += point.Coordinates[1];
            }

            CartesianPoint centroid = new CartesianPoint
            {
                Coordinates = new List<double> { sumX / count, sumY / count, 0 }
            };

            return centroid;
        }

        private List<CartesianPoint> SortPointsAnticlockwise(List<CartesianPoint> points)
        {
            CartesianPoint centroid = CalculateCentroid(points);

            points.Sort((a, b) =>
            {
                double angleA = Math.Atan2(a.Coordinates[1] - centroid.Coordinates[1], a.Coordinates[0] - centroid.Coordinates[0]);
                double angleB = Math.Atan2(b.Coordinates[1] - centroid.Coordinates[1], b.Coordinates[0] - centroid.Coordinates[0]);

                // Sort by angle, and if equal, sort by distance to centroid (to break ties)
                int angleComparison = angleA.CompareTo(angleB);
                if (angleComparison == 0)
                {
                    // Compare distances from centroid to break ties
                    double distanceA = Distance(a, centroid);
                    double distanceB = Distance(b, centroid);
                    return distanceA.CompareTo(distanceB);
                }
                return angleComparison;
            });

            return points;
        }

        private static double Distance(CartesianPoint a, CartesianPoint b)
        {
            double dx = a.Coordinates[0] - b.Coordinates[0];
            double dy = a.Coordinates[1] - b.Coordinates[1];
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private double GetAzimuthAngle(Line line, double direction)
        {
            Point3d startPt = line.StartPoint;
            Point3d endPt = line.EndPoint;

            // Calculate the angle in radians
            double angleInRadians = Math.Atan2(endPt.Y - startPt.Y, endPt.X - startPt.X);

            // Convert the angle to degrees
            double angleInDegrees = Math.Round(angleInRadians * (180.0 / Math.PI), 2);

            double B6 = angleInDegrees; // Replace with your actual value
            double result = (B6 + 90) % 360;
            if (result < 0)
            {
                result += 360; // Ensure the result is positive
            }

            double E5 = 90 - direction; // Replace with your actual value
            double D6 = result; // Replace with your actual value

            double azimuth = (E5 - D6) < 0 ? (E5 - D6) + 360 : (E5 - D6);

            return azimuth;
        }

        private void HideWindows(Database db, PromptSelectionResult res, bool visible)
        {
            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                windowLines = new List<Polyline>();
                if (res.Status == PromptStatus.OK)
                {
                    SelectionSet selectionSet = res.Value;

                    ExportValidation exportValidation = new ExportValidation();

                    foreach (SelectedObject so in selectionSet)
                    {
                        ObjectId objId = so.ObjectId;

                        Entity entity = transaction.GetObject(objId, OpenMode.ForWrite) as Entity;
                        if (entity is Polyline)
                        {
                            if (entity.Layer == StringConstants.windowLayerName)
                            {
                                entity.Visible = visible;
                                windowLines.Add(entity as Polyline);
                            }
                        }
                    }
                }

                transaction.Commit();
            }
        }

        private void UnHideWindows(Database db, bool visible)
        {
            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                foreach (Polyline so in windowLines)
                {
                    ObjectId objId = so.ObjectId;

                    Entity entity = transaction.GetObject(objId, OpenMode.ForWrite) as Entity;
                    if (entity is Polyline)
                    {
                        if (entity.Layer == StringConstants.windowLayerName)
                        {
                            entity.Visible = visible;
                            //windowLines.Add(entity as Polyline);
                        }
                    }
                }

                transaction.Commit();
            }
        }

        private void CreateTreeNodes(List<EDSRoomTag> edsRooms, List<Line> wallLines, List<Polyline> windowLines, TreeView treeView)
        {
            segmentCount = new List<LineSelectCount>();
            rooms = new List<EDSExcelRoom>();

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
                            var matchLines = wallLines.FindAll(x => Math.Round(x.Length, 4).Equals(Math.Round(rmLine.Length, 4)));

                            var macLine = matchLines.Find(x => (x.StartPoint.Equals(rmLine.StartPoint) && x.EndPoint.Equals(rmLine.EndPoint)) || (x.EndPoint.Equals(rmLine.StartPoint) && x.StartPoint.Equals(rmLine.EndPoint)));

                            if (macLine != null)
                            {
                                LineSegment segment = new LineSegment(macLine.StartPoint, macLine.EndPoint);
                                if (CheckIfLineSegmentExist(segment))
                                {
                                    // Increment the count if it's already there
                                    foreach (var line in segmentCount)
                                    {
                                        if (line.lineSegment.StartPoint.Equals(segment.StartPoint) && line.lineSegment.EndPoint.Equals(segment.EndPoint))
                                        {
                                            line.iCount++;
                                            line.spaces.Add(room.textHandleId);
                                        }
                                        else if (line.lineSegment.EndPoint.Equals(segment.StartPoint) && line.lineSegment.StartPoint.Equals(segment.EndPoint))
                                        {
                                            line.iCount++;
                                            line.spaces.Add(room.textHandleId);
                                        }
                                    }
                                }
                                else
                                {
                                    // Otherwise, add it to the dictionary
                                    segmentCount.Add(new LineSelectCount() { lineSegment = segment, iCount = 1, spaces = new List<string>() { room.textHandleId } });
                                }

                                if (!room.allWalls.Any(x => x.ObjectId == macLine.ObjectId))
                                {
                                    if (rmLine.StartPoint.IsEqualTo(macLine.StartPoint) && rmLine.EndPoint.IsEqualTo(macLine.EndPoint))
                                    {
                                        room.allWalls.Add(macLine);
                                        FindWindowsForWall(macLine.Id, windowLines, ref wallWindows);
                                        //break;
                                    }
                                    else if (rmLine.EndPoint.IsEqualTo(macLine.StartPoint) && rmLine.StartPoint.IsEqualTo(macLine.EndPoint))
                                    {
                                        room.allWalls.Add(macLine);
                                        FindWindowsForWall(macLine.Id, windowLines, ref wallWindows);
                                        //break;
                                    }
                                }
                            }

                            //var point = GetMidpoint(rmLine);
                            ////var matchLines = wallLines.FindAll(x => x.Length.Equals(rmLine.Length));
                            //foreach (var macLine in wallLines)
                            //{
                            //    if (/*LinesOverlap(rmLine, macLine) && AreLinesParallel(rmLine, macLine)*/IsPointOnLine(macLine, point))
                            //    {
                            //        if (!room.allWalls.Any(x => x.ObjectId == macLine.ObjectId))
                            //        {
                            //            room.allWalls.Add(macLine);
                            //            FindWindowsForWall(macLine.Id, windowLines, ref wallWindows);
                            //            break;
                            //        }

                            //    }
                            //}
                        }
                        room.allWindows = wallWindows;
                    }
                }

                EDSExport eDSExport = new EDSExport();
                eDSExport.mthdOfSortExternalWall(room.allWalls);

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
                    EDSWall eDSWall = new EDSWall();
                    var wall = eDSWall.GetXDataForLine(line);
                    eDSExcelWall.wall = wall;

                    if (segmentCount.Find(x => (x.lineSegment.StartPoint.Equals(line.StartPoint) && x.lineSegment.EndPoint.Equals(line.EndPoint)) || (x.lineSegment.EndPoint.Equals(line.StartPoint) && x.lineSegment.StartPoint.Equals(line.EndPoint))).iCount == 1)
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

        private bool CheckIfLineSegmentExist(LineSegment lineSegment)
        {
            bool result = false;

            foreach (var line in segmentCount)
            {
                if (line.lineSegment.StartPoint.Equals(lineSegment.StartPoint) && line.lineSegment.EndPoint.Equals(lineSegment.EndPoint))
                {
                    return true;
                }
                else if (line.lineSegment.EndPoint.Equals(lineSegment.StartPoint) && line.lineSegment.StartPoint.Equals(lineSegment.EndPoint))
                {
                    return true;
                }
            }

            return result;
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
                                //polyline.Layer = StringConstants.windowLayerName;
                                polyline.Visible = false;

                                BlockTableRecord currentSpace = transaction.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                                var id = currentSpace.AppendEntity(obj as Entity);
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

    }
}
