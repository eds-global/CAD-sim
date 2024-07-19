using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;

namespace EDS.UserControls
{
    public partial class LispCommands : UserControl
    {
        public LispCommands()
        {
            InitializeComponent();
        }

        private void btnBreakLine_Click(object sender, EventArgs e)
        {
                Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;

                string cmd = "brkline ";

                doc.SendStringToExecute(cmd, false, false, false);
        }
    }
}
