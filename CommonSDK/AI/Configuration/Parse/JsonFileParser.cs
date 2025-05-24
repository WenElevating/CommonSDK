using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Model;
using Newtonsoft.Json;

namespace CommonSDK.AI.Configuration.Parse
{
    internal class JsonFileParser : IFileParser
    {
        public PlatformConfiguration Parse(string path)
        {
            ArgumentNullException.ThrowIfNull(path, "path");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{path} is not exist!");
            }

            using StreamReader streamReader = new(path);
            string configurationJson = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<PlatformConfiguration>(configurationJson) ?? throw new JsonException("Parse platform configuration failed!");
        }
    }
}
