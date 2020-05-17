using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Signbook.Models;
using Signbook.Transitions;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Api;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;

namespace Signbook.Services.News
{
    public class NewsService : INewsService
    {
        private IApiService _apiService;
        private object _sqliteService;
        private IConnectivity _connectivity;

        public NewsService(IApiService apiService, IConnectivity connectivity)
        {
            _apiService = apiService;
            _connectivity = connectivity;
        }

        public async Task<ObservableRangeCollection<Models.News>> GetMainNewsCollection(string url)
        {
            try
            {
                if (_connectivity.IsConnected)
                {
                    var client = new WebClient();
                    var content = await client.DownloadStringTaskAsync(url);
                    var newString = string.Join(" ", Regex.Split(content, @"(?:\r\n|\n|\r)"));
                    //int xxxx = newString.Length;
                    //var xxxxX = newString.Substring(1, xxxx - 3);
                    var transitionalList = JsonConvert.DeserializeObject<TransitionalList<NewsApi>>(newString);
                    var list = transitionalList.ToModel<Models.News>();
                    if (list == null || list.Count <= 0)
                        return new ObservableRangeCollection<Models.News>();
                    return list;
                }
                else
                {
                    return new ObservableRangeCollection<Models.News>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
