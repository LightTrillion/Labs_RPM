using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Core;

namespace labs_RPM.Proxy
{
    public class RealImage : IImage
    {
        private string _filename;
        public RealImage(string filename)
        {
            _filename = filename;
            LoadFromDisk();
        }
        private void LoadFromDisk() => Console.WriteLine($"[System] Loading heavy image: {_filename}...");
        public void Display() => Console.WriteLine($"Displaying {_filename}");
    }
}
