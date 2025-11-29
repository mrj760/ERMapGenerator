namespace ERMapGenerator
{
    partial class ERMapGenerator
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERMapGenerator));
            copyrightInfoStr = new System.Windows.Forms.Label();
            versionStr = new System.Windows.Forms.Label();
            mapConfigurationGroupBox = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            zoomLevelComboBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            groundLevelComboBox = new System.Windows.Forms.ComboBox();
            progressLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            automateButton = new System.Windows.Forms.Button();
            automationModeTabControl = new System.Windows.Forms.TabControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            drawTileDebugInfoCheckBox = new System.Windows.Forms.CheckBox();
            outputFolderGroupBox = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            outputFolderPathLabel = new System.Windows.Forms.Label();
            outputFolderButton = new System.Windows.Forms.Button();
            tabPage1 = new System.Windows.Forms.TabPage();
            // mapDisplayGroupBox = new System.Windows.Forms.GroupBox();
            // mapDisplayOpenMapImageLabel = new System.Windows.Forms.Label();
            // mapDisplayPictureBox = new System.Windows.Forms.PictureBox();
            InputFolderGroupBox = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            inputFolderPathLabel = new System.Windows.Forms.Label();
            BrowseInputFolderButton = new System.Windows.Forms.Button();
            gameModFolderGroupBox = new System.Windows.Forms.GroupBox();
            label5 = new System.Windows.Forms.Label();
            gameModFolderPathLabel = new System.Windows.Forms.Label();
            browseGameModFolderButton = new System.Windows.Forms.Button();
            mapConfigurationGroupBox.SuspendLayout();
            automationModeTabControl.SuspendLayout();
            tabPage2.SuspendLayout();
            outputFolderGroupBox.SuspendLayout();
            tabPage1.SuspendLayout();
            // mapDisplayGroupBox.SuspendLayout();
            // ((System.ComponentModel.ISupportInitialize)mapDisplayPictureBox).BeginInit();
            InputFolderGroupBox.SuspendLayout();
            gameModFolderGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // copyrightInfoStr
            // 
            copyrightInfoStr.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            copyrightInfoStr.AutoSize = true;
            copyrightInfoStr.ForeColor = System.Drawing.Color.Gray;
            copyrightInfoStr.Location = new System.Drawing.Point(393, 6);
            copyrightInfoStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            copyrightInfoStr.Name = "copyrightInfoStr";
            copyrightInfoStr.Size = new System.Drawing.Size(174, 15);
            copyrightInfoStr.TabIndex = 1;
            copyrightInfoStr.Text = "© Pear, 2025 All rights reserved.";
            // 
            // versionStr
            // 
            versionStr.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            versionStr.AutoSize = true;
            versionStr.ForeColor = System.Drawing.Color.Gray;
            versionStr.Location = new System.Drawing.Point(330, 6);
            versionStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            versionStr.Name = "versionStr";
            versionStr.Size = new System.Drawing.Size(48, 15);
            versionStr.TabIndex = 11;
            versionStr.Text = "Version:";
            // 
            // mapConfigurationGroupBox
            // 
            mapConfigurationGroupBox.Controls.Add(label3);
            mapConfigurationGroupBox.Controls.Add(zoomLevelComboBox);
            mapConfigurationGroupBox.Controls.Add(label2);
            mapConfigurationGroupBox.Controls.Add(groundLevelComboBox);
            mapConfigurationGroupBox.Location = new System.Drawing.Point(12, 24);
            mapConfigurationGroupBox.Name = "mapConfigurationGroupBox";
            mapConfigurationGroupBox.Size = new System.Drawing.Size(555, 78);
            mapConfigurationGroupBox.TabIndex = 15;
            mapConfigurationGroupBox.TabStop = false;
            mapConfigurationGroupBox.Text = "Map Configuration";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(138, 25);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(72, 15);
            label3.TabIndex = 8;
            label3.Text = "Zoom Level:";
            // 
            // zoomLevelComboBox
            // 
            zoomLevelComboBox.FormattingEnabled = true;
            zoomLevelComboBox.Location = new System.Drawing.Point(140, 43);
            zoomLevelComboBox.Name = "zoomLevelComboBox";
            zoomLevelComboBox.Size = new System.Drawing.Size(125, 23);
            zoomLevelComboBox.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(80, 15);
            label2.TabIndex = 6;
            label2.Text = "Ground Level:";
            // 
            // groundLevelComboBox
            // 
            groundLevelComboBox.FormattingEnabled = true;
            groundLevelComboBox.Location = new System.Drawing.Point(9, 43);
            groundLevelComboBox.Name = "groundLevelComboBox";
            groundLevelComboBox.Size = new System.Drawing.Size(125, 23);
            groundLevelComboBox.TabIndex = 5;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Location = new System.Drawing.Point(49, 582);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new System.Drawing.Size(57, 15);
            progressLabel.TabIndex = 11;
            progressLabel.Text = "Waiting...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 582);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(42, 15);
            label4.TabIndex = 10;
            label4.Text = "Status:";
            // 
            // automateButton
            // 
            automateButton.Location = new System.Drawing.Point(12, 601);
            automateButton.Name = "automateButton";
            automateButton.Size = new System.Drawing.Size(555, 23);
            automateButton.TabIndex = 4;
            automateButton.Text = "Automate!";
            automateButton.UseVisualStyleBackColor = true;
            automateButton.Click += AutomateButton_Click;
            // 
            // automationModeTabControl
            // 
            automationModeTabControl.Controls.Add(tabPage2);
            automationModeTabControl.Controls.Add(tabPage1);
            automationModeTabControl.Location = new System.Drawing.Point(12, 186);
            automationModeTabControl.Name = "automationModeTabControl";
            automationModeTabControl.SelectedIndex = 0;
            automationModeTabControl.Size = new System.Drawing.Size(555, 393);
            automationModeTabControl.TabIndex = 16;
            automationModeTabControl.SelectedIndexChanged += AutomationModeTabControl_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(drawTileDebugInfoCheckBox);
            tabPage2.Controls.Add(outputFolderGroupBox);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(547, 365);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Unpack/Stitch Map";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // drawTileDebugInfoCheckBox
            // 
            drawTileDebugInfoCheckBox.AutoSize = true;
            drawTileDebugInfoCheckBox.Location = new System.Drawing.Point(4, 80);
            drawTileDebugInfoCheckBox.Name = "drawTileDebugInfoCheckBox";
            drawTileDebugInfoCheckBox.Size = new System.Drawing.Size(137, 19);
            drawTileDebugInfoCheckBox.TabIndex = 17;
            drawTileDebugInfoCheckBox.Text = "Draw Tile Debug Info";
            drawTileDebugInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputFolderGroupBox
            // 
            outputFolderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            outputFolderGroupBox.Controls.Add(label7);
            outputFolderGroupBox.Controls.Add(outputFolderPathLabel);
            outputFolderGroupBox.Controls.Add(outputFolderButton);
            outputFolderGroupBox.Location = new System.Drawing.Point(3, 4);
            outputFolderGroupBox.Name = "outputFolderGroupBox";
            outputFolderGroupBox.Size = new System.Drawing.Size(541, 72);
            outputFolderGroupBox.TabIndex = 16;
            outputFolderGroupBox.TabStop = false;
            outputFolderGroupBox.Text = "Output Folder";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 49);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(34, 15);
            label7.TabIndex = 2;
            label7.Text = "Path:";
            // 
            // outputFolderPathLabel
            // 
            outputFolderPathLabel.AutoSize = true;
            outputFolderPathLabel.Location = new System.Drawing.Point(37, 49);
            outputFolderPathLabel.Name = "outputFolderPathLabel";
            outputFolderPathLabel.Size = new System.Drawing.Size(29, 15);
            outputFolderPathLabel.TabIndex = 1;
            outputFolderPathLabel.Text = "N/A";
            // 
            // outputFolderButton
            // 
            outputFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            outputFolderButton.Location = new System.Drawing.Point(6, 23);
            outputFolderButton.Name = "outputFolderButton";
            outputFolderButton.Size = new System.Drawing.Size(532, 23);
            outputFolderButton.TabIndex = 0;
            outputFolderButton.Text = "Browse";
            outputFolderButton.UseVisualStyleBackColor = true;
            outputFolderButton.Click += OutputFolderButton_Click;
            // 
            // tabPage1
            // 
            // tabPage1.Controls.Add(mapDisplayGroupBox);
            tabPage1.Controls.Add(InputFolderGroupBox);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(547, 365);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Repack/Tile Map";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // mapDisplayGroupBox
            // 
            // mapDisplayGroupBox.Controls.Add(mapDisplayOpenMapImageLabel);
            // mapDisplayGroupBox.Controls.Add(mapDisplayPictureBox);
            // mapDisplayGroupBox.Location = new System.Drawing.Point(3, 81);
            // mapDisplayGroupBox.Name = "mapDisplayGroupBox";
            // mapDisplayGroupBox.Size = new System.Drawing.Size(541, 361);
            // mapDisplayGroupBox.TabIndex = 13;
            // mapDisplayGroupBox.TabStop = false;
            // mapDisplayGroupBox.Text = "Map Display";
            // 
            // mapDisplayOpenMapImageLabel
            // 
            // mapDisplayOpenMapImageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            // mapDisplayOpenMapImageLabel.Location = new System.Drawing.Point(3, 19);
            // mapDisplayOpenMapImageLabel.Name = "mapDisplayOpenMapImageLabel";
            // mapDisplayOpenMapImageLabel.Size = new System.Drawing.Size(535, 339);
            // mapDisplayOpenMapImageLabel.TabIndex = 3;
            // mapDisplayOpenMapImageLabel.Text = "Open a map image to view map data...";
            // mapDisplayOpenMapImageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mapDisplayPictureBox
            // 
            // mapDisplayPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            // mapDisplayPictureBox.Location = new System.Drawing.Point(6, 19);
            // mapDisplayPictureBox.Name = "mapDisplayPictureBox";
            // mapDisplayPictureBox.Size = new System.Drawing.Size(529, 336);
            // mapDisplayPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // mapDisplayPictureBox.TabIndex = 12;
            // mapDisplayPictureBox.TabStop = false;
            // mapDisplayPictureBox.MouseDown += MapDisplayPictureBox_MouseDown;
            // mapDisplayPictureBox.MouseMove += MapDisplayPictureBox_MouseMove;
            // mapDisplayPictureBox.MouseUp += MapDisplayPictureBox_MouseUp;
            // 
            // mapImageGroupBox
            // 
            InputFolderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            InputFolderGroupBox.Controls.Add(label1);
            InputFolderGroupBox.Controls.Add(inputFolderPathLabel);
            InputFolderGroupBox.Controls.Add(BrowseInputFolderButton);
            InputFolderGroupBox.Cursor = System.Windows.Forms.Cursors.Default;
            InputFolderGroupBox.Location = new System.Drawing.Point(3, 3);
            InputFolderGroupBox.Name = "InputFolderGroupBox";
            InputFolderGroupBox.Size = new System.Drawing.Size(541, 72);
            InputFolderGroupBox.TabIndex = 14;
            InputFolderGroupBox.TabStop = false;
            InputFolderGroupBox.Text = "Input Folder";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 49);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 15);
            label1.TabIndex = 2;
            label1.Text = "Path:";
            // 
            // mapImageFilePathLabel
            // 
            inputFolderPathLabel.AutoSize = true;
            inputFolderPathLabel.Location = new System.Drawing.Point(37, 49);
            inputFolderPathLabel.Name = "inputFolderPathLabel";
            inputFolderPathLabel.Size = new System.Drawing.Size(29, 15);
            inputFolderPathLabel.TabIndex = 1;
            inputFolderPathLabel.Text = "N/A";
            // 
            // browseMapImageButton
            // 
            BrowseInputFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            BrowseInputFolderButton.Location = new System.Drawing.Point(6, 23);
            BrowseInputFolderButton.Name = "BrowseInputFolderButton";
            BrowseInputFolderButton.Size = new System.Drawing.Size(529, 23);
            BrowseInputFolderButton.TabIndex = 0;
            BrowseInputFolderButton.Text = "Browse";
            BrowseInputFolderButton.UseVisualStyleBackColor = true;
            BrowseInputFolderButton.Click += BrowseInputFolderButtonClick;
            // 
            // gameModFolderGroupBox
            // 
            gameModFolderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            gameModFolderGroupBox.Controls.Add(label5);
            gameModFolderGroupBox.Controls.Add(gameModFolderPathLabel);
            gameModFolderGroupBox.Controls.Add(browseGameModFolderButton);
            gameModFolderGroupBox.Location = new System.Drawing.Point(12, 108);
            gameModFolderGroupBox.Name = "gameModFolderGroupBox";
            gameModFolderGroupBox.Size = new System.Drawing.Size(555, 72);
            gameModFolderGroupBox.TabIndex = 15;
            gameModFolderGroupBox.TabStop = false;
            gameModFolderGroupBox.Text = "Game/Mod Folder";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 49);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(34, 15);
            label5.TabIndex = 2;
            label5.Text = "Path:";
            // 
            // gameModFolderPathLabel
            // 
            gameModFolderPathLabel.AutoSize = true;
            gameModFolderPathLabel.Location = new System.Drawing.Point(37, 49);
            gameModFolderPathLabel.Name = "gameModFolderPathLabel";
            gameModFolderPathLabel.Size = new System.Drawing.Size(29, 15);
            gameModFolderPathLabel.TabIndex = 1;
            gameModFolderPathLabel.Text = "N/A";
            // 
            // browseGameModFolderButton
            // 
            browseGameModFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            browseGameModFolderButton.Location = new System.Drawing.Point(6, 23);
            browseGameModFolderButton.Name = "browseGameModFolderButton";
            browseGameModFolderButton.Size = new System.Drawing.Size(546, 23);
            browseGameModFolderButton.TabIndex = 0;
            browseGameModFolderButton.Text = "Browse";
            browseGameModFolderButton.UseVisualStyleBackColor = true;
            browseGameModFolderButton.Click += BrowseGameModFolderButton_Click;
            // 
            // ERMapGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(577, 637);
            Controls.Add(progressLabel);
            Controls.Add(mapConfigurationGroupBox);
            Controls.Add(gameModFolderGroupBox);
            Controls.Add(label4);
            Controls.Add(automationModeTabControl);
            Controls.Add(versionStr);
            Controls.Add(copyrightInfoStr);
            Controls.Add(automateButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            Text = "ERMapGenerator";
            Theme = MetroFramework.MetroThemeStyle.Dark;
            Shown += ERMapGenerator_Shown;
            mapConfigurationGroupBox.ResumeLayout(false);
            mapConfigurationGroupBox.PerformLayout();
            automationModeTabControl.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            outputFolderGroupBox.ResumeLayout(false);
            outputFolderGroupBox.PerformLayout();
            tabPage1.ResumeLayout(false);
            // mapDisplayGroupBox.ResumeLayout(false);
            // ((System.ComponentModel.ISupportInitialize)mapDisplayPictureBox).EndInit();
            InputFolderGroupBox.ResumeLayout(false);
            InputFolderGroupBox.PerformLayout();
            gameModFolderGroupBox.ResumeLayout(false);
            gameModFolderGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label copyrightInfoStr;
        private Label versionStr;
        private GroupBox mapConfigurationGroupBox;
        private Button automateButton;
        private Label label2;
        private ComboBox groundLevelComboBox;
        private Label label3;
        private ComboBox zoomLevelComboBox;
        private Label progressLabel;
        private Label label4;
        private TabControl automationModeTabControl;
        private TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        // private GroupBox mapDisplayGroupBox;
        // private Label mapDisplayOpenMapImageLabel;
        // private PictureBox mapDisplayPictureBox;
        private System.Windows.Forms.GroupBox InputFolderGroupBox;
        private Label label1;
        private Label inputFolderPathLabel;
        private Button BrowseInputFolderButton;
        private System.Windows.Forms.GroupBox gameModFolderGroupBox;
        private Label label5;
        private Label gameModFolderPathLabel;
        private Button browseGameModFolderButton;
        private GroupBox outputFolderGroupBox;
        private Label label7;
        private Label outputFolderPathLabel;
        private Button outputFolderButton;
        private CheckBox drawTileDebugInfoCheckBox;
    }
}