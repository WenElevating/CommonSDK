using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Configuration.Enum;
using CommonSDK.AI.Configuration.Parse;

namespace CommonSDK.AI.Configuration
{
    internal class ConfigurationParserFactory
    {
        /// <summary>
        /// Create file parser by configuration type
        /// </summary>
        /// <param name="configurationType"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">This means that this file type is not currently supported</exception>
        public static IFileParser Create(ConfigurationType configurationType)
        {
            switch (configurationType)
            {
                case ConfigurationType.JSON:
                    return new JsonFileParser();
                case ConfigurationType.XAML:
                    throw new NotSupportedException("It don't support configuration type");
                case ConfigurationType.XML:
                    throw new NotSupportedException("It don't support configuration type");
                case ConfigurationType.YAML:
                    throw new NotSupportedException("It don't support configuration type");
                default:
                    throw new NotSupportedException("It don't support configuration type");
            }
            throw new NotSupportedException("It don't support configuration type");
        }
    }
}
