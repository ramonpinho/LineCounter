using LineCounter.Models.Results;
using LineCounter.Models;
using System.Reflection;
using System.Text.Json;

namespace LineCounter.Controls
{
    /// <summary>
    /// The control responsible to write the output files.
    /// It gets the file content from internal assembly resources.
    /// It uses the settings to manage languange definitions.
    /// </summary>
    internal class FileControl
    {        
        /// <summary>
        /// Requires an object of ISettings to work properly.
        /// </summary>
        /// <param name="settings"></param>
        public FileControl(ISettings settings)
        {
            this.settings = settings;
        }
        /// <summary>
        /// Write the results in files in chosen output folder.
        /// </summary>
        /// <param name="reportResult"></param>
        public void WriteResults(ReportResult reportResult)
        {
            if (settings == null)
                return;

            string folder;

            if (Path.IsPathFullyQualified(settings.Output))
                folder = settings.Output;
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string path = assembly.Location.Replace("LineCounter.dll", "");
                folder = $"{path}\\{settings.Output}";
            }

            folder = $"{folder}\\{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            StreamWriter? writer = null;
            try
            {
                string treeFilename = "tree.htm";
                if (settings.Language.ToLower() != "en-us")
                    treeFilename = $"tree-{settings.Language.ToLower()}.htm";
                string content = GetContentFor(treeFilename);
                string filename = $"{folder}\\tree.htm";

                content = content.Replace("[[TITLE]]", settings.Title).Replace("[[FOLDER]]", settings.SourceFolder).Replace("[[DATE_TIME]]", DateTime.Now.ToString("yyyy-MM-dd")).Replace("[[EXCEPTIONS]]", settings.Exceptions);
                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("scripts.tree.js");
                string extFolder = $"{folder}\\scripts";
                filename = $"{extFolder}\\tree.js";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                string _json = JsonSerializer.Serialize(reportResult);
                content = content.Replace("var _json = '';", $"var _json = '{_json}';").Replace("\\\\", "/");

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("scripts.stylemanager.js");
                filename = $"{extFolder}\\stylemanager.js";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("scripts.component.treeView.js");
                extFolder = $"{folder}\\scripts\\component";
                filename = $"{extFolder}\\treeView.js";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("scripts.component.constants.js");
                filename = $"{extFolder}\\constants.js";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                // CSS
                content = GetContentFor("css.tree.css");
                extFolder = $"{folder}\\css";
                filename = $"{extFolder}\\tree.css";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.site.css");
                filename = $"{extFolder}\\site.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                // Theme bright folder
                content = GetContentFor("css.bright.site-theme.css");
                extFolder = $"{folder}\\css\\bright";
                filename = $"{extFolder}\\site-theme.css";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.bright.site-ext.css");
                filename = $"{extFolder}\\site-ext.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.bright.tree.css");
                filename = $"{extFolder}\\tree.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                // A green theme
                content = GetContentFor("css.agreen.site-theme.css");
                extFolder = $"{folder}\\css\\agreen";
                filename = $"{extFolder}\\site-theme.css";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.agreen.site-ext.css");
                filename = $"{extFolder}\\site-ext.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.agreen.tree.css");
                filename = $"{extFolder}\\tree.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;
                // A blue theme
                content = GetContentFor("css.ablue.site-theme.css");
                extFolder = $"{folder}\\css\\ablue";
                filename = $"{extFolder}\\site-theme.css";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.ablue.site-ext.css");
                filename = $"{extFolder}\\site-ext.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.ablue.tree.css");
                filename = $"{extFolder}\\tree.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;
                // Dark theme
                content = GetContentFor("css.dark.site-theme.css");
                extFolder = $"{folder}\\css\\dark";
                filename = $"{extFolder}\\site-theme.css";

                if (!Directory.Exists(extFolder))
                    Directory.CreateDirectory(extFolder);

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.dark.site-ext.css");
                filename = $"{extFolder}\\site-ext.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;

                content = GetContentFor("css.dark.tree.css");
                filename = $"{extFolder}\\tree.css";

                writer = new StreamWriter(filename);
                writer.Write(content);
                writer.Close();
                writer.Dispose();
                writer = null;
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }
        /// <summary>
        /// An internal reference to settings object.
        /// </summary>
        private ISettings? settings = null;
        /// <summary>
        /// Get one of the internal assembly resources.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetContentFor(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"LineCounter.Resources.Template.{fileName}";

            Stream? stream = null;
            string result = "";

            try
            {
                stream = assembly.GetManifestResourceStream(resourceName);

                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    stream.Close();
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

            return result;
        }
    }
}
