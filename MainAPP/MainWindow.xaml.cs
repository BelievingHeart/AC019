using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.ToolBlock;
//using CognexImageFile;
using Lib_ActiveX;
using Lib_MeasurementUtilities;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CogToolBlock Block1, Block2;
        private readonly WindowTweak _windowTweak1;
        private readonly WindowTweak _windowTweak2;
        private CogRecordDisplay _display1;
        private CogRecordDisplay _display2;
        private DataLogger _dataLogger1 = new DataLogger(new List<string>(){"时间", "W1", "W2", "W3", "L"}, AppDomain.CurrentDomain.BaseDirectory + "/log" + "/左");
        private DataLogger _dataLogger2 = new DataLogger(new List<string>(){"时间", "W1", "W2", "W3", "L"}, AppDomain.CurrentDomain.BaseDirectory + "/log" + "/右");



        public MainWindow()
        {
            InitializeComponent();

            LoadVpps();
            _windowTweak1 = new WindowTweak(){ToolBlock = Block1, VppPath = App.Current.VppBlockPath1};
            _windowTweak2 = new WindowTweak(){ToolBlock = Block2, VppPath = App.Current.VppBlockPath2};
        }

        private void ShowWindowTweak(object sender, EventArgs e)
        {
            var me = (CogDisplay) sender;
            if (me == _display1)
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
            InitDisplay(((DisplayPanel) Host1.Child).Display, out _display1);
            InitDisplay(((DisplayPanel)Host2.Child).Display, out _display2);
        }

        private void InitDisplay(CogRecordDisplay displaySrc, out CogRecordDisplay displayDst)
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
            Block1 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlockPath1);
            Block2 = (CogToolBlock) CogSerializer.LoadObjectFromFile(App.Current.VppBlockPath2);
        }


        private void RunManually(object sender, RoutedEventArgs e)
        {
            var me = (Button) sender;

            // run block
            var blockToRun = me == BtnRun1 ? Block1 : Block2;
            bool passed = RunBlock(ref blockToRun);
            // show result
            me.Background = passed ? Brushes.Green : Brushes.Red;
            // refresh display
            var displayToRefresh = me == BtnRun1 ? _display1 : _display2;
            RefreshDisplay(ref displayToRefresh, blockToRun);
            // Log measurements
            LogMeasurements(blockToRun);

        }

        private void LogMeasurements(CogToolBlock blockToRun)
        {
            double W1 = (double) blockToRun.Outputs["Wone"].Value;
            double W2 = (double) blockToRun.Outputs["Wtwo"].Value;
            double W3 = (double) blockToRun.Outputs["Wthree"].Value;
            double L = (double) blockToRun.Outputs["L"].Value;

            var lineVals = new List<string>() { DateTime.Now.ToString("HH:mm:ss"), W1.ToString("f3"), W2.ToString("f3"), W3.ToString("f3"), L.ToString("f3")};
            var line = string.Join(",", lineVals);
            var dataLogger = blockToRun == Block1 ? _dataLogger1 : _dataLogger2;
            dataLogger.WriteLine(line);
            dataLogger.CleanOutdatedFiles();
        }
        

        private void RefreshDisplay(ref CogRecordDisplay displayToRefresh, CogToolBlock blockToRun)
        {
            displayToRefresh.Record = blockToRun.CreateLastRunRecord().SubRecords["CogIPOneImageTool1.OutputImage"];
            displayToRefresh.Fit();
        }

        private bool RunBlock(ref CogToolBlock blockToRun)
        {
            blockToRun.Run();

            return blockToRun.RunStatus.Result == CogToolResultConstants.Accept;
        }
    }
}