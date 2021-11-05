using System;
using System.Diagnostics;

namespace AccountManager
{
    public class Tests
    {
        internal static void RunTests()
        {
            test_shortenString();
        }

        private static void test_shortenString()
        {
            Debug.Assert(Program.ShortenString("0123456789", 7) == "0123...", "A 10 char string truncated to 7 chars should be shortened.");
            Debug.Assert(Program.ShortenString("0123456789", 1) == "0", "A 10 char string truncated to 1 chars should be shortened.");
            Debug.Assert(Program.ShortenString("0123456789", -1) == "", "A 10 char string truncated to 1 chars should be shortened.");

        }
    }
}
