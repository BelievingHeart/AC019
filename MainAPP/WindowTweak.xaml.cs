using System.Windows;
using System.Windows.Forms.Integration;
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
        public CogToolBlock ToolBlock;
        public WindowTweak()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var blockEdit = ((BlockPanel) Host.Child).BlockEdit;
            blockEdit.Subject = ToolBlock;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
