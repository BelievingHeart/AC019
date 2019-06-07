using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using Cognex.VisionPro;
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

        public MainWindow()
        {
            InitializeComponent();

            LoadVpps();
            _windowTweak1 = new WindowTweak(){ToolBlock = App.Current.Block1};
            _windowTweak2 = new WindowTweak(){ToolBlock = App.Current.Block2};
        }

        private void ShowWindowTweak(object sender, EventArgs e)
        {
            var me = (Button) sender;
            if (me == BtnRun1)
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
            App.Current.Display1 = ((DisplayPanel)Host1.Child).display;
            App.Current.Display1.CreateControl();
            App.Current.Display1.HorizontalScrollBar = false;
            App.Current.Display1.VerticalScrollBar = false;
            App.Current.Display1.AutoFit = true;

            App.Current.Display2 = ((DisplayPanel)Host2.Child).display;
            App.Current.Display2.CreateControl();
            App.Current.Display2.HorizontalScrollBar = false;
            App.Current.Display2.VerticalScrollBar = false;
            App.Current.Display2.AutoFit = true;

            // 
      
        }

        private void LoadVpps()
        {
            App.Current.Block1 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlock1);
            App.Current.Block2 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlock2);
        }


        private void RunManually(object sender, RoutedEventArgs e)
        {
            var me = (Button)sender;
            if (me == BtnRun1)
            {
                _windowTweak1.ShowDialog();
            }
            else
            {
                _windowTweak2.ShowDialog();
            }
        }

    }
}