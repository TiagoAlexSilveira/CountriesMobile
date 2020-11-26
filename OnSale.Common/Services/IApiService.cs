using OnSale.Common.Responses;
using System.Threading.Tasks;

namespace OnSale.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetCountries(string urlBase, string controller);
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

    }
}
