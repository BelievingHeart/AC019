using System.Runtime.InteropServices;
using System.Windows;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Global resources can be accessed via App.Current.
        public new static App Current
        {
            get { return Application.Current as App; }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
//            string guid = GetGuid();
//            using (Mutex mutex = new Mutex(false, "Global\\" + guid))
//            {
//                if (!mutex.WaitOne(0, false))
//                {
//                    MessageBox.Show("程序已运行");
//                    
//                }
//            }


        }

        private static string GetGuid()
        {
            var assembly = typeof(Application).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            return attribute.Value;
        }

    }
}
