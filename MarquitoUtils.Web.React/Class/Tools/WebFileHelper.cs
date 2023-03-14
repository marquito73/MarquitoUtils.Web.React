using MarquitoUtils.Web.React.Class.Tools;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Components.General;
using MarquitoUtils.Web.React.Class.Config;
using MarquitoUtils.Web.React.Class.Enums;
using System.Reflection;
using System.Text;
using CustomFile = MarquitoUtils.Main.Class.Entities.File.CustomFile;
using MarquitoUtils.Main.Class.Enums;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebFileHelper
    {
        private static string STATIC_DIR = "StaticWebComponents";

        private static string RC = "\n";

        private static string TAB = "\t";

        private static List<string> STATIC_FILE_EXT_AS_TEXT = 
            new List<string> { "js", "css", "svg", "txt" };

        private static string COLOR_FILENAME = "Colors";

        private static string COLOR_NAMESPACE = "MarquitoUtils.Web.css.Utility";
        private static List<string> EXCLUDED_FILES = new List<string> { ".Files.Web.Config." };

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public static string GetExecutingLocationPath(Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }

        public static WebConfig ReadConfig()
        {
            return ReadConfig(Assembly.GetExecutingAssembly());
        }

        public static WebConfig ReadConfig(Assembly assembly)
        {
            return WebConfigReader.readWebConfigDataFromFile(@"D:\Users\TB\Visual\source\repos\MarquitoUtils.Web\MarquitoUtils.Web\Files\Web\Config\DefaultWebConfig.xml", 
                assembly);
        }

        /// <summary>
        /// Get list of static files
        /// </summary>
        /// <returns>List of static files</returns>
        public static List<CustomFile> GetStaticFiles()
        {
            List<CustomFile> staticFiles = new List<CustomFile>();

            // Assembly
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            foreach (string name in currentAssembly.GetManifestResourceNames())
            {
                if (name.Contains(".Files.Web.") && CheckIfFileNotInExcludedFiles(name))
                {
                    string nameSpace = GetWithoutFilename(name);
                    string filenameWithExt = GetFilename(name);
                    //staticFilesNames.Add(nameSpace, fileName);

                    // Filename
                    string filename = filenameWithExt.Split(".")[0];
                    // Extension
                    string extension = filenameWithExt.Split(".")[1];

                    object content = GetResourceContent(currentAssembly, nameSpace, filename, extension);
                    // Object represent filename with his data
                    CustomFile staticFile;

                    if (content.GetType().Equals(typeof(string))) {
                        staticFile = new CustomFile(filename, extension, nameSpace, 
                            Utils.GetAsString(content));
                    }
                    else
                    {
                        staticFile = new CustomFile(filename, extension, nameSpace, 
                            Utils.GetAsBytes(content));
                    }


                    //File staticFile = new File(filename, extension, nameSpace, content);
                    staticFile.RemoveStringFromNameSpace("Files.Web.");

                    staticFiles.Add(staticFile);
                }
            }

            // List of colors
            List<EnumColor> colorList = EnumClass.GetEnumList<EnumColor, EnumColorAttr>();
            // Add color file to static files
            staticFiles.Add(GetColorStaticFile(colorList));

            return staticFiles;
        }

        private static CustomFile GetColorStaticFile(List<EnumColor> colors)
        {
            StringBuilder sbColorCssFile = new StringBuilder();
            // Let's foreach color, and write the css for colors
            foreach (EnumColor color in colors)
            {
                // The color name
                sbColorCssFile.Append("/*").Append(color.Attr().CssColor).Append("*/").Append(RC);
                // The color
                sbColorCssFile.Append(".").Append(color.GetCssColor()).Append("{").Append(RC)
                    .Append(TAB).Append("color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // The color hover
                sbColorCssFile.Append(".").Append(color.GetCssHoverColor()).Append(":hover")
                    .Append("{").Append(RC)
                    .Append(TAB).Append("color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // The background color
                sbColorCssFile.Append(".").Append(color.GetCssBackgroundColor()).Append("{").Append(RC)
                    .Append(TAB).Append("background-color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // The background color
                sbColorCssFile.Append(".").Append(color.GetCssHoverBackgroundColor()).Append(":hover")
                    .Append("{").Append(RC)
                    .Append(TAB).Append("background-color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // The border color
                sbColorCssFile.Append(".").Append(color.GetCssBorderColor()).Append("{").Append(RC)
                    .Append(TAB).Append("border-color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // The border color
                sbColorCssFile.Append(".").Append(color.GetCssHoverBorderColor()).Append(":hover")
                    .Append("{").Append(RC)
                    .Append(TAB).Append("border-color: ").Append(color.GetCssRgbaColor())
                    .Append(" !important;").Append(RC).Append("}").Append(RC);
                // Put a empty line
                sbColorCssFile.Append(RC);
            }

            CustomFile staticFile = new CustomFile(COLOR_FILENAME, "css", COLOR_NAMESPACE, 
                sbColorCssFile.ToString());

            return staticFile;
        }

        /// <summary>
        /// Get resource file content from an assembly
        /// </summary>
        /// <param name="assembly">The assembly contain the file</param>
        /// <param name="nameSpace">The namespace of the file</param>
        /// <param name="filename">The filename</param>
        /// <param name="extension">The filename</param>
        /// <returns>The content of the file</returns>
        private static object GetResourceContent(Assembly assembly, string nameSpace, 
            string filename, string extension)
        {
            string resourceName = nameSpace + "." + filename + "." + extension;
            object resource = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {

                if (STATIC_FILE_EXT_AS_TEXT.Contains(extension))
                {
                    // We can read file as text
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        resource = reader.ReadToEnd();
                    }
                }
                else
                {
                    resource = Utils.ReadAllBytes(stream);
                }
            }
            return resource;
        }

        private static string GetFilename(string namespaceAndName)
        {
            string[] names = namespaceAndName.Split('.');

            int count = names.Length;

            return names[count - 2] + "." + names[count - 1];
        }

        private static string GetWithoutFilename(string namespaceAndName)
        {
            string filename = GetFilename(namespaceAndName);

            return namespaceAndName.Replace("." + filename, "");
        }

        public static string WriteWebTempFiles(List<CustomFile> staticsFiles)
        {
            // Create temp folder for store statics files
            string tempDirPath = Path.Combine(Directory.GetCurrentDirectory(), STATIC_DIR);

            Directory.CreateDirectory(tempDirPath);

            foreach (CustomFile staticFile in staticsFiles)
            {
                WriteWebTempFile(tempDirPath, staticFile);
            }

            return tempDirPath;
        }

        private static void WriteWebTempFile(string tempDirPath, CustomFile staticFile)
        {
            // Construct entire file path
            StringBuilder sbTempPathDir = new StringBuilder();
            sbTempPathDir.Append(Path.Combine(
                staticFile.GetNameSpaceAsCombinedPath(tempDirPath), staticFile.FileName))
                .Append(".").Append(staticFile.Extension);
            //string staticFilePath = Path.Combine(tempDirPath, staticFile.FileName) + "." + staticFile.Extension;
            // Create the path if not exist
            (new FileInfo(sbTempPathDir.ToString())).Directory.Create();
            // Create the file
            using (FileStream stream = System.IO.File.Create(sbTempPathDir.ToString()))
            {
                if (STATIC_FILE_EXT_AS_TEXT.Contains(staticFile.Extension))
                {
                    // We can write file as text
                    byte[] fileContent = new UTF8Encoding(true).GetBytes(staticFile.Content);
                    stream.Write(fileContent, 0, fileContent.Length);
                } 
                else
                {
                    // We need to write file as binary
                    using (var writer = new BinaryWriter(stream, Encoding.Unicode, false))
                    {
                        writer.Write(staticFile.BinaryContent);
                    }
                }
            }
        }

        // TODO Récupérer les fichier du cache, préalablement stockés, pour pouvoir les ajouter dynamiquement
        public static WebFileImport GetMainWebFileImport(EnumIcon loadingIcon, string containerIdForLoading)
        {
            WebFileImport webFileImport = new WebFileImport();

            List<CustomFile> staticsFiles = GetStaticFiles();
            // Import css files
            staticsFiles.Where(file => file.Extension.Equals("css"))
                .ToList()
                .ForEach(file => webFileImport.ImportCss.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));
            //webFileImport.ImportCss.Add("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css");
            // Import js files

            // Import jquery first and jquery-ui second and icheck third
            staticsFiles.Where(file => file.Extension.Equals("js") && file.FileName.Equals("jquery"))
                .ToList()
                .ForEach(file => webFileImport.ImportJs.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));
            staticsFiles.Where(file => file.Extension.Equals("js") && file.FileName.Equals("jquery-ui"))
                .ToList()
                .ForEach(file => webFileImport.ImportJs.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));
            staticsFiles.Where(file => file.Extension.Equals("js") && file.FileName.Equals("icheck"))
                .ToList()
                .ForEach(file => webFileImport.ImportJs.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));
            staticsFiles.Where(file => file.Extension.Equals("js") && file.FileName.Equals("Component"))
                .ToList()
                .ForEach(file => webFileImport.ImportJs.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));

            // Import others js files
            staticsFiles.Where(file => file.Extension.Equals("js") && 
            !(new List<string> { "jquery", "jquery-ui", "jquery-ui.min", "icheck", "Component" }.Contains(file.FileName)))
                .ToList()
                .ForEach(file => webFileImport.ImportJs.Add(Path.Combine(STATIC_DIR, file.GetCompleteNameSpaceAsPathName())));

            // Main js functions
            webFileImport.JsFunctions.Add("document.ajax = new oAjax();");
            webFileImport.JsFunctions.Add("document.action = new oAction();");
            webFileImport.JsFunctions.Add("document.frag = new oFrag();");
            webFileImport.JsFunctions.Add("document.loading = new oLoading();");
            webFileImport.JsFunctions.Add(WebUtils.GetJavaScriptFunction("document.loading.init",
                containerIdForLoading, loadingIcon.Attr().IconCss) + ";");

            return webFileImport;
        }

        private static bool CheckIfFileNotInExcludedFiles(string fileName)
        {
            bool fileNotInExcludedFiles = true;

            foreach (string excludedFileName in EXCLUDED_FILES)
            {
                if (fileName.Contains(excludedFileName)) {
                    fileNotInExcludedFiles = false;
                }
            }

            return fileNotInExcludedFiles;
        }
    }
}
