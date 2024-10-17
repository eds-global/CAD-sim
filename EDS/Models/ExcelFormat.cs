using OfficeOpenXml;
using ResourceLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using static System.Windows.Forms.LinkLabel;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;
using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;

namespace EDS.Models
{
    public class DuplicateWall
    {
        public string type1 { get; set; }
        public string type2 { get; set; }
    }
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

    public class EDSeQuestFloor
    {
        public string spaceName { get; set; }
        public List<Point3d> spacePoints { get; set; }
        public string floorName { get; set; }
        public int azimuth { get; set; }
        public string shape { get; set; }
        public int floorHeight { get; set; }
        public int spaceHeight { get; set; }
        public string diagramData { get; set; }
        public List<EDSeQuestRoom> rooms { get; set; }
    }

    public class EDSeQuestRoom
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string SpaceName { get; set; }
        public string BelongSpace { get; set; }
        public string Shape { get; set; }
        public string ZoneType { get; set; }
        public string PeopleSchedule { get; set; }
        public string LightingSchedule { get; set; }
        public string EquipSchedule { get; set; }
        public string InfSchedule { get; set; }
        public string InfMethod { get; set; }
        public double InfFlowArea { get; set; }
        public int PeopleHgLat { get; set; }
        public int PeopleHgSens { get; set; }
        public int EquipLatent { get; set; }
        public int EquipSensible { get; set; }
        public double LightingWArea { get; set; }
        public double EquipmentWArea { get; set; }
        public int AreaPerson { get; set; }
        public string CSubSrcBtuh { get; set; }
        public string CSubSrcKw { get; set; }
        public string CActivityDesc { get; set; }
        public string roomName { get; set; }
        public List<Point3d> roomPoints { get; set; }
        public List<Point3d> oRoomPoints { get; set; }

    }

    public class EDSeQuestExteriorWall
    {
        public string Name { get; set; }  // EL1  Wall (G.S1.E1)
        public string Type { get; set; }  // EXTERIOR-WALL
        public string Construction { get; set; }  // EL1 EWall Construction
        public string Location { get; set; }  // SPACE-V1
        public bool ShadingSurface { get; set; }  // YES/NO

        // Constructor

        public EDSeQuestExteriorWall()
        {
        }
        public EDSeQuestExteriorWall(string name, string type, string construction, string location, bool shadingSurface)
        {
            Name = name;
            Type = type;
            Construction = construction;
            Location = location;
            ShadingSurface = shadingSurface;
        }
    }

    public class EDSeQuestInteriorWall
    {
        public string Name { get; set; }  // "EL1 NE Wall (G.S1.I1)"
        public string WallType { get; set; } = "INTERIOR-WALL";  // Wall type is fixed as INTERIOR-WALL
        public string NextTo { get; set; }  // "EL1 East Perim Spc (G.E2)"
        public string Construction { get; set; }  // "EL1 IWall Construction"
        public string Location { get; set; }  // "SPACE-V2"

        public void PrintFormattedData(string file)
        {
            EDSScan.WriteContentToINPFile($"\"{Name}\" = {WallType}", file);
            EDSScan.WriteContentToINPFile($"   NEXT-TO         = \"{NextTo}\"", file);
            EDSScan.WriteContentToINPFile($"   CONSTRUCTION    = \"{Construction}\"", file);
            EDSScan.WriteContentToINPFile($"   LOCATION        = {Location}", file);
            EDSScan.WriteContentToINPFile("   ..", file);
        }
    }


    public class EDSQuestWindow
    {
        public string Name { get; set; }  // "EL1  Win (G.S1.E1.W1)"
        public string GlassType { get; set; }  // "EL1 Window Type #2 GT"
        public double FrameWidth { get; set; }  // 0.108333
        public double X { get; set; }  // 1.28851
        public double Y { get; set; }  // 3.10833
        public double Height { get; set; }  // 5.00333
        public double Width { get; set; }  // 72.473
        public double FrameConduct { get; set; }  // 2.781

        public void PrintFormattedData(string file)
        {
            EDSScan.WriteContentToINPFile($"\"{Name}\" = WINDOW", file);
            EDSScan.WriteContentToINPFile($"   GLASS-TYPE       = \"{GlassType}\"", file);
            EDSScan.WriteContentToINPFile($"   FRAME-WIDTH      = {FrameWidth}", file);
            EDSScan.WriteContentToINPFile($"   X                = {X}", file);
            EDSScan.WriteContentToINPFile($"   Y                = {Y}", file);
            EDSScan.WriteContentToINPFile($"   HEIGHT           = {Height}", file);
            EDSScan.WriteContentToINPFile($"   WIDTH            = {Width}", file);
            EDSScan.WriteContentToINPFile($"   FRAME-CONDUCT    = {FrameConduct}", file);
            EDSScan.WriteContentToINPFile("   ..", file);
        }
    }

    public class EDSQuestRoof
    {
        public string Name { get; set; }  // "EL1 Roof (G.E2.E4)"
        public string Construction { get; set; }  // "EL1 Roof Construction"
        public string Location { get; set; }  // TOP
        public bool ShadingSurface { get; set; }  // YES (true or false)

        public void PrintFormattedData(string file)
        {
            EDSScan.WriteContentToINPFile($"\"{Name}\" = EXTERIOR-WALL", file);
            EDSScan.WriteContentToINPFile($"   CONSTRUCTION     = \"{Construction}\"", file);
            EDSScan.WriteContentToINPFile($"   LOCATION         = {Location}", file);
            EDSScan.WriteContentToINPFile($"   SHADING-SURFACE  = {(ShadingSurface ? "YES" : "NO")}", file);
            EDSScan.WriteContentToINPFile("   ..", file);
        }
    }

    public class EDSQuestFL
    {
        public string Name { get; set; }  // "EL1 Flr (G.E2.U2)"
        public string Construction { get; set; }  // "EL1 UFCons (G.S1.U2)"
        public string Location { get; set; }  // BOTTOM

        public void PrintFormattedData(string file)
        {
            EDSScan.WriteContentToINPFile($"\"{Name}\" = UNDERGROUND-WALL", file);
            EDSScan.WriteContentToINPFile($"   CONSTRUCTION     = \"{Construction}\"", file);
            EDSScan.WriteContentToINPFile($"   LOCATION         = {Location}", file);
            EDSScan.WriteContentToINPFile("   ..", file);
        }
    }

    public class EDSQuestZone
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double FlowArea { get; set; }
        public double OAFlowPer { get; set; }
        public int DesignHeatTemp { get; set; }
        public string HeatTempSchedule { get; set; }
        public int DesignCoolTemp { get; set; }
        public string CoolTempSchedule { get; set; }
        public string SizingOption { get; set; }
        public string Space { get; set; }

        public override string ToString()
        {
            return $"\"{Name}\" = ZONE\n" +
                   $"   TYPE             = {Type}\n" +
                   $"   FLOW/AREA        = {FlowArea}\n" +
                   $"   OA-FLOW/PER      = {OAFlowPer}\n" +
                   $"   DESIGN-HEAT-T    = {DesignHeatTemp}\n" +
                   $"   HEAT-TEMP-SCH    = \"{HeatTempSchedule}\"\n" +
                   $"   DESIGN-COOL-T    = {DesignCoolTemp}\n" +
                   $"   COOL-TEMP-SCH    = \"{CoolTempSchedule}\"\n" +
                   $"   SIZING-OPTION    = {SizingOption}\n" +
                   $"   SPACE            = \"{Space}\"\n" +
                   "   ..";
        }
    }

    public class EDSQuestSystemData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string HeatSource { get; set; }
        public string ZoneHeatSource { get; set; }
        public string BaseboardSource { get; set; }
        public double SizingRatio { get; set; }
        public string HumidifierType { get; set; }
        public int MaxSupplyTemp { get; set; }
        public int MinSupplyTemp { get; set; }
        public int MaxHumidity { get; set; }
        public int MinHumidity { get; set; }
        public int EconoLimitTemp { get; set; }
        public int EnthalpyLimit { get; set; }
        public string EconoLockout { get; set; }
        public string OAControl { get; set; }
        public string FanSchedule { get; set; }
        public double SupplyStatic { get; set; }
        public double SupplyEff { get; set; }
        public double ReturnEff { get; set; }
        public double CoolingEIR { get; set; }
        public int FurnaceAux { get; set; }
        public double FurnaceHIR { get; set; }
        public string HumidifierLoc { get; set; }
        public string ControlZone { get; set; }

        public override string ToString()
        {
            return $"\"{Name}\" = SYSTEM\n" +
                   $"   TYPE             = {Type}\n" +
                   $"   HEAT-SOURCE      = {HeatSource}\n" +
                   $"   ZONE-HEAT-SOURCE = {ZoneHeatSource}\n" +
                   $"   BASEBOARD-SOURCE = {BaseboardSource}\n" +
                   $"   SIZING-RATIO     = {SizingRatio}\n" +
                   $"   HUMIDIFIER-TYPE  = {HumidifierType}\n" +
                   $"   MAX-SUPPLY-T     = {MaxSupplyTemp}\n" +
                   $"   MIN-SUPPLY-T     = {MinSupplyTemp}\n" +
                   $"   MAX-HUMIDITY     = {MaxHumidity}\n" +
                   $"   MIN-HUMIDITY     = {MinHumidity}\n" +
                   $"   ECONO-LIMIT-T    = {EconoLimitTemp}\n" +
                   $"   ENTHALPY-LIMIT   = {EnthalpyLimit}\n" +
                   $"   ECONO-LOCKOUT    = {EconoLockout}\n" +
                   $"   OA-CONTROL       = {OAControl}\n" +
                   $"   FAN-SCHEDULE     = \"{FanSchedule}\"\n" +
                   $"   SUPPLY-STATIC    = {SupplyStatic}\n" +
                   $"   SUPPLY-EFF       = {SupplyEff}\n" +
                   $"   RETURN-EFF       = {ReturnEff}\n" +
                   $"   COOLING-EIR      = {CoolingEIR}\n" +
                   $"   FURNACE-AUX      = {FurnaceAux}\n" +
                   $"   FURNACE-HIR      = {FurnaceHIR}\n" +
                   $"   HUMIDIFIER-LOC   = {HumidifierLoc}\n" +
                   $"   CONTROL-ZONE     = \"{ControlZone}\"\n" +
                   "   ..";
        }
    }

    public class EDSScan
    {
        List<Polyline> windowLines = new List<Polyline>();
        List<EDSExcelRoom> rooms = new List<EDSExcelRoom>();
        List<LineSelectCount> segmentCount = new List<LineSelectCount>();
        public static int iWin = 0;
        public static int iInt = 0;
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

                            WriteLineSegmentsToFile(segmentCount.Select(x => x.lineSegment).ToList(), "E:\\text.txt");

                            CreateQuestFile(rooms);

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

        public void WriteLineSegmentsToFile(List<LineSegment> lineSegments, string filePath)
        {
            foreach (var lineSegment in lineSegments)
            {
                lineSegment.WriteToFile(filePath);
            }
        }

        public List<int> FindSequence(List<Point3d> Points, List<Line> lines)
        {
            int n = Points.Count;
            List<int> sequence = new List<int>();
            bool[] visited = new bool[n];
            int currentIndex = 0;

            while (sequence.Count < n)
            {
                sequence.Add(currentIndex);
                visited[currentIndex] = true;
                bool foundNext = false;

                for (int i = 0; i < n; i++)
                {
                    if (!visited[i] && LineExists(Points[currentIndex], Points[i], lines))
                    {
                        currentIndex = i;
                        foundNext = true;
                        break;
                    }
                }

                if (!foundNext)
                {
                    break;  // If no valid next point is found, exit
                }
            }

            // Check if last point connects back to the first point
            if (sequence.Count == n && LineExists(Points[sequence[n - 1]], Points[sequence[0]], lines))
            {
                return sequence;
            }
            else
            {
                return null;  // No valid sequence found
            }
        }

        private bool LineExists(Point3d point1, Point3d point2, List<Line> lines)
        {
            foreach (var line in lines)
            {
                if ((LineSort.ArePointsEqual(line.StartPoint, point1) && LineSort.ArePointsEqual(line.EndPoint, point2)) ||
                    (LineSort.ArePointsEqual(line.StartPoint, point2) && LineSort.ArePointsEqual(line.EndPoint, point1)))
                {
                    return true;
                }
            }
            return false;
        }



        private void CreateQuestFile(List<EDSExcelRoom> rooms)
        {
            List<DuplicateWall> visitedRooms = new List<DuplicateWall>();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;

            string dllPath = Assembly.GetExecutingAssembly().Location;
            string directoryName = System.IO.Path.GetDirectoryName(dllPath);

            var name = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name;

            if (System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".inp")))
                System.IO.File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".inp"));

            var eQuestFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(name), System.IO.Path.GetFileName(name).Split('.')[0] + ".inp");

            string content1 = GetFormattedPreDefinedData();

            WriteContentToINPFile(content1, eQuestFile);

            string content = GetFormattedPolygon();

            WriteContentToINPFile(content, eQuestFile);

            doc.LockDocument();
            var floorLines = new List<Line>();
            var roomLines = new List<List<Line>>();

            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                foreach (EDSExcelRoom room in rooms)
                {
                    List<Line> tempRoomLines = new List<Line>();
                    if (room == null) continue;
                    foreach (var wall in room.walls)
                    {
                        if (wall == null) continue;

                        Line line = transaction.GetObject(CADUtilities.HandleToObjectId(wall.wall.wallHandleId), OpenMode.ForRead) as Line;

                        if (segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.EndPoint))).iCount == 1)
                        {
                            floorLines.Add(line);
                        }

                        tempRoomLines.Add(line);
                    }

                    roomLines.Add(tempRoomLines);
                }
            }

            EDSeQuestFloor seQuestFloor = new EDSeQuestFloor();
            seQuestFloor.rooms = new List<EDSeQuestRoom>();

            LineSort lineSort = new LineSort();
            var floorPoints = lineSort.SortAntiPoints(floorLines);

            Point3d lowermostLeft = FindLowermostLeft(floorPoints.ToList());

            if (GenericModule.IsExtWllSrtBbyClkWs)
            {
                //List<Point3d> finalPoints = GetAllTranslatedPoints(floorLines, floorPoints);

                var indexes = FindSequence(floorPoints, floorLines);

                List<Point3d> finalPoints = new List<Point3d>();
                for (int i = 0; i < indexes.Count; i++)
                    finalPoints.Add(floorPoints[indexes[i]]);

                finalPoints = ConvertPointsToFeet(finalPoints);

                WritePointsToInpFile(finalPoints.ToList(), "EL1 Floor Polygon", eQuestFile);

                seQuestFloor.spaceName = "EL1 Floor Polygon";
                seQuestFloor.spacePoints = finalPoints;

            }
            else
            {
                //List<Point3d> finalPoints = GetAllTranslatedPoints(floorLines, floorPoints);

                var indexes = FindSequence(floorPoints, floorLines);

                List<Point3d> finalPoints = new List<Point3d>();
                for (int i = 0; i < indexes.Count; i++)
                    finalPoints.Add(floorPoints[indexes[i]]);

                finalPoints = ConvertPointsToFeet(floorPoints);

                WritePointsToInpFile(finalPoints.ToList(), "EL1 Floor Polygon", eQuestFile);

                seQuestFloor.spaceName = "EL1 Floor Polygon";
                seQuestFloor.spacePoints = finalPoints;

                /* Old Code
                //next index
                var nFloorPoints = new List<Point3d>();
                ForAntiClockWisePoints(floorPoints, lowermostLeft, nFloorPoints);

                WritePointsToInpFile(nFloorPoints.ToList(), "EL1 Floor Polygon", eQuestFile);

                */

            }

            WriteContentToINPFile("   ..", eQuestFile);

            int iNo = 0;

            foreach (EDSExcelRoom room in rooms)
            {
                EDSeQuestRoom eDSeQuestRoom = new EDSeQuestRoom();
                EDSExport eDSExport = new EDSExport();
                // lines = eDSExport.mthdOfSortExternalWallAnti(roomLines[iNo]);
                //var points = lines.Select(x => x.EndPoint).ToList();

                floorPoints = lineSort.SortAntiPoints(roomLines[iNo]);

                List<Point3d> point3Ds = new List<Point3d>();
                var indexes = FindSequence(floorPoints, roomLines[iNo]);

                foreach (var ind in indexes)
                    point3Ds.Add(floorPoints[ind]);

                floorPoints = point3Ds;

                var lowerLeftMostPoint = FindLowermostLeft(floorPoints);

                if (GenericModule.IsExtWllSrtBbyClkWs)
                {
                    List<Point3d> nRoomPoints = GetAllTranslatedPoints(roomLines[iNo], floorPoints, lowerLeftMostPoint);

                    var fPoints = ConvertPointsToFeet(nRoomPoints);

                    if (!(fPoints[0].X == 0.0 && fPoints[0].Y == 0.0))
                    {
                        var newList = new List<Point3d>();
                        fPoints = CheckMakeZeroFirst(fPoints, floorPoints, ref newList);
                        floorPoints = newList;

                        point3Ds = new List<Point3d>();
                        indexes = FindSequence(floorPoints, roomLines[iNo]);

                        foreach (var ind in indexes)
                            point3Ds.Add(floorPoints[ind]);

                        floorPoints = point3Ds;
                    }

                    WritePointsToInpFile(fPoints.ToList(), "EL1 Space Polygon " + (iNo + 1), eQuestFile);

                    GetEDSQuestRoom(iNo, eDSeQuestRoom, lowerLeftMostPoint, nRoomPoints, floorPoints);
                }
                else
                {
                    List<Point3d> nRoomPoints = GetAllTranslatedPoints(roomLines[iNo], floorPoints, lowerLeftMostPoint);

                    var fPoints = ConvertPointsToFeet(nRoomPoints);

                    if (!(fPoints[0].X == 0.0 && fPoints[0].Y == 0.0))
                    {
                        var newList = new List<Point3d>();
                        fPoints = CheckMakeZeroFirst(fPoints, floorPoints, ref newList);
                        floorPoints = newList;

                        point3Ds = new List<Point3d>();
                        indexes = FindSequence(floorPoints, roomLines[iNo]);

                        foreach (var ind in indexes)
                            point3Ds.Add(floorPoints[ind]);

                        floorPoints = point3Ds;

                    }
                    WritePointsToInpFile(fPoints.ToList(), "EL1 Space Polygon " + (iNo + 1), eQuestFile);

                    GetEDSQuestRoom(iNo, eDSeQuestRoom, lowerLeftMostPoint, nRoomPoints, floorPoints);
                }

                iNo++;
                WriteContentToINPFile("   ..", eQuestFile);
                seQuestFloor.rooms.Add(eDSeQuestRoom);
            }

            iNo = 0;
            foreach (var room in seQuestFloor.rooms)
            {
                if (!(room.roomPoints[0].X == 0.0 && room.roomPoints[0].Y == 0.0))
                {
                    room.roomPoints = CheckMakeZeroFirstMirror(room.roomPoints);
                }

                var nPoints = new List<Point3d>() { room.roomPoints[0] };
                var tPoints = new List<Point3d>();

                for (var i = 1; i < room.roomPoints.Count; i++)
                    tPoints.Add(new Point3d((room.roomPoints[i].Y / 304.8), (room.roomPoints[i].X / 304.8), 0));

                tPoints.Reverse();
                nPoints.AddRange(tPoints);

                WritePointsToInpFile(nPoints.ToList(), "EL1 Space Polygon " + (iNo + 1) + " - SMirro", eQuestFile);
                WriteContentToINPFile("   ..", eQuestFile);
                iNo++;
            }

            content = GetFormattedFloorText();

            WriteContentToINPFile(content, eQuestFile);

            seQuestFloor.floorName = "EL1 Ground Flr";
            seQuestFloor.azimuth = 360;
            seQuestFloor.shape = "POLYGON";
            seQuestFloor.floorHeight = 12;
            seQuestFloor.spaceHeight = 12;
            seQuestFloor.diagramData = "*Bldg Envelope & Loads 1 Diag Data*";

            WriteContentToINPFile(GetFloorINP(seQuestFloor), eQuestFile);
            WriteContentToINPFile("   ..", eQuestFile);

            iNo = 0;

            foreach (var room in seQuestFloor.rooms)
            {
                iWin = 1;
                iInt = 1;
                WriteContentToINPFile(GetFormattedDataForSpace(room), eQuestFile);
                WriteContentToINPFile("   ..", eQuestFile);

                for (int i = 0; i < room.roomPoints.Count; i++)
                {
                    if (i == room.oRoomPoints.Count - 1)
                    {
                        var currPoint = room.oRoomPoints[i];
                        var nextPoint = room.oRoomPoints[0];

                        var lineSegment = segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, currPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, nextPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, currPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, nextPoint)));
                        if (lineSegment != null)
                        {
                            if (lineSegment.iCount == 1)
                            {
                                EDSeQuestExteriorWall questWall = new EDSeQuestExteriorWall()
                                {
                                    Name = $"EL1  Wall {room.BelongSpace} (G.S{(iNo + 1)}.E{(i + 1)})",
                                    Type = "EXTERIOR-WALL",
                                    Construction = "EL1 EWall Construction",
                                    Location = $"SPACE-V{i + 1}",
                                    ShadingSurface = true
                                };
                                WriteContentToINPFile(GetFormattedWallData(questWall), eQuestFile);
                                WriteContentToINPFile("   ..", eQuestFile);
                            }
                            else
                            {
                                var index = rooms.FindIndex(x => x.room.textHandleId.Equals(lineSegment.spaces.Last()));
                                if (ContainsInversePair(visitedRooms, room.SpaceName, seQuestFloor.rooms[index].SpaceName))
                                {

                                }
                                else
                                {
                                    EDSeQuestInteriorWall eDSeQuestInteriorWall = new EDSeQuestInteriorWall()
                                    {
                                        Name = $"EL1 NE Wall (G.S{(iNo + 1)}.I{iInt})",
                                        NextTo = seQuestFloor.rooms[index].SpaceName,
                                        Construction = "EL1 IWall Construction",
                                        Location = $"SPACE-V{i + 1}",
                                    };

                                    eDSeQuestInteriorWall.PrintFormattedData(eQuestFile);
                                    visitedRooms.Add(new DuplicateWall() { type1 = room.SpaceName, type2 = seQuestFloor.rooms[index].SpaceName });
                                    iInt++;
                                }

                            }
                        }
                        var line = WriteWindowContentToINPFile(doc, currPoint, nextPoint, rooms[iNo], iNo, eQuestFile);
                    }
                    else
                    {
                        var currPoint = room.oRoomPoints[i];
                        var nextPoint = room.oRoomPoints[i + 1];

                        var lineSegment = segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, currPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, nextPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, currPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, nextPoint)));
                        //var ty = segmentCount.FindAll(x => Math.Round(x.lineSegment.EndPoint.Y, 4).Equals(currPoint.Y) || Math.Round(x.lineSegment.StartPoint.Y, 4).Equals(currPoint.Y));
                        if (lineSegment != null)
                        {
                            if (lineSegment.iCount == 1)
                            {
                                EDSeQuestExteriorWall questWall = new EDSeQuestExteriorWall()
                                {
                                    Name = $"EL1  Wall {room.BelongSpace} (G.S{(iNo + 1)}.E{(i + 1)})",
                                    Type = "EXTERIOR-WALL",
                                    Construction = "EL1 EWall Construction",
                                    Location = $"SPACE-V{i + 1}",
                                    ShadingSurface = true
                                };
                                WriteContentToINPFile(GetFormattedWallData(questWall), eQuestFile);
                                WriteContentToINPFile("   ..", eQuestFile);
                            }
                            else
                            {
                                var index = rooms.FindIndex(x => x.room.textHandleId.Equals(lineSegment.spaces.Last()));
                                if (room.SpaceName != seQuestFloor.rooms[index].SpaceName)
                                {
                                    if (ContainsInversePair(visitedRooms, room.SpaceName, seQuestFloor.rooms[index].SpaceName))
                                    {

                                    }
                                    else
                                    {
                                        EDSeQuestInteriorWall eDSeQuestInteriorWall = new EDSeQuestInteriorWall()
                                        {
                                            Name = $"EL1 NE Wall (G.S{(iNo + 1)}.I{iInt})",
                                            NextTo = seQuestFloor.rooms[index].SpaceName,
                                            Construction = "EL1 IWall Construction",
                                            Location = $"SPACE-V{i + 1}",
                                        };

                                        eDSeQuestInteriorWall.PrintFormattedData(eQuestFile);
                                        visitedRooms.Add(new DuplicateWall() { type1 = room.SpaceName, type2 = seQuestFloor.rooms[index].SpaceName });
                                        iInt++;
                                    }
                                }

                            }
                        }
                        var line = WriteWindowContentToINPFile(doc, currPoint, nextPoint, rooms[iNo], iNo, eQuestFile);
                        //WriteLineSegmentsToFile(segmentCount.Select(x => x.lineSegment).ToList(), "E:\\text.txt");
                    }


                    /*
                    if (i == room.oRoomPoints.Count - 1)
                    {
                        var currPoint = room.oRoomPoints[i];
                        var nextPoint = room.oRoomPoints[0];

                        var line = WriteWindowContentToINPFile(doc, currPoint, nextPoint, rooms[iNo], iNo, eQuestFile);

                    }
                    else
                    {
                        var currPoint = room.oRoomPoints[i];
                        var nextPoint = room.oRoomPoints[i + 1];

                    }
                    */
                }

                EDSQuestRoof eDSQuestRoof = new EDSQuestRoof
                {
                    Name = $"EL1 Roof (G.S{(iNo + 1)}.E2)",
                    Construction = "EL1 Roof Construction",
                    Location = "TOP",
                    ShadingSurface = true
                };

                eDSQuestRoof.PrintFormattedData(eQuestFile);

                EDSQuestFL eDSQuestFL = new EDSQuestFL
                {
                    Name = $"EL1 Flr (G.S{(iNo + 1)}.U1)",
                    Construction = $"EL1 UFCons (G.S1.U2)",
                    Location = "BOTTOM"
                };

                eDSQuestFL.PrintFormattedData(eQuestFile);

                iNo++;
            }

            var eContent = GetFormattedElectricalData();

            WriteContentToINPFile(eContent, eQuestFile);

            iNo = 0;
            foreach (var room in seQuestFloor.rooms)
            {
                EDSQuestZone zone = new EDSQuestZone
                {
                    Name = $"EL1 South Perim Zn (G.S{iNo + 1})",
                    Type = "CONDITIONED",
                    FlowArea = 0.5,
                    OAFlowPer = 20,
                    DesignHeatTemp = 72,
                    HeatTempSchedule = "S1 Sys1 (PSZ) Heat Sch",
                    DesignCoolTemp = 75,
                    CoolTempSchedule = "S1 Sys1 (PSZ) Cool Sch",
                    SizingOption = "ADJUST-LOADS",
                    Space = room.SpaceName
                };

                EDSQuestSystemData system = new EDSQuestSystemData
                {
                    Name = $"EL1 Sys1 (PSZ) (G.S{iNo + 1})",
                    Type = "PSZ",
                    HeatSource = "FURNACE",
                    ZoneHeatSource = "NONE",
                    BaseboardSource = "NONE",
                    SizingRatio = 1.15,
                    HumidifierType = "NONE",
                    MaxSupplyTemp = 120,
                    MinSupplyTemp = 55,
                    MaxHumidity = 100,
                    MinHumidity = 0,
                    EconoLimitTemp = 70,
                    EnthalpyLimit = 30,
                    EconoLockout = "NO",
                    OAControl = "OA-TEMP",
                    FanSchedule = "S1 Sys1 (PSZ) Fan Sch",
                    SupplyStatic = 1.25,
                    SupplyEff = 0.53,
                    ReturnEff = 0.53,
                    CoolingEIR = 0.263548,
                    FurnaceAux = 0,
                    FurnaceHIR = 1.25,
                    HumidifierLoc = "IN-AIR-HANDLER",
                    ControlZone = zone.Name
                };

                WriteContentToINPFile(system.ToString(), eQuestFile);
                WriteContentToINPFile(zone.ToString(), eQuestFile);

                iNo++;
            }

            var endContent = GetFormattedEndData();

            WriteContentToINPFile(endContent, eQuestFile);

        }

        string GetFormattedEndData()
        {
            string data = @"
$ *********************************************************
$ **                                                     **
$ **                Metering & Misc HVAC                 **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Equipment Controls
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Load Management
$ ---------------------------------------------------------



$ *********************************************************
$ **                                                     **
$ **                    Utility Rates                    **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Ratchets
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Block Charges
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Utility Rates
$ ---------------------------------------------------------

'SCE GS-2 Elec Rate TOU-Opt' = UTILITY-RATE    
   LIBRARY-ENTRY 'SCE GS-2 Elec Rate TOU-Opt'
   ..
'SoCalGas GN-10 Gas Rate' = UTILITY-RATE    
   LIBRARY-ENTRY 'SoCalGas GN-10 Gas Rate'
   ..


$ *********************************************************
$ **                                                     **
$ **                 Output Reporting                    **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Loads Non-Hourly Reporting
$ ---------------------------------------------------------

LOADS-REPORT    
   VERIFICATION     = ( ALL-VERIFICATION )
   SUMMARY          = ( ALL-SUMMARY )
   ..


$ ---------------------------------------------------------
$              Systems Non-Hourly Reporting
$ ---------------------------------------------------------

SYSTEMS-REPORT  
   VERIFICATION     = ( ALL-VERIFICATION )
   SUMMARY          = ( ALL-SUMMARY )
   ..


$ ---------------------------------------------------------
$              Plant Non-Hourly Reporting
$ ---------------------------------------------------------

PLANT-REPORT    
   VERIFICATION     = ( ALL-VERIFICATION )
   SUMMARY          = ( ALL-SUMMARY )
   ..


$ ---------------------------------------------------------
$              Economics Non-Hourly Reporting
$ ---------------------------------------------------------

ECONOMICS-REPORT
   VERIFICATION     = ( ALL-VERIFICATION )
   SUMMARY          = ( ALL-SUMMARY )
   ..


$ ---------------------------------------------------------
$              Hourly Reporting
$ ---------------------------------------------------------


'Hourly Report' = HOURLY-REPORT   
   LIBRARY-ENTRY 'Hourly Report'
   ..


$ ---------------------------------------------------------
$              THE END
$ ---------------------------------------------------------

END ..
COMPUTE ..
STOP ..";
            return data;
        }
        string GetFormattedElectricalData()
        {
            string data = @"
$ *********************************************************
$ **                                                     **
$ **              Electric & Fuel Meters                 **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Electric Meters
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Fuel Meters
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Master Meters
$ ---------------------------------------------------------

'MASTER-METERS 1' = MASTER-METERS   
   MSTR-ELEC-METER  = 'EM1'
   MSTR-FUEL-METER  = 'FM1'
   ..


$ *********************************************************
$ **                                                     **
$ **      HVAC Circulation Loops / Plant Equipment       **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Pumps
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Heat Exchangers
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Circulation Loops
$ ---------------------------------------------------------

'DHW Plant 1 Loop (1)' = CIRCULATION-LOOP
   TYPE             = DHW
   DESIGN-HEAT-T    = 135
   PROCESS-FLOW     = ( 0.479338 )
   PROCESS-SCH      = ( 'DHW Eqp NRes Sch' )
   PROCESS-T        = ( 135 )
   ..


$ ---------------------------------------------------------
$              Chillers
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Boilers
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Domestic Water Heaters
$ ---------------------------------------------------------

'DHW Plant 1 Wtr Htr (1)' = DW-HEATER       
   TYPE             = GAS
   TANK-VOLUME      = 130.198
   CAPACITY         = 0.173528
   HIR-FPLR         = 'DW-Gas-Pilotless-HIR-fPLR'
   TANK-UA          = 5.42491
   LOCATION         = ZONE
   ZONE-NAME        = 'EL1 Core Zn (G.C5)'
   DHW-LOOP         = 'DHW Plant 1 Loop (1)'
   C-RECOV-EFF      = 0.8
   C-STBY-LOSS-FRAC = 2.02966
   C-TANK-EXT-RVAL  = 12
   ..


$ ---------------------------------------------------------
$              Heat Rejection
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Tower Free Cooling
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Photovoltaic Modules
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Electric Generators
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Thermal Storage
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Ground Loop Heat Exchangers
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Compliance DHW (residential dwelling units)
$ ---------------------------------------------------------



$ *********************************************************
$ **                                                     **
$ **            Steam & Chilled Water Meters             **
$ **                                                     **
$ *********************************************************

$ ---------------------------------------------------------
$              Steam Meters
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Chilled Water Meters
$ ---------------------------------------------------------



$ *********************************************************
$ **                                                     **
$ **               HVAC Systems / Zones                  **
$ **                                                     **
$ *********************************************************
";
            return data;
        }

        bool ContainsInversePair(List<DuplicateWall> duplicateWalls, string type1, string type2)
        {
            if (duplicateWalls.Count > 0)
            {
                if (duplicateWalls.Any(x => (x.type2.Equals(type1) && x.type1.Equals(type2)) || (x.type2.Equals(type2) && x.type1.Equals(type1))))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private List<Point3d> CheckMakeZeroFirst(List<Point3d> roomPoints, List<Point3d> floorPoints, ref List<Point3d> nfPoints)
        {
            List<Point3d> newRoomPoints = new List<Point3d>();
            var index = roomPoints.FindIndex(x => x.X == 0.0 && x.Y == 0.0);
            if (index == roomPoints.Count - 1)
            {
                newRoomPoints.Add(roomPoints[index]);
                nfPoints.Add(floorPoints[index]);

                for (int iNo = 0; iNo < roomPoints.Count - 1; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    nfPoints.Add(floorPoints[iNo]);
                }
            }
            else
            {
                for (int iNo = index; iNo < roomPoints.Count; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    nfPoints.Add(floorPoints[iNo]);
                }

                for (int iNo = 0; iNo < index; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    nfPoints.Add(floorPoints[iNo]);
                }
            }

            return newRoomPoints;
        }
        private List<Point3d> CheckMakeZeroFirstMirror(List<Point3d> roomPoints)
        {
            List<Point3d> newRoomPoints = new List<Point3d>();
            var index = roomPoints.FindIndex(x => x.X == 0.0 && x.Y == 0.0);
            if (index == roomPoints.Count - 1)
            {
                newRoomPoints.Add(roomPoints[index]);
                //nfPoints.Add(floorPoints[index]);

                for (int iNo = 0; iNo < roomPoints.Count - 1; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    //nfPoints.Add(floorPoints[iNo]);
                }
            }
            else
            {
                for (int iNo = index; iNo < roomPoints.Count - 1; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    //nfPoints.Add(floorPoints[iNo]);
                }

                for (int iNo = 0; iNo < index; iNo++)
                {
                    newRoomPoints.Add(roomPoints[iNo]);
                    //nfPoints.Add(floorPoints[iNo]);
                }
            }

            return newRoomPoints;
        }
        private void GetEDSQuestRoom(int iNo, EDSeQuestRoom eDSeQuestRoom, Point3d lowerLeftMostPoint, List<Point3d> nRoomPoints, List<Point3d> oRoomPoints)
        {
            eDSeQuestRoom.X = lowerLeftMostPoint.X / 304.8;
            eDSeQuestRoom.Y = lowerLeftMostPoint.Y / 304.8;
            eDSeQuestRoom.roomName = "EL1 Space Polygon " + (iNo + 1);
            eDSeQuestRoom.roomPoints = nRoomPoints;
            eDSeQuestRoom.BelongSpace = "Space " + (iNo + 1) + " ";
            eDSeQuestRoom.SpaceName = $"EL2  Perim Spc (G.S{iNo + 1})";
            eDSeQuestRoom.Shape = "POLYGON";
            eDSeQuestRoom.ZoneType = "CONDITIONED";
            eDSeQuestRoom.PeopleSchedule = "EL1 Bldg Occup Sch";
            eDSeQuestRoom.LightingSchedule = "EL1 Bldg InsLt Sch";
            eDSeQuestRoom.EquipSchedule = "EL1 Bldg Misc Sch";
            eDSeQuestRoom.InfSchedule = "ZG0-S1 (PSZ) P-Inf Sch";
            eDSeQuestRoom.InfMethod = "AIR-CHANGE";
            eDSeQuestRoom.InfFlowArea = 0.0330057;
            eDSeQuestRoom.PeopleHgLat = 200;
            eDSeQuestRoom.PeopleHgSens = 250;
            eDSeQuestRoom.EquipLatent = 0;
            eDSeQuestRoom.EquipSensible = 1;
            eDSeQuestRoom.LightingWArea = 1.49;
            eDSeQuestRoom.EquipmentWArea = 0.75;
            eDSeQuestRoom.AreaPerson = 200;
            eDSeQuestRoom.CSubSrcBtuh = "( 0, 0, 0 )";
            eDSeQuestRoom.CSubSrcKw = "( 0, 0, 0 )";
            eDSeQuestRoom.CActivityDesc = "*Office (Executive/Private)*";
            eDSeQuestRoom.oRoomPoints = oRoomPoints;
        }

        private string GetFormattedFloorText()
        {
            return @"$ *********************************************************
$ **                                                     **
$ **      Floors / Spaces / Walls / Windows / Doors      **
$ **                                                     **
$ *********************************************************
";
        }

        private string GetFormattedPolygon()
        {
            return @"$ ---------------------------------------------------------
$              Polygons
$ ---------------------------------------------------------
";
        }

        private string GetFormattedPreDefinedData()
        {
            return @"INPUT ..


$ ---------------------------------------------------------
$              Abort, Diagnostics
$ ---------------------------------------------------------




$ ---------------------------------------------------------
$              Global Parameters
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Title, Run Periods, Design Days, Holidays
$ ---------------------------------------------------------

TITLE           
   LINE-1           = *Project 40*
   ..

""Entire Year"" = RUN-PERIOD-PD
   BEGIN-MONTH      = 1
   BEGIN-DAY        = 1
   BEGIN-YEAR       = 2024
   END-MONTH        = 12
   END-DAY          = 31
   END-YEAR         = 2024
   ..

""Cooling Design Day"" = DESIGN-DAY      
   TYPE             = COOLING
   DRYBULB-HIGH     = 91
   DRYBULB-RANGE    = 14
   WETBULB-AT-HIGH  = 67
   MONTH            = 7
   NUMBER-OF-DAYS   = 120
   ..
""Heating Design Day"" = DESIGN-DAY      
   TYPE             = HEATING
   DRYBULB-HIGH     = 37
   ..

""Standard US Holidays"" = HOLIDAYS        
   LIBRARY-ENTRY ""US""
   ..


$ ---------------------------------------------------------
$              Compliance Data
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Site and Building Data
$ ---------------------------------------------------------

""Site Data"" = SITE-PARAMETERS 
   ALTITUDE         = 97
   ..

""Building Data"" = BUILD-PARAMETERS
   HOLIDAYS         = ""Standard US Holidays""
   ..



PROJECT-DATA    
   ..


$ ---------------------------------------------------------
$              Materials / Layers / Constructions
$ ---------------------------------------------------------

""EL1 EWall Cons Mat 2 (8.6)"" = MATERIAL        
   TYPE             = RESISTANCE
   RESISTANCE       = 8.6
   ..
""EL1 Roof Cons Mat 4 (2.8)"" = MATERIAL        
   TYPE             = RESISTANCE
   RESISTANCE       = 2.8
   ..
""EL2 UFlr Cons Mat 1 (10.47)"" = MATERIAL        
   TYPE             = RESISTANCE
   RESISTANCE       = 10.47
   ..
""EL1 UFMat (G.S1.U2.M1)"" = MATERIAL        
   TYPE             = RESISTANCE
   RESISTANCE       = 9.03259
   ..
""EL1 UFMat (G.C5.U6.M1)"" = MATERIAL        
   TYPE             = RESISTANCE
   RESISTANCE       = 100
   ..

""EL1 EWall Cons Layers"" = LAYERS          
   MATERIAL         = ( ""Plywd 5/8in (PW04)"", ""Insul Bd 3/4in (IN62)"", 
         ""EL1 EWall Cons Mat 2 (8.6)"", ""GypBd 1/2in (GP01)"" )
   ..
""EL1 Roof Cons Layers"" = LAYERS          
   MATERIAL         = ( ""Blt-Up Roof 3/8in (BR01)"", ""Polyurethane 3in (IN46)"", 
         ""Plywd 5/8in (PW04)"", ""EL1 Roof Cons Mat 4 (2.8)"", 
         ""AcousTile 1/2in (AC02)"" )
   ..
""EL1 Ceilg Cons Layers"" = LAYERS          
   MATERIAL         = ( ""AcousTile 1/2in (AC02)"" )
   ..
""EL1 IFlr Cons Layers"" = LAYERS          
   MATERIAL         = ( ""Conc HW 140lb 6in (HF-C13)"", ""Linoleum Tile (LT01)"" )
   ..
""EL2 GFlr Cons Layers"" = LAYERS          
   MATERIAL         = ( ""EL2 UFlr Cons Mat 1 (10.47)"", 
         ""Conc HW 140lb 6in (HF-C13)"", ""Linoleum Tile (LT01)"" )
   ..
""EL1 UFLyrs (G.S1.U2)"" = LAYERS          
   MATERIAL         = ( ""EL1 UFMat (G.S1.U2.M1)"", ""Light Soil, Damp 12in"", 
         ""Conc HW 140lb 6in (HF-C13)"", ""Linoleum Tile (LT01)"" )
   ..
""EL1 UFLyrs (G.C5.U6)"" = LAYERS          
   MATERIAL         = ( ""EL1 UFMat (G.C5.U6.M1)"", ""Light Soil, Damp 12in"", 
         ""Conc HW 140lb 6in (HF-C13)"", ""Linoleum Tile (LT01)"" )
   ..

""EL1 EWall Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   ABSORPTANCE      = 0.6
   ROUGHNESS        = 4
   LAYERS           = ""EL1 EWall Cons Layers""
   ..
""EL1 Roof Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   ABSORPTANCE      = 0.6
   ROUGHNESS        = 1
   LAYERS           = ""EL1 Roof Cons Layers""
   ..
""EL1 Ceilg Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL1 Ceilg Cons Layers""
   ..
""EL1 IWall Construction"" = CONSTRUCTION    
   TYPE             = U-VALUE
   U-VALUE          = 2.7
   ..
""EL1 IFlr Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL1 IFlr Cons Layers""
   ..
""EL1 IFlSP Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL1 IFlr Cons Layers""
   ..
""EL2 GFlr Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL2 GFlr Cons Layers""
   ..
""EL2 GFlSP Construction"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL2 GFlr Cons Layers""
   ..
""EL1 UFCons (G.S1.U2)"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL1 UFLyrs (G.S1.U2)""
   ..
""EL1 UFCons (G.C5.U6)"" = CONSTRUCTION    
   TYPE             = LAYERS
   LAYERS           = ""EL1 UFLyrs (G.C5.U6)""
   ..


$ ---------------------------------------------------------
$              Glass Type Codes
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Glass Types
$ ---------------------------------------------------------

""EL1 Window Type #1 GT"" = GLASS-TYPE      
   TYPE             = GLASS-TYPE-CODE
   GLASS-TYPE-CODE  = ""2004""
   ..
""EL1 Window Type #2 GT"" = GLASS-TYPE      
   TYPE             = GLASS-TYPE-CODE
   GLASS-TYPE-CODE  = ""2203""
   ..
""EL1 Door Type #1 GT"" = GLASS-TYPE      
   TYPE             = GLASS-TYPE-CODE
   GLASS-TYPE-CODE  = ""1001""
   ..


$ ---------------------------------------------------------
$              Window Layers
$ ---------------------------------------------------------



$ ---------------------------------------------------------
$              Lamps / Luminaries / Lighting Systems
$ ---------------------------------------------------------





$ ---------------------------------------------------------
$              Day Schedules
$ ---------------------------------------------------------

""EL1 Bldg Occup WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0, &D, &D, &D, &D, &D, 0.099, 0.702, 0.9, &D, &D, 
         0.504, &D, 0.9, &D, &D, 0.702, 0.297, 0.099, &D, &D, &D, 0 )
   ..
""EL1 Bldg Occup WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0 )
   ..
""EL1 Bldg InsLt WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.05, &D, &D, &D, &D, &D, 0.2965, 0.798, 0.9, &D, &D, 
         &D, &D, &D, &D, &D, 0.798, 0.5005, 0.2965, &D, 0.101, &D, 0.05 )
   ..
