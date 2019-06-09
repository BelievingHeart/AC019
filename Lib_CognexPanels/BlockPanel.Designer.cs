namespace Lib_CognexPanels
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
            this.BlockEdit = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.BlockEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // BlockEdit
            // 
            this.BlockEdit.AllowDrop = true;
            this.BlockEdit.ContextMenuCustomizer = null;
            this.BlockEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlockEdit.Location = new System.Drawing.Point(0, 0);
            this.BlockEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BlockEdit.MinimumSize = new System.Drawing.Size(734, 0);
            this.BlockEdit.Name = "BlockEdit";
            this.BlockEdit.ShowNodeToolTips = true;
            this.BlockEdit.Size = new System.Drawing.Size(734, 607);
            this.BlockEdit.SuspendElectricRuns = false;
            this.BlockEdit.TabIndex = 1;
            // 
            // BlockPanel
            // 
            this.Controls.Add(this.BlockEdit);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Size = new System.Drawing.Size(720, 607);
            this.Text = "BlockPanel";
            ((System.ComponentModel.ISupportInitialize)(this.BlockEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 BlockEdit;
    }
}

