using System.IO;
using Application.Services.Scoped;

namespace Application.Services.Scoped.Impl
{
    public class FileService: IFileService
    {
        
        public bool CreateDirectoryIfNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }
            Directory.CreateDirectory(path);
            return true;
        }

        public bool DeleteFileIfExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            File.Delete(fileName);
            return true;

        }
    }
}