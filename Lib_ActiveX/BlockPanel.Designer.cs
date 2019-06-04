﻿namespace Lib_ActiveX
{
    partial class BlockPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockPanel));
            this.axCogDisplay1 = new AxCognex.VisionPro.Interop.AxCogDisplay();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.axCogDisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.SuspendLayout();
            // 
            // axCogDisplay1
            // 
            this.axCogDisplay1.Enabled = true;
            this.axCogDisplay1.Location = new System.Drawing.Point(43, 29);
            this.axCogDisplay1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.axCogDisplay1.Name = "axCogDisplay1";
            this.axCogDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCogDisplay1.OcxState")));
            this.axCogDisplay1.Size = new System.Drawing.Size(288, 288);
            this.axCogDisplay1.TabIndex = 0;
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(0, 0);
            this.cogToolBlockEditV21.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(734, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(734, 607);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 1;
            // 
            // BlockPanel
            // 
            this.ClientSize = new System.Drawing.Size(720, 607);
            this.Controls.Add(this.cogToolBlockEditV21);
            this.Controls.Add(this.axCogDisplay1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BlockPanel";
            this.Text = "BlockPanel";
            ((System.ComponentModel.ISupportInitialize)(this.axCogDisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxCognex.VisionPro.Interop.AxCogDisplay axCogDisplay1;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
    }
}
