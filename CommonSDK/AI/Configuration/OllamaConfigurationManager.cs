using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Configuration.Enum;
using CommonSDK.AI.Configuration.Parse;
using CommonSDK.AI.Model;

namespace CommonSDK.AI.Configuration
{
    internal class OllamaConfigurationManager : AIConfigurationManager
    {
        private IFileParser? fileParser;

        public ConfigurationType CurrentConfigurationType => _configurationType;

        public PlatformConfiguration Configuration { get; set; }

        public OllamaConfigurationManager(string path) : base(path)
        {
            _configurationType = ConfigurationTypeParser.GetConfigurationTypeByPath(path);

            Configuration = Initialize(path, _configurationType);
        }

        public override PlatformConfiguration Initialize(string path, ConfigurationType configurationType)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            return InitializeConfigurationByType(path, configurationType);
        }

        internal PlatformConfiguration InitializeConfigurationByType(string path, ConfigurationType configurationType) 
        {
            fileParser = ConfigurationParserFactory.Create(configurationType);

            return fileParser.Parse(path);
        }
    }
}
