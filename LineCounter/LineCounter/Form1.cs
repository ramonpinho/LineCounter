using LineCounter.Controls;
using LineCounter.Models;
using LineCounter.Models.lang;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace LineCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (_currentSettings == null)
                return;

            Cursor currentCursor = Cursor;

            try
            {
                Cursor = Cursors.WaitCursor;
                toggle(false);
                txtResults.Clear();

                _currentSettings.SourceFolder = txtFolder.Text;
                _currentSettings.Extensions = GetValueFromUI(lstExtensions, _currentSettings.ExtensionHistory);
                _currentSettings.Exceptions = GetValueFromUI(lstExceptions, _currentSettings.ExceptionsHistory);
                _currentSettings.Title = txtTitle.Text;
                _currentSettings.Output = txtOutput.Text;

                _currentSettings.UpdateHistory();

                //TODO: Add input validation
                SaveSettings();

                CounterControl counterControl = new CounterControl(_currentSettings);
                counterControl.LogEvent += CounterControl_LogEvent;
                counterControl.Run();

            }
            finally
            {
                Cursor = currentCursor;
                toggle(true);
            }
        }
        /// <summary>
        /// Get value from UI combo box and update history, case it is a new value.
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="history"></param>
        /// <returns></returns>
        private string GetValueFromUI(ComboBox lst, List<SettingMru> history)
        {
            string text = lst.Text;
            bool found = false;
            foreach (SettingMru ext in history)
                if (text == ext.Value)
                    found = true;
            if (!found)
            {
                SettingMru ext = new SettingMru() { Value = text, Count = 1, Order = history.Count };
                history.Add(ext);
            }
            return text;
        }

        private void CounterControl_LogEvent(object? sender, EventArgs e)
        {
            LogCounterEventArgs? args = e as LogCounterEventArgs;
            if (args != null)
            {
                txtResults.AppendText(args.Message);
                txtResults.AppendText(Environment.NewLine);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (_currentSettings != null && _currentSettings.SourceFolder != null && Directory.Exists(_currentSettings.SourceFolder))
                folderBrowserDialog1.InitialDirectory = _currentSettings.SourceFolder;
            // Select the source folder
            if (resourceManager != null && resourceManager.Dialogs != null)
                folderBrowserDialog1.Description = resourceManager.Dialogs.FolderBrowserDialog2;
            DialogResult result = folderBrowserDialog1.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    txtFolder.Text = folderBrowserDialog1.SelectedPath;
                    txtResults.Clear();
                    // Automatically add to repositories:
                    btnAddToList_Click(sender, e);
                    break;
            }
        }
        /// <summary>
        /// Enables or disable the UI componentes.
        /// </summary>
        /// <param name="value"></param>
        private void toggle(bool value)
        {
            exitToolStripMenuItem.Enabled = value;
            btnProcess.Enabled = value;
            txtFolder.Enabled = value;
            lstExtensions.Enabled = value;
            lstExceptions.Enabled = value;
            txtOutput.Enabled = value;

            btnSelectFolder.Enabled = value;
            btnSelectOutput.Enabled = value;
            aboutToolStripMenuItem.Enabled = value;

            if (value)
                ValidateAddButton();
            else
                btnAddToList.Enabled = false;
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            if (_currentSettings != null && _currentSettings.Output != null && Directory.Exists(_currentSettings.Output))
                folderBrowserDialog1.InitialDirectory = _currentSettings.Output;
            // Select the folder for the output files
            if(resourceManager != null && resourceManager.Dialogs != null)
                folderBrowserDialog1.Description = resourceManager.Dialogs.FolderBrowserDialog1;
            DialogResult result = folderBrowserDialog1.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                    txtOutput.Text = folderBrowserDialog1.SelectedPath;
                    break;
            }
        }

        private string settingsPath = "";
        private string settingsFile = "";
        private Settings? _currentSettings = null;
        private bool _loading = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            _loading = true;
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingsPath = $"{appData}\\LineCounter\\";
            settingsFile = $"{settingsPath}settings.json";

            if (File.Exists(settingsFile))
            {
                StreamReader? reader = null;
                try
                {
                    reader = new StreamReader(settingsFile);
                    string json = reader.ReadToEnd();

                    _currentSettings = JsonSerializer.Deserialize<Settings>(json);

                    if (_currentSettings != null)
                    {
                        txtFolder.Text = _currentSettings.SourceFolder;
                        txtTitle.Text = _currentSettings.Title;
                        txtOutput.Text = _currentSettings.Output;

                        lstLanguage.SelectedItem = _currentSettings.Language;
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }

            if (_currentSettings == null)
                _currentSettings = new Settings();

            // Adding protection case lists are empty, add default values to the list
            if (_currentSettings.ExtensionHistory.Count == 0)
            {
                SettingMru mru = new SettingMru();
                mru.Value = Settings.DEFAULT_EXT;
                _currentSettings.ExtensionHistory.Add(mru);
            }

            extensionHistoryBindingSource.DataSource = _currentSettings.ExtensionHistory;
            lstExtensions.DisplayMember = "Value";
            lstExtensions.ValueMember = "Value";

            lstExtensions.SelectedValue = _currentSettings.Extensions;

            if (_currentSettings.ExceptionsHistory.Count == 0)
            {
                SettingMru mru = new SettingMru();
                mru.Value = Settings.DEFAULT_EXCEPTION;
                _currentSettings.ExceptionsHistory.Add(mru);
            }

            exceptionsHistoryBindingSource.DataSource = _currentSettings.ExceptionsHistory;
            lstExceptions.DisplayMember = "Value";
            lstExceptions.ValueMember = "Value";

            lstExceptions.SelectedValue = _currentSettings.Exceptions;

            sourceFolderCollectionBindingSource.DataSource = _currentSettings.SourceFolderCollection;

            if (!string.IsNullOrEmpty(_currentSettings.Language))
            {
                LoadLang(_currentSettings.Language);
            }
            else
            {
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                LoadLang(cultureInfo.Name);
            }

            _loading = false;
        }

        private ResourceManager? resourceManager = null;
        private void LoadLang(string lang)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = assembly.Location.Replace("LineCounter.dll", "");
            string langFile = $"{path}Resources\\lang\\{lang}.json";

            if (File.Exists(langFile))
            {
                StreamReader? reader = null;
                try
                {
                    reader = new StreamReader(langFile);
                    string json = reader.ReadToEnd();
                    resourceManager = JsonSerializer.Deserialize<ResourceManager>(json);
                    CounterControl.ResourceManager = resourceManager;

                    if (resourceManager != null)
                    {
                        form1BindingSource.DataSource = resourceManager.Form1;
                        form1BindingSource.ResetBindings(false);

                        if (_currentSettings != null)
                        {
                            _currentSettings.Language = lang;
                        }

                        if (resourceManager.Form1 != null)
                        {
                            // Menus:
                            aboutToolStripMenuItem.Text = resourceManager.Form1.BtnAbout;
                            helpToolStripMenuItem.Text = resourceManager.Form1.Help;
                            exitToolStripMenuItem.Text = resourceManager.Form1.BtnExit;
                            fileToolStripMenuItem.Text = resourceManager.Form1.File;
                            languageToolStripMenuItem.Text = resourceManager.Form1.Language;

                            //tooltips:
                            toolTip1.SetToolTip(txtFolder, resourceManager.Form1.TxtFolder_tooltip); // "The root folder where the files will be counted."
                            toolTip1.SetToolTip(btnSelectFolder, resourceManager.Form1.BtnSelectFolder_tooltip); // Click here to select a source folder
                            toolTip1.SetToolTip(txtTitle, resourceManager.Form1.TxtTitle_tooltip); // "The title that will be used on result page."
                            toolTip1.SetToolTip(btnSelectOutput, resourceManager.Form1.BtnSelectOutput_tooltip); // "Click here to select the output folder."
                            toolTip1.SetToolTip(btnAddToList, resourceManager.Form1.BtnAddToList_tooltip); //  "Add the source to the list below"
                            toolTip1.SetToolTip(gridRepositories, resourceManager.Form1.Grid_tooltip); //  "Select a row and press delete to remove a folder from the list."
                            toolTip1.SetToolTip(btnClear, resourceManager.Form1.BtnClear_tooltip); //  "Clear the repository list"
                            toolTip1.SetToolTip(lstExtensions, resourceManager.Form1.LstFilesExtensions_tooltip); //  "File extensions that will be taken for counting lines. Separte them by commas or semicolons."
                            toolTip1.SetToolTip(lstExceptions, resourceManager.Form1.LstExceptions_tooltip); //  "The exception list sepparated by commas and semicolons. Any file name or path containing a word here will be ignored."
                            toolTip1.SetToolTip(txtOutput, resourceManager.Form1.TxtOutput_tooltip); //  "The path where report files will be saved."
                        }
                        
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
        }

        private void SaveSettings()
        {
            if (_currentSettings != null)
            {
                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);

                StreamWriter? writer = null;

                try
                {
                    if (File.Exists(settingsFile))
                        File.Delete(settingsFile);

                    writer = new StreamWriter(settingsFile);
                    string _json = JsonSerializer.Serialize(_currentSettings);
                    writer.Write(_json);
                    writer.Close();
                    writer.Dispose();
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                        writer.Dispose();
                    }
                }

            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog(this);
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (_currentSettings != null)
            {
                int order = _currentSettings.SourceFolderCollection.Count;

                SettingMru? mru = Find(txtFolder.Text, _currentSettings.SourceFolderCollection);

                if (mru == null)
                {
                    mru = new SettingMru() { Value = txtFolder.Text, Count = 1, Order = order };
                    _currentSettings.SourceFolderCollection.Add(mru);
                    sourceFolderCollectionBindingSource.ResetBindings(false);

                    txtFolder.Text = "";

                    ValidateAddButton();
                }
            }
        }

        private SettingMru? Find(string value, IList<SettingMru> list)
        {
            foreach (SettingMru s in list)
                if (s.Value == value)
                    return s;

            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_currentSettings != null)
            {
                _currentSettings.SourceFolderCollection.Clear();
                sourceFolderCollectionBindingSource.ResetBindings(false);
                ValidateAddButton();
            }
        }

        private void ValidateAddButton()
        {
            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                btnAddToList.Enabled = false;
                return;
            }

            if (_currentSettings != null)
            {
                SettingMru? mru = Find(txtFolder.Text, _currentSettings.SourceFolderCollection);
                btnAddToList.Enabled = mru == null;
            }
            else
                btnAddToList.Enabled = false;
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            ValidateAddButton();
        }

        private void lstLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_loading) return;

            string? lang = lstLanguage.SelectedItem as string;
            if (lang != null)
            {
                LoadLang(lang);
                SaveSettings();
                
                foreach (ToolStripMenuItem item in menuStrip1.Items)
                {
                    if (item.HasDropDownItems)
                    {
                        item.DropDown.Close();
                    }
                }
            }
        }
    }
}