using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp.services.scoped
{
    public interface IRunner
    {
        public Task<int?> Process(Uri inputUrl, string outputDir, int? lastId);
    }
}