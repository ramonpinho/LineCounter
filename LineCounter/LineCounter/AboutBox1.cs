using LineCounter.Controls;
using LineCounter.Models.lang;
using System.Reflection;

namespace LineCounter
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();

            if(CounterControl.ResourceManager != null && CounterControl.ResourceManager.AboutBox1 != null)
            {
                Text = $"{CounterControl.ResourceManager.AboutBox1.Title} {AssemblyTitle}";
                labelProductName.Text = AssemblyProduct;
                labelVersion.Text = $"{CounterControl.ResourceManager.AboutBox1.Version} {AssemblyVersion}";
                labelCopyright.Text = AssemblyCopyright;
                labelCompanyName.Text = AssemblyCompany;
                textBoxDescription.Text = CounterControl.ResourceManager.AboutBox1.Description;
            }

        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }

                string assemblyTitle = "";
                Assembly? assembly = Assembly.GetExecutingAssembly();
                if (assembly != null)
                {
                    assemblyTitle = Path.GetFileNameWithoutExtension(assembly.Location);
                }

                return assemblyTitle;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                string assemblyVersion = "";
                Assembly? assembly = Assembly.GetExecutingAssembly();
                AssemblyName? assemblyName = null;
                if (assembly != null)
                    assemblyName = assembly.GetName();
                Version? version = null;
                if(assemblyName != null)
                    version = assemblyName.Version;
                if(version != null)
                    assemblyVersion = version.ToString();
                return assemblyVersion;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
