using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient = new HttpClient(new HttpClientHandler()
        {
            ClientCertificateOptions = ClientCertificateOption.Automatic
        });
        private readonly JsonSerializer _serializer = new JsonSerializer();
        public ApiService()
        {
        }
        public async Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri,
             Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional, new()
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                var fullUri = GetFullUrl(uri, param);
                responseMessage = await _httpClient.GetAsync(fullUri);
                responseMessage.EnsureSuccessStatusCode();

                using (var stream = await responseMessage.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<TBaseTransitional>(json);
                }
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                responseMessage?.Dispose();
                return new TBaseTransitional();
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
        public async Task<TBaseTransitional> GetAsync<TBaseTransitional>(Dictionary<string, string> headers, string uri,
     Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional, new()
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(200);
                client.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                var fullUri = GetFullUrl(uri, param);
                responseMessage = await client.GetAsync(fullUri);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<TBaseTransitional>(jsonString);
                if (jsonObject == null)
                    return (TBaseTransitional)Activator.CreateInstance(typeof(TBaseTransitional));
                return jsonObject;
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                responseMessage?.Dispose();
                return new TBaseTransitional();
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
        public async Task<TransitionalList<TBaseTransitional>> GetList<TBaseTransitional>(
            Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
            where TBaseTransitional : BaseTransitional
        {
            HttpResponseMessage responseMessage = null;
            try
            {

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                var fullUri = GetFullUrl(uri, param);

                responseMessage = await _httpClient.GetAsync(fullUri);

                responseMessage.EnsureSuccessStatusCode();

                using (var stream = await responseMessage.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var list = _serializer.Deserialize<TransitionalList<TBaseTransitional>>(json);
                    return list;
                }
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                responseMessage?.Dispose();
                return new TransitionalList<TBaseTransitional>();
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
        public async Task<TResponse> Post<TResponse, TRequest>(TRequest entity,
            Dictionary<string, string> headers, string contentType, string uri) where TRequest : BaseTransitional
            where
            TResponse : class
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                var client = new HttpClient();
                if (headers != null)
                {
                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                StringContent dateContent = null;
                if (entity != null)
                {
                    var json = JsonConvert.SerializeObject(entity);
                    dateContent = new StringContent(json, Encoding.UTF8, contentType);
                }

                responseMessage = await client.PostAsync(uri, dateContent);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonString);
                return jsonObject;
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                responseMessage?.Dispose();
                throw;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
        public async Task<TResponse> PostWithOutContent<TResponse>(Dictionary<string, string> headers, string contentType, string uri) where
            TResponse : class
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                var client = new HttpClient();
                if (headers != null)
                {
                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                responseMessage = await client.PostAsync(uri, null);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonString);
                return jsonObject;
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                responseMessage?.Dispose();
                throw;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
        public string GetFullUrl(string uri, Dictionary<string, string> param)
        {
            StringBuilder query = new StringBuilder().Append("?");
            if (param == null || param.Count <= 0)
            {
                return !string.IsNullOrEmpty(uri) ? uri : "";
            }

            KeyValuePair<string, string> lastElement = param.ElementAt(param.Count - 1);
            foreach (KeyValuePair<string, string> item in param)
            {
                bool flag = item.Key == lastElement.Key;
                if (!flag)
                {
                    query.Append(item.Key + "=" + item.Value).Append("&");
                }
                else
                {
                    query.Append(item.Key + "=" + item.Value);
                }
            }
            return $"{uri}/{query}";
        }
        public async Task<string> GetRedirect(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
        {
            HttpResponseMessage responseMessage = null;
            try
            {

                var httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };

                var client = new HttpClient(httpClientHandler);

                client.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                var fullUri = GetFullUrl(uri, param);
                responseMessage = await client.GetAsync(fullUri);
                var stringResponse = await responseMessage.Content.ReadAsStringAsync();

                return stringResponse;
            }
            catch (Exception exception)
            {
                Crashes.TrackError((exception));
                throw;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
    }
}
