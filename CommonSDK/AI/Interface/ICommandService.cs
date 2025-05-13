using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.AI.Interface
{
    public interface ICommandService
    {
        public string GetRunModelCommand(string executeablePath, string modelId);

        public string GetStopModelCommand(string executeablePath, string modelId);
    }
}
