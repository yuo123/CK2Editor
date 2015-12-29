using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class TestsReference
    {
        internal static readonly string MIN_TEST_PATH = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Min_Test_Save.ck2");
        internal static readonly string TEST_PATH = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Test_Save.ck2");
        internal static readonly string FULL_TEST_PATH = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\CK2Editor\Full_Test_Save.ck2");

        internal const string FORMAT_PATH = @"Formats\CK2Save.xml";
    }
}
