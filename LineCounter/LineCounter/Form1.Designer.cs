namespace LineCounter
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblInfoProgram = new Label();
            form1BindingSource = new BindingSource(components);
            lblFolder = new Label();
            txtFolder = new TextBox();
            btnSelectFolder = new Button();
            lblFilesExtensions = new Label();
            txtResults = new TextBox();
            lblResults = new Label();
            btnProcess = new Button();
            toolTip1 = new ToolTip(components);
            txtTitle = new TextBox();
            btnSelectOutput = new Button();
            btnAddToList = new Button();
            gridRepositories = new DataGridView();
            valueDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            orderDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            countDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sourceFolderCollectionBindingSource = new BindingSource(components);
            settingsBindingSource = new BindingSource(components);
            btnClear = new Button();
            lstExtensions = new ComboBox();
            extensionHistoryBindingSource = new BindingSource(components);
            lstExceptions = new ComboBox();
            exceptionsHistoryBindingSource = new BindingSource(components);
            txtOutput = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            lblExceptions = new Label();
            lblTitle = new Label();
            lblOutput = new Label();
            lblRepositories = new Label();
            resourceManagerBindingSource = new BindingSource(components);
            statusStrip1 = new StatusStrip();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            lstLanguage = new ToolStripComboBox();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)form1BindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridRepositories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sourceFolderCollectionBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)settingsBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)extensionHistoryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)exceptionsHistoryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resourceManagerBindingSource).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblInfoProgram
            // 
            lblInfoProgram.AutoSize = true;
            lblInfoProgram.DataBindings.Add(new Binding("Text", form1BindingSource, "LblInfoProgram", true));
            lblInfoProgram.Location = new Point(12, 36);
            lblInfoProgram.Name = "lblInfoProgram";
            lblInfoProgram.Size = new Size(341, 15);
            lblInfoProgram.TabIndex = 0;
            lblInfoProgram.Text = "The program that counts number of lines in your project codes.";
            // 
            // form1BindingSource
            // 
            form1BindingSource.DataSource = typeof(Models.lang.Form1);
            // 
            // lblFolder
            // 
            lblFolder.AutoSize = true;
            lblFolder.DataBindings.Add(new Binding("Text", form1BindingSource, "LblFolder", true));
            lblFolder.Location = new Point(12, 126);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(130, 15);
            lblFolder.TabIndex = 3;
            lblFolder.Text = "&Select the source folder";
            // 
            // txtFolder
            // 
            txtFolder.Location = new Point(12, 144);
            txtFolder.Name = "txtFolder";
            txtFolder.Size = new Size(249, 23);
            txtFolder.TabIndex = 4;
            toolTip1.SetToolTip(txtFolder, "The root folder where the files will be counted.");
            txtFolder.TextChanged += txtFolder_TextChanged;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(267, 137);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(32, 34);
            btnSelectFolder.TabIndex = 5;
            btnSelectFolder.Text = "...";
            toolTip1.SetToolTip(btnSelectFolder, "Click here to select a source folder.");
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lblFilesExtensions
            // 
            lblFilesExtensions.AutoSize = true;
            lblFilesExtensions.DataBindings.Add(new Binding("Text", form1BindingSource, "LblFilesExtensions", true));
            lblFilesExtensions.Location = new Point(379, 126);
            lblFilesExtensions.Name = "lblFilesExtensions";
            lblFilesExtensions.Size = new Size(132, 15);
            lblFilesExtensions.TabIndex = 7;
            lblFilesExtensions.Text = "&Filter by files extensions";
            // 
            // txtResults
            // 
            txtResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResults.BackColor = Color.WhiteSmoke;
            txtResults.BorderStyle = BorderStyle.FixedSingle;
            txtResults.Location = new Point(475, 256);
            txtResults.Multiline = true;
            txtResults.Name = "txtResults";
            txtResults.ReadOnly = true;
            txtResults.ScrollBars = ScrollBars.Vertical;
            txtResults.Size = new Size(455, 246);
            txtResults.TabIndex = 18;
            // 
            // lblResults
            // 
            lblResults.AutoSize = true;
            lblResults.DataBindings.Add(new Binding("Text", form1BindingSource, "LblResults", true));
            lblResults.Location = new Point(475, 238);
            lblResults.Name = "lblResults";
            lblResults.Size = new Size(47, 15);
            lblResults.TabIndex = 17;
            lblResults.Text = "Results:";
            // 
            // btnProcess
            // 
            btnProcess.DataBindings.Add(new Binding("Text", form1BindingSource, "BtnProcess", true));
            btnProcess.Location = new Point(852, 194);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 34);
            btnProcess.TabIndex = 16;
            btnProcess.Text = "&Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // toolTip1
            // 
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            // 
            // txtTitle
            //             
            txtTitle.Location = new Point(12, 90);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(310, 23);
            txtTitle.TabIndex = 2;
            txtTitle.Text = "Ramon Pinho's LineCounter Results";
            toolTip1.SetToolTip(txtTitle, "The title that will be used on result page.");
            // 
            // btnSelectOutput
            //             
            btnSelectOutput.Location = new Point(791, 194);
            btnSelectOutput.Name = "btnSelectOutput";
            btnSelectOutput.Size = new Size(32, 34);
            btnSelectOutput.TabIndex = 15;
            btnSelectOutput.Text = "...";
            toolTip1.SetToolTip(btnSelectOutput, "Click here to select the output folder.");
            btnSelectOutput.UseVisualStyleBackColor = true;
            btnSelectOutput.Click += btnSelectOutput_Click;
            // 
            // btnAddToList
            // 
            btnAddToList.DataBindings.Add(new Binding("Text", form1BindingSource, "BtnAddToList", true));            
            btnAddToList.Location = new Point(231, 177);
            btnAddToList.Name = "btnAddToList";
            btnAddToList.Size = new Size(68, 34);
            btnAddToList.TabIndex = 6;
            btnAddToList.Text = "Add";
            toolTip1.SetToolTip(btnAddToList, "Add the source to the list below");            
            btnAddToList.UseVisualStyleBackColor = true;
            btnAddToList.Click += btnAddToList_Click;
            // 
            // gridRepositories
            // 
            gridRepositories.AllowUserToAddRows = false;
            gridRepositories.AutoGenerateColumns = false;
            gridRepositories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRepositories.Columns.AddRange(new DataGridViewColumn[] { valueDataGridViewTextBoxColumn, orderDataGridViewTextBoxColumn, countDataGridViewTextBoxColumn });
            
            gridRepositories.DataSource = sourceFolderCollectionBindingSource;
            gridRepositories.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridRepositories.Location = new Point(12, 217);
            gridRepositories.Name = "gridRepositories";
            gridRepositories.RowTemplate.Height = 25;
            gridRepositories.Size = new Size(442, 245);
            gridRepositories.TabIndex = 12;
            toolTip1.SetToolTip(gridRepositories, "Select a row and press delete to remove a folder from the list.");
            // 
            // valueDataGridViewTextBoxColumn
            // 
            valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            valueDataGridViewTextBoxColumn.HeaderText = "Value";
            valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            valueDataGridViewTextBoxColumn.Width = 350;
            // 
            // orderDataGridViewTextBoxColumn
            // 
            orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            orderDataGridViewTextBoxColumn.HeaderText = "Order";
            orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            orderDataGridViewTextBoxColumn.Visible = false;
            // 
            // countDataGridViewTextBoxColumn
            // 
            countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            countDataGridViewTextBoxColumn.HeaderText = "Count";
            countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            countDataGridViewTextBoxColumn.Visible = false;
            // 
            // sourceFolderCollectionBindingSource
            // 
            sourceFolderCollectionBindingSource.DataMember = "SourceFolderCollection";
            sourceFolderCollectionBindingSource.DataSource = settingsBindingSource;
            // 
            // settingsBindingSource
            // 
            settingsBindingSource.DataSource = typeof(Models.Settings);
            // 
            // btnClear
            // 
            btnClear.DataBindings.Add(new Binding("Text", form1BindingSource, "BtnClear", true));
            btnClear.Location = new Point(379, 468);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 34);
            btnClear.TabIndex = 19;
            btnClear.TabStop = false;
            btnClear.Text = "&Clear";
            toolTip1.SetToolTip(btnClear, "Clear the repository list");
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lstExtensions
            // 
            lstExtensions.DataSource = extensionHistoryBindingSource;
            lstExtensions.DisplayMember = "Value";
            lstExtensions.FormattingEnabled = true;
            lstExtensions.Location = new Point(379, 144);
            lstExtensions.Name = "lstExtensions";
            lstExtensions.Size = new Size(210, 23);
            lstExtensions.TabIndex = 8;
            toolTip1.SetToolTip(lstExtensions, "File extensions that will be taken for counting lines. Separte them by commas or semicolons.");
            // 
            // extensionHistoryBindingSource
            // 
            extensionHistoryBindingSource.DataMember = "ExtensionHistory";
            extensionHistoryBindingSource.DataSource = settingsBindingSource;
            // 
            // lstExceptions
            // 
            lstExceptions.DataSource = exceptionsHistoryBindingSource;
            lstExceptions.DisplayMember = "Value";
            lstExceptions.FormattingEnabled = true;
            lstExceptions.Location = new Point(611, 144);
            lstExceptions.Name = "lstExceptions";
            lstExceptions.Size = new Size(210, 23);
            lstExceptions.TabIndex = 10;
            toolTip1.SetToolTip(lstExceptions, "The exception list sepparated by commas and semicolons. Any file name or path containing a word here will be ignored.");
            // 
            // exceptionsHistoryBindingSource
            // 
            exceptionsHistoryBindingSource.DataMember = "ExceptionsHistory";
            exceptionsHistoryBindingSource.DataSource = settingsBindingSource;
            // 
            // txtOutput
            //             
            txtOutput.Location = new Point(475, 201);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(310, 23);
            txtOutput.TabIndex = 14;
            txtOutput.Text = "Temp";
            toolTip1.SetToolTip(txtOutput, "The path where report files will be saved.");
            // 
            // lblExceptions
            // 
            lblExceptions.AutoSize = true;
            lblExceptions.DataBindings.Add(new Binding("Text", form1BindingSource, "LblExceptions", true));
            lblExceptions.Location = new Point(611, 126);
            lblExceptions.Name = "lblExceptions";
            lblExceptions.Size = new Size(64, 15);
            lblExceptions.TabIndex = 9;
            lblExceptions.Text = "&Exceptions";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.DataBindings.Add(new Binding("Text", form1BindingSource, "LblTitle", true));
            lblTitle.Location = new Point(12, 72);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(29, 15);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "&Title";
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.DataBindings.Add(new Binding("Text", form1BindingSource, "LblOutput", true));
            lblOutput.Location = new Point(475, 183);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(45, 15);
            lblOutput.TabIndex = 13;
            lblOutput.Text = "Output";
            // 
            // lblRepositories
            // 
            lblRepositories.AutoSize = true;
            lblRepositories.DataBindings.Add(new Binding("Text", form1BindingSource, "LblRepositories", true));
            lblRepositories.Location = new Point(12, 183);
            lblRepositories.Name = "lblRepositories";
            lblRepositories.Size = new Size(71, 15);
            lblRepositories.TabIndex = 11;
            lblRepositories.Text = "Repositories";
            // 
            // resourceManagerBindingSource
            // 
            resourceManagerBindingSource.DataSource = typeof(Models.lang.ResourceManager);
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 516);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(942, 22);
            statusStrip1.TabIndex = 22;
            statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(942, 24);
            menuStrip1.TabIndex = 23;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { languageToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lstLanguage });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(126, 22);
            languageToolStripMenuItem.Text = "Language";
            // 
            // lstLanguage
            // 
            lstLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            lstLanguage.Items.AddRange(new object[] { "en-US", "pt-BR" });
            lstLanguage.Name = "lstLanguage";
            lstLanguage.Size = new Size(121, 23);
            lstLanguage.SelectedIndexChanged += lstLanguage_SelectedIndexChanged;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(126, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += btnExit_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += btnAbout_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(942, 538);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(btnClear);
            Controls.Add(gridRepositories);
            Controls.Add(lblRepositories);
            Controls.Add(btnAddToList);
            Controls.Add(lstExceptions);
            Controls.Add(lstExtensions);
            Controls.Add(btnSelectOutput);
            Controls.Add(txtOutput);
            Controls.Add(lblOutput);
            Controls.Add(txtTitle);
            Controls.Add(lblTitle);
            Controls.Add(lblExceptions);
            Controls.Add(btnProcess);
            Controls.Add(lblResults);
            Controls.Add(txtResults);
            Controls.Add(lblFilesExtensions);
            Controls.Add(btnSelectFolder);
            Controls.Add(txtFolder);
            Controls.Add(lblFolder);
            Controls.Add(lblInfoProgram);
            DataBindings.Add(new Binding("Text", form1BindingSource, "Title", true));
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(955, 538);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Line Counter";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)form1BindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridRepositories).EndInit();
            ((System.ComponentModel.ISupportInitialize)sourceFolderCollectionBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)settingsBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)extensionHistoryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)exceptionsHistoryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)resourceManagerBindingSource).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInfoProgram;
        private Label lblFolder;
        private TextBox txtFolder;
        private Button btnSelectFolder;
        private Label lblFilesExtensions;
        private ToolTip toolTip1;
        private TextBox txtResults;
        private Label lblResults;
        private Button btnProcess;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label lblExceptions;
        private Label lblTitle;
        private TextBox txtTitle;
        private Button btnSelectOutput;
        private TextBox txtOutput;
        private Label lblOutput;
        private ComboBox lstExtensions;
        private BindingSource extensionHistoryBindingSource;
        private BindingSource settingsBindingSource;
        private ComboBox lstExceptions;
        private BindingSource exceptionsHistoryBindingSource;
        private Button btnAddToList;
        private Label lblRepositories;
        private DataGridView gridRepositories;        
        private BindingSource sourceFolderCollectionBindingSource;
        private DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private Button btnClear;
        private BindingSource resourceManagerBindingSource;
        private BindingSource form1BindingSource;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripComboBox lstLanguage;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}