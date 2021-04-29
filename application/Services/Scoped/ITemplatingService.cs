using System.Threading.Tasks;
using Application.Models;

namespace Application.Services.Scoped
{
    public interface ITemplatingService
    {

        public string GenerateInvoiceContent(EventContent item);
    }
}