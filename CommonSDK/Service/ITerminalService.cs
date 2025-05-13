using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.Service
{
    public interface ITerminalService
    {
        /// <summary>
        /// Aysnc run special exe file
        /// </summary>
        /// <param name="executableFilePath">executable file path</param>
        /// <returns>execute result</returns>
        public Task<bool> ExecuteCommandAsync(string executableFilePath);

    }
}
