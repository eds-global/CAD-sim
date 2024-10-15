using EDS.AEC;
using EDS.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;

namespace EDS.Models
{
    public class EDSRoomTag
    {
        public static List<EDSRoom> storedRooms = new List<EDSRoom>();

        public string buildingType { get; set; }
        public string spaceType { get; set; }
        public string lpdType { get; set; }
        public string epdType { get; set; }
        public string occupancyType { get; set; }
        public string freshAirType { get; set; }
        public string floorFinishType { get; set; }
        public string ceilingFinishType { get; set; }
        public string lpdText1 { get; set; }
        public string lpdText2 { get; set; }
        public string epdText1 { get; set; }
        public string epdText2 { get; set; }
        public string occupancyText1 { get; set; }
        public string occupancyText2 { get; set; }
        public string roomArea { get; set; }
        public string roomCount { get; set; }
        public string curveHandleId { get; set; }
        public int levelId { get; set; }
        public string textHandleId { get; set; }
        public List<Line> allWalls { get; set; }
        public Dictionary<ObjectId, List<ZwSoft.ZwCAD.DatabaseServices.Polyline>> allWindows { get; set; }

        public EDSRoomTag() { }

        public EDSRoomTag(string spaceType, string lpdType, string epdType, string occupancyType, string freshAirType, string floorFinishType, string ceilingFinishType, string lpdText1, string lpdText2, string epdText1, string epdText2, string occupancyText1, string occupancyText2, string curveHandleId, string roomArea, string roomLevel)
        {
            this.spaceType = spaceType;
            this.lpdType = lpdType;
            this.epdType = epdType;
            this.occupancyType = occupancyType;
            this.freshAirType = freshAirType;
            this.floorFinishType = floorFinishType;
            this.ceilingFinishType = ceilingFinishType;
            this.lpdText1 = lpdText1;
            this.lpdText2 = lpdText2;
            this.epdText1 = epdText1;
            this.epdText2 = epdText2;
            this.occupancyText1 = occupancyText1;
            this.occupancyText2 = occupancyText2;
            this.curveHandleId = curveHandleId;
            this.roomArea = roomArea;
        }

        private void SetXDataForRoom(EDSRoomTag roomTag, ObjectId objectId, string areaValue)
        {
            CADUtilities.SetXData(objectId, StringConstants.buildingType, roomTag.buildingType);
            CADUtilities.SetXData(objectId, StringConstants.spaceType, roomTag.spaceType);
            CADUtilities.SetXData(objectId, StringConstants.lpdType, roomTag.lpdType);
            CADUtilities.SetXData(objectId, StringConstants.lpdText1, roomTag.lpdText1);
            CADUtilities.SetXData(objectId, StringConstants.lpdText2, roomTag.lpdText2);
            CADUtilities.SetXData(objectId, StringConstants.epdType, roomTag.epdType);
            CADUtilities.SetXData(objectId, StringConstants.epdText1, roomTag.epdText1);
            CADUtilities.SetXData(objectId, StringConstants.epdText2, roomTag.epdText2);
            CADUtilities.SetXData(objectId, StringConstants.occupancyType, roomTag.occupancyType);
            CADUtilities.SetXData(objectId, StringConstants.occupancyText1, roomTag.occupancyText1);
            CADUtilities.SetXData(objectId, StringConstants.occupancyText2, roomTag.occupancyText2);
            CADUtilities.SetXData(objectId, StringConstants.freshAirType, roomTag.freshAirType);
            CADUtilities.SetXData(objectId, StringConstants.floorFinishType, roomTag.floorFinishType);
            CADUtilities.SetXData(objectId, StringConstants.ceilingFinishType, roomTag.ceilingFinishType);
            CADUtilities.SetXData(objectId, StringConstants.textHandleId, objectId.Handle.ToString());
            CADUtilities.SetXData(objectId, StringConstants.roomArea, areaValue);
            CADUtilities.SetXData(objectId, StringConstants.roomLevel, roomTag.levelId.ToString());
        }


