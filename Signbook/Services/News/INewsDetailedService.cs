using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Utilities;

namespace Signbook.Services.NewsDetailed
{
    public interface INewsDetailedService
    {
        Task<ObservableRangeCollection<Models.NewsDetailed>> GetMainNewsCollection(string url);
    }
}
