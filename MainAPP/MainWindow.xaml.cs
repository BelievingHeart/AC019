using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AxCognex.VisionPro.Interop;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AxCognex.VisionPro.Interop.AxCogDisplay _display1 = new AxCogDisplay(), _display2 = new AxCogDisplay();
        private WindowsFormsHost _hostDisplay1, _hostDisplay2;
        private WindowTweak _windowTweak1 = new WindowTweak(), _windowTweak2 = new WindowTweak();
        private Button _btnRun1= new Button(){Content = ""}, _btnRun2 = new Button(){Content = ""};
        public MainWindow()
        {
            InitializeComponent();

            // 
            _btnRun1.MouseDoubleClick += ShowWindowTweak1;

            // init hosts
            _hostDisplay1 = new WindowsFormsHost(){Child = _display1};
            _hostDisplay2 = new WindowsFormsHost(){Child = _display2};

            // set layouts of the main window
            Grid.SetColumn(_hostDisplay1, 0);
            Grid.SetColumn(_hostDisplay2, 0);
            Grid.SetRow(_hostDisplay1, 0);
            Grid.SetRow(_hostDisplay2, 1);
            Grid_Main.Children.Add(_hostDisplay1);
            Grid_Main.Children.Add(_hostDisplay2);


            Grid.SetColumn(_btnRun1, 1);
            Grid.SetColumn(_btnRun2, 1);
            Grid.SetRow(_btnRun1, 0);
            Grid.SetRow(_btnRun2, 1);
            Grid_Main.Children.Add(_btnRun1);
            Grid_Main.Children.Add(_btnRun2);
        }

        private void ShowWindowTweak1(object sender, EventArgs e)
        {
            _windowTweak1.Show();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
     

        }

        private void _btnBlockForm_OnClick(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
