using System.IO;
using System.Threading.Tasks;

namespace Application.Services.Scoped
{
    public interface IFileService
    {
        public bool CreateDirectoryIfNotExists(string path);
        public bool DeleteFileIfExists(string fileName);
    }
}