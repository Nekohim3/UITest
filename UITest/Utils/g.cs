using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITest.Utils
{
    public static class g
    {
        public static PageManager.PageManager PageManager { get; }

        static g()
        {
            PageManager = new PageManager.PageManager();
        }
    }
}
