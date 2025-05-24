using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Configuration.Enum;

namespace CommonSDK.AI.Configuration
{
    internal class ConfigurationTypeParser
    {
        private static readonly string span = ".";

        private static readonly string jsonSuffix = "json";

        private static readonly string xmlSuffix = "xml";

        private static readonly string xamlSuffix = "xaml";

        private static readonly string yamlSuffix = "yaml";

        /// <summary>
        /// Get configuration type
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">No suffix found</exception>
        public static ConfigurationType GetConfigurationTypeByPath(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            var pathList = path.Split(span);

            if (pathList.Length <= 1)
            {
                throw new ArgumentException("It can't find file suffix!");
            }

            var suffix = pathList[^1];

            if (suffix == jsonSuffix)
            {
                return ConfigurationType.JSON;
            }

            if (suffix == xmlSuffix)
            {
                return ConfigurationType.XML;
            }

            if (suffix == xamlSuffix)
            {
                return ConfigurationType.XAML;
            }

            if (suffix == yamlSuffix)
            {
                return ConfigurationType.YAML;
            }

            throw new NotSupportedException("Unable to find a matching suffix");
        }
    }
}
