using System.IO;

namespace csharp.services.scoped.impl
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