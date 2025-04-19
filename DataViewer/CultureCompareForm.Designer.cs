namespace DataViewer
{
    partial class CultureCompareForm
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
            Center1 = new ListBox();
            Center2 = new ListBox();
            canvasPanel = new Panel();
            SuspendLayout();
            // 
            // Center1
            // 
            Center1.Dock = DockStyle.Left;
            Center1.ForeColor = Color.FromArgb(192, 0, 0);
            Center1.FormattingEnabled = true;
            Center1.Location = new Point(0, 0);
            Center1.Name = "Center1";
            Center1.Size = new Size(92, 576);
            Center1.TabIndex = 0;
            Center1.SelectedIndexChanged += Center1_SelectedIndexChanged;
            // 
            // Center2
            // 
            Center2.Dock = DockStyle.Right;
            Center2.ForeColor = Color.FromArgb(0, 0, 192);
            Center2.FormattingEnabled = true;
            Center2.Location = new Point(1442, 0);
            Center2.Name = "Center2";
            Center2.Size = new Size(108, 576);
            Center2.TabIndex = 1;
            Center2.SelectedIndexChanged += Center2_SelectedIndexChanged;
            // 
            // canvasPanel
            // 
            canvasPanel.Dock = DockStyle.Fill;
            canvasPanel.Location = new Point(92, 0);
            canvasPanel.Name = "canvasPanel";
            canvasPanel.Size = new Size(1350, 576);
            canvasPanel.TabIndex = 2;
            canvasPanel.Paint += canvasPanel_Paint;
            // 
            // CultureCompareForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1550, 576);
            Controls.Add(canvasPanel);
            Controls.Add(Center2);
            Controls.Add(Center1);
            Name = "CultureCompareForm";
            Text = "CultureCompare";
            ResumeLayout(false);
        }

        #endregion

        private ListBox Center1;
        private ListBox Center2;
        private Panel canvasPanel;
    }
}