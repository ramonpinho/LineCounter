namespace LineCounter.Models.lang
{
    /// <summary>
    /// The resource manager main structre that is mapped to json files with language translations.
    /// </summary>
    public class ResourceManager
    {
        public string Language { get; set; } = "";
        public string LanguageName { get; set; } = "";
        public Form1? Form1 { get; set; } = null;
        public Dialogs? Dialogs { get; set; } = null;
        public Controlsmessage? ControlsMessage { get; set; } = null;
        public AboutBox1? AboutBox1 { get; set; } = null;
    }
}