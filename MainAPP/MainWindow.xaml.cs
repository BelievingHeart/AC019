using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
//using CognexImageFile;
using Lib_ActiveX;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private WindowTweak _windowTweak1, _windowTweak2;
        public CogDisplay Display1;
        public CogDisplay Display2;

        public MainWindow()
        {
            InitializeComponent();

            LoadVpps();
            _windowTweak1 = new WindowTweak(){ToolBlock = App.Current.Block1, VppPath = App.Current.VppBlockPath1};
            _windowTweak2 = new WindowTweak(){ToolBlock = App.Current.Block2, VppPath = App.Current.VppBlockPath2};
        }

        private void ShowWindowTweak(object sender, EventArgs e)
        {
            var me = (CogDisplay) sender;
            if (me == Display1)
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
            // Init displays
            InitDisplay(((DisplayPanel) Host1.Child).Display, out Display1);
            InitDisplay(((DisplayPanel)Host2.Child).Display, out Display2);
        }

        private void InitDisplay(CogDisplay displaySrc, out CogDisplay displayDst)
        {
            displayDst = displaySrc;
            displayDst.CreateControl();
            displayDst.HorizontalScrollBar = false;
            displayDst.VerticalScrollBar = false;
            displayDst.AutoFit = true;
            displayDst.DoubleClick += ShowWindowTweak;
        }


        private void LoadVpps()
        {
            App.Current.Block1 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlockPath1);
            App.Current.Block2 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlockPath2);
        }


        private void RunManually(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}