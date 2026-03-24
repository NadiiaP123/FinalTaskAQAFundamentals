using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Config
{
    public sealed class RunSettings
    {
        public required bool Parallel { get; set; }
        public required int Threads { get; set; }
    }
}
