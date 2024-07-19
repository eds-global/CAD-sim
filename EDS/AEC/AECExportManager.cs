using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using EDS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EDS
{
    internal class AECExportManager
    {
        string headerMessage = "EDS Group";

        string roomTag_Layer = "RoomTag";
        string wall_Layer = "Wall";
        string window_Layer = "Window";

        List<ObjectId> listOfOthersId = new List<ObjectId>();

        List<RoomTag> listOfRoomTagData = new List<RoomTag>();
        List<Wall> listOfWallLineData = new List<Wall>();
        List<Window> listOfWindowData = new List<Window>();

        AECData aecData = new AECData();

        double wallTextHeight = 0;

        string wallType_XData = "Wall Type";

        int windowCount = 0;

        public AECExportManager()
        {
        }

        internal AECData Export(string folderPath, SelectionSet sset, ProgressBar progressBar1)
        {
            progressBar1.Value = 0; progressBar1.Update();
            if (selectObjects(sset))
            {
                progressBar1.Value = 25; progressBar1.Update();
                if (CreateRooms())
                {
                    progressBar1.Value = 45; progressBar1.Update();
                    if (GetWallAndWindowDataOfRooms())
                    {
                        progressBar1.Value = 60; progressBar1.Update();

                        mthdOfSortExternalWall();

                        progressBar1.Value = 75; progressBar1.Update();
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(aecData);

                        string fileNameOnly = Path.GetFileNameWithoutExtension(CADUtilities.GetCurrentDrawingName());

                        string dataFilePath = Path.Combine(folderPath, fileNameOnly + ".json");

                        using (var writer = new StreamWriter(dataFilePath))
                        {
                            writer.Write(jsonString);
                        }

                        return aecData;
                    }
                }
            }

            return null;
        }

        private bool GetWallAndWindowDataOfRooms()
        {
            int wallTag_LayerColor = 3;

            try
            {
                int count = 0;
                foreach (Room room in aecData.Rooms)
                {
                    count++;

                    ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n Extracting Wall & Window Data for {0} ({1} of {2})" , room.Name, count, aecData.Rooms.Count);                    

                    try
                    {
                        string wallTag_Layer = "WallTag_" + wallTag_LayerColor.ToString();

                        ObjectId boundaryId = CADUtilities.HandleToObjectId(room.Handle);

                        List<Line> listOfBoundaryLine = CADUtilities.ExplodePolyline(boundaryId);

                        int wallCount = 1;

                        foreach (Line boundaryLine in listOfBoundaryLine)
                        {
                            Wall newWallData = setWallDataFromBoundary(boundaryLine, room);

                            ObjectId newWallId = CADUtilities.HandleToObjectId(newWallData.Handle);

                            if (newWallId.IsValid == true && newWallId.IsErased == false)
                            {
                                string wallName = "Wall_" + wallCount.ToString();

                                newWallData.Name = wallName;

                                double wallAngle = GetWallNameRotationAngle(newWallId, newWallData);

                                Point3d wallMidPoint = GetWallTextPoint(newWallId, boundaryId);

                                //ObjectId wallNameId = CADUtilities.CreateMText(wallName, wallMidPoint, wallTextHeight, wallAngle, wallTag_Layer, wallTag_LayerColor, "MC");

                                List<Window> listOfNewWindowData = GetListOfWindowExistInWall(newWallId, boundaryId, wallAngle);
                                newWallData.Windows = listOfNewWindowData;

                                Wall newWallData11 = new Wall(newWallData.Layer, newWallData.Name, newWallData.RoomName, newWallData.StartPoint, newWallData.EndPoint, newWallData.Handle, newWallData.Windows, newWallData.CommonWallName);

                                room.Walls.Add(newWallData11);

                                wallCount++;
                            }
                        }

                        if (room.Walls.Count != 0)
                        {
                            //listOfAllNewWallData.AddRange(listOfWallData);
                            //listOfListOfWallData.Add(listOfWallData);
                        }

                        wallTag_LayerColor++;
                    }
                    catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                    {
                        //MessageBox.Show(ex.Message + " : " + room.Name + " : " + room.Handle);
                        //acDoc.Editor.WriteMessage("\n Exception : {0} while processing {1}", ex.Message, room.Name);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message + " : " + room.Name + " : " + room.Handle);
                        //acDoc.Editor.WriteMessage("\n Exception : {0} while processing {1}", ex.Message, room.Name);
                    }
                }

            }
            catch (ZwSoft.ZwCAD.Runtime.Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //acDoc.Editor.WriteMessage("\n Exception : {0}", ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //acDoc.Editor.WriteMessage("\n Exception : {0}", ex.Message);
            }

            //DeleteRooms();

            return true;
        }

        private List<Window> GetListOfWindowExistInWall(ObjectId newWallId, ObjectId boundaryId, double wallAngle)
        {
            List<Window> listOfNewWindowData = new List<Window>();

            foreach (Window windowData in listOfWindowData)
            {
                ObjectId windowId = CADUtilities.HandleToObjectId(windowData.Handle);                

                List<Point3d> listOfInsctPoint = CADUtilities.GetIntersectPointBtwTwoIds(windowId, newWallId);

                if (listOfInsctPoint.Count != 0)
                {
                    string windowName = getWindowName(windowId, boundaryId, wallAngle);

                    windowData.Name = windowName;

                    listOfNewWindowData.Add(windowData);
                }
            }

            return listOfNewWindowData;
        }

        private string getWindowName(ObjectId windowId, ObjectId boundaryId, double wallAngle)
        {
            Point3d windowTextPoint = CADUtilities.GetMidPoint(windowId);

            string windowName = "Window_" + windowCount.ToString();

            Point3d offstPoint = new Point3d((1234567890), (1234567890), 0);
            double offst = 1.2 * wallTextHeight;

            ObjectId offstWallId_Pstv = CADUtilities.OffsetEntity(windowId, offst, offstPoint, false);
            ObjectId offstWallId_Ngtv = CADUtilities.OffsetEntity(windowId, -offst, offstPoint, false);

            double offstWallLngth_Pstv = CADUtilities.GetLengthOfPolyline(offstWallId_Pstv);
            double offstWallLngth_Ngtv = CADUtilities.GetLengthOfPolyline(offstWallId_Ngtv);

            List<Line> listOfExplodedWndwLine = CADUtilities.ExplodePolyline(offstWallId_Ngtv);
            if (offstWallLngth_Pstv > offstWallLngth_Ngtv)
            {
                listOfExplodedWndwLine = CADUtilities.ExplodePolyline(offstWallId_Pstv);
            }

            CADUtilities.Erase(offstWallId_Pstv);
            CADUtilities.Erase(offstWallId_Ngtv);

            if (listOfExplodedWndwLine.Count != 0)
            {
                listOfExplodedWndwLine = listOfExplodedWndwLine.OrderByDescending(x => x.Length).ToList();

                for (int i = 0; i < 2; i++)
                {
                    if (i < listOfExplodedWndwLine.Count)
                    {
                        Line windowLine = listOfExplodedWndwLine[i];

                        Point3d windowMidPoint = CADUtilities.GetMidPoint(windowLine);

                        if (CADUtilities.PointIsInsideThePolyline(boundaryId, windowMidPoint) == false)
                        {
                            windowTextPoint = windowMidPoint;

                            break;
                        }
                    }
                }
            }

            //ObjectId windowNameId = CADUtilities.CreateMText(windowName, windowTextPoint, wallTextHeight, wallAngle, window_Layer, 3, "MC");

            windowCount++;

            return windowName;
        }

        private Point3d GetWallTextPoint(ObjectId wallId, ObjectId boundaryId)
        {
            Point3d wallMidPoint = CADUtilities.GetMidPoint(wallId);

            Point3d offstPoint = new Point3d((1234567890), (1234567890), 0);

            double offst = 1.2 * wallTextHeight;

            ObjectId offstWallId_Pstv = CADUtilities.OffsetEntity(wallId, offst, offstPoint, false);
            ObjectId offstWallId_Ngtv = CADUtilities.OffsetEntity(wallId, -offst, offstPoint, false);

            Point3d offstPstvWall_MidPnt = CADUtilities.GetMidPoint(offstWallId_Pstv);
            Point3d offstNgtvWall_MidPnt = CADUtilities.GetMidPoint(offstWallId_Ngtv);

            if (CADUtilities.PointIsInsideThePolyline(boundaryId, offstPstvWall_MidPnt))
            {
                wallMidPoint = offstPstvWall_MidPnt;
            }
            else if (CADUtilities.PointIsInsideThePolyline(boundaryId, offstNgtvWall_MidPnt))
            {
                wallMidPoint = offstNgtvWall_MidPnt;
            }

            CADUtilities.Erase(offstWallId_Pstv);
            CADUtilities.Erase(offstWallId_Ngtv);

            return wallMidPoint;
        }

        private double GetWallNameRotationAngle(ObjectId newWallId, Wall newWallData)
        {
            double wallAngle = CADUtilities.GetAngleOfLine(newWallId);

            List<Point3d> listOfPoint = new List<Point3d>();
            listOfPoint.Add(newWallData.StartPoint);
            listOfPoint.Add(newWallData.EndPoint);

            if (Utilities.IsAxisX_Angle(wallAngle))
            {
                listOfPoint = listOfPoint.OrderBy(x => x.X).ToList();
            }
            else
            {
                listOfPoint = listOfPoint.OrderBy(x => x.Y).ToList();
            }

            wallAngle = CADUtilities.GetAngleOfLine(listOfPoint[0], listOfPoint[1]);

            return wallAngle;
        }

        private Wall setWallDataFromBoundary(Line boundaryLine, Room boundaryData)
        {
            Point3d midPntOfBoundryLn = CADUtilities.GetMidPoint(boundaryLine);

            Wall newWallData = null;

            foreach (Wall wallData in listOfWallLineData)
            {
                Point3d midPointOfWall = CADUtilities.GetMidPoint(CADUtilities.HandleToObjectId(wallData.Handle));

                double distance = CADUtilities.GetLengthOfLine(midPntOfBoundryLn, midPointOfWall);

                if (distance <= 5)
                {
                    newWallData = new Wall(wallData.Layer, wallData.Name, boundaryData.Name, wallData.StartPoint, wallData.EndPoint, wallData.Handle, wallData.Windows, wallData.CommonWallName);
                    break;
                }
            }

            return newWallData;
        }

        private void DeleteRooms()
        {
            foreach (Room boundaryData in aecData.Rooms)
            {
                CADUtilities.Erase(CADUtilities.HandleToObjectId(boundaryData.Handle));                
            }
        }

        private bool CreateRooms()
        {
            setVisibilityOfObject(false);

            int count = 0;
            foreach (RoomTag roomTagData in listOfRoomTagData)
            {
                count++;

                ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n Creating room {0} of {1}", count, listOfRoomTagData.Count);

                CreateRoomDataByRoomTag(roomTagData);
            }

            setVisibilityOfObject(true);

            return true;
        }

        private void CreateRoomDataByRoomTag(RoomTag roomTagData)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (DocumentLock docLock = doc.LockDocument())
            {

                DBObjectCollection objs = ed.TraceBoundary(roomTagData.TextPoint, true);

                if (objs.Count == 0)
                {
                    string msg = "Selected walls are not close properly. Kindly review it and run the tool again.";

                    MessageBox.Show(msg, headerMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    double area = 0;

                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {
                        BlockTableRecord cspace = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                        foreach (DBObject obj in objs)
                        {
                            Curve acCurve = obj as Curve;

                            if (acCurve != null)
                            {
                                ObjectId curveId = cspace.AppendEntity(acCurve);

                                tr.AddNewlyCreatedDBObject(acCurve, true);

                                ObjectId idmain = curveId;

                                area = acCurve.Area;

                                Entity acEntity = obj as Entity;

                                acEntity.Visible = false;

                                Room roomData = new Room();
                                roomData.Handle = acEntity.Handle.ToString();
                                roomData.Area = area;
                                roomData.Name = roomTagData.RoomName;
                                aecData.Rooms.Add(roomData);
                            }
                        }

                        tr.Commit();
                    }
                }

            }
        }


        private void Reset()
        {
            wallTextHeight = 0;
            windowCount = 1;

            listOfRoomTagData.Clear();
            listOfWallLineData.Clear();
            listOfWindowData.Clear();

            listOfOthersId.Clear();

            aecData.Rooms.Clear();
        }

        private bool selectObjects(SelectionSet sset)
        {
            Reset();

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

                            ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n Processing {0} of {1}", count, sset.Count);

                            ObjectId objId = so.ObjectId;

                            Entity ent = tr.GetObject(objId, OpenMode.ForWrite) as Entity;

                            if (setRoomTagData(ent, objId) == false)
                            {
                                if (setWallData(ent, objId) == false)
                                {
                                    if (setWindowData(ent, objId) == false)
                                    {
                                        listOfOthersId.Add(objId);
                                    }
                                }
                            }
                        }
                    }

                    tr.Commit();
                }
            }

            return true;
        }

        private bool setRoomTagData(Entity ent, ObjectId objId)
        {
            bool flag = false;

            Point3d textPoint = new Point3d();

            string text = "";

            string layer = ent.Layer;

            if (layer == roomTag_Layer)
            {  
                if (ent is MText)
                {
                    MText mTxt = ent as MText;

                    textPoint = mTxt.Location;

                    text = mTxt.Contents;

                    wallTextHeight = mTxt.TextHeight;

                    flag = true;
                }
                else if (ent is DBText)
                {
                    DBText dbTxt = ent as DBText;

                    textPoint = dbTxt.Position;

                    text = dbTxt.TextString;

                    wallTextHeight = dbTxt.Height;

                    flag = true;
                }
            }

            if (flag == true)
            {
                RoomTag roomData = new RoomTag();

                roomData.Layer = layer;
                roomData.RoomName = text;
                roomData.TextPoint = textPoint;
                roomData.TextId = objId;

                listOfRoomTagData.Add(roomData);
            }

            return flag;
        }

        private bool setWallData(Entity ent, ObjectId objId)
        {
            bool flag = false;

            string layer = ent.Layer;

            if (layer == wall_Layer)
            {
                flag = true;

                if (ent is Line)
                {
                    Line wallLine = ent as Line;

                    Wall wallData = new Wall(layer, "", "", wallLine.StartPoint, wallLine.EndPoint, ent.Handle.ToString()  , new List<Window>(), "");

                    listOfWallLineData.Add(wallData);
                }
            }

            return flag;
        }

        private bool setWindowData(Entity ent, ObjectId objId)
        {
            bool flag = false;

            string layer = ent.Layer;

            if (layer == window_Layer)
            {
                flag = true;

                if (ent is ZwSoft.ZwCAD.DatabaseServices.Polyline)
                {
                    ZwSoft.ZwCAD.DatabaseServices.Polyline windowPoly = ent as ZwSoft.ZwCAD.DatabaseServices.Polyline;

                    Window windowData = new Window();

                    windowData.Layer = layer;

                    double width = 0;
                    double length = 0;
                    getLengthAndWidthofWindow(windowPoly, out width, out length);

                    windowData.Width = width;
                    windowData.Length = length;

                    windowData.Handle = ent.Handle.ToString();

                    windowData.SillHeight = CADUtilities.GetXData(objId, "Sill Height");
                    windowData.HeightOfVisionWindow = CADUtilities.GetXData(objId, "Height of Vision Window");
                    windowData.HeightOfDaylightWindow = CADUtilities.GetXData(objId, "Height of Daylight Window");

                    listOfWindowData.Add(windowData);
                }
            }

            return flag;
        }

        private void getLengthAndWidthofWindow(ZwSoft.ZwCAD.DatabaseServices.Polyline windowPoly, out double width, out double length)
        {
            double value1 = Math.Abs(windowPoly.GeometricExtents.MaxPoint.X - windowPoly.GeometricExtents.MinPoint.X);
            double value2 = Math.Abs(windowPoly.GeometricExtents.MaxPoint.Y - windowPoly.GeometricExtents.MinPoint.Y);

            width = Math.Min(value1, value2);
            length = Math.Max(value1, value2);

            List<double> listOfLength = new List<double>();

            DBObjectCollection acDbObjCol = new DBObjectCollection();
            windowPoly.Explode(acDbObjCol);
            foreach (Entity acEnt in acDbObjCol)
            {
                if (acEnt is Line)
                {
                    Line acLine = acEnt as Line;
                    listOfLength.Add(acLine.Length);
                }
            }

            if (listOfLength.Count != 0)
            {
                listOfLength = listOfLength.OrderBy(x => x).ToList();

                width = listOfLength[0];

                length = listOfLength[(listOfLength.Count - 1)];
            }

            width = Math.Round(width, 2);

            length = Math.Round(length, 2);
        }

        private void setVisibilityOfObject(bool IsVisible)
        {
            foreach (Window windowData in listOfWindowData)
            {
                CADUtilities.ChangeVisibility(CADUtilities.HandleToObjectId(windowData.Handle), IsVisible);
            }

            foreach (RoomTag roomTagData in listOfRoomTagData)
            {
                CADUtilities.ChangeVisibility(roomTagData.TextId, IsVisible);
            }

            foreach (ObjectId otherId in listOfOthersId)
            {
                CADUtilities.ChangeVisibility(otherId, IsVisible);
            }
        }


        #region Function is used to sort external wall and Attach with X-Data
        private void mthdOfSortExternalWall()
        {
            List<Room> Rooms = aecData.Rooms;

            List<Wall> listOfInternalWall = new List<Wall>();
            List<Wall> listOfExternalWall = new List<Wall>();

            GetWallWithCommonWall(aecData, out listOfInternalWall, out listOfExternalWall);

            if (listOfInternalWall.Count == 0 && listOfExternalWall.Count == 0)
            {
                return;
            }

            mthdOfSortExternalWall(listOfExternalWall);
        }

        #region Function is used to identify external and internal wal. Assign XData
        private void GetWallWithCommonWall(AECData aecData, out List<Wall> listOfInternalWall, out List<Wall> listOfExternalWall)
        {
            listOfInternalWall = new List<Wall>();
            listOfExternalWall = new List<Wall>();

            List<Wall> listOfWallLineData = new List<Wall>();

            List<string> listOfHandle = new List<string>();

            foreach (Room room in aecData.Rooms)
            {
                foreach (Wall wall in room.Walls)
                {
                    string handle = wall.Handle;

                    ObjectId wallId = CADUtilities.HandleToObjectId(handle);

                    if (wallId.IsErased == false && wallId.IsValid == true)
                    {
                        listOfWallLineData.Add(wall);
                    }
                }
            }

            List<List<Wall>> grpList = listOfWallLineData.GroupBy(x => x.Handle).Select(x => x.ToList()).ToList();

            foreach (List<Wall> wallList in grpList)
            {
                Wall _wallData = wallList[0];

                ObjectId wallId = CADUtilities.HandleToObjectId(_wallData.Handle);

                if (wallList.Count == 1)
                {
                    CADUtilities.SetXData(wallId, wallType_XData, "Externall");
                    listOfExternalWall.Add(_wallData);
                }
                else
                {
                    CADUtilities.SetXData(wallId, wallType_XData, "Internal");
                    listOfInternalWall.Add(_wallData);
                }
            }
        }

        #endregion

        #region Function is used to sort external wall clock wise or anti clock wise
        private void mthdOfSortExternalWall(List<Wall> listOfWallData)
        {
            if (listOfWallData.Count == 0)
            {
                return;
            }

            DirectionData drctionData = GetDirectionData(listOfWallData);

            sortPointAsPerDrctnPnts(drctionData, listOfWallData);
        }
        private DirectionData GetDirectionData(List<Wall> listOfWallData)
        {
            Wall wallData = listOfWallData[0];

            DirectionData drctionData = new DirectionData();

            int index = -1;
            double MinY = 0;

            foreach (Wall _wall in listOfWallData)
            {
                Point3d _startPoint = _wall.StartPoint;
                Point3d _endPoint = _wall.EndPoint;

                double angle11 = CADUtilities.GetAngleOfLine(_startPoint, _endPoint);

                if (CADUtilities.IsHorzObj(_startPoint, _endPoint))
                {
                    if (index == -1)
                    {
                        drctionData.StartPoint = _startPoint;
                        drctionData.EndPoint = _endPoint;

                        index++;
                    }
                    else
                    {
                        MinY = Math.Min(drctionData.StartPoint.Y, drctionData.EndPoint.Y);

                        double _minY = Math.Min(_startPoint.Y, _endPoint.Y);

                        if (MinY >= _minY)
                        {
                            drctionData.StartPoint = _startPoint;
                            drctionData.EndPoint = _endPoint;

                            index++;
                        }
                    }
                }
            }

            if (index == -1)
            {
                drctionData.StartPoint = wallData.StartPoint;
                drctionData.EndPoint = wallData.EndPoint;
            }

            drctionData = getDirectionAsPerGivenDirection(drctionData);

            return drctionData;
        }
        private DirectionData getDirectionAsPerGivenDirection(DirectionData drctionData)
        {
            double angle = CADUtilities.GetAngleOfLine(drctionData.StartPoint, drctionData.EndPoint);

            List<Point3d> listOfPoint = new List<Point3d>();
            listOfPoint.Add(drctionData.StartPoint);
            listOfPoint.Add(drctionData.EndPoint);

            if (Utilities.IsAxisX_Angle(angle))
            {
                if (GenericModule.IsExtWllSrtBbyClkWs)
                {
                    listOfPoint = listOfPoint.OrderByDescending(x => x.X).ToList();
                }
                else
                {
                    listOfPoint = listOfPoint.OrderBy(x => x.X).ToList();
                }
            }
            else
            {
                if (GenericModule.IsExtWllSrtBbyClkWs)
                {
                    listOfPoint = listOfPoint.OrderByDescending(x => x.Y).ToList();
                }
                else
                {
                    listOfPoint = listOfPoint.OrderBy(x => x.Y).ToList();
                }
            }

            drctionData.StartPoint = listOfPoint[0];

            drctionData.EndPoint = listOfPoint[1];

            double angle11 = CADUtilities.GetAngleOfLine(drctionData.StartPoint, drctionData.EndPoint);

            return drctionData;
        }
        private void sortPointAsPerDrctnPnts(DirectionData drctionData, List<Wall> listOfWallData)
        {
            Point3d startPnt_Dirctn = drctionData.StartPoint;
            Point3d endPnt_Dirctn = drctionData.EndPoint;

            bool flagOfPointExist = true;

            int counter = 1;

            List<int> listOfIndexStored = new List<int>();

            List<Point3d> listOfStoredPoint = new List<Point3d>();

            while (flagOfPointExist)
            {
                bool _isSameDirctn = false;
                int indx_byDrctn = -1;

                Wall existWallData = new Wall();

                flagOfPointExist = GetIndexOfPointsExist(counter, listOfWallData, ref listOfIndexStored, startPnt_Dirctn, endPnt_Dirctn, ref _isSameDirctn, ref indx_byDrctn, ref existWallData);

                if (indx_byDrctn != -1)
                {
                    listOfIndexStored.Add(indx_byDrctn);

                    if (_isSameDirctn)
                    {
                        startPnt_Dirctn = existWallData.StartPoint;
                        endPnt_Dirctn = existWallData.EndPoint;
                    }
                    else
                    {
                        startPnt_Dirctn = existWallData.EndPoint;
                        endPnt_Dirctn = existWallData.StartPoint;
                    }

                    ObjectId wallId = CADUtilities.HandleToObjectId(existWallData.Handle);
                    modifyLineObjectId(wallId, startPnt_Dirctn, endPnt_Dirctn);

                    if (counter == 1 && !listOfStoredPoint.Contains(startPnt_Dirctn))
                    {
                        //CADUtilities.CreateMText("Start", startPnt_Dirctn, 200, 0, "MC");
                        listOfStoredPoint.Add(startPnt_Dirctn);
                    }

                    if (!listOfStoredPoint.Contains(endPnt_Dirctn))
                    {
                        //CADUtilities.CreateMText((counter.ToString()), endPnt_Dirctn, 200, 0, "MC");
                    }

                    counter++;
                }
            }
        }
        private bool GetIndexOfPointsExist(int counter, List<Wall> listOfWallData, ref List<int> listOfIndexStored, Point3d startPnt_Dirctn, Point3d endPnt_Dirctn, ref bool _isSameDirctn, ref int indx_byDrctn, ref Wall existWallData)
        {
            double tolerance = 1;

            bool flag = false;

            for (int index = 0; index < listOfWallData.Count; index++)
            {
                if (listOfIndexStored.Contains(index))
                {
                    continue;
                }

                Wall wallData = listOfWallData[index];
                Point3d startPoint = wallData.StartPoint;
                Point3d endPoint = wallData.EndPoint;

                double length_Strt1 = CADUtilities.GetLengthOfLine(startPnt_Dirctn, startPoint);
                double length_Strt2 = CADUtilities.GetLengthOfLine(startPnt_Dirctn, endPoint);

                double minLength_Strt = Math.Min(length_Strt1, length_Strt2);

                double length_End1 = CADUtilities.GetLengthOfLine(endPnt_Dirctn, startPoint);
                double length_End2 = CADUtilities.GetLengthOfLine(endPnt_Dirctn, endPoint);

                double minLength_End = Math.Min(length_End1, length_End2);

                if (counter == 1)
                {
                    if (minLength_End <= tolerance && minLength_Strt <= tolerance)
                    {
                        flag = true;
                    }
                }
                else
                {
                    if (minLength_End <= tolerance)
                    {
                        flag = true;
                    }
                }

                if (flag == true)
                {
                    if (counter == 1)
                    {
                        if (length_Strt1 <= tolerance)
                        {
                            _isSameDirctn = true;
                        }
                    }
                    else
                    {
                        if (length_End1 <= tolerance)
                        {
                            _isSameDirctn = true;
                        }
                    }

                    indx_byDrctn = index;
                    existWallData = wallData;

                    break;
                }
            }

            return flag;
        }
        private void modifyLineObjectId(ObjectId objId, Point3d startPnt_Dirctn, Point3d endPnt_Dirctn)
        {
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock docLock = acDoc.LockDocument())
            {
                try
                {
                    Database db = acDoc.Database;

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = tr.GetObject(objId, OpenMode.ForWrite) as Entity;

                        if (ent is Line)
                        {
                            Line acLine = ent as Line;

                            acLine.StartPoint = startPnt_Dirctn;

                            acLine.EndPoint = endPnt_Dirctn;
                        }

                        tr.Commit();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        #region Helper Classes
        private class DirectionData
        {
            public Point3d StartPoint { set; get; }
            public Point3d EndPoint { set; get; }
        }

        #endregion

        #endregion

        #endregion

    }
}