""EL1 Bldg InsLt WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.015 )
   ..
""EL1 Bldg InsLt HDD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0 )
   ..
""EL1 Bldg OffEq WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.12, &D, &D, &D, &D, &D, 0.2214, 0.7596, 0.9, &D, &D, 
         0.7362, &D, 0.9, &D, &D, 0.822, 0.4164, 0.2214, &D, 0.159, &D, 0.12 )
   ..
""EL1 Bldg OffEq WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.036 )
   ..
""EL1 Bldg OffEq HDD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0 )
   ..
""EL1 Bldg Misc WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.2, &D, &D, &D, &D, &D, 0.298, 0.802, 0.9, &D, &D, 
         &D, &D, &D, &D, &D, &D, 0.501, 0.298, &D, 0.2 )
   ..
""EL1 Bldg Misc WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.06 )
   ..
""EL1 Bldg Misc HDD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0 )
   ..
""DHW Eqp NRes WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.05, &D, &D, &D, &D, &D, 0.101, 0.5005, &D, 0.9, &D, 
         &D, &D, &D, &D, 0.696, 0.5005, 0.2965, 0.203, &D, &D, 0.05 )
   ..
""DHW Eqp NRes WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0.015 )
   ..
""DHW Eqp NRes HDD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 0 )
   ..
