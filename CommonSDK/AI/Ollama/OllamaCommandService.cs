using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Interface;

namespace CommonSDK.AI.Ollama
{
    public class OllamaCommandService : ICommandService
    {
        public string GetRunModelCommand(string executeablePath, string modelId)
        {
            if (string.IsNullOrEmpty(executeablePath))
            {
                throw new FileNotFoundException("The ollama exe is not find!");
            }
            
            string command = $"{executeablePath} run {modelId}";
            Debug.WriteLine($"[OllamaCommandService] -> {command}");

            return command;
        }

        public string GetStopModelCommand(string executeablePath, string modelId)
        {
            if (string.IsNullOrEmpty(executeablePath))
            {
                throw new FileNotFoundException("The ollama exe is not find!");
            }

            string command = $"{executeablePath} stop {modelId}";
            Debug.WriteLine($"[OllamaCommandService] -> {command}");

            return command;
        }
    }
}
