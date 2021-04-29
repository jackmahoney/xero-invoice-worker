using System.Threading.Tasks;
using Application.Models;
using Application.Services.Scoped;

namespace Application.Services.Scoped.Impl
{
    public class TemplatingService: ITemplatingService
    {

        public string GenerateInvoiceContent(EventContent item)
        {
            // I would use Razor templating and css etc if I had time
            var template = @"
<html>
    <head><title>Invoice {0}</title></head>
    <body>
        <h1>Invoice {0}</h1>
        <h3>Status {1}</h2>
        <h3>Created {2}</h2>
    </body>
</html>
";
            return string.Format(template, item.InvoiceNumber, item.Status, item.CreatedDateUtc);
        }
    }
}