""ZG0-S1 (PSZ) P-Inf WD"" = DAY-SCHEDULE-PD
   TYPE             = MULTIPLIER
   VALUES           = ( 1, &D, &D, &D, &D, 1.049, 1.25, &D, 0.944, 0.5, 1.25, 
         &D, &D, &D, 0.5, 0.944, 1.25, &D, 1.148, 1, &D, 1.049, &D, 1 )
   ..
""ZG0-S1 (PSZ) P-Inf WEH"" = DAY-SCHEDULE-PD
   TYPE             = MULTIPLIER
   VALUES           = ( 1 )
   ..
""ZG0-S1 (PSZ) P-Inf HDD"" = DAY-SCHEDULE-PD
   TYPE             = MULTIPLIER
   VALUES           = ( 1, &D, &D, &D, &D, &D, &D, 0.5, &D, &D, &D, &D, &D, 
         &D, &D, &D, &D, &D, 1 )
   ..
""ZG0-S1 (PSZ) C-Inf WD"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 1, &D, &D, &D, &D, &D, &D, 0.5, &D, &D, &D, &D, &D, 
         &D, &D, &D, &D, &D, 1 )
   ..
""ZG0-S1 (PSZ) C-Inf WEH"" = DAY-SCHEDULE-PD
   TYPE             = FRACTION
   VALUES           = ( 1 )
   ..
