using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZwSoft.ZwCAD.Runtime;
using System.Net.NetworkInformation;
using System.Data.Common;
using System.Windows.Media.Animation;
using ZwSoft.ZwCAD.GraphicsSystem;
using ZwSoft.ZwCAD.GraphicsInterface;

namespace EDS
{
    internal class CADUtilities
    {
        public static string GetCurrentDrawingName()
        {          
            return ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name;
        }

        public static Point3d GetPoint(string msg)
        {
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            PromptPointOptions pPtOpts = new PromptPointOptions("");
            pPtOpts.Message = "\n" + msg;

            PromptPointResult pPtRes;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);

            if (pPtRes.Status == PromptStatus.Cancel)
            {
                throw new ZwSoft.ZwCAD.Runtime.Exception();
                ;
            }

            return pPtRes.Value;
        }

        public static Point3d GetPoint(string msg, Point3d basePoint)
        {
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            PromptPointOptions pPtOpts = new PromptPointOptions("");
            pPtOpts.Message = "\n" + msg;
            pPtOpts.UseBasePoint = true;
            pPtOpts.BasePoint = basePoint;

            PromptPointResult pPtRes;
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);

            if (pPtRes.Status == PromptStatus.Cancel)
            {
                throw new ZwSoft.ZwCAD.Runtime.Exception();
                ;
            }

