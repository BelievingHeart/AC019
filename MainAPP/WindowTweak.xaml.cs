using System.Windows;
using Lib_ActiveX;

namespace MainAPP
{
    /// <summary>
    /// Interaction logic for WindowTweak.xaml
    /// </summary>
    public partial class WindowTweak : Window
    {
        public WindowTweak()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            var blockForm = new BlockPanel();
            // Assign the MaskedTextBox control as the host control's child.
            host.Child = blockForm;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
