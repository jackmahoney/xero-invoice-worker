using System.Threading.Tasks;
using Application.Models;

namespace Application.Services.Scoped
{
    public interface IInvoiceService
    {
        public Task<string> ReconcileInvoiceEvent(string outputDirectory, Event item);
    }
}