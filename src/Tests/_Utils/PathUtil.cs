using System;
using System.IO;
using System.Reflection;

namespace ProcentCqrs.Tests._Utils
{
    public class PathUtil
    {
        public static string GetAbsoluteTestLocation()
        {
            // some test runner might shadow copy the tested assemblies, making Assembly.Location unsuitable
            // for the purpose of finding current assembly's physical location on disk
            string codeBaseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            return new Uri(codeBaseLocation).AbsolutePath;
        }

        public static string GetTestFilesLocation()
        {
            return Path.Combine(GetAbsoluteTestLocation(), "TestFiles");
        }
    }
}