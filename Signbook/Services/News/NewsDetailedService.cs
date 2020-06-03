using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Signbook.Models;
using Signbook.Transitions;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Api;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;

namespace Signbook.Services.NewsDetailed
{
    public class NewsDetailedService : INewsDetailedService
    {
        private IApiService _apiService;
        private object _sqliteService;
        private IConnectivity _connectivity;

        public NewsDetailedService(IApiService apiService, IConnectivity connectivity)
        {
            _apiService = apiService;
            _connectivity = connectivity;
        }

        public async Task<ObservableRangeCollection<Models.NewsDetailed>> GetMainNewsCollection(string url)
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
                    var transitionalList = JsonConvert.DeserializeObject<TransitionalList<NewsDetailedApi>>(newString);
                    var list = transitionalList.ToModel<Models.NewsDetailed>();
                    if (list == null || list.Count <= 0)
                        return new ObservableRangeCollection<Models.NewsDetailed>();
                    return list;
                }
                else
                {
                    return new ObservableRangeCollection<Models.NewsDetailed>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