""S1 Sys1 (PSZ) Fan WD"" = DAY-SCHEDULE-PD
   TYPE             = ON/OFF/FLAG
   VALUES           = ( 0, &D, &D, &D, &D, &D, &D, 1, &D, &D, &D, &D, &D, &D, 
         &D, &D, &D, &D, 0 )
   ..
""S1 Sys1 (PSZ) Fan WEH"" = DAY-SCHEDULE-PD
   TYPE             = ON/OFF/FLAG
   VALUES           = ( 0 )
   ..
""S1 Sys1 (PSZ) Cool WD"" = DAY-SCHEDULE-PD
   TYPE             = TEMPERATURE
   VALUES           = ( 82, &D, &D, &D, &D, &D, &D, 76, &D, &D, &D, &D, &D, 
         &D, &D, &D, &D, &D, 82 )
   ..
""S1 Sys1 (PSZ) Cool WEH"" = DAY-SCHEDULE-PD
   TYPE             = TEMPERATURE
   VALUES           = ( 82 )
   ..
""S1 Sys1 (PSZ) Heat WD"" = DAY-SCHEDULE-PD
   TYPE             = TEMPERATURE
   VALUES           = ( 64, &D, &D, &D, &D, &D, &D, 70, &D, &D, &D, &D, &D, 
         &D, &D, &D, &D, &D, 64 )
   ..
