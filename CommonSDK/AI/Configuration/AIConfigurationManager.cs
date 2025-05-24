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
    internal abstract class AIConfigurationManager
    {
        protected ConfigurationType _configurationType;

        public string FilePath { get; set; }


        protected AIConfigurationManager(string filePath)
        {
            FilePath = filePath;     
        }

        /// <summary>
        /// Provides a way to load the configuration in a Json file
        /// </summary>
        /// <param name="path"></param>
        public abstract PlatformConfiguration Initialize(string path, ConfigurationType configurationType);
    }
}
