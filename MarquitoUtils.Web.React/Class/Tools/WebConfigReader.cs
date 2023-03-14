using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Web.React.Class.Config;
using MarquitoUtils.Web.React.Class.Enums;
using System.Reflection;
using System.Xml.Linq;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class WebConfigReader
    {
        public static WebConfig readWebConfigDataFromFile(string configurationFilePath, Assembly assembly)
        {
            // Web config
            WebConfig config = new WebConfig();

            // Loop of each data of file
            XDocument configurationXml = XDocument.Load(configurationFilePath);
            foreach (XElement appNode in configurationXml.Descendants("Configuration"))
            {
                // The application name
                string originApp = appNode.Attribute("application").Value.Trim();
                // Loop of each component
                foreach (XElement configGroupNode in appNode.Descendants("Components"))
                {
                    // Loop of each background config
                    foreach (XElement backgroundNode in configGroupNode.Descendants("Background"))
                    {
                        // Loop of each config
                        foreach (XElement configNode in backgroundNode.Descendants("Config"))
                        {
                            string configTag = configNode.Attribute("tag").Value.Trim();
                            switch (configTag)
                            {
                                case "color":
                                    EnumColor color = EnumClass
                                        .GetEnumByName<EnumColor, EnumColorAttr>(configNode.Value.Trim());
                                    config.DefaultBackgroundColor = color;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    // Loop of each border config
                    foreach (XElement backgroundNode in configGroupNode.Descendants("Border"))
                    {
                        // Loop of each config
                        foreach (XElement configNode in backgroundNode.Descendants("Config"))
                        {
                            string configTag = configNode.Attribute("tag").Value.Trim();
                            switch (configTag)
                            {
                                case "color":
                                    EnumColor color = EnumClass
                                        .GetEnumByName<EnumColor, EnumColorAttr>(configNode.Value.Trim());
                                    config.DefaultBorderColor = color;
                                    break;
                                case "style":
                                    config.DefaultBorderStyle = configNode.Value.Trim();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    // Loop of each font config
                    foreach (XElement backgroundNode in configGroupNode.Descendants("Font"))
                    {
                        // Loop of each config
                        foreach (XElement configNode in backgroundNode.Descendants("Config"))
                        {
                            string configTag = configNode.Attribute("tag").Value.Trim();
                            switch (configTag)
                            {
                                case "color":
                                    EnumColor color = EnumClass
                                        .GetEnumByName<EnumColor, EnumColorAttr>(configNode.Value.Trim());
                                    config.DefaultFontColor = color;
                                    break;
                                case "size":
                                    EnumSize size = EnumSize.GetSizeByName(configNode.Value.Trim());
                                    config.DefaultFontSize = size;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return config;
        }
    }
}
