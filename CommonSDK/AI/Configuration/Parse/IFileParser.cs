using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Model;

namespace CommonSDK.AI.Configuration.Parse
{
    internal interface IFileParser
    {
        PlatformConfiguration Parse(string path);
    }
}
