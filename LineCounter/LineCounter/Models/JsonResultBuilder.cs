namespace LineCounter.Models
{
    /// <summary>
    /// The old structure that was used to generate the output.
    /// Obsolete.
    /// </summary>
    internal class JsonResultBuilder
    {
        //public List <BaseLevelInfo>? ExtResults;
        //public List <BaseLevelInfo>? Folders;
        public int FinalTotalFiles { get; set; } = 0;
        public int FinalTotalLines { get; set; } = 0;
        public long FinalTotalSize { get; set; } = 0;
        public int TotalSkips { get; set; } = 0;
    }
}
