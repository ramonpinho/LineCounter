namespace LineCounter.Models
{
    /// <summary>
    /// The structure to the history lists.
    /// TODO: Implement the most recent used in the lists an order them.
    /// Maybe it will be easier to sort them with a date time...
    /// </summary>
    public class SettingMru
    {
        public string Value { get; set; } = "";
        public int Order { get; set; }
        public int Count { get; set; }
    }
}