""S1 Sys1 (PSZ) Heat WEH"" = DAY-SCHEDULE-PD
   TYPE             = TEMPERATURE
   VALUES           = ( 64 )
   ..

$ ---------------------------------------------------------
$              Week Schedules
$ ---------------------------------------------------------

""EL1 Bldg Occup Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""EL1 Bldg Occup WD"", &D, &D, &D, &D, 
         ""EL1 Bldg Occup WEH"", &D, &D, ""EL1 Bldg Occup WEH"" )
   ..
""EL1 Bldg InsLt Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""EL1 Bldg InsLt WD"", &D, &D, &D, &D, 
         ""EL1 Bldg InsLt WEH"", &D, &D, ""EL1 Bldg InsLt HDD"" )
   ..
""EL1 Bldg OffEq Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""EL1 Bldg OffEq WD"", &D, &D, &D, &D, 
         ""EL1 Bldg OffEq WEH"", &D, &D, ""EL1 Bldg OffEq HDD"" )
   ..
""EL1 Bldg Misc Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""EL1 Bldg Misc WD"", &D, &D, &D, &D, 
         ""EL1 Bldg Misc WEH"", &D, &D, ""EL1 Bldg Misc HDD"" )
   ..
""DHW Eqp NRes Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""DHW Eqp NRes WD"", &D, &D, &D, &D, ""DHW Eqp NRes WEH"", 
         &D, &D, ""DHW Eqp NRes HDD"" )
   ..
