namespace Lib_ActiveX
{
    partial class DisplayPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayPanel));
            this.display = new Cognex.VisionPro.Display.CogDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.display.ColorMapLowerRoiLimit = 0D;
            this.display.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.display.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.display.ColorMapUpperRoiLimit = 1D;
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.display.MouseWheelSensitivity = 1D;
            this.display.Name = "display";
            this.display.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("display.OcxState")));
            this.display.Size = new System.Drawing.Size(284, 261);
            this.display.TabIndex = 0;
            // 
            // DisplayPanel
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.display);
            this.Name = "DisplayPanel";
            this.Text = "DisplayPanel";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Cognex.VisionPro.Display.CogDisplay display;
    }
}