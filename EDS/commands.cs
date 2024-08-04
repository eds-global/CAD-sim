using System.Windows.Forms;
using EDS;
using EDS.UserControls;
using ResourceLib;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.Windows;

namespace EDS
{
    public class commands
    {
        private Editor _editor = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public static void EditorReactorOnOff()
        {
            if (CEditorReactor.IsAdded)
            {
                CEditorReactor.TheNetEditorReactor.RemoveEventHandler();
            }
            else
            {
                CEditorReactor.TheNetEditorReactor.AddEventHandler();
            }
            CEditorReactor.IsAdded = !CEditorReactor.IsAdded;
        }

        private static ZwSoft.ZwCAD.Windows.PaletteSet EDS_PaletteSet;

        public static ProjectInformationPalette projectInfoPalette = null;
        public static ExportPalette exportPalette = null;
        public static WindowsDataPalette windowsDataPalette = null;
        public static LispCommands LispCommandsPalette = null;
        public static WallDataPalette wallDataPalette = null;
        public static RoomDataPalette roomDataPalette = null;

        [CommandMethod("EDS")]
        public static void EDS()
        {
            if (EDS_PaletteSet == null)
            {
                EDS_PaletteSet = new ZwSoft.ZwCAD.Windows.PaletteSet("EDS", new System.Guid("A61D0875-A507-4b73-8B5F-9266BEACD596"));
                EDS_PaletteSet.Visible = true;
                EDS_PaletteSet.Size = new System.Drawing.Size(400,500);

                projectInfoPalette = new ProjectInformationPalette();
                EDS_PaletteSet.Add("Project Information", projectInfoPalette);

                exportPalette = new ExportPalette();
                EDS_PaletteSet.Add("Export", exportPalette);

                windowsDataPalette = new WindowsDataPalette();
                EDS_PaletteSet.Add("Windows", windowsDataPalette);

                LispCommandsPalette = new LispCommands();
                EDS_PaletteSet.Add("Lisp Commands", LispCommandsPalette);

                wallDataPalette = new WallDataPalette();
                EDS_PaletteSet.Add("Walls",wallDataPalette);

                roomDataPalette = new RoomDataPalette();
                EDS_PaletteSet.Add("Rooms", roomDataPalette);
            }
            else
            {
                EDS_PaletteSet.Visible = true;
            }                
        }
    }
}

