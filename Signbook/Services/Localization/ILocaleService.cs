using System;
using System.Collections.Generic;
using System.Text;

namespace Signbook.Services.Localization
{
    public interface ILocaleService
    {
        void ChangeLocaleConfiguration(string language);
    }
}
