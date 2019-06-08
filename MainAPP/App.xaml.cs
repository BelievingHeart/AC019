using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string VppDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "/VPP";
        public string VppBlockPath1, VppBlockPath2;

        // Global resources can be accessed via App.Current.
        public new static App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            VppBlockPath1 = Path.Combine(VppDir, "BlockLeft.vpp");
            VppBlockPath2 = Path.Combine(VppDir, "BlockRight.vpp");
        }

    }
}