        public EDSRoomTag GetXDataForRoom(ObjectId objectId)
        {
            EDSRoomTag roomTag = new EDSRoomTag();

            roomTag.buildingType = CADUtilities.GetXData(objectId,StringConstants.buildingType);
            roomTag.spaceType = CADUtilities.GetXData(objectId, StringConstants.spaceType);
            roomTag.lpdType = CADUtilities.GetXData(objectId, StringConstants.lpdType);
            roomTag.lpdText1 = CADUtilities.GetXData(objectId, StringConstants.lpdText1);
            roomTag.lpdText2 = CADUtilities.GetXData(objectId, StringConstants.lpdText2);
            roomTag.epdType = CADUtilities.GetXData(objectId, StringConstants.epdType);
            roomTag.epdText1 = CADUtilities.GetXData(objectId, StringConstants.epdText1);
            roomTag.epdText2 = CADUtilities.GetXData(objectId, StringConstants.epdText2);
            roomTag.occupancyType = CADUtilities.GetXData(objectId, StringConstants.occupancyType);
            roomTag.occupancyText1 = CADUtilities.GetXData(objectId, StringConstants.occupancyText1);
            roomTag.occupancyText2 = CADUtilities.GetXData(objectId, StringConstants.occupancyText2);
            roomTag.freshAirType = CADUtilities.GetXData(objectId, StringConstants.freshAirType);
            roomTag.floorFinishType = CADUtilities.GetXData(objectId, StringConstants.floorFinishType);
            roomTag.ceilingFinishType = CADUtilities.GetXData(objectId, StringConstants.ceilingFinishType);
            roomTag.textHandleId = CADUtilities.GetXData(objectId, StringConstants.textHandleId);
            roomTag.roomArea = CADUtilities.GetXData(objectId, StringConstants.roomArea);
            roomTag.levelId = int.Parse(CADUtilities.GetXData(objectId, StringConstants.roomLevel));

            return roomTag;
        }

        public void CreateRoom(EDSRoomTag roomTag)
        {
            RoomDataPalette.DrawRoom = true;
            EDSCreation.CreateLayer(StringConstants.roomLayerName, 2);

            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;

            doc.LockDocument();

            //PromptPointOptions promptPointOptions = new PromptPointOptions("\nSpecify location of Space Type" + (RoomDataPalette.roomTag.Contains("-") == true ? RoomDataPalette.roomTag.Split('-')[0] : RoomDataPalette.roomTag) + " " + roomId.ToString() + " or [Exit]: ");
            PromptPointResult promptPointResult = null;

            do
            {
                var roomId = GetRoomList(roomTag.levelId, RoomDataPalette.roomTag);
                roomId += 1;

                PromptPointOptions promptPointOptions = new PromptPointOptions("\nSpecify location of Space Type " + RoomDataPalette.roomTag.Split('-')[0] + " " + roomId.ToString() + " or [Exit]: ");
                promptPointResult = doc.Editor.GetPoint(promptPointOptions);

                if (promptPointResult.Status == PromptStatus.OK)
                {
                    using (Transaction addTransaction = db.TransactionManager.StartTransaction())
                    {
                        BlockTable blockTable = addTransaction.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord blockTableRecord = addTransaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


                        EDSCreation.CreateLayer(StringConstants.roomLayerName, 2);

                        DBObjectCollection objectCollection = editor.TraceBoundary(promptPointResult.Value, false);

                        if (false)
                        {
                            string msg = "Selected walls are not close properly. Kindly review it and run the tool again.";

                            System.Windows.Forms.MessageBox.Show(msg, "EDS Global", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //BlockTable blockTable = (BlockTable)transaction.GetObject(db.BlockTableId, OpenMode.ForRead);
                            BlockTableRecord modelSpace = (BlockTableRecord)addTransaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                            DBText dbText = new DBText
                            {
                                Position = promptPointResult.Value,
                                Height = 100.0,
                                TextString = (RoomDataPalette.roomTag.Contains("-") == true ? RoomDataPalette.roomTag.Split('-')[0] : RoomDataPalette.roomTag) + "-" + roomId.ToString(),
                            };

                            ObjectId objectId = new ObjectId();

                            string areaValue = "";
                            dbText.Layer = StringConstants.roomLayerName;
                            objectId = modelSpace.AppendEntity(dbText);
                            addTransaction.AddNewlyCreatedDBObject(dbText, true);
                            RoomDataPalette.roomTag = (RoomDataPalette.roomTag.Contains("-") == true ? RoomDataPalette.roomTag.Split('-')[0] : RoomDataPalette.roomTag) + "-" + roomId.ToString();
                            SetXDataForRoom(roomTag, objectId, areaValue);
                            roomId += 1;

                            //EDSCreation.UpdateSpaceData(RoomDataPalette.roomTag);
                        }

                        addTransaction.Commit();
                        //Thread.Sleep(500);


                    }
                }
            } while (promptPointResult.Status == PromptStatus.OK);
        }

        public EDSRoomTag SelectRoom()
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;
            doc.LockDocument();
            PromptEntityOptions promptEntityOptions1 = new PromptEntityOptions("\nSelect the Space Type or [Exit]: ");
            PromptEntityResult promptEntityOptions = doc.Editor.GetEntity(promptEntityOptions1);

            if (promptEntityOptions.Status == PromptStatus.OK)
            {

                using (Transaction transaction = db.TransactionManager.StartTransaction())
                {
                    var entity = transaction.GetObject(promptEntityOptions.ObjectId, OpenMode.ForRead) as Entity;

                    if (entity.Layer == StringConstants.roomLayerName)
                    {
                        return GetXDataForRoom(promptEntityOptions.ObjectId);
                    }
                }
            }

            return null;
        }

