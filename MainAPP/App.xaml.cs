using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _appMutex;

        // Global resources can be accessed via App.Current.
        public new static App Current
        {
            get { return Application.Current as App; }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool aIsNewInstance; 
            _appMutex = new Mutex(true, "AC019折料机CCD测量应用", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("CCD程序已运行");
                App.Current.Shutdown();
            }


        }


    }
}
