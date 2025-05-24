using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.Util;

namespace CommonSDK.Service
{
    internal class TerminalServiceFactory
    {
        public static ITerminalService Create()
        {
            var platform = SystemUtil.GetPlatform();
            if (platform == OSPlatform.Windows)
            {
                return new WindowsTerminalService();
            }

            throw new NotSupportedException("The platform is not support terminal service!");
        }
    }
}
