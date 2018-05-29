using BookCatalog.Bootstrap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Bootstrap
{
    public class ApplicationMapper
    {
        public static void Init()
        {
            Setup.RegisterTypes(Setup.GetUnityConfig());
        }
    }
}
