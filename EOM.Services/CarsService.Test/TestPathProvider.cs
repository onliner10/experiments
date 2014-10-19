using System.IO;
using Nancy;

namespace CarsService.Test
{
    public class TestPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}