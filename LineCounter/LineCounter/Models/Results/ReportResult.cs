namespace LineCounter.Models.Results
{
    /// <summary>
    /// The root of report structure that will be serialized to JSON.
    /// </summary>
    public class ReportResult
    {
        /// <summary>
        /// The list of children root folders.
        /// In this level each children will be a repository.
        /// </summary>
        public ChildResult[]? Root { get; set; }
    }
}
