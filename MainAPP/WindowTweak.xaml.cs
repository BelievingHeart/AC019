using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Lib_ActiveX;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for WindowTweak.xaml
    /// </summary>
    public partial class WindowTweak : Window
    {
        public CogToolBlock ToolBlock { get; set; }
        public string VppPath { get; set; }

        // Disable close button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public WindowTweak()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Disable close button
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            var blockEdit = ((BlockPanel) Host.Child).BlockEdit;
            blockEdit.Subject = ToolBlock;
        }

        private void TrySaveVpp()
        {

            try
            {
                CogSerializer.SaveObjectToFile(ToolBlock, VppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
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
}
