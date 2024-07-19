using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;

[assembly: ExtensionApplication(typeof(EDS.PlugInApplication))]

namespace EDS
{
    public class PlugInApplication : IExtensionApplication
    {
        public void Initialize()
        {
            commands.EditorReactorOnOff();
            commands.EDS();
        }

        public void Terminate()
        {
            // Add your uninitialize code here.
        }
    }
}
