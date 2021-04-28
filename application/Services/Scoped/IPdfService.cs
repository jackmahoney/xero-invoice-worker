using System;

namespace csharp.services.scoped
{
    public interface IPdfService
    {

        public void WritePdf(string html, string path);
    }
}