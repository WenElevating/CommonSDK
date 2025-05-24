using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.AI.Model
{
    /// <summary>
    /// Provide a platform configuration entity
    /// </summary>
    internal class PlatformConfiguration
    {
        [NotNull]
        public string ModelId { get; set; }

        [NotNull]
        public string Endpoint { get; set; }

        [NotNull]
        public string ExectuePath { get; set; }
    }
}
