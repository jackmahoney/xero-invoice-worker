using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp.services.scoped
{
    public interface IRunner
    {
        public Task Process(Uri inputUrl, string outputDir);
    }
}