using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using System.Windows.Media;
using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using Lib_ActiveX;
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
            InitializeComponent();

            _leiSai = leiSai;
            _leiSai.Triggered += LeiSaiOnTriggered;
            _leiSai.StartListening();

            _dataLogger = dataLogger;

            // set up style of recordDisplay
            _recordDisplay = recordDisplay;
            _recordDisplay.CreateControl();
            _recordDisplay.AutoFit = true;
            _recordDisplay.VerticalScrollBar = false;
            _recordDisplay.HorizontalScrollBar = false;
            _recordDisplay.DoubleClick += (sender, args) => ShowDialog();

            _vppPath = vppPath;
            _toolBlock = (CogToolBlock) CogSerializer.LoadObjectFromFile(_vppPath, typeof(BinaryFormatter));

            _btnRunManually = btnRunManually;
            _btnRunManually.Click += BtnRunOnClick;

            _resultCategories = resultCategories;
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
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
    }

    public enum ResultCategories
    {
        OK_NG, OK_NG_NOPRODUCT
    }
}
