using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Runtime;
using Application = ZwSoft.ZwCAD.ApplicationServices.Application;

namespace EDS
{
    class CEditorReactor
    {
        static public CEditorReactor TheNetEditorReactor { get; } = new CEditorReactor();
        static public bool IsAdded { get; set; } = false;

        private Document _document = Application.DocumentManager.MdiActiveDocument;
        private Editor _editor = Application.DocumentManager.MdiActiveDocument.Editor;

        public CEditorReactor() { }

        public void AddEventHandler()
        {
            try
            {
                _document.ViewChanged += new EventHandler(callback_ViewChanged);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RemoveEventHandler()
        {
            try
            {
                _document.ViewChanged -= new EventHandler(callback_ViewChanged);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void callback_ViewChanged(object sender, EventArgs e)
        {
            ProjectInformationPalette.LoadProjectInformation();
            //_editor.WriteMessage("\ncallback_ViewChanged");
        }
    }
}
