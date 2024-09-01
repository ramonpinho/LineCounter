namespace LineCounter.Models
{
    internal class ExtLevel : BaseLevelInfo
    {
        public override int TotalFiles { get { return _totalFiles; } }
        public override int TotalLines { get { return _totalLines; } }
        public override long TotalSize { get { return _totalSize; } }

        public string Extension { get; set; } = "";

        public void SetTotalFiles(int value)
        {
            _totalFiles = value;
        }

        public void SetTotalLines(int value)
        {
            _totalLines = value;
        }

        public void SetTotalSize(long value)
        {
            _totalSize = value;
        }

        public ExtLevel()
        {
            Type = "Extension";
        }

        public override string ToString()
        {
            return $"{base.ToString()},TotalFiles={TotalFiles},TotalLines={TotalLines},TotalSize={TotalSize}";
        }

        private int _totalFiles;
        private int _totalLines;
        private long _totalSize;
        
    }
}
