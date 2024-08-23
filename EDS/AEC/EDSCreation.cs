using EDS.UserControls;
using ResourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;
using static System.Windows.Forms.LinkLabel;
using Line = ZwSoft.ZwCAD.DatabaseServices.Line;
using EDS.Models;

namespace EDS.AEC
{
    public static class EDSCreation
    {
        //public static Dictionary<string, int> roomSpaceType = new Dictionary<string, int>();

        public static bool CreateLayer(string layerName, short indexValue)
        {
            try
            {
                Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database db = doc.Database;
                // Start a transaction
                using (doc.LockDocument())
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        // Get the Layer table from the current database
                        LayerTable layerTable = (LayerTable)trans.GetObject(HostApplicationServices.WorkingDatabase.LayerTableId, OpenMode.ForRead);

                        // Check if the layer already exists
                        if (!layerTable.Has(layerName))
                        {
                            // Open the layer table for write
                            layerTable.UpgradeOpen();

                            // Create a new layer table record
                            LayerTableRecord newLayer = new LayerTableRecord
                            {
                                Name = layerName,
                                Color = Color.FromColorIndex(ColorMethod.ByAci, indexValue) // 1 = red color
                            };

                            // Add the new layer to the layer table
                            layerTable.Add(newLayer);

                            // Add the new layer table record to the transaction
                            trans.AddNewlyCreatedDBObject(newLayer, true);
                        }

                        // Commit the transaction
                        trans.Commit();
                    }
                }

                Console.WriteLine("Layer created successfully.");

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false;
        }


        public static void GetRoomData()
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            {
                Editor editor = doc.Editor;
                Transaction transaction = editor.Document.Database.TransactionManager.StartTransaction();
                PromptSelectionResult selectionResult = editor.SelectAll();

                if (selectionResult.Value != null)
                {
                    foreach (ObjectId objectId in selectionResult.Value.GetObjectIds())
                    {
                        Entity entity = transaction.GetObject(objectId, OpenMode.ForRead) as Entity;
                        if (entity is DBText)
                        {
                            var dbText = entity as DBText;
                            if (dbText.TextString.Split('-').Count() > 1)
                            {
                                EDSRoomTag.storedRooms.Add(new EDSRoom()
                                {
                                    RoomName = dbText.TextString,
                                    RoomId = int.Parse(dbText.TextString.Split('-')[1])
                                });
                            }
                        }
                    }
                }

                transaction.Commit();
            }

            #region EDS_SpaceData

            //public static void SaveAndGetSpaceData(List<string> spaceTypes)
            //{
            //    Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            //    using (doc.LockDocument())
            //    {
            //        Editor ed = doc.Editor;
            //        Transaction trans = ed.Document.Database.TransactionManager.StartTransaction();
            //        try
            //        {
            //            DBDictionary nod = (DBDictionary)trans.GetObject(ed.Document.Database.NamedObjectsDictionaryId, OpenMode.ForWrite);
            //            if (nod.Contains("EDS_SpaceData"))
            //            {
            //                ObjectId entryId = nod.GetAt("EDS_SpaceData");
            //                Xrecord myXrecord = null;
            //                myXrecord = (Xrecord)trans.GetObject(entryId, OpenMode.ForWrite);

            //                nod.UpgradeOpen();

            //                ResultBuffer rb = myXrecord.Data;

            //                foreach (var space in spaceTypes)
            //                {
            //                    var xDataValue = rb.AsArray().ToList().Find(x => x.Value.ToString().Contains(space));
            //                    if (xDataValue != null)
            //                    {
            //                        roomSpaceType.Add(space, int.Parse(xDataValue.Value.ToString().Split(':')[1]));
            //                    }
            //                    else
            //                    {
            //                        rb.Add(new TypedValue((int)DxfCode.Text, space + " : " + 101.ToString()));
            //                    }
            //                }

            //                myXrecord.Data = rb;
            //                nod.SetAt("EDS_SpaceData", myXrecord);
            //            }
            //            else
            //            {
            //                nod.UpgradeOpen();
            //                Xrecord myXrecord = new Xrecord();

            //                ResultBuffer data = new ResultBuffer();
            //                foreach (var str in spaceTypes)
            //                    data.Add(new TypedValue((int)DxfCode.Text, str + " : " + 101.ToString()));

            //                foreach (var str2 in spaceTypes)
            //                    roomSpaceType.Add(str2, 101);

            //                myXrecord.Data = data;
            //                nod.SetAt("EDS_SpaceData", myXrecord);
            //                trans.AddNewlyCreatedDBObject(myXrecord, true);
            //            }
            //            trans.Commit();
            //        }
            //        catch (System.Exception ex)
            //        {
            //            ed.WriteMessage("a problem occurred because " + ex.Message);
            //        }
            //        finally
            //        {
            //            trans.Dispose();
            //        }

            //    }
            //}

            //public static bool UpdateSpaceData(string spaceType)
            //{
            //    Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            //    using (doc.LockDocument())
            //    {
            //        Editor ed = doc.Editor;
            //        Transaction trans = ed.Document.Database.TransactionManager.StartTransaction();
            //        try
            //        {
            //            DBDictionary nod = (DBDictionary)trans.GetObject(ed.Document.Database.NamedObjectsDictionaryId, OpenMode.ForWrite);
            //            if (nod.Contains("EDS_SpaceData"))
            //            {
            //                ObjectId entryId = nod.GetAt("EDS_SpaceData");
            //                Xrecord myXrecord = null;
            //                myXrecord = (Xrecord)trans.GetObject(entryId, OpenMode.ForWrite);

            //                nod.UpgradeOpen();

            //                ResultBuffer data = new ResultBuffer();

            //                ResultBuffer rb = myXrecord.Data;
            //                var resultBufferList = rb.AsArray().ToList();

            //                nod.Remove("EDS_SpaceData");

            //                foreach (TypedValue result in resultBufferList)
            //                {
            //                    if (result.Value != null)
            //                    {
            //                        if (result.Value.ToString().Contains(spaceType))
            //                        {
            //                            int lastValue = int.Parse(result.Value.ToString().Split(':')[1]);
            //                            if (roomSpaceType.ContainsKey(spaceType))
            //                                roomSpaceType[spaceType] = lastValue + 1;

            //                            data.Add(new TypedValue((int)DxfCode.Text, spaceType + " : " + (lastValue + 1).ToString()));
            //                        }
            //                        else
            //                        {
            //                            data.Add(new TypedValue((int)DxfCode.Text, result));
            //                        }
            //                    }
            //                }

            //                myXrecord.Data = data;
            //                nod.SetAt("EDS_SpaceData", myXrecord);
            //            }
            //            //else
            //            //{
            //            //    nod.UpgradeOpen();
            //            //    Xrecord myXrecord = new Xrecord();

            //            //    ResultBuffer data = new ResultBuffer();
            //            //    foreach (var str in spaceTypes)
            //            //        data.Add(new TypedValue((int)DxfCode.Text, str + " : " + 101.ToString()));

            //            //    foreach (var str2 in spaceTypes)
            //            //        roomSpaceType.Add(str2, 101);

            //            //    myXrecord.Data = data;
            //            //    nod.SetAt("EDS_SpaceData", myXrecord);
            //            //    trans.AddNewlyCreatedDBObject(myXrecord, true);
            //            //}
            //            trans.Commit();
            //        }
            //        catch (System.Exception ex)
            //        {
            //            ed.WriteMessage("a problem occurred because " + ex.Message);
            //        }
            //        finally
            //        {
            //            trans.Dispose();
            //        }

            //    }

            //    return false;
            //} 

            #endregion
        }

    }
}
