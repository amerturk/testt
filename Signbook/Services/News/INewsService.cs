using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Utilities;

namespace Signbook.Services.News
{
    public interface INewsService 
    {
        Task<ObservableRangeCollection<Models.News>> GetMainNewsCollection(string url);
    }
}
