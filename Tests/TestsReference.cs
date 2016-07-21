using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Tests
{
    class TestsReference
    {
        internal static readonly string BASE_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..");
        internal static readonly string MIN_TEST_PATH = Path.Combine(BASE_PATH, "Min_Test_Save.ck2");
        internal static readonly string TEST_PATH = Path.Combine(BASE_PATH, "Test_Save.ck2");
        internal static readonly string FULL_TEST_PATH = Path.Combine(BASE_PATH, "Full_Test_Save.ck2");

        internal const string FORMAT_PATH = @"Formats\CK2Save.xml";
    }
}
