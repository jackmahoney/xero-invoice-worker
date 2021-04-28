using System.Threading.Tasks;
using csharp.models;

namespace csharp.services.scoped
{
    public interface IInvoiceService
    {
        public Task ReconcileInvoiceEvent(string outputDirectory, Event item);
    }
}