        public void UpdateRoom(EDSRoomTag roomTag, bool lpdCheck, bool epdCheck, bool occuCheck, bool freshAirCheck, bool floorCheck, bool ceilCheck)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;
            doc.LockDocument();

            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = transaction.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                PromptSelectionResult res = editor.SelectImplied();
                if (res.Value == null)
                {
                    System.Windows.Forms.MessageBox.Show("No items selected for update");
                    return;
                }
                else
                {
                    foreach (SelectedObject set in res.Value)
                    {
                        var entity = transaction.GetObject(set.ObjectId, OpenMode.ForWrite) as Entity;

                        if (entity.Layer == StringConstants.roomLayerName)
                        {
                            if (lpdCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdType, roomTag.lpdType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdText1, roomTag.lpdText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdText2, roomTag.lpdText2);
                            }

                            if (epdCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdType, roomTag.epdType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdText1, roomTag.epdText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdText2, roomTag.epdText2);
                            }

                            if (occuCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyType, roomTag.occupancyType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyText1, roomTag.occupancyText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyText2, roomTag.occupancyText2);
                            }

                            if (freshAirCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.freshAirType, roomTag.freshAirType);
                            }

                            if (floorCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.floorFinishType, roomTag.floorFinishType);
                            }

                            if (ceilCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.ceilingFinishType, roomTag.ceilingFinishType);
                            }

                            //CADUtilities.SetXData(set.ObjectId, StringConstants.roomLevel, roomTag.roomLevel);
                        }
                        else
                            return;
                    }
                }

                editor.SetImpliedSelection(new List<ObjectId>().ToArray());

                transaction.Commit();
            }
        }

        public void MatchRoom(bool lpdCheck, bool epdCheck, bool occuCheck, bool freshAirCheck, bool floorCheck, bool ceilCheck)
        {
            Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;
            doc.LockDocument();

            using (Transaction transaction = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = transaction.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                PromptEntityOptions sourceOptions = new PromptEntityOptions("\nSelect source object: ");
                PromptEntityResult sourceResult = editor.GetEntity(sourceOptions);

                if (sourceResult.Status != PromptStatus.OK)
                    return;

                // Retrieve the source object
                Entity sourceEntity = (Entity)transaction.GetObject(sourceResult.ObjectId, OpenMode.ForRead);

                var roomTag = GetXDataForRoom(sourceEntity.ObjectId);

                // Prompt user to select target objects
                PromptSelectionOptions targetOptions = new PromptSelectionOptions();
                targetOptions.MessageForAdding = "\nSelect target objects: ";
                PromptSelectionResult targetResult = editor.GetSelection(targetOptions);

                if (targetResult.Status != PromptStatus.OK)
                    return;

                if (targetResult.Value == null)
                {
                    System.Windows.Forms.MessageBox.Show("No items selected for match");
                    return;
                }
                else
                {
                    foreach (SelectedObject set in targetResult.Value)
                    {
                        var entity = transaction.GetObject(set.ObjectId, OpenMode.ForWrite) as Entity;

                        if (entity.Layer == StringConstants.roomLayerName)
                        {
                            if (lpdCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdType, roomTag.lpdType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdText1, roomTag.lpdText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.lpdText2, roomTag.lpdText2);
                            }

                            if (epdCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdType, roomTag.epdType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdText1, roomTag.epdText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.epdText2, roomTag.epdText2);
                            }

                            if (occuCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyType, roomTag.occupancyType);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyText1, roomTag.occupancyText1);
                                CADUtilities.SetXData(set.ObjectId, StringConstants.occupancyText2, roomTag.occupancyText2);
                            }

                            if (freshAirCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.freshAirType, roomTag.freshAirType);
                            }

                            if (floorCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.floorFinishType, roomTag.floorFinishType);
                            }

                            if (ceilCheck)
                            {
                                CADUtilities.SetXData(set.ObjectId, StringConstants.ceilingFinishType, roomTag.ceilingFinishType);
                            }

                            //CADUtilities.SetXData(set.ObjectId, StringConstants.roomLevel, roomTag.roomLevel);
                        }
                        else
                            return;
                    }
                }
                transaction.Commit();
            }
        }

        public List<string> GetAllLayers()
        {
            List<string> layers = new List<string>();
            // Get the current document
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            doc.LockDocument();
            // Open the LayerTable for read
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                LayerTable layerTable = tr.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (layerTable != null)
                {
                    // Iterate through each layer in the layer table
                    foreach (ObjectId layerId in layerTable)
                    {
                        LayerTableRecord layer = tr.GetObject(layerId, OpenMode.ForRead) as LayerTableRecord;

                        layers.Add(layer.Name);
                    }
                }

                // Commit the transaction
                tr.Commit();
            }

            return layers;
        }

        public int GetRoomList(int levelPrefix, string spaceType)
        {
            EDSRoomTag.storedRooms.Clear();

            EDSCreation.GetRoomData();

            var foundRooms = EDSRoomTag.storedRooms.FindAll(x => x.RoomName.Contains(spaceType));
            if (foundRooms.Count > 0)
            {
                if (EDSRoomTag.storedRooms.Any(x => x.RoomId.ToString().StartsWith(levelPrefix.ToString())) && EDSRoomTag.storedRooms.Any(x => x.RoomName.Contains(spaceType)))
                {
                    var allRooms = EDSRoomTag.storedRooms.FindAll(x => x.RoomId.ToString().StartsWith(levelPrefix.ToString()) && x.RoomName.Contains(spaceType));
                    return allRooms.Max(x => x.RoomId);
                }
                else
                {
                    var stringValue = (levelPrefix.ToString()) + "00";
                    return int.Parse(stringValue);
                }
            }
            else
            {
                var stringValue = (levelPrefix.ToString()) + "00";
                return int.Parse(stringValue);
            }
        }
    }

    public class EDSRoom
    {
        public string RoomName { get; set; }
        public int RoomId { get; set; }
    }
}
