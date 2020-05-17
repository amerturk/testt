using Signbook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Services.Api;

namespace Signbook.Services
{
    public class StartUpServices : IStartUpServices
    {
        private IApiService _apiServices;
        public StartUpServices(IApiService apiServices)
        {
            _apiServices = apiServices;
        }
        /// <summary>
        /// get intro data from data source(internet, cache .. etc) and change lastpage property according to index
        /// </summary>
        /// <returns></returns>
//        public async Task<ObservableCollection<Intro>> DownloadIntros()
//        {
//            #region StaticData
//            /*             
//    var intros = new ObservableCollection<Intro>()
//    {
//        new Intro
//        {
//            TitleEnglish = "Namaa le mashro3k",
//            TitleArabic = "نماء لمشروعك",
//            DescriptionArabic = @"نماء لك. حلق في عالم النجاح، مع مركز نماء لتمكين الشباب ورواد الأعمال الاجتماعيين؛ عند استفادتك من مجموعة الخدمات المتنوعة للأفراد. كتنمية وتطوير قدراتك الشخصية، بالإضافة إلى تحسين نمط حياتك؛ عن طريق تعلم المهارات الحياتية، عبر الورش التدريبية، التي تؤهلك لأن تكون ملهماً لغيرك، ومرشداً للآخرين، وفرداً فعالاً في المجتمع؛ ",
//            DescriptionEnglish = @"What is this service? Obtain necessary experience and management shills, and get qualified to carry out successful business projects, through our pachage of training and consultation services in collaboration with local and international entities, using curriculums recognized by the International Labor Organization (ILO). How is it delivered? Training workshops for entrepreneurs, such as: Generate Your Business Idea. Start Your Business. ",
//            Image = "http://img0bm.b8cdn.com/images/logo/99/404099_logo_1463390200_n.png",
//            URL = "https://www.youtube.com/watch?v=GEd9Wynu5YU",
//            Background = "startupbackground1.png"},
//        new Intro
//        {
//            TitleEnglish = "Kon sharek namaa",
//            TitleArabic = "كن شريك نماء",
//            DescriptionArabic = @"نماء لك. حلق في عالم النجاح، مع مركز نماء لتمكين الشباب ورواد الأعمال الاجتماعيين؛ عند استفادتك من مجموعة الخدمات المتنوعة للأفراد. كتنمية وتطوير قدراتك الشخصية، بالإضافة إلى تحسين نمط حياتك؛ عن طريق تعلم المهارات الحياتية، عبر الورش التدريبية، التي تؤهلك لأن تكون ملهماً لغيرك، ومرشداً للآخرين، وفرداً فعالاً في المجتمع؛ ",
//            DescriptionEnglish = @"What is this service? Obtain necessary experience and management shills, and get qualified to carry out successful business projects, through our pachage of training and consultation services in collaboration with local and international entities, using curriculums recognized by the International Labor Organization (ILO). How is it delivered? Training workshops for entrepreneurs, such as: Generate Your Business Idea. Start Your Business.",
//            Image = "http://img0bm.b8cdn.com/images/logo/99/404099_logo_1463390200_n.png",
//            URL = "https://www.youtube.com/watch?v=GEd9Wynu5YU",
//            Background = "startupbackground2.png"
//        },
//        new Intro
//        {
//            TitleEnglish = "Namaa lak",
//            TitleArabic = "نماء لك",
//            DescriptionArabic = @"نماء لك. حلق في عالم النجاح، مع مركز نماء لتمكين الشباب ورواد الأعمال الاجتماعيين؛ عند استفادتك من مجموعة الخدمات المتنوعة للأفراد. كتنمية وتطوير قدراتك الشخصية، بالإضافة إلى تحسين نمط حياتك؛ عن طريق تعلم المهارات الحياتية، عبر الورش التدريبية، التي تؤهلك لأن تكون ملهماً لغيرك، ومرشداً للآخرين، وفرداً فعالاً في المجتمع؛ ",
//            DescriptionEnglish = @"What is this service? Obtain necessary experience and management shills, and get qualified to carry out successful business projects, through our pachage of training and consultation services in collaboration with local and international entities, using curriculums recognized by the International Labor Organization (ILO). How is it delivered? Training workshops for entrepreneurs, such as: Generate Your Business Idea. Start Your Business.",
//            Image = "http://img0bm.b8cdn.com/images/logo/99/404099_logo_1463390200_n.png",
//            URL = "https://www.youtube.com/watch?v=GEd9Wynu5YU",
//            Background = "startupbackground3.png"
//        }
//};
//* */

//            #endregion            return intros;
//            #region OldMapping
//            //var jsonObject = await _apiServices.APIGet(null, Urls.Intros, null);
//            //var data = jsonObject["data"];
//            //var included = jsonObject["included"];
//            //for (int i = 0; i < data.Count(); i++)
//            //{
//            //    var item = data[i];
//            //    var attributes = item["attributes"];
//            //    var intro = new Intro();
//            //    intro.TitleEnglish = attributes["title"].Value<string>();
//            //    intro.TitleArabic = attributes["ara_ttl"].Value<string>();
//            //    var arabody = attributes["ara_body"];
//            //    intro.DescriptionArabic = arabody != null && arabody["value"] != null ? arabody["value"].ToString():String.Empty;
//            //    var engbody = attributes["eng_body"];
//            //    intro.DescriptionEnglish = engbody != null && engbody["value"] != null ? engbody["value"].ToString() : String.Empty;
//            //    if (included != null)
//            //    {
//            //        var itemImage = included[i];
//            //        var imgAttributes = itemImage["attributes"];
//            //        var links = imgAttributes != null && imgAttributes["url"] != null ? imgAttributes["url"].ToString() : String.Empty;
//            //        intro.Image = links;
//            //    }
//            //    intro.BackgroundImage = $"startupbackground{i+1}.png";
//            //    intro.URL = "https://www.youtube.com/watch?v=GEd9Wynu5YU";
//            //    intros.Add(intro);
//            //} 
//            #endregion
//            ObservableCollection<Intro> intros = new ObservableCollection<Intro>();
//            var data = await _apiServices.APIGet(null, Urls.Intros, null);
//            for (int i = 0; i < data.Count; i++)
//            {
//                try
//                {
//                    var item = data[i];
//                    var intro = new Intro();
//                    intro.TitleArabic = item?["ara_ttl"]?.Value<string>();
//                    intro.TitleEnglish = item?["eng_ttl"]?.Value<string>();
//                    intro.DescriptionArabic = item?["ara_desc"]?.Value<string>();
//                    intro.DescriptionEnglish = item?["eng_desc"]?.Value<string>();
//                    intro.URLArabic = item?["ara_vid_url"]?.Value<string>();
//                    intro.URLEnglish = item?["eng_vid_url"]?.Value<string>();
//                    intro.ImageArabic = item?["ara_desc"]?.Value<string>();
//                    intro.ImageEnglish = Settings.BaseAddress + item?["eng_img"]?.Value<string>();
//                    intro.ImageArabic = Settings.BaseAddress + item?["ara_img"]?.Value<string>();
//                    intro.BackgroundImage = $"startupbackground{i + 1}.png";
//                    intros.Add(intro);
//                }
//                catch (Exception exception)
//                {
//                    Console.WriteLine(exception.Message);
//                    var properties = new Dictionary<string, string>
//                           {
//                             { "ApiService", "ApiServiceError" }
//                       };
//                    Crashes.TrackError(exception, properties);
//                    return new ObservableCollection<Intro>();
//                }
//            }

//            await DownloadConfigures();
//            return intros;
//        }
//        async Task<bool> DownloadConfigures()
//        {
//            string fullUri = _apiServices.GetURI(Urls.GetConfiguresAndEmails);
//            HttpClient Client = new HttpClient();
//            try
//            {
//                var response2 = await Client.GetAsync(fullUri);
//                var jsonString2 = await response2.Content.ReadAsStringAsync();
//                var jsonObject = JToken.Parse(jsonString2);
//                int responseCode2 = (int)response2.StatusCode;
//                if (responseCode2 == 200)
//                {
//                    try
//                    {
//                        Utils.Settings.AdminEmail = (string)jsonObject[0].SelectToken("admin_email");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.AdminEmail = "";
//                    }
//                    try
//                    {
//                        Utils.Settings.PurchaseEmail = (string)jsonObject[0].SelectToken("purchase_email");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.PurchaseEmail = "";
//                    }
//                    try
//                    {
//                        Utils.Settings.IOSLink = (string)jsonObject[0].SelectToken("ios_link");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.IOSLink = "";
//                    }
//                    try
//                    {
//                        Utils.Settings.AndroidLink = (string)jsonObject[0].SelectToken("android_link");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.AndroidLink = "";
//                    }
//                    try
//                    {
//                        Utils.Settings.HowToUseArabic = (string)jsonObject[0].SelectToken("ar_howToUse_link");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.HowToUseArabic = "";
//                    }
//                    try
//                    {
//                        Utils.Settings.HowToUseEnglish = (string)jsonObject[0].SelectToken("en_howToUse_link");
//                    }
//                    catch (Exception e)
//                    {
//                        Utils.Settings.HowToUseEnglish = "";
//                    }
//                    return true;
//                }
//                return false;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }

//        }
    }

}
