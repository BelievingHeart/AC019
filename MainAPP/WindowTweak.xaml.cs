using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using Lib_CognexPanels;
using Lib_MeasurementUtilities;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for WindowTweak.xaml
    /// </summary>
    public partial class WindowTweak : Window
    {
        private enum ResultType
        {
            OK, NG, NOPORDUCT
        }
        private ResultCategories _resultCategories;
        private CogToolBlock _toolBlock;
        private readonly string _vppPath;
        private LeiSai _leiSai;
        private DataLogger _dataLogger;
        private CogRecordDisplay _recordDisplay;
        private Button _btnRunManually;

        // Disable close button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public WindowTweak(LeiSai leiSai, DataLogger dataLogger, CogRecordDisplay recordDisplay, string vppPath,
            Button btnRunManually, ResultCategories resultCategories = ResultCategories.OK_NG)
        {
            // set up style of recordDisplay
            _recordDisplay = recordDisplay;
            _recordDisplay.CreateControl();
            _recordDisplay.AutoFit = true;
            _recordDisplay.VerticalScrollBar = false;
            _recordDisplay.HorizontalScrollBar = false;
            _recordDisplay.DoubleClick += (sender, args) => ShowDialog();

            _resultCategories = resultCategories;

            // disable button before vpp is loaded
            _btnRunManually = btnRunManually;
            _btnRunManually.Click += BtnRunOnClick;
            _btnRunManually.IsEnabled = false;

            _vppPath = vppPath;

            // vpp loaded here
            InitializeComponent();

            // Triggered can only be subscribed after vpp is loaded
            _leiSai = leiSai;
            _leiSai.Triggered += LeiSaiOnTriggered;
            _leiSai.StartListening();

            _dataLogger = dataLogger;
        }



        private async Task SubmitResultAsync(ResultType result)
        {
            switch (result)
            {
                case ResultType.OK: await _leiSai.ReportOKAsync(); break;
                case ResultType.NG: await _leiSai.ReportNGAsync(); break;
                case ResultType.NOPORDUCT: await _leiSai.ReportNoProductAsync(); break;
                default:
                    throw new ArgumentOutOfRangeException("result", result, null);
            }
        }

 
        private void RefreshDisplay()
        {
            _recordDisplay.Record = _toolBlock.CreateLastRunRecord().SubRecords["CogIPOneImageTool1.OutputImage"];
            _recordDisplay.Fit();
        }

        private ResultType RunBlock()
        {
            _toolBlock.Run();

            if (_resultCategories == ResultCategories.OK_NG_NOPRODUCT)
            {
                var alignTool = (CogPMAlignTool) _toolBlock.Tools["判断有无料"];
                if (alignTool.Results.Count == 0) return ResultType.NOPORDUCT;
            }

            return _toolBlock.RunStatus.Result == CogToolResultConstants.Accept ? ResultType.OK : ResultType.NG;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Disable close button
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            var blockEdit = ((BlockPanel) Host.Child).BlockEdit;
 

            blockEdit.Subject = _toolBlock;
        }

        private void TrySaveVpp()
        {

            try
            {
                CogSerializer.SaveObjectToFile(_toolBlock, _vppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                MessageBox.Show("保存成功");
            }
            catch (Exception e)
            {
                MessageBox.Show("保存失败:\n" + e.Message);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            var SaveDecision = MessageBox.Show("是否保存?", "退出", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No);
            if (SaveDecision == MessageBoxResult.Yes)
            {
                TrySaveVpp();
            }
            Hide();
        }

        private async void LeiSaiOnTriggered(object sender, EventArgs e)
        {
            var result = await Task.Run(() => RunBlock());
            await SubmitResultAsync(result);

            _btnRunManually.Background = result == ResultType.OK ? Brushes.Green : result == ResultType.NG ? Brushes.Red : Brushes.Yellow;
            // refresh display
            RefreshDisplay();
            // Log measurements
            LogMeasurements();
        }

        private void BtnRunOnClick(object sender, RoutedEventArgs e)
        {
            ResultType result = RunBlock();
            // show result
            _btnRunManually.Background = result == ResultType.OK ? Brushes.Green : result == ResultType.NG ? Brushes.Red : Brushes.Yellow;
            // refresh display
            RefreshDisplay();
            // Log measurements
            LogMeasurements();
        }

        private void LogMeasurements()
        {
            _dataLogger.WriteLine(_toolBlock, "", "Wone", "Wtwo", "Wthree", "L");
            _dataLogger.CleanOutdatedFiles();
        }

        private static void CloseCognexCamera(CogToolBlock toolblock)
        {
            if (toolblock == null) return;

            for (int i = 0; i < toolblock.Tools.Count; i++)
            {
                if (toolblock.Tools[i] is CogAcqFifoTool)
                {
                    var fifo = (CogAcqFifoTool)toolblock.Tools[i];
                    if (fifo.Operator != null && fifo.Operator.FrameGrabber != null)
                    {
                        fifo.Operator.FrameGrabber.Disconnect(false);
                    }
                }
                else if (toolblock.Tools[i] is CogToolBlock)
                {
                    CogToolBlock tb = (CogToolBlock)toolblock.Tools[i];
                    CloseCognexCamera(tb);
                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CloseCognexCamera(_toolBlock);
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            _toolBlock = await Task.Run(() =>
                (CogToolBlock)CogSerializer.LoadObjectFromFile(_vppPath, typeof(BinaryFormatter)));
            _btnRunManually.IsEnabled = true;
        }
    }

    public enum ResultCategories
    {
        OK_NG, OK_NG_NOPRODUCT
    }
}
