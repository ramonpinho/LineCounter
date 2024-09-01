namespace LineCounter.Models
{
    internal abstract class BaseLevelInfo: IComparable<BaseLevelInfo>
    {
        public string Type { get; set; } = "";
        public string Name { get; set; } = "";
        public string DisplayName { get; set; } = "";

        public abstract int TotalFiles { get; }
        public abstract int TotalLines { get; }
        public abstract long TotalSize { get; }

        public int CompareTo(BaseLevelInfo? other)
        {
            return other == null?0: DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString()
        {
            return $"{Type}={Name}";
        }
    }
}
