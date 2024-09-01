namespace LineCounter.Models.Results
{
    /// <summary>
    /// One node of the tree's result.
    /// </summary>
    public class ChildResult
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public ChildResult[]? Children { get; set; }
        public string? Css { get; set; }
        public string? Action { get; set; }
        public bool? Expanded { get; set; }
    }

}
