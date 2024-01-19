using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class Services
    {
        public static string GetDirectoryToFileFromDataFolder(string fileName)
        {
            var pathOrigin = Directory.GetCurrentDirectory();
            var index = pathOrigin.IndexOf("Solution1");
            var pathPart = pathOrigin.Substring(0, index);
            var path = $@"{pathPart}\Solution1\ProjectClock.BussinesLogic\Data\{fileName}";
            return path;
        }
    }
}
