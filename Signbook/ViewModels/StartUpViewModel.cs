using Signbook.Models;
using Signbook.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Signbook.ViewModels
{
    public class StartUpViewModel
    {
        private IStartUpServices _startUpServices;
        public StartUpViewModel(IStartUpServices startUpServices)
        {
            _startUpServices = startUpServices;
        }
        /// <summary>
        /// return intro after make processing  
        /// </summary>
        /// <returns></returns>
        //public async Task<ObservableCollection<Intro>> GetIntros()
        //{
        //    //var intros = await _startUpServices.DownloadIntros();
        //    foreach (var intro in intros)
        //    {
        //        if (intros.Count - 1 == intros.IndexOf(intro))
        //        {
        //            intro.LastPage = true;
        //        }
        //    }
        //    return intros;
        //}
    }

}
