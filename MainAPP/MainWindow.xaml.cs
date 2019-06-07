using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using AxCognex.VisionPro.Interop;
using CognexImageFile;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AxCognex.VisionPro.Interop.AxCogDisplay _display1, _display2;
        private WindowsFormsHost _hostDisplay1, _hostDisplay2;
        private WindowTweak _windowTweak1 = new WindowTweak(), _windowTweak2 = new WindowTweak();
        private Button _btnRun1 = new Button() {Content = ""}, _btnRun2 = new Button() {Content = ""};

        public MainWindow()
        {
            InitializeComponent();

            // 
            _btnRun1.MouseDoubleClick += ShowWindowTweak;
            _btnRun2.MouseDoubleClick += ShowWindowTweak;

            // init hosts
            _display1 = new AxCogDisplay();
//            _display1.BeginInit();
//            _display1.CreateControl();
//            _display1.HorizontalScrollBar = false;
//            _display1.VerticalScrollBar = false;
//            _display1.EndInit();

            _display2 = new AxCogDisplay();
//            _display2.BeginInit();
            _display2.CreateControl();
//            _display2.HorizontalScrollBar = false;
//            _display2.VerticalScrollBar = false;
//            _display2.EndInit();

            _hostDisplay1 = new WindowsFormsHost() {Child = _display1};
            _hostDisplay2 = new WindowsFormsHost() {Child = _display2};

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

        private void ShowWindowTweak(object sender, EventArgs e)
        {
            var me = (Button) sender;
            if (me == _btnRun1)
            {
                _windowTweak1.ShowDialog();
            }
            else
            {
                _windowTweak2.ShowDialog();
            }
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var imagePath = @"C:\Users\rocke\Desktop\项目\AC019\样品\images\01-00-00.bmp";
            CogImageFile imageFile = new CogImageFile();
            imageFile.Open(imagePath, CogImageFileModeConstants.cogImageFileModeRead);
            _display1.CreateControl();
            _display1.Image = imageFile[0]; 
            _display1.HorizontalScrollBar = false;
            _display1.AutoFit = true;
        }
    }
}