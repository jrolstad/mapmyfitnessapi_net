using System.IO;
using System.Reflection;

namespace mapmyfitnessapi_sdk.unit.tests.helpers
{
    public class TestFileReader
    {
        public static string ReadFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(fileName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();

                return result;
            }
        }
    }
}