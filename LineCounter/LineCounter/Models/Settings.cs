namespace LineCounter.Models
{
    /// <summary>
    /// The application settings.
    /// All users settings that is stored in app folder will be exposed here.
    /// </summary>
    public class Settings: ISettings
    {
        /// <summary>
        /// Used as default extensions value.
        /// </summary>
        public const string DEFAULT_EXT = "*.*";
        /// <summary>
        /// Default exception list.
        /// </summary>
        public const string DEFAULT_EXCEPTION = "\\obj;\\debug;\\bin";
        /// <summary>
        /// Defines the current language to be used.
        /// Language resources must exist so the application works properly.
        /// </summary>
        public string Language { get; set; } = "";
        /// <summary>
        /// The last report title used.
        /// </summary>
        public string Title { get; set; } = "";
        /// <summary>
        /// The last SourceFolder used.
        /// Since the SouceFolderCollection was added this field might be removed.
        /// </summary>
        public string SourceFolder { get; set; } = "";
        /// <summary>
        /// Stores the list of source folders that will be analysed.
        /// There is no history for this list.
        /// TODO: Add option to user save this list in another file that can later be re-loaded.
        /// </summary>
        public List<SettingMru> SourceFolderCollection { get; set; } = new List<SettingMru>();
        /// <summary>
        /// The last extensions values used.
        /// Since the ExtensionLst was added this property is obsolete.
        /// </summary>
        public string Extensions { get; set; } = "";
        /// <summary>
        /// The history of extensions used in this application.
        /// During process, the extensions will define the files the program will get.
        /// The history is loaded in "Filter by files extension" combo box.
        /// It is stored as used settings.
        /// TODO: Add a limit to this list size.
        /// </summary>
        public string[] ExtensionsLst {
            get
            {
                if(_extensions == null)
                {
                    string ext;
                    if (string.IsNullOrEmpty(Extensions))
                        ext = DEFAULT_EXT;
                    else
                        ext = Extensions;
                    _extensions = ext.ToLower().Split(_separators);
                }

                return _extensions;
            }
        }
        /// <summary>
        /// Similar to Extensions, this was the last exceptions values used.
        /// However, after ExcetpionLst was added, this property is obsolete.
        /// </summary>
        public string Exceptions { get; set; } = "";
        /// <summary>
        /// Keeps the history of exception values that will be used to ignore files and folders during the process.
        /// The exception history is saved within user settings file, and loaded in Exception List.
        /// TODO: Add a limit to this list size.
        /// </summary>
        public string[] ExceptionLst
        {
            get
            {
                if (_exceptions == null)
                {
                    string ext;
                    if (string.IsNullOrEmpty(Exceptions))
                        ext = DEFAULT_EXCEPTION;
                    else
                        ext = Exceptions;
                    _exceptions = ext.ToLower().Split(_separators);
                }

                return _exceptions;
            }
        }
        /// <summary>
        /// The last output folder used.
        /// The files will be stored in this folder when running the process.
        /// </summary>
        public string Output { get; set; } = "";
        /// <summary>
        /// The source folders list to be stored.
        /// Currently not being used.
        /// </summary>
        public List<SettingMru> SourceHistory { get { return _sourceHistory; } set { _sourceHistory = value; } }
        /// <summary>
        /// The extensions history list to be stored.
        /// </summary>
        public List<SettingMru> ExtensionHistory { get { return _extensionHistory; } set { _extensionHistory = value; } }
        /// <summary>
        /// The exception history list to be stored.
        /// </summary>
        public List<SettingMru> ExceptionsHistory { get { return _exceptionsHistory; } set { _exceptionsHistory = value; } }
        /// <summary>
        /// The output folders history.
        /// Currently not being used.
        /// </summary>
        public List<SettingMru> OutputHistory { get { return _outputHistory; } set { _outputHistory = value; } }

        private List<SettingMru> _sourceHistory = new List<SettingMru>();
        private List<SettingMru> _extensionHistory = new List<SettingMru>();
        private List<SettingMru> _exceptionsHistory = new List<SettingMru>() ;
        private List<SettingMru> _outputHistory = new List<SettingMru>();
        /// <summary>
        /// The separator to split values for extensions and exceptions values.
        /// </summary>
        private char[] _separators = { ';', ',' };
        private string[]? _extensions = null;
        private string[]? _exceptions = null;
        /// <summary>
        /// Before save the settings file, update the history list with the current selected values.
        /// </summary>
        internal void UpdateHistory()
        {                        
            UpdateHistory(SourceFolder, SourceHistory);
            UpdateHistory(Extensions, ExtensionHistory);
            UpdateHistory(Exceptions, ExceptionsHistory);
            UpdateHistory(Output, OutputHistory);

            foreach (SettingMru mru in SourceFolderCollection)
                UpdateHistory(mru.Value, SourceHistory);
        }
        /// <summary>
        /// I value is not in the list, add it.
        /// Maybe I should keep a dictionary of these lists?
        /// Can be static?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sourceList"></param>
        private void UpdateHistory(string value, List<SettingMru> sourceList)
        {            
            SettingMru? setting = null;

            foreach (SettingMru mru in sourceList)
                if (mru.Value == value)
                    setting = mru;

            if (setting == null)
            {
                if(!string.IsNullOrEmpty(value))
                {
                    SettingMru newMru = new SettingMru() { Value = value, Order = sourceList.Count, Count = 1 };
                    sourceList.Add(newMru);
                }
            }
            else
            {
                setting.Count++;
            }
        }
    }
}
