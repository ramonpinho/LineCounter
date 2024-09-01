using LineCounter.Models;
using LineCounter.Models.lang;
using LineCounter.Models.Results;
using LineCounter.Utils;

namespace LineCounter.Controls
{
    /// <summary>
    /// The main application control.
    /// It manages the ResourceManager.
    /// 
    /// </summary>
    internal class CounterControl
    {
        /// <summary>
        /// Keeps a reference to the loaded resource manager for every where use.
        /// </summary>
        public static ResourceManager? ResourceManager { get; set; }
        /// <summary>
        /// The event that happens when there is something important insinde this control to write down in external log or screen.
        /// </summary>
        public event EventHandler? LogEvent;
        /// <summary>
        /// It requires a loaded ISettings to work.
        /// </summary>
        /// <param name="settings"></param>
        public CounterControl(ISettings settings)
        {            
            this.settings = settings;
        }
        /// <summary>
        /// While processing, any relevant message will be temporaly stored in this list and it will be available to external control.
        /// Every time a new Run() happens, the messages are reset.
        /// </summary>
        public List<string>? ValidationMessages { get { return _validationMessages; }  }
        /// <summary>
        /// A reference to settings.
        /// </summary>
        private ISettings? settings = null;
        /// <summary>
        /// Run!
        /// I mean, this will start the control process and generate the output report files.
        /// </summary>
        internal void Run()
        {
            if (settings == null)
                return;

            if (ResourceManager == null || ResourceManager.ControlsMessage == null)
                return;

            if (Validate())
            {
                CounterInfoNode root = new CounterInfoNode(settings);
                root.DisplayName = ResourceManager.ControlsMessage.OVERALL;

                WriteLog($"{ResourceManager.ControlsMessage.OF_TOTAL} {settings.SourceFolderCollection.Count} {ResourceManager.ControlsMessage.REPOSITORIES}");

                Dictionary<string, CounterInfoNode> dic = new Dictionary<string, CounterInfoNode>();

                foreach (SettingMru mru in settings.SourceFolderCollection)                
                {
                    WriteLog($"{ResourceManager.ControlsMessage.Processing} {mru.Value}...");
                    CounterInfoNode repository = new CounterInfoNode(root, mru.Value);
                    //repository.DisplayName = $"";
                    root.AddChildren(repository);
                    repository.Compute();

                    root.ChildrenTotalFiles += repository.TotalFiles;
                    root.ChildrenTotalLines += repository.TotalLines;
                    root.ChildrenTotalSize += repository.TotalSize;

                    foreach (CounterInfoNode info in repository.ExtensionsInfoList)
                    {
                        if (!dic.ContainsKey(info.Name))
                        {
                            CounterInfoNode extInfoCounter = new();
                            extInfoCounter.Name = info.Name;
                            extInfoCounter.DisplayName = info.Name;

                            extInfoCounter.ChildrenTotalFiles = info.TotalFiles;
                            extInfoCounter.ChildrenTotalLines = info.TotalLines;
                            extInfoCounter.ChildrenTotalSize = info.TotalSize;

                            extInfoCounter.Children.Add(info);

                            dic.Add(info.Name, extInfoCounter);
                        }
                        else
                        {
                            dic[info.Name].ChildrenTotalFiles += info.TotalFiles;
                            dic[info.Name].ChildrenTotalLines += info.TotalLines;
                            dic[info.Name].ChildrenTotalSize += info.TotalSize;

                            dic[info.Name].Children.Add(info);
                        }
                    }
                    
                }

                root.ExtensionsInfoList.Clear();
                root.ExtensionsInfoList.AddRange(dic.Values);

                GenerateJsonResult(root);

            }
        }
        /// <summary>
        /// Generante a json with the result that will be used in HTML outpu files.
        /// </summary>
        /// <param name="root"></param>
        private void GenerateJsonResult(CounterInfoNode root)
        {
            if (settings == null)
                return;

            if (ResourceManager == null || ResourceManager.ControlsMessage == null)
                return;

            WriteLog(ResourceManager.ControlsMessage.GenerateJsonResult01);

            ReportResult reportResult = new ReportResult();

            reportResult.Root = new ChildResult[3];
            
            ChildResult childResult = new ChildResult();
            reportResult.Root[0] = childResult;
            childResult.Expanded = true;
            childResult.Description = root.DisplayName;

            childResult.Children = new ChildResult[3];            
            childResult.Children[0] = CreateChild($"{ResourceManager.ControlsMessage.TOTAL_FILES}: {root.TotalFiles.ToString("#,###")}");
            
            childResult.Children[1] = CreateChild($"{ResourceManager.ControlsMessage.TOTAL_LINES}: {root.TotalLines.ToString("#,###")}");
            
            string _totalSize = Numbers.FormatByteLength(root.TotalSize);
            childResult.Children[2] = CreateChild($"{ResourceManager.ControlsMessage.TOTAL_SIZE}: {_totalSize}");
            
            //childResult.Children[3] = CreateChild($"{TXT_SKIPS}: {root.TotalSkips}");
            
            childResult = CreateChild($"{ResourceManager.ControlsMessage.TXT_REPOSITORIES} ({root.Children.Count})");
            reportResult.Root[1] = childResult;
            childResult.Children = GetChildren(root.Children);

            childResult = CreateChild($"{ResourceManager.ControlsMessage.TXT_TOTAL_BY_EXT} ({root.ExtensionsInfoList.Count})");
            reportResult.Root[2] = childResult;
            childResult.Children = GetChildren(root.ExtensionsInfoList);

            WriteLog(ResourceManager.ControlsMessage.GenerateJsonResult02);
            FileControl fileControl = new FileControl(settings);
            fileControl.WriteResults(reportResult);
            WriteLog(ResourceManager.ControlsMessage.GenerateJsonResult03);
        }
        /// <summary>
        /// The main function to build a tree structure.
        /// Returns the ChildResult which is a node in a tree structure.
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        private static ChildResult[] GetChildren(List<CounterInfoNode> children)
        {
            List<ChildResult> lstResult = new List<ChildResult>();

            if (ResourceManager == null || ResourceManager.ControlsMessage == null)
                return lstResult.ToArray();

            foreach (var child in children)
            {
                if (child.TotalFiles == 0)
                    continue; // skip folder if there is no files to count there. TODO: Make this an option.

                string _tsize = Numbers.FormatByteLength(child.TotalSize);
                string node = $"{child.DisplayName} : {child.TotalFiles.ToString("#,###")} {ResourceManager.ControlsMessage.Files}, {child.TotalLines.ToString("#,###")} {ResourceManager.ControlsMessage.Lines} ({_tsize})";

                ChildResult c = CreateChild(node);
                lstResult.Add(c);

                if (child.Children.Count > 0)
                    c.Children = GetChildren(child.Children);
            }
            return lstResult.ToArray();
        }
        /// <summary>
        /// Creates a single ChildReresult with a given description. Object to be part of a tree.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static ChildResult CreateChild(string description)
        {
            ChildResult child = new ChildResult();            
            child.Description = description;
            return child;
        }        

        private List<string>? _validationMessages = null;
        /// <summary>
        /// A validation happens before going on with the control processing.
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            if (ResourceManager == null || ResourceManager.ControlsMessage == null)
            {
                AddMessage("ERROR!");
                return false;
            }

            WriteLog($"{ResourceManager.ControlsMessage.Validating}...");

            ResetMessages();

            if (settings == null)
            {
                AddMessage(ResourceManager.ControlsMessage.MSG_SETTINGS_NOT_DEFINED);
                return false;
            }

            if(settings.SourceFolderCollection.Count == 0)
            {
                if (string.IsNullOrEmpty(settings.SourceFolder))
                {
                    AddMessage(ResourceManager.ControlsMessage.MSG_FOLDER_EMPTY);
                    return false;
                }
                else
                {
                    SettingMru mru = new SettingMru() { Value = settings.SourceFolder, Count = 1, Order = 0 };
                    settings.SourceFolderCollection.Add(mru);
                }
            }            

            return true;
        }
        /// <summary>
        /// Add a message to the message list.
        /// </summary>
        /// <param name="message"></param>
        private void AddMessage(string message)
        {
            if(_validationMessages != null)
                _validationMessages.Add(message);
        }
        /// <summary>
        /// Reset the message list.
        /// </summary>
        private void ResetMessages()
        {
            _validationMessages = new List<string>();
        }
        /// <summary>
        /// Function to raise the log events.
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            LogEvent?.Invoke(this, new LogCounterEventArgs(msg));
        }
    }
}
