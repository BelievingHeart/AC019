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
            this.Display = new Cognex.VisionPro.CogRecordDisplay();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.Display)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.SuspendLayout();
            // 
            // Display
            // 
            this.Display.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.Display.ColorMapLowerRoiLimit = 0D;
            this.Display.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.Display.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.Display.ColorMapUpperRoiLimit = 1D;
            this.Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display.Location = new System.Drawing.Point(0, 0);
            this.Display.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.Display.MouseWheelSensitivity = 1D;
            this.Display.Name = "Display";
            this.Display.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Display.OcxState")));
            this.Display.Size = new System.Drawing.Size(284, 261);
            this.Display.TabIndex = 0;
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(0, 0);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.Size = new System.Drawing.Size(75, 23);
            this.cogRecordDisplay1.TabIndex = 0;
            this.cogRecordDisplay1.Enter += new System.EventHandler(this.cogRecordDisplay1_Enter);
            // 
            // DisplayPanel
            // 
            this.Controls.Add(this.Display);
            this.Size = new System.Drawing.Size(284, 261);
            this.Text = "DisplayPanel";
            ((System.ComponentModel.ISupportInitialize)(this.Display)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Cognex.VisionPro.CogRecordDisplay Display;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
    }
}