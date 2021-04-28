using System.IO;
using System.Threading.Tasks;

namespace csharp.services.scoped
{
    public interface IFileService
    {
        public bool CreateDirectoryIfNotExists(string path);
        public bool DeleteFileIfExists(string fileName);
    }
}