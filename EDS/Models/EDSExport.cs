using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace EDS.Models
{
    public class EDSExport
    {
        #region Function is used to sort external wall clock wise or anti clock wise
        public void mthdOfSortExternalWall(List<Line> listOfLineData)
        {
            if (listOfLineData.Count == 0)
            {
                return;
            }

            DirectionData drctionData = GetDirectionData(listOfLineData);

            sortPointAsPerDrctnPnts(drctionData, listOfLineData);
        }

        private DirectionData GetDirectionData(List<Line> listOfLineData)
        {
            Line lineData = listOfLineData[0];

            DirectionData drctionData = new DirectionData();

            int index = -1;
            double MinY = 0;

            foreach (Line _line in listOfLineData)
            {
                Point3d _startPoint = _line.StartPoint;
                Point3d _endPoint = _line.EndPoint;

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
                drctionData.StartPoint = lineData.StartPoint;
                drctionData.EndPoint = lineData.EndPoint;
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

        private void sortPointAsPerDrctnPnts(DirectionData drctionData, List<Line> listOfLineData)
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

                Line existLineData = new Line();

                flagOfPointExist = GetIndexOfPointsExist(counter, listOfLineData, ref listOfIndexStored, startPnt_Dirctn, endPnt_Dirctn, ref _isSameDirctn, ref indx_byDrctn, ref existLineData);

                if (indx_byDrctn != -1)
                {
                    listOfIndexStored.Add(indx_byDrctn);

                    if (_isSameDirctn)
                    {
                        startPnt_Dirctn = existLineData.StartPoint;
                        endPnt_Dirctn = existLineData.EndPoint;
                    }
                    else
                    {
                        startPnt_Dirctn = existLineData.EndPoint;
                        endPnt_Dirctn = existLineData.StartPoint;
                    }

                    modifyLineObjectId(existLineData.ObjectId, startPnt_Dirctn, endPnt_Dirctn);

                    if (counter == 1 && !listOfStoredPoint.Contains(startPnt_Dirctn))
                    {
                        listOfStoredPoint.Add(startPnt_Dirctn);
                    }

                    counter++;
                }
            }
        }

        public List<Line> mthdOfSortExternalWallAnti(List<Line> listOfLineData)
        {
            if (listOfLineData.Count == 0)
            {
                return new List<Line>(); // Return an empty list if there are no lines
            }

            DirectionData drctionData = GetDirectionData(listOfLineData);
            return sortPointAsPerDrctnPntsAntiClockwise(drctionData, listOfLineData);
        }

        private List<Line> sortPointAsPerDrctnPntsAntiClockwise(DirectionData drctionData, List<Line> listOfLineData)
        {
            Point3d startPnt_Dirctn = drctionData.StartPoint;
            Point3d endPnt_Dirctn = drctionData.EndPoint;

            bool flagOfPointExist = true;
            int counter = 1;

            List<int> listOfIndexStored = new List<int>();
            List<Line> newLineList = new List<Line>(); // List to store new lines

            while (flagOfPointExist)
            {
                bool _isSameDirctn = false;
                int indx_byDrctn = -1;
                Line existLineData = new Line(); // To hold the existing line data

                flagOfPointExist = GetIndexOfPointsExist(counter, listOfLineData, ref listOfIndexStored, startPnt_Dirctn, endPnt_Dirctn, ref _isSameDirctn, ref indx_byDrctn, ref existLineData);

                if (indx_byDrctn != -1)
                {
                    listOfIndexStored.Add(indx_byDrctn);

                    if (_isSameDirctn)
                    {
                        startPnt_Dirctn = existLineData.StartPoint;
                        endPnt_Dirctn = existLineData.EndPoint;
                    }
                    else
                    {
                        startPnt_Dirctn = existLineData.EndPoint;
                        endPnt_Dirctn = existLineData.StartPoint;
                    }

                    // Create a new line with the current start and end points
                    Line newLine = new Line(startPnt_Dirctn, endPnt_Dirctn);
                    newLineList.Add(newLine); // Add the new line to the list

                    counter++;
                }
            }

            return newLineList; // Return the list of new lines
        }

        private bool GetIndexOfPointsExist(int counter, List<Line> listOfLineData, ref List<int> listOfIndexStored, Point3d startPnt_Dirctn, Point3d endPnt_Dirctn, ref bool _isSameDirctn, ref int indx_byDrctn, ref Line existLineData)
        {
            double tolerance = 1;

            bool flag = false;

            for (int index = 0; index < listOfLineData.Count; index++)
            {
                if (listOfIndexStored.Contains(index))
                {
                    continue;
                }

                Line lineData = listOfLineData[index];
                Point3d startPoint = lineData.StartPoint;
                Point3d endPoint = lineData.EndPoint;

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
                    existLineData = lineData;

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
        #endregion

    }

    public class DirectionData
    {
        public Point3d StartPoint { set; get; }
        public Point3d EndPoint { set; get; }
    }
}
