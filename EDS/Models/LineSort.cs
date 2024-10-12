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
    internal class LineSort
    {
        List<Point3d> GetUniqueEndpoints(List<Line> lines)
        {
            HashSet<Point3d> uniquePoints = new HashSet<Point3d>();

            foreach (Line line in lines)
            {
                Point3d startPoint = new Point3d(Math.Round(line.StartPoint.X, 4), Math.Round(line.StartPoint.Y, 4), 0);
                Point3d endPoint = new Point3d(Math.Round(line.EndPoint.X, 4), Math.Round(line.EndPoint.Y, 4), 0);

                uniquePoints.Add(startPoint);
                uniquePoints.Add(endPoint);
            }

            return uniquePoints.ToList();
        }

        Point3d CalculateCentroidPoint(List<Point3d> points)
        {
            double sumX = 0;
            double sumY = 0;

            foreach (Point3d point in points)
            {
                sumX += point.X;
                sumY += point.Y;
            }

            return new Point3d(sumX / points.Count, sumY / points.Count, 0);
        }

        List<Point3d> SortPointsClockwise(List<Point3d> points, Point3d centroid)
        {
            return points.OrderBy(p =>
            {
                double angle = Math.Atan2(p.Y - centroid.Y, p.X - centroid.X);
                return angle;
            }).ToList();
        }

        public void SortPoints(List<Line> lines)
        {
            //1. Get the unique points from the lines.
            List<Point3d> uniquePoints = GetUniqueEndpoints(lines);

            // 2. Calculate the centroid of the unique points
            Point3d centroid1 = CalculateCentroidPoint(uniquePoints);

            // 3. Sort the points in clockwise order based on the angle they form with the centroid
            List<Point3d> sortedClockwisePoints = SortPointsClockwise(uniquePoints, centroid1);

            if (!GenericModule.IsExtWllSrtBbyClkWs)
                sortedClockwisePoints.Reverse();

            CheckForClockOrAntiClock(sortedClockwisePoints);

            for (int i = 0; i < sortedClockwisePoints.Count; i++)
            {
                if (sortedClockwisePoints[i] != null)
                {
                    if (i == sortedClockwisePoints.Count - 1)
                    {
                        var currPoint = sortedClockwisePoints.ElementAt(i);
                        var nextPoint = sortedClockwisePoints.ElementAt(0);

                        var line = lines.Find(x => (ArePointsEqual(x.StartPoint, currPoint) && ArePointsEqual(x.EndPoint, nextPoint)) || (ArePointsEqual(x.StartPoint, nextPoint) && ArePointsEqual(x.EndPoint, currPoint)));
                        if (line != null)
                            modifyLineObjectId(line.ObjectId, currPoint, nextPoint);
                    }
                    else
                    {
                        var currPoint = sortedClockwisePoints.ElementAt(i);
                        var nextPoint = sortedClockwisePoints.ElementAt(i + 1);

                        var line = lines.Find(x => (ArePointsEqual(x.StartPoint, currPoint) && ArePointsEqual(x.EndPoint, nextPoint)) || (ArePointsEqual(x.StartPoint, nextPoint) && ArePointsEqual(x.EndPoint, currPoint)));
                        if (line != null)
                            modifyLineObjectId(line.ObjectId, currPoint, nextPoint);
                    }
                }
            }

            //CheckForClockOrAntiClock(sortedClockwisePoints);
        }

        public List<Point3d> SortAntiPoints(List<Line> lines)
        {
            List<Point3d> uniquePoints = GetUniqueEndpoints(lines);

            // 2. Calculate the centroid of the unique points
            Point3d centroid1 = CalculateCentroidPoint(uniquePoints);

            // 3. Sort the points in clockwise order based on the angle they form with the centroid
            List<Point3d> sortedClockwisePoints = SortPointsClockwise(uniquePoints, centroid1);

            sortedClockwisePoints.Reverse();

            CheckForAntiClock(sortedClockwisePoints);

            return sortedClockwisePoints;
        }

        private void CheckForClockOrAntiClock(List<Point3d> sortedClockwisePoints)
        {

            var points = new (double x, double y, double z)[sortedClockwisePoints.Count];

            for (int i = 0; i < sortedClockwisePoints.Count; i++)
            {
                points[i].x = sortedClockwisePoints[i].X;
                points[i].y = sortedClockwisePoints[i].Y;
                points[i].z = 0;
            }

            // Calculate the signed area using the shoelace formula
            double area = CalculateSignedArea(points);

            // Determine if the points are clockwise or counterclockwise
            if (area < 0)
            {
                if (!GenericModule.IsExtWllSrtBbyClkWs)
                    sortedClockwisePoints.Reverse();
            }
            else
            {
                if (GenericModule.IsExtWllSrtBbyClkWs)
                    sortedClockwisePoints.Reverse();
            }
        }

        private void CheckForAntiClock(List<Point3d> sortedClockwisePoints)
        {

            var points = new (double x, double y, double z)[sortedClockwisePoints.Count];

            for (int i = 0; i < sortedClockwisePoints.Count; i++)
            {
                points[i].x = sortedClockwisePoints[i].X;
                points[i].y = sortedClockwisePoints[i].Y;
                points[i].z = 0;
            }

            // Calculate the signed area using the shoelace formula
            double area = CalculateSignedArea(points);

            // Determine if the points are clockwise or counterclockwise
            if (area < 0)
            {
                sortedClockwisePoints.Reverse();
            }
            else
            {

            }
        }


        double CalculateSignedArea((double x, double y, double z)[] points)
        {
            double sum1 = 0; // x1y2 + x2y3 + ... + xn-1yn + xny1
            double sum2 = 0; // y1x2 + y2x3 + ... + yn-1xn + ynx1

            int n = points.Length;

            for (int i = 0; i < n; i++)
            {
                double x1 = points[i].x;
                double y1 = points[i].y;
                double x2 = points[(i + 1) % n].x; // Wrap around to the first point
                double y2 = points[(i + 1) % n].y;

                sum1 += x1 * y2;
                sum2 += y1 * x2;
            }

            // The signed area
            return 0.5 * (sum1 - sum2);
        }

        // Function to reverse the start and end points of a line
        private Line modifyLineObjectId(ObjectId objId, Point3d startPnt_Dirctn, Point3d endPnt_Dirctn)
        {
            Line line = new Line(); ;
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
                        line = ent as Line;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return line;
        }

        // Helper function to check if two points are equal, accounting for a tolerance
        public static bool ArePointsEqual(Point3d p1, Point3d p2)
        {
            if ((Math.Round(p1.X, 4) == Math.Round(p2.X, 4)) && (Math.Round(p1.Y, 4) == Math.Round(p2.Y, 4)) && (Math.Round(p1.Z, 4) == Math.Round(p2.Z, 4)))
                return true;
            else
                return false;
        }

    }
}
