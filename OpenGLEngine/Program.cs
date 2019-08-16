using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenGLEngine
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            new Window().Run(60);
        }
    }
}
