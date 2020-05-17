using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Services.Api
{
    public interface IApiService
    {
        Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri,
            Dictionary<string, string> param = null)
            where  TBaseTransitional : BaseTransitional , new();
        Task<TBaseTransitional> GetAsync<TBaseTransitional>(Dictionary<string, string> headers, string uri,
    Dictionary<string, string> param = null)
    where TBaseTransitional : BaseTransitional, new();

        Task<TransitionalList<TBaseTransitional>> GetList<TBaseTransitional>(Dictionary<string, string> headers,
            string uri, Dictionary<string, string> param = null)
            where TBaseTransitional : BaseTransitional;

        Task<TResponse> Post<TResponse, TRequest>(TRequest entity,
            Dictionary<string, string> headers, string contentType, string uri) where TRequest : BaseTransitional
            where TResponse : class;

         Task<TResponse> PostWithOutContent<TResponse>(Dictionary<string, string> headers,
            string contentType, string uri) where
            TResponse : class;

         Task<string> GetRedirect(Dictionary<string, string> headers, string uri,
             Dictionary<string, string> param = null);
    }
}
