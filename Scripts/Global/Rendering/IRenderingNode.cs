using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rendering
{
    public interface IRenderingNode
    {
        void Execute();
        void Clear();
    }
}
