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
            get
            {
                return Application.Current as App;
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

    }
}