""ZG0-S1 (PSZ) P-Inf Wk"" = WEEK-SCHEDULE-PD
   TYPE             = MULTIPLIER
   DAY-SCHEDULES    = ( ""ZG0-S1 (PSZ) P-Inf WD"", &D, &D, &D, &D, 
         ""ZG0-S1 (PSZ) P-Inf WEH"", &D, &D, ""ZG0-S1 (PSZ) P-Inf HDD"" )
   ..
""ZG0-S1 (PSZ) C-Inf Wk"" = WEEK-SCHEDULE-PD
   TYPE             = FRACTION
   DAY-SCHEDULES    = ( ""ZG0-S1 (PSZ) C-Inf WD"", &D, &D, &D, &D, 
         ""ZG0-S1 (PSZ) C-Inf WEH"" )
   ..
""S1 Sys1 (PSZ) Fan Wk"" = WEEK-SCHEDULE-PD
   TYPE             = ON/OFF/FLAG
   DAY-SCHEDULES    = ( ""S1 Sys1 (PSZ) Fan WD"", &D, &D, &D, &D, 
         ""S1 Sys1 (PSZ) Fan WEH"" )
   ..
""S1 Sys1 (PSZ) Cool Wk"" = WEEK-SCHEDULE-PD
   TYPE             = TEMPERATURE
   DAY-SCHEDULES    = ( ""S1 Sys1 (PSZ) Cool WD"", &D, &D, &D, &D, 
         ""S1 Sys1 (PSZ) Cool WEH"" )
   ..
