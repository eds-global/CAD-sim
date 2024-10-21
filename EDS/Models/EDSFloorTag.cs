using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;
using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;

namespace EDS.Models
{
    public class EDSFloorTag
    {
        public EDSFloorTag() { }
        public string floorName { get; set; }
        public Polyline floorPolyLine { get; set; }
        public ObjectId floorObjectId { get; set; }

        public List<EDSExcelRoom> roomTags = new List<EDSExcelRoom>();
        public List<Line> floorLines = new List<Line>();
        public List<List<Line>> roomLines = new List<List<Line>>();
    }
}

public class LayerFilter
{
    public static List<string> GetFilteredLayer()
    {
        // Get the current document and database
        Document doc = Application.DocumentManager.MdiActiveDocument;
        Database db = doc.Database;
        doc.LockDocument();
        // List to store filtered layer prefixes (first 4 unique characters)
        HashSet<string> filteredLayerPrefixes = new HashSet<string>();

        using (Transaction trans = db.TransactionManager.StartTransaction())
        {
            // Open the LayerTable for read
            LayerTable layerTable = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

            // Regular expression patterns for #B01 to #B04 and #F00 to #F15
            string patternB = @"#B0[1-4]"; // Matches #B01, #B02, #B03, #B04
            string patternF = @"#F0[0-9]|#F1[0-5]"; // Matches #F00 to #F15

            // Loop through all the layers in the LayerTable
            foreach (ObjectId layerId in layerTable)
            {
                LayerTableRecord layer = trans.GetObject(layerId, OpenMode.ForRead) as LayerTableRecord;

                // Check if the layer name matches the patterns
                if (Regex.IsMatch(layer.Name, patternB) || Regex.IsMatch(layer.Name, patternF))
                {
                    // Get the first 4 unique characters of the layer name
                    string prefix = new string(layer.Name.Distinct().Take(4).ToArray());

                    // Add to the HashSet to ensure uniqueness
                    filteredLayerPrefixes.Add(prefix);
                }
            }

            // Commit the transaction
            trans.Commit();
        }

        return filteredLayerPrefixes.ToList();
    }
}


public class PolylineContainmentChecker
{
    // Check if any polyline in the list lies inside another polyline
    public static List<(Polyline Inner, ZwSoft.ZwCAD.DatabaseServices.Polyline Outer)> GetContainedPolylines(List<Polyline> polylines)
    {
        List<(Polyline Inner, Polyline Outer)> containedPolylines = new List<(Polyline Inner, Polyline Outer)>();

        // Compare each polyline with every other polyline in the list
        for (int i = 0; i < polylines.Count; i++)
        {
            for (int j = 0; j < polylines.Count; j++)
            {
                if (i != j)
                {
                    Polyline outerPolyline = polylines[i];
                    Polyline innerPolyline = polylines[j];

                    // Check if the inner polyline is inside the outer polyline
                    if (IsPolylineInside(outerPolyline, innerPolyline))
                    {
                        containedPolylines.Add((innerPolyline, outerPolyline));
                    }
                }
            }
        }
        return containedPolylines;
    }

    // Check if a polyline is completely inside another polyline
    private static bool IsPolylineInside(Polyline outerPolyline, Polyline innerPolyline)
    {
        for (int i = 0; i < innerPolyline.NumberOfVertices; i++)
        {
            Point3d innerPoint = innerPolyline.GetPoint3dAt(i);

            // Check if the point is inside the outer polyline
            if (!IsPointInsidePolyline(outerPolyline, innerPoint))
            {
                return false; // If any point is outside, it's not fully contained
            }
        }
        return true; // All points of the inner polyline are inside the outer polyline
    }

    // Point-in-polygon check using the crossing number algorithm (2D projection)
    private static bool IsPointInsidePolyline(Polyline polyline, Point3d point)
    {
        // Convert the 3D point to 2D (XY plane)
        Point2d testPoint = new Point2d(point.X, point.Y);

        bool isInside = false;
        int numVertices = polyline.NumberOfVertices;

        for (int i = 0, j = numVertices - 1; i < numVertices; j = i++)
        {
            Point2d vertex1 = polyline.GetPoint2dAt(i);
            Point2d vertex2 = polyline.GetPoint2dAt(j);

            // Check if the point lies within the polyline using the crossing number algorithm
            if (((vertex1.Y > testPoint.Y) != (vertex2.Y > testPoint.Y)) &&
                (testPoint.X < (vertex2.X - vertex1.X) * (testPoint.Y - vertex1.Y) / (vertex2.Y - vertex1.Y) + vertex1.X))
            {
                isInside = !isInside;
            }
        }

        return isInside;
    }
}