            return pPtRes.Value;
        }
        private static void RegisterXDataApp(string appName, Database acCurDb, Transaction acTrans)
        {
            RegAppTable acRegAppTbl = acTrans.GetObject(acCurDb.RegAppTableId, OpenMode.ForRead) as RegAppTable;

            if (acRegAppTbl.Has(appName) == false)
            {
                using (RegAppTableRecord acRegAppTblRec = new RegAppTableRecord())
                {
                    acRegAppTblRec.Name = appName;

                    acTrans.GetObject(acCurDb.RegAppTableId, OpenMode.ForWrite);
                    acRegAppTbl.Add(acRegAppTblRec);
                    acTrans.AddNewlyCreatedDBObject(acRegAppTblRec, true);
                }
            }
        }

        internal static void SetXData(ObjectId id, string appName, string xdataStr)
        {
            try
            {
                Database acCurDb;
                acCurDb = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;

                var doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                using (doc.LockDocument())
                {
                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        RegisterXDataApp(appName, acCurDb, acTrans);

                        using (ResultBuffer rb = new ResultBuffer())
                        {
                            rb.Add(new TypedValue((int)DxfCode.ExtendedDataRegAppName, appName));
                            rb.Add(new TypedValue((int)DxfCode.ExtendedDataAsciiString, xdataStr));

                            Entity acEnt = acTrans.GetObject(id, OpenMode.ForWrite) as Entity;

                            if (null != acEnt) // don't know why its coming null
                            {
                                acEnt.XData = rb;
                            }
                        }

                        acTrans.Commit();
                    }
                }

            }
            catch (ZwSoft.ZwCAD.Runtime.Exception ex)
            {

            }
        }

        public static ObjectId HandleToObjectId(string handle)
        {
            long ln = Convert.ToInt64(handle, 16);
            Handle hn = new Handle(ln);
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            ObjectId id = db.GetObjectId(false, hn, 0);
            return id;
        }

        public static double GetLengthOfPolyline(ObjectId id)
        {
            double lengthOfLine = 0;

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = tr.GetObject(id, OpenMode.ForRead) as Entity;
                        if (ent is ZwSoft.ZwCAD.DatabaseServices.Polyline)
                        {
                            ZwSoft.ZwCAD.DatabaseServices.Polyline acPoly = ent as ZwSoft.ZwCAD.DatabaseServices.Polyline;
                            lengthOfLine = acPoly.Length;
                        }
                        else if (ent is Polyline2d)
                        {
                            Polyline2d acPoly2d = ent as Polyline2d;
                            lengthOfLine = acPoly2d.Length;
                        }
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
            return lengthOfLine;
        }

        public static List<Point3d> GetIntersectPointBtwTwoIds(ObjectId id1, ObjectId id2)
        {
            List<Point3d> listOfInsctPoint = new List<Point3d>();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent1 = tr.GetObject(id1, OpenMode.ForRead) as Entity;
                        Entity ent2 = tr.GetObject(id2, OpenMode.ForRead) as Entity;
                        Point3dCollection pts3D = new Point3dCollection();

                        ent1.IntersectWith(ent2, Intersect.OnBothOperands, pts3D, IntPtr.Zero, IntPtr.Zero);
                        if (pts3D.Count == 0)
                        {
                            ent2.IntersectWith(ent1, Intersect.OnBothOperands, pts3D, IntPtr.Zero, IntPtr.Zero);
                        }
                        foreach (Point3d pt in pts3D)
                        {
                            listOfInsctPoint.Add(pt);
                        }

                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
            return listOfInsctPoint;
        }

        public static bool PointIsInsideThePolyline(ObjectId polyId, Point3d point)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable blkTbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord blkTblRec = tr.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        Entity ent = tr.GetObject(polyId, OpenMode.ForRead) as Entity;
                        if (ent is ZwSoft.ZwCAD.DatabaseServices.Polyline)
                        {
                            ZwSoft.ZwCAD.DatabaseServices.Polyline pline = ent as ZwSoft.ZwCAD.DatabaseServices.Polyline;
                            double tolerance = Tolerance.Global.EqualPoint;
                            using (MPolygon mpg = new MPolygon())
                            {
                                mpg.AppendLoopFromBoundary(pline, true, tolerance);
                                return mpg.IsPointInsideMPolygon(point, tolerance).Count == 1;
                            }
                        }
                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    //doc.Editor.WriteMessage("\n Exception : {0}", ex.Message);
                    return false;
                }
            }
            return false;
        }

        private static bool IsRightDirection(Editor ed, Curve pCurv, Point3d p, Vector3d vDir)
        {
            try
            {
                Vector3d vNormal = Vector3d.ZAxis;
                if (pCurv.IsPlanar)
                {
                    Plane plane = pCurv.GetPlane();
                    vNormal = plane.Normal;
                    p = p.Project(plane, vDir);
                }
                Point3d pNear = pCurv.GetClosestPointTo(p, true);
                Vector3d vSide = p - pNear;
                Vector3d vDeriv = pCurv.GetFirstDerivative(pNear);
                if (vNormal.CrossProduct(vDeriv).DotProduct(vSide) < 0.0)
                    return true;
                else
                    return false;
            }
            catch (ZwSoft.ZwCAD.Runtime.Exception)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static ObjectId OffsetEntity(ObjectId id, double offsetValue, Point3d offsetPoint, bool visible)
        {
            ObjectId offsetId = new ObjectId();
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {
                        Curve curve = tr.GetObject(id, OpenMode.ForWrite) as Curve;
                        if (curve != null)
                        {
                            BlockTableRecord btr = tr.GetObject(curve.BlockId, OpenMode.ForWrite) as BlockTableRecord;
                            if (btr != null)
                            {
                                Point3d pDir = (Point3d)(ZwSoft.ZwCAD.ApplicationServices.Application.GetSystemVariable("VIEWDIR"));
                                if (pDir != null)
                                {
                                    Point3d pWCS = offsetPoint.TransformBy(ed.CurrentUserCoordinateSystem);
                                    double offset = IsRightDirection(ed, curve, pWCS, pDir.GetAsVector()) ? offsetValue : -offsetValue;
                                    DBObjectCollection acDbObjCol = curve.GetOffsetCurves(offset);
                                    foreach (DBObject obj in acDbObjCol)
                                    {
                                        Curve subCurv = obj as Curve;
                                        if (subCurv != null)
                                        {
                                            subCurv.Visible = visible;
                                            offsetId = btr.AppendEntity(subCurv);
                                            tr.AddNewlyCreatedDBObject(subCurv, true);
                                        }
                                    }
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            return offsetId;
        }

        public static void CreateLayer(string layerName, int colorIndex)
        {
            short colorIndexInShort = Convert.ToInt16(colorIndex);
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        LayerTable ltb = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                        //create a new layout.
                        if (!ltb.Has(layerName))
                        {
                            ltb.UpgradeOpen();

                            LayerTableRecord newLayer = new LayerTableRecord();

                            newLayer.Name = layerName;

                            newLayer.Color = ZwSoft.ZwCAD.Colors.Color.FromColorIndex(ZwSoft.ZwCAD.Colors.ColorMethod.ByAci, colorIndexInShort);

                            ltb.Add(newLayer);

                            tr.AddNewlyCreatedDBObject(newLayer, true);
                        }

                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
        }

        public static MText setAlignmentOfMText(MText mt, string textAligmnt)
        {
            if (textAligmnt == "MC")
            {
                mt.Attachment = AttachmentPoint.MiddleCenter;
            }
            else if (textAligmnt == "BC")
            {
                mt.Attachment = AttachmentPoint.BottomCenter;
            }
            else if (textAligmnt == "TR")
            {
                mt.Attachment = AttachmentPoint.TopRight;
            }
            else if (textAligmnt == "TL")
            {
                mt.Attachment = AttachmentPoint.TopLeft;
            }
            else if (textAligmnt == "BR")
            {
                mt.Attachment = AttachmentPoint.BottomRight;
            }
            else if (textAligmnt == "BL")
            {
                mt.Attachment = AttachmentPoint.BottomLeft;
            }
            return mt;
        }

        public static ObjectId CreateMText(string text, Point3d mtLoc, double textHeight, double rotationAngle, string textAligmnt)
        {
            ObjectId mtId = new ObjectId();

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        // Create our new MText and set its properties
                        MText mt = new MText();
                        mt.Location = mtLoc;
                        mt.Contents = text;
                        mt.TextHeight = textHeight;
                        rotationAngle = (rotationAngle * Math.PI) / 180;
                        mt.Rotation = rotationAngle;
                        mt = setAlignmentOfMText(mt, textAligmnt);

                        // Open the block table, the model space and
                        // add our MText
                        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                        BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        mtId = ms.AppendEntity(mt);
                        tr.AddNewlyCreatedDBObject(mt, true);

                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
            return mtId;
        }

        public static double GetAngleOfLine(ObjectId id)
        {
            double angleOfLine = 0;

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = tr.GetObject(id, OpenMode.ForRead) as Entity;
                        if (ent is Line)
                        {
                            Line acLine = ent as Line;
                            angleOfLine = (180 / Math.PI) * acLine.Angle;
                        }
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
            return angleOfLine;
        }

        public static double GetAngleOfLine(Point3d point1, Point3d point2)
        {
            double angleOfLine = 0;
            using (Line acLine = new Line(point1, point2))
            {
                angleOfLine = (180 / Math.PI) * acLine.Angle;
            }
            return angleOfLine;
        }

        public static Point3d GetMidPoint(ObjectId id)
        {
            Point3d midPoint = new Point3d();
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;

                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {
                        Curve curve = tr.GetObject(id, OpenMode.ForWrite) as Curve;

                        if (curve != null)
                        {
                            double d1 = curve.GetDistanceAtParameter(curve.StartParam);
                            double d2 = curve.GetDistanceAtParameter(curve.EndParam);
                            if (d1 == 0 && d2 == 0)
                            {
                                return midPoint;
                            }
                            else
                            {
                                midPoint = curve.GetPointAtDist(d1 + ((d2 - d1) / 2.0));
                            }
                        }
                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
            return midPoint;
        }

        public static double GetLengthOfLine(Point3d point1, Point3d point2)
        {
            double lengthOfLine = 0;
            using (Line acLine = new Line(point1, point2))
            {
                lengthOfLine = acLine.Length;
            }
            return lengthOfLine;
        }

        public static Point3d GetMidPoint(Line acLine)
        {
            Point3d startPoint = acLine.StartPoint;
            Point3d endPoint = acLine.EndPoint;

            Point3d midPoint = GetMidPointBtwTwoPoint(startPoint, endPoint);

            return midPoint;
        }

        public static Point3d GetMidPointBtwTwoPoint(Point3d startPoint, Point3d endPoint)
        {
            double minX = Math.Min(startPoint.X, endPoint.X);
            double minY = Math.Min(startPoint.Y, endPoint.Y);
            double maxX = Math.Max(startPoint.X, endPoint.X);
            double maxY = Math.Max(startPoint.Y, endPoint.Y);

            double centerX = minX + ((maxX - minX) / 2);
            double centerY = minY + ((maxY - minY) / 2);

            Point3d midPoint = new Point3d(centerX, centerY, 0);

            return midPoint;
        }

        public static void ChangeVisibility(ObjectId id, bool visible)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;
                    Editor ed = doc.Editor;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable blkTbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord blkTblRec = tr.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;
                        ent.Visible = visible;

                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void Erase(ObjectId id)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    if (id.IsValid == false || id.IsErased == true)
                    {
                        return;
                    }
                    if (id.Database.TransactionManager.TopTransaction != null)
                    {
                        id.GetObject(OpenMode.ForWrite).Erase();
                    }
                    else
                    {
#pragma warning disable CS0618
                        using (var obj = id.Open(OpenMode.ForWrite))
                            obj.Erase();
#pragma warning restore CS0618
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                }
            }
        }

        public static List<Line> ExplodePolyline(ObjectId polylineId)
        {
            List<Line> listOfExplodeEnt = new List<Line>();

            if (polylineId.IsValid == false || polylineId.IsErased == true)
            {
                return listOfExplodeEnt;
            }
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                try
                {
                    Database db = doc.Database;

                    Editor ed = doc.Editor;

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable blkTbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord blkTblRec = tr.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                        Entity ent = tr.GetObject(polylineId, OpenMode.ForRead) as Entity;
                        if (ent is ZwSoft.ZwCAD.DatabaseServices.Polyline)
                        {
                            DBObjectCollection acDbObjCol = new DBObjectCollection();
                            ZwSoft.ZwCAD.DatabaseServices.Polyline acPoly = ent as ZwSoft.ZwCAD.DatabaseServices.Polyline;
                            acPoly.Explode(acDbObjCol);
                            foreach (Entity acEnt in acDbObjCol)
                            {
                                if (acEnt is Line)
                                {
                                    Line acLine = acEnt as Line;
                                    listOfExplodeEnt.Add(acLine);
                                }
                            }
                        }

                        tr.Commit();
                    }
                }
                catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return listOfExplodeEnt;
        }

        ///////////////////////////////////////////////////

        public static void SetUCSWorld()
        {
            // ' Get the current document and database
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            Editor ed = acDoc.Editor;
            ed.CurrentUserCoordinateSystem = new Matrix3d(new double[16]{
   1.0, 0.0, 0.0, 0.0,
   0.0, 1.0, 0.0, 0.0,
   0.0, 0.0, 1.0, 0.0,
   0.0, 0.0, 0.0, 1.0});

            ed.UpdateScreen();
        }
        //public static List<Point3d> GetIntersectPointBtwTwoIds(ObjectId id1, ObjectId id2)
        //{
        //    List<Point3d> listOfInsctPoint = new List<Point3d>();

        //    Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    using (DocumentLock docLock = doc.LockDocument())
        //    {
        //        try
        //        {
        //            Database db = doc.Database;
        //            Editor ed = doc.Editor;
        //            using (Transaction tr = db.TransactionManager.StartTransaction())
        //            {
        //                Entity ent1 = tr.GetObject(id1, OpenMode.ForRead) as Entity;
        //                Entity ent2 = tr.GetObject(id2, OpenMode.ForRead) as Entity;
        //                Point3dCollection pts3D = new Point3dCollection();

        //                ent1.IntersectWith(ent2, Intersect.OnBothOperands, pts3D, IntPtr.Zero, IntPtr.Zero);
        //                if (pts3D.Count == 0)
        //                {
        //                    ent2.IntersectWith(ent1, Intersect.OnBothOperands, pts3D, IntPtr.Zero, IntPtr.Zero);
        //                }
        //                foreach (Point3d pt in pts3D)
        //                {
        //                    listOfInsctPoint.Add(pt);
        //                }

        //                tr.Commit();
        //            }
        //        }
        //        catch (ZwSoft.ZwCAD.Runtime.Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    return listOfInsctPoint;
        //}
        public static string CreateCircle(Point3d centerPoint, double radian, string layerName, int colorIndex)
        {
            ObjectId circleId = new ObjectId();
            string handle = "";
            try
            {
                CreateLayer(layerName, colorIndex);
                // Get the current document and database
                ZwSoft.ZwCAD.ApplicationServices.Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                using (DocumentLock docLock = doc.LockDocument())
                {
                    ZwSoft.ZwCAD.DatabaseServices.Database acCurDb = doc.Database;

                    // Start a transaction
                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        // Open the Block table for read
                        BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                        // Open the Block table record Model space for write
                        BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        // Create a line that starts at 5,5 and ends at 12,3
                        using (Circle acCircle = new Circle())
                        {
                            acCircle.Center = centerPoint;
                            acCircle.Radius = radian;
                            acCircle.ColorIndex = 256;
                            acCircle.Layer = layerName;
                            circleId = acBlkTblRec.AppendEntity(acCircle);
                            handle = acCircle.Handle.ToString();
                            acTrans.AddNewlyCreatedDBObject(acCircle, true);
                        }
                        // Save the new object to the database
                        acTrans.Commit();
                    }
                }
            }
            catch (ZwSoft.ZwCAD.Runtime.Exception ex)
            {

            }
            return handle;
        }

        public static string CreateLine(Point3d startPoint, Point3d endPoint, string layerName, int colorIndex)
        {
            ObjectId lineId = new ObjectId();
            string handle = "";
            try
            {
                CreateLayer(layerName, colorIndex);
                // Get the current document and database
                ZwSoft.ZwCAD.ApplicationServices.Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                using (DocumentLock docLock = doc.LockDocument())
                {
                    ZwSoft.ZwCAD.DatabaseServices.Database acCurDb = doc.Database;

                    // Start a transaction
                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        // Open the Block table for read
                        BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                        // Open the Block table record Model space for write
                        BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        // Create a line that starts at 5,5 and ends at 12,3
                        using (Line acLine = new Line())
                        {
                            acLine.StartPoint = startPoint;
                            acLine.EndPoint = endPoint;
                            acLine.ColorIndex = 256;
                            acLine.Layer = layerName;
                            lineId = acBlkTblRec.AppendEntity(acLine);
                            handle = acLine.Handle.ToString();
                            acTrans.AddNewlyCreatedDBObject(acLine, true);
                        }
                        // Save the new object to the database
                        acTrans.Commit();
                    }
                }
            }
            catch (ZwSoft.ZwCAD.Runtime.Exception ex)
            {

            }
            return handle;
        }

        //public static void CreateLayer(string layerName, int colorIndex)
        //{
        //    short colorIndexInShort = Convert.ToInt16(colorIndex);
        //    Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    using (DocumentLock docLock = doc.LockDocument())
        //    {
        //        try
        //        {
        //            Database db = doc.Database;
        //            Editor ed = doc.Editor;
        //            using (Transaction tr = db.TransactionManager.StartTransaction())
        //            {
        //                LayerTable ltb = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

        //                //create a new layout.
        //                if (!ltb.Has(layerName))
        //                {
        //                    ltb.UpgradeOpen();

        //                    LayerTableRecord newLayer = new LayerTableRecord();

        //                    newLayer.Name = layerName;

        //                    newLayer.Color = ZwSoft.ZwCAD.Colors.Color.FromColorIndex(ZwSoft.ZwCAD.Colors.ColorMethod.ByAci, colorIndexInShort);

        //                    ltb.Add(newLayer);

        //                    tr.AddNewlyCreatedDBObject(newLayer, true);
        //                }

        //                tr.Commit();
        //            }
        //        }
        //        catch (ZwSoft.ZwCAD.Runtime.Exception ex)
        //        {

        //        }
        //    }
        //}
        //public static double GetLengthOfLine(Point3d point1, Point3d point2)
        //{
        //    double lengthOfLine = 0;
        //    using (Line acLine = new Line(point1, point2))
        //    {
        //        lengthOfLine = acLine.Length;
        //    }
        //    return lengthOfLine;
        //}

        public static ObjectId selectObject(string msg)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptEntityOptions peo = new PromptEntityOptions(msg);
            //peo.SetRejectMessage("\nA Block reference must be selcted");
            //peo.AddAllowedClass(typeof(BlockReference), true);
            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return ObjectId.Null;

            return per.ObjectId;
        }

        public static SelectionSet selectObjects(string message)
        {
            SelectionSet acSSet = null;

            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock locDoc = acDoc.LockDocument())
            {
                Editor ed = acDoc.Editor;

                Database db = HostApplicationServices.WorkingDatabase;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    PromptSelectionResult acSSPrompt = default(PromptSelectionResult);

                    PromptSelectionOptions acSlcOpt = new PromptSelectionOptions();

                    acSlcOpt.MessageForAdding = message;

                    acSSPrompt = ed.GetSelection(acSlcOpt);

                    if (acSSPrompt.Status == PromptStatus.OK)
                    {
                        acSSet = acSSPrompt.Value;
                    }

                    tr.Commit();
                }
            }

            return acSSet;
        }

        public static string GetXData(ObjectId id, string appName)
        {
            string xdataStr = "";

            Database acCurDb;
            acCurDb = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;

            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                Entity acEnt = acTrans.GetObject(id, OpenMode.ForRead) as Entity;

                ResultBuffer rb = acEnt.GetXDataForApplication(appName);

                if (rb != null)
                {
                    bool appNameFound = false;
                    foreach (TypedValue typeVal in rb)
                    {
                        if (string.Equals(appName, typeVal.Value))
                        {
                            appNameFound = true;
                            continue;
                        }

                        if (appNameFound == true)
                        {
                            xdataStr = (string)typeVal.Value;
                            break;
                        }
                    }
                }

                acTrans.Abort();
            }

            return xdataStr;
        }

        internal static DBObjectCollection Offset(ObjectId id, double disttance)
        {
            DBObjectCollection acDbObjColl = null;

            Database acCurDb;
            acCurDb = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;

            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (DocumentLock docLock = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    Entity acEnt = acTrans.GetObject(id, OpenMode.ForRead) as Entity;

                    if (acEnt != null)
                    {
                        Curve curveObj = acEnt as Curve;

                        if (curveObj != null)
                        {
                            acDbObjColl = curveObj.GetOffsetCurves(disttance);

                            try
                            {
                                BlockTable acBlkTbl;
                                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                                BlockTableRecord acBlkTblRec;
                                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                                foreach (Entity ent in acDbObjColl)
                                {
                                    acBlkTblRec.AppendEntity(ent);
                                    acTrans.AddNewlyCreatedDBObject(ent, true);
                                }
                            }
                            catch (ZwSoft.ZwCAD.Runtime.Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }

                    acTrans.Commit();
                }
            }
            return acDbObjColl;
        }

        internal static ObjectId CreateRectangle(Point2d a, Point2d b, Point2d c, Point2d d, string layerName, int colorIndex)
        {
            ObjectId recId = ObjectId.Null;
            // Get the current document and database
            Document acDoc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock docLock = acDoc.LockDocument())
            {
                // Start a transaction
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    CreateLayer(layerName, colorIndex);

                    // Open the Block table for read
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a polyline with two segments (3 points)
                    using (ZwSoft.ZwCAD.DatabaseServices.Polyline acPoly = new ZwSoft.ZwCAD.DatabaseServices.Polyline())
                    {
                        acPoly.AddVertexAt(0, a, 0, 0, 0);
                        acPoly.AddVertexAt(1, b, 0, 0, 0);
                        acPoly.AddVertexAt(2, c, 0, 0, 0);
                        acPoly.AddVertexAt(2, d, 0, 0, 0);

                        acPoly.Closed = true;

                        acPoly.Layer = layerName;

                        // Add the new object to the block table record and the transaction
                        recId = acBlkTblRec.AppendEntity(acPoly);
                        acTrans.AddNewlyCreatedDBObject(acPoly, true);
                    }

                    // Save the new object to the database
                    acTrans.Commit();
                }
            }

            return recId;
        }

        public static bool IsHorzObj(Point3d strtPoint, Point3d endPoint)
        {
            bool check = false;

            double Height = Math.Abs(strtPoint.Y - endPoint.Y);

            double Width = Math.Abs(strtPoint.X - endPoint.X);

            if (Height > Width)
            {
                check = false;
            }
            else
            {
                check = true;
            }

            return check;
        }

    }
}
