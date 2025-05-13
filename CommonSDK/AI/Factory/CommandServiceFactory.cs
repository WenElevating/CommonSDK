using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Enum;
using CommonSDK.AI.Interface;
using CommonSDK.AI.Ollama;

namespace CommonSDK.AI.Factory
{
    public class CommandServiceFactory
    {
        public static ICommandService Create(AIPlatform platform)
        {
            switch (platform)
            {
                case AIPlatform.Ollama:
                    return new OllamaCommandService();
                case AIPlatform.OpenAi:
                    break;
            }

            throw new NotSupportedException("The Ai Platform is not support!");
        }
    }
}
