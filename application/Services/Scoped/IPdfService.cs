using System;

namespace Application.Services.Scoped
{
    public interface IPdfService
    {

        public void WritePdf(string html, string path);
    }
}