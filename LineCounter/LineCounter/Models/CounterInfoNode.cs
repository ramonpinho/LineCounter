
namespace LineCounter.Models
{
    /// <summary>
    /// The tree node that keeps the numbers of all folder levels that are analised.
    /// TODO: Add some statics such mean characters per line, mean words per file, total line comments.
    /// TODO: Add the skips files and folder.
    /// </summary>
    internal class CounterInfoNode : IComparable<CounterInfoNode>
    {
        /// <summary>
        /// Gets or sets the total files in this node.
        /// Does not take account the children values.
        /// </summary>
        public int NodeTotalFiles { get; set; }
        /// <summary>
        /// Gets or sets the total lines from all the files in this node.
        /// Also does not take account the children values.
        /// </summary>
        public int NodeTotalLines { get; set; }
        /// <summary>
        /// Gets or sets the total size in bytes of the analysed files in this node.
        /// </summary>
        public long NodeTotalSize { get; set; }
        /// <summary>
        /// Gets or sets the total files from this node's children.
        /// </summary>
        public int ChildrenTotalFiles { get; set; }
        /// <summary>
        /// Gets or sets the total lines from this node's children.
        /// </summary>
        public int ChildrenTotalLines { get; set; }
        /// <summary>
        /// Gets or sets the total size from this node's children.
        /// </summary>
        public long ChildrenTotalSize { get; set; }
        /// <summary>
        /// Gets the total files (this node total files + this children total files).
        /// </summary>
        public int TotalFiles {
            get {
                return NodeTotalFiles + ChildrenTotalFiles;
            }
        }
        /// <summary>
        /// Gets the total line (this node total lines + this children total lines).
        /// </summary>
        public int TotalLines {
            get {
                return NodeTotalLines + ChildrenTotalLines;
            }
        }
        /// <summary>
        /// Gets the total size (this node total size + this children total size).
        /// </summary>
        public long TotalSize {
            get {
                return NodeTotalSize + ChildrenTotalSize;
            }
        }
        /// <summary>
        /// This node type. I think it is no longer being used.
        /// </summary>
        public string Type { get; set; } = "";
        /// <summary>
        /// The name that could be the folder name or the extension.
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// The text that will be used to display in report.
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// A reference to the settings.
        /// </summary>
        private ISettings? settings = null;
        /// <summary>
        /// Default constructor.
        /// Used for root nodes.
        /// </summary>
        public CounterInfoNode()
        {

        }
        /// <summary>
        /// Also used for root nodes, but gets the settings.
        /// </summary>
        /// <param name="settings"></param>
        public CounterInfoNode(ISettings settings)
        {
            this.settings = settings;
        }
        /// <summary>
        /// Creates a node with a known parent.
        /// It is tied to a folder.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="folder"></param>
        public CounterInfoNode(CounterInfoNode parent, string folder)
        {
            this.folder = folder;
            settings = parent.settings;

            string? d = Path.GetDirectoryName(folder);
            if (!string.IsNullOrEmpty(d))
            {
                Name = folder.Replace(d, "").Replace("\\", "");
                if (string.IsNullOrEmpty(parent.folder)) // for now, this is means this is the root foolder for repository. Then, use full path.
                    DisplayName = folder;
                else
                    DisplayName = $"/{Name}";
            }
            else
            {
                Name = folder;
                DisplayName = folder;
            }
        }
        /// <summary>
        /// Compare one of these to another. Used for sortings.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CounterInfoNode? other)
        {
            return other == null ? 0 : DisplayName.CompareTo(other.DisplayName);
        }
        /// <summary>
        /// Be a string!
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Type}={Name}";
        }
        /// <summary>
        /// Returns the folder value that this node refers to.
        /// </summary>
        public string Folder { get { return folder; } }
        /// <summary>
        /// The folder.
        /// </summary>
        private string folder = "";
        /// <summary>
        /// Count how many skipped files or folders occurred in this folder.
        /// </summary>
        private int totalSkips;
        /// <summary>
        /// The extensions lists found in this folder level with their countings.
        /// </summary>
        public List<CounterInfoNode> ExtensionsInfoList { get { return _extensionsInfoList; } }
        private List<CounterInfoNode> _extensionsInfoList = new List<CounterInfoNode>();
        /// <summary>
        /// The list of this node's children.
        /// </summary>
        public List<CounterInfoNode> Children { get { return children; } }
        private List<CounterInfoNode> children = new List<CounterInfoNode>();
        /// <summary>
        /// Compute will happen when trying to access one of the properties.
        /// Avoiding then unecessary computation while still processing values.
        /// </summary>
        public void Compute()
        {
            if (settings == null)
                return;

            ComputeChildren();

            Dictionary<string, CounterInfoNode> dic = _extensionsInfoList.ToDictionary(f => f.Name);

            foreach (string ext in settings.ExtensionsLst)
            {
                string[] files = Directory.GetFiles(folder, ext, SearchOption.TopDirectoryOnly);

                CounterInfoNode? extInfoCounter = null;                

                foreach (string file in files)
                {
                    bool skip = false;
                    foreach (string exp in settings.ExceptionLst)
                        if (file.ToLower().Contains(exp))
                        {
                            skip = true;
                            break;
                        }

                    if (skip)
                    {
                        totalSkips++;
                        continue;
                    }

                    string ext2 = Path.GetExtension(file);

                    if(!dic.ContainsKey(ext2))                    
                    {
                        extInfoCounter = new CounterInfoNode();
                        extInfoCounter.Name = ext2;                        
                        extInfoCounter.DisplayName = Name;
                        dic.Add(ext2,extInfoCounter);
                    }
                    else
                    {
                        if (extInfoCounter == null)
                            extInfoCounter = dic[ext2];
                    }

                    string? fileFolder = Path.GetDirectoryName(file);

                    if (string.IsNullOrEmpty(fileFolder))
                        continue;

                    StreamReader? reader = null;
                    try
                    {
                        //TODO: Add protection to skip big files
                        //TODO: Add protection to only open text files? Avoid binary files?
                        FileInfo fileInfo = new FileInfo(file);
                        NodeTotalSize += fileInfo.Length;
                        extInfoCounter.NodeTotalSize += fileInfo.Length;

                        reader = new StreamReader(file);
                        while (!reader.EndOfStream)
                        {
                            reader.ReadLine();
                            NodeTotalLines += 1;
                            extInfoCounter.NodeTotalLines += 1;
                        }

                        NodeTotalFiles += 1;
                        extInfoCounter.NodeTotalFiles += 1;
                    }
                    //catch (Exception ex)
                    //{
                    //    //WriteLog(ex.Message);
                    //}
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
            _extensionsInfoList.Clear();
            _extensionsInfoList.AddRange(dic.Values);
        }
        /// <summary>
        /// Compute the children's values.
        /// </summary>
        private void ComputeChildren()
        {
            if (settings == null)
                return;

            if (string.IsNullOrEmpty(folder))
                return;

            if (!Directory.Exists(folder))
                return;

            Dictionary<string, CounterInfoNode> dic = _extensionsInfoList.ToDictionary(f => f.Name);

            string[] childrenDir = Directory.GetDirectories(folder);
            foreach (string childDir in childrenDir)
            {
                bool _skip = false;
                foreach(string exc in settings.ExceptionLst)
                    if(childDir.Contains(exc))
                        _skip = true;

                if (_skip)
                {
                    totalSkips++;
                    continue;
                }

                CounterInfoNode child = new CounterInfoNode(this, childDir);
                AddChildren(child);
                child.Compute();

                ChildrenTotalFiles += child.TotalFiles;
                ChildrenTotalLines += child.TotalLines;
                ChildrenTotalSize += child.TotalSize;

                foreach(CounterInfoNode f in child.ExtensionsInfoList)
                {
                    if(dic.ContainsKey(f.Name))
                    {
                        dic[f.Name].ChildrenTotalFiles += f.TotalFiles;
                        dic[f.Name].ChildrenTotalLines += f.TotalLines;
                        dic[f.Name].ChildrenTotalSize += f.TotalSize;

                        dic[f.Name].Children.Add(f);
                    }
                    else
                    {
                        CounterInfoNode extInfoCounter = new();
                        extInfoCounter.Name = f.Name;
                        extInfoCounter.DisplayName = Name;

                        extInfoCounter.ChildrenTotalFiles = f.TotalFiles;
                        extInfoCounter.ChildrenTotalLines = f.TotalLines;
                        extInfoCounter.ChildrenTotalSize = f.TotalSize;

                        extInfoCounter.Children.Add(f);

                        dic.Add(f.Name, extInfoCounter);
                    }
                }
            }

            _extensionsInfoList.Clear();
            _extensionsInfoList.AddRange(dic.Values);
        }
        /// <summary>
        /// Add a children node to this node.
        /// </summary>
        /// <param name="child"></param>
        internal void AddChildren(CounterInfoNode child)
        {
            children.Add(child);            
        }
    }
}
