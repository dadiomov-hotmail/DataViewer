namespace DataViewer
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DomainListBox = new ListBox();
            DataChoiceListBox = new ListBox();
            ModeChoicePanel = new Panel();
            TopModePanel = new Panel();
            ModeChoicePanel.SuspendLayout();
            SuspendLayout();
            // 
            // DomainListBox
            // 
            DomainListBox.FormattingEnabled = true;
            DomainListBox.Location = new Point(0, 3);
            DomainListBox.Name = "DomainListBox";
            DomainListBox.Size = new Size(135, 44);
            DomainListBox.TabIndex = 0;
            DomainListBox.SelectedIndexChanged += DomainListBox_SelectedIndexChanged;
            // 
            // DataChoiceListBox
            // 
            DataChoiceListBox.FormattingEnabled = true;
            DataChoiceListBox.Location = new Point(0, 53);
            DataChoiceListBox.Name = "DataChoiceListBox";
            DataChoiceListBox.Size = new Size(135, 244);
            DataChoiceListBox.TabIndex = 1;
            DataChoiceListBox.SelectedIndexChanged += DataChoiceListBox_SelectedIndexChanged;
            // 
            // ModeChoicePanel
            // 
            ModeChoicePanel.Controls.Add(DomainListBox);
            ModeChoicePanel.Controls.Add(DataChoiceListBox);
            ModeChoicePanel.Dock = DockStyle.Left;
            ModeChoicePanel.Location = new Point(0, 0);
            ModeChoicePanel.Name = "ModeChoicePanel";
            ModeChoicePanel.Size = new Size(140, 579);
            ModeChoicePanel.TabIndex = 2;
            // 
            // TopModePanel
            // 
            TopModePanel.Dock = DockStyle.Fill;
            TopModePanel.Location = new Point(140, 0);
            TopModePanel.Name = "TopModePanel";
            TopModePanel.Size = new Size(1443, 579);
            TopModePanel.TabIndex = 3;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1583, 579);
            Controls.Add(TopModePanel);
            Controls.Add(ModeChoicePanel);
            Name = "Form";
            Text = "Data Viewer";
            ModeChoicePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox DomainListBox;
        private ListBox DataChoiceListBox;
        private Panel ModeChoicePanel;
        private Panel TopModePanel;
    }
}
