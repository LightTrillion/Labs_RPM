using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM.Bridge
{
    public class VectorRenderer : IRenderer
    {
        public void RenderShape(string shapeName) => Console.WriteLine($"Drawing {shapeName} as Vector.");
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderShape(string shapeName) => Console.WriteLine($"Drawing {shapeName} as pixels (Raster).");
    }
}
