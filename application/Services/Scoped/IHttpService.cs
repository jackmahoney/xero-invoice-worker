using System.Threading.Tasks;

namespace Application.Services.Scoped
{
    public interface IHttpService
    {
        public Task<string> GetStringAsync(string url);
    }
}