""S1 Sys1 (PSZ) Heat Wk"" = WEEK-SCHEDULE-PD
   TYPE             = TEMPERATURE
   DAY-SCHEDULES    = ( ""S1 Sys1 (PSZ) Heat WD"", &D, &D, &D, &D, 
         ""S1 Sys1 (PSZ) Heat WEH"" )
   ..

$ ---------------------------------------------------------
$              Annual Schedules
$ ---------------------------------------------------------

""EL1 Bldg Occup Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""EL1 Bldg Occup Wk"" )
   ..
""EL1 Bldg InsLt Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""EL1 Bldg InsLt Wk"" )
   ..
""EL1 Bldg OffEq Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""EL1 Bldg OffEq Wk"" )
   ..
""EL1 Bldg Misc Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""EL1 Bldg Misc Wk"" )
   ..
""DHW Eqp NRes Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""DHW Eqp NRes Wk"" )
   ..
""ZG0-S1 (PSZ) P-Inf Sch"" = SCHEDULE-PD
   TYPE             = MULTIPLIER
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""ZG0-S1 (PSZ) P-Inf Wk"" )
   ..
""ZG0-S1 (PSZ) C-Inf Sch"" = SCHEDULE-PD
   TYPE             = FRACTION
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""ZG0-S1 (PSZ) C-Inf Wk"" )
   ..
""S1 Sys1 (PSZ) Fan Sch"" = SCHEDULE-PD
   TYPE             = ON/OFF/FLAG
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""S1 Sys1 (PSZ) Fan Wk"" )
   ..
""S1 Sys1 (PSZ) Cool Sch"" = SCHEDULE-PD
   TYPE             = TEMPERATURE
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""S1 Sys1 (PSZ) Cool Wk"" )
   ..
""S1 Sys1 (PSZ) Heat Sch"" = SCHEDULE-PD
   TYPE             = TEMPERATURE
   MONTH            = ( 12 )
   DAY              = ( 31 )
   WEEK-SCHEDULES   = ( ""S1 Sys1 (PSZ) Heat Wk"" )
   ..";
        }

        public Line WriteWindowContentToINPFile(Document document, Point3d startPoint, Point3d endPoint, EDSExcelRoom edsExcelWalls, int iNo, string file)
        {
            using (Transaction transaction = document.Database.TransactionManager.StartTransaction())
            {
                foreach (EDSExcelWall wal in edsExcelWalls.walls)
                {
                    Line line = transaction.GetObject(CADUtilities.HandleToObjectId(wal.wall.wallHandleId), OpenMode.ForRead) as Line;
                    if ((LineSort.ArePointsEqual(line.StartPoint, startPoint) && LineSort.ArePointsEqual(line.EndPoint, endPoint)) || (LineSort.ArePointsEqual(line.EndPoint, startPoint) && LineSort.ArePointsEqual(line.StartPoint, endPoint)))
                    {
                        if (wal.windows != null)
                        {
                            if (wal.windows.Count > 0)
                            {
                                foreach (EDSWindow eDSWindow in wal.windows)
                                {
                                    Polyline polyline = transaction.GetObject(CADUtilities.HandleToObjectId(eDSWindow.WindHandleId), OpenMode.ForRead) as Polyline;
                                    var point3DCollection = FindLinePolylineIntersection(line, polyline);

                                    if (point3DCollection.Count > 0)
                                    {
                                        var distance1 = startPoint.DistanceTo(point3DCollection[0]);
                                        var distance2 = startPoint.DistanceTo(point3DCollection[1]);

                                        //double minDistance;

                                        //if (distance1 < distance2)
                                        //    minPoint = point3DCollection[0];
                                        //else
                                        //    minPoint = point3DCollection[1];

                                        EDSQuestWindow eDSQuestWindow = new EDSQuestWindow();
                                        eDSQuestWindow.X = (distance1 < distance2) ? (distance1 / 304.8) : (distance2 / 304.8);
                                        eDSQuestWindow.Y = double.Parse(eDSWindow.SillHeight) / 304.8;
                                        eDSQuestWindow.Width = double.Parse(eDSWindow.Width) / 304.8;
                                        eDSQuestWindow.Height = double.Parse(eDSWindow.Height) / 304.8;
                                        eDSQuestWindow.FrameWidth = 0.108333;
                                        eDSQuestWindow.Name = $"EL1  Win Space {(iNo + 1)} (G.S{(iNo + 1)}.E1.W{iWin})";
                                        eDSQuestWindow.GlassType = "EL1 Window Type #2 GT";
                                        eDSQuestWindow.FrameConduct = 2.781;

                                        eDSQuestWindow.PrintFormattedData(file);

                                        iWin++;
                                    }
                                }
                            }
                        }
                    }
                }

                transaction.Commit();
            }
            return null;
        }
        public Point3dCollection FindLinePolylineIntersection(Line line, Polyline polyline)
        {
            Point3dCollection intersectionPoints = new Point3dCollection();
            line.IntersectWith(polyline, Intersect.OnBothOperands, intersectionPoints, IntPtr.Zero, IntPtr.Zero);

            return intersectionPoints;
        }
        public string GetFormattedWallData(EDSeQuestExteriorWall questWall)
        {
            return $"" +
                $"\"{questWall.Name}\" = {questWall.Type}\n" +
                   $"   CONSTRUCTION     = \"{questWall.Construction}\"\n" +
                   $"   LOCATION         = {questWall.Location}\n" +
                   $"   SHADING-SURFACE  = {(questWall.ShadingSurface ? "YES" : "NO")}";
        }

        List<Point3d> ConvertPointsToFeet(List<Point3d> points)
        {
            List<Point3d> point3Ds = new List<Point3d>();
            const double mmToFeet = 304.8;

            // Iterate through each point and convert its coordinates to feet
            foreach (var point in points)
            {
                point3Ds.Add(new Point3d(point.X / mmToFeet, point.Y / mmToFeet, point.Z / mmToFeet));
            }

            return point3Ds;
        }

        private List<Point3d> GetAllTranslatedPoints(List<Line> floorLines, List<Point3d> floorPoints, Point3d leftMostPoint)
        {
            var nFloorPoints = floorPoints;
            //ForClockWisePoints(floorPoints, lowermostLeft, nFloorPoints);

            var tempPoints = new List<Point3d>();
            var newPoint1 = TranslatePoint(ConvertPoint3dTo2d(nFloorPoints[0]), ConvertPoint3dTo2d(leftMostPoint));
            tempPoints.Add(Convert2dToPoint3d(newPoint1.x, newPoint1.y));

            for (int i = 0; i < nFloorPoints.Count - 1; i++)
            {
                var floorPoint = nFloorPoints[i + 1];
                var newPoint = TranslatePoint(ConvertPoint3dTo2d(floorPoint), ConvertPoint3dTo2d(leftMostPoint));
                tempPoints.Add(Convert2dToPoint3d(newPoint.x, newPoint.y));
            }

            var indexes = FindSequence(nFloorPoints, floorLines);

            var finalPoints = new List<Point3d>();
            for (int i = 0; i < indexes.Count; i++)
                finalPoints.Add(tempPoints[indexes[i]]);
            return finalPoints;
        }

        private string GetFloorINP(EDSeQuestFloor seQuestFloor)
        {
            string content1 = $@"""{seQuestFloor.floorName}"" = FLOOR           
   AZIMUTH          = {seQuestFloor.azimuth}
   POLYGON          = ""{seQuestFloor.spaceName}""
   SHAPE            = {seQuestFloor.shape}
   FLOOR-HEIGHT     = {seQuestFloor.floorHeight}
   SPACE-HEIGHT     = {seQuestFloor.spaceHeight}
   C-DIAGRAM-DATA   = {seQuestFloor.diagramData}";

            return content1;
        }

        public string GetFormattedDataForSpace(EDSeQuestRoom questRoom)
        {
            return $@"""{questRoom.SpaceName}"" = SPACE
   X                ={questRoom.X}
   Y                ={questRoom.Y}
   SHAPE            = {questRoom.Shape}
   ZONE-TYPE        = {questRoom.ZoneType}
   PEOPLE-SCHEDULE  = ""{questRoom.PeopleSchedule}""
   LIGHTING-SCHEDUL = ( ""{questRoom.LightingSchedule}"" )
   EQUIP-SCHEDULE   = ( ""{questRoom.EquipSchedule}"" )
   INF-SCHEDULE     = ""{questRoom.InfSchedule}""
   INF-METHOD       = {questRoom.InfMethod}
   INF-FLOW/AREA    = {questRoom.InfFlowArea}
   PEOPLE-HG-LAT    = {questRoom.PeopleHgLat}
   PEOPLE-HG-SENS   = {questRoom.PeopleHgSens}
   EQUIP-LATENT     = ( {questRoom.EquipLatent} )
   EQUIP-SENSIBLE   = ( {questRoom.EquipSensible} )
   LIGHTING-W/AREA  = ( {questRoom.LightingWArea} )
   EQUIPMENT-W/AREA = ( {questRoom.EquipmentWArea} )
   AREA/PERSON      = {questRoom.AreaPerson}
   POLYGON          = ""{questRoom.roomName}""
   C-SUB-SRC-BTUH   = {questRoom.CSubSrcBtuh}
   C-SUB-SRC-KW     = {questRoom.CSubSrcKw}
   C-ACTIVITY-DESC  = {questRoom.CActivityDesc}";
        }
        private static void ForClockWisePoints(List<Point3d> floorPoints, Point3d lowermostLeft, List<Point3d> nFloorPoints)
        {
            //Reverse
            var index = floorPoints.FindIndex(x => x.Equals(lowermostLeft));

            if (index == 0)
            {
                //nFloorPoints.Add(new Point3d(0, 0, 0));

                nFloorPoints.Add(lowermostLeft);

                for (int i = (floorPoints.Count - 1); i >= 1; i--)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }
            }
            else if (index == (floorPoints.Count - 1))
            {
                //nFloorPoints.Add(new Point3d(0, 0, 0));

                nFloorPoints.Add(lowermostLeft);

                for (int i = 0; i <= (floorPoints.Count - 2); i++)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }
            }
            else
            {
                //nFloorPoints.Add(new Point3d(0, 0, 0));

                for (int i = index; i >= 0; i--)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }

                for (int i = (floorPoints.Count - 1); i > index; i--)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }
            }
        }

        public static (double x, double y) TranslatePoint((double x, double y) point, (double x, double y) origin)
        {
            return (point.x - origin.x, point.y - origin.y);
        }

        public (double x, double y) ConvertPoint3dTo2d(Point3d point)
        {
            // Cast the x and y coordinates to int and ignore the z coordinate.
            double x = (double)point.X;
            double y = (double)point.Y;

            return (x, y);
        }

        public Point3d Convert2dToPoint3d(double x, double y)
        {
            // Assign a default value for the z coordinate, such as 0.
            double z = 0;

            // Create and return the Point3d object.
            return new Point3d(x, y, z);
        }
        private static void ForAntiClockWisePoints(List<Point3d> floorPoints, Point3d lowermostLeft, List<Point3d> nFloorPoints)
        {
            //Reverse
            var index = floorPoints.FindIndex(x => x.Equals(lowermostLeft));

            if (index == 0)
            {
                //nFloorPoints.Add(new Point3d(0, 0, 0));
                foreach (var point in floorPoints.ToList())
                    nFloorPoints.Add(point);
            }
            else if (index == (floorPoints.Count - 1))
            {
                //nFloorPoints.Add(new Point3d(0, 0, 0));

                nFloorPoints.Add(lowermostLeft);

                for (int i = 0; i <= (floorPoints.Count - 2); i++)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }
            }
            else
            {
                //nFloorPoints.Add(lowermostLeft);
                //nFloorPoints.Add(new Point3d(0, 0, 0));

                for (int i = index; i <= floorPoints.Count - 1; i++)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }

                for (int i = 0; i < index; i++)
                {
                    nFloorPoints.Add(floorPoints[i]);
                }
            }
        }

        private void WritePointsToInpFile(List<Point3d> points, string polygonName, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"\"{polygonName}\" = POLYGON");

                for (int i = 0; i < points.Count; i++)
                {
                    string vertexName = $"V{i + 1}";
                    string formattedPoint = $"( {points[i].X:F2}, {points[i].Y:F2} )";
                    writer.WriteLine($"\t{vertexName} = {formattedPoint}");
                }
            }
        }

        // Method to find the lowermost left point
        //private Point3d FindLowermostLeft(List<Point3d> points)
        //{
        //    return points.Aggregate((lowermostLeft, next) =>
        //        (Math.Round(next.Y, 4) < Math.Round(lowermostLeft.Y, 4) ||
        //        (Math.Round(next.Y, 4) == Math.Round(lowermostLeft.Y, 4) && Math.Round(next.X, 4) < Math.Round(lowermostLeft.X, 4)))
        //        ? next : lowermostLeft
        //    );
        //}
        private Point3d FindLowermostLeft(List<Point3d> points)
        {
            if (points == null || points.Count == 0)
                throw new ArgumentException("The points list cannot be null or empty.");

            // Step 1: Find the leftmost point(s) based on the X coordinate
            double minX = points.Min(p => p.X);
            var leftmostPoints = points.Where(p => p.X == minX).ToList();

            // Step 2: Among the leftmost points, find the one with the lowest Y coordinate
            return leftmostPoints.Aggregate((lowermost, next) =>
                next.Y < lowermost.Y ? next : lowermost
            );
        }



        public static void WriteContentToINPFile(string content, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(content);
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
                        if (segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.EndPoint))).iCount == 1)
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
                        if (segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.EndPoint))).iCount == 1)
                        {

                        }
                        else
                        {
                            Surface surface = new Surface();
                            surface.ConstructionIdRef = "ASHRAE_189.1-2009_IntWall_Mass_ClimateZone_5";
                            surface.SurfaceType = "Interior Wall";
                            surface.Id = line.Handle.ToString();

                            List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                            var data = segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.EndPoint))).spaces.Distinct();
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
                                Entity entity = transaction.GetObject(CADUtilities.HandleToObjectId(room.walls[iNo].wall.wallHandleId), OpenMode.ForRead) as Entity;
                                Line line1 = entity as Line;

                                if (segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line1.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line1.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line1.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line1.EndPoint))).iCount == 1)
                                {
                                    worksheet.Cells[rowCount, 3].Value = room.walls[iNo].wall.wallHandleId;
                                    worksheet.Cells[rowCount, 4].Value = room.room.spaceType.ToString() + "_" + room.walls[iNo].wall.wallHandleId;
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
                            //var matchLines = wallLines.FindAll(x => Math.Round(x.Length, 4).Equals(Math.Round(rmLine.Length, 4)));

                            //var linel = wallLines.Select(x => Math.Round(x.Length, 4)).ToList();

                            var macLine = wallLines.Find(x => (LineSort.ArePointsEqual(x.StartPoint, rmLine.StartPoint) && LineSort.ArePointsEqual(x.EndPoint, rmLine.EndPoint)) || (LineSort.ArePointsEqual(x.EndPoint, rmLine.StartPoint) && LineSort.ArePointsEqual(x.StartPoint, rmLine.EndPoint)));

                            if (macLine != null)
                            {
                                LineSegment segment = new LineSegment(macLine.StartPoint, macLine.EndPoint);
                                if (CheckIfLineSegmentExist(segment))
                                {
                                    // Increment the count if it's already there
                                    foreach (var line in segmentCount)
                                    {
                                        if (LineSort.ArePointsEqual(line.lineSegment.StartPoint, segment.StartPoint) && LineSort.ArePointsEqual(line.lineSegment.EndPoint, segment.EndPoint))
                                        {
                                            line.iCount++;
                                            line.spaces.Add(room.textHandleId);
                                        }
                                        else if (LineSort.ArePointsEqual(line.lineSegment.EndPoint, segment.StartPoint) && LineSort.ArePointsEqual(line.lineSegment.StartPoint, segment.EndPoint))
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
                                    if ((LineSort.ArePointsEqual(rmLine.StartPoint, macLine.StartPoint)) && (LineSort.ArePointsEqual(rmLine.EndPoint, macLine.EndPoint)))
                                    {
                                        room.allWalls.Add(macLine);
                                        FindWindowsForWall(macLine.Id, windowLines, ref wallWindows);
                                        //break;
                                    }
                                    else if ((LineSort.ArePointsEqual(rmLine.EndPoint, macLine.StartPoint)) && (LineSort.ArePointsEqual(rmLine.StartPoint, macLine.EndPoint)))
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

                LineSort lineSort = new LineSort();
                lineSort.SortPoints(room.allWalls);

                //EDSExport eDSExport = new EDSExport();
                //eDSExport.mthdOfSortExternalWall(room.allWalls);

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

                    if (segmentCount.Find(x => (LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.EndPoint)) || (LineSort.ArePointsEqual(x.lineSegment.EndPoint, line.StartPoint) && LineSort.ArePointsEqual(x.lineSegment.StartPoint, line.EndPoint))).iCount == 1)
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
                if (LineSort.ArePointsEqual(line.lineSegment.StartPoint, lineSegment.StartPoint) && LineSort.ArePointsEqual(line.lineSegment.EndPoint, lineSegment.EndPoint))
                {
                    return true;
                }
                else if (LineSort.ArePointsEqual(line.lineSegment.EndPoint, lineSegment.StartPoint) && LineSort.ArePointsEqual(line.lineSegment.StartPoint, lineSegment.EndPoint))
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
