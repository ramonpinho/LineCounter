namespace LineCounter.Models
{
    /// <summary>
    /// The settings interface that the controls know about.
    /// </summary>
    internal interface ISettings
    {
        string Title { get; set; }
        string SourceFolder { get; set; }
        List<SettingMru> SourceFolderCollection { get; set; }
        string Extensions { get; set; }
        string[] ExtensionsLst { get; }

        string Exceptions { get; set; }
        string[] ExceptionLst { get; }

        string Output { get; set; }

        string Language { get; set; }
    }
}
