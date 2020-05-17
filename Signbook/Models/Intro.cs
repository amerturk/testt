using System;
using System.Collections.Generic;
using System.Text;

namespace Signbook.Models
{
    /// <summary>
    /// model to hold start up page data which will bind with carousel view in runtime
    /// </summary>
    public class Intro
    {
        public bool isurl
        {
            get
            {
                if (URLArabic == null)
                    return false;
                else
                    return true;
            }
        }
        string uri = "https://www.youtube.com/embed/6tX3Hf-M8Dk";
        public string VideoURLArabic
        {
            get
            {
                return $"{URLArabic}?rel=0&amp;showinfo=1";
            }
        }
        public string VideoURLEnglish
        {
            get
            {
                return $"{URLEnglish}?rel=0&amp;showinfo=1";
            }
        }
        public string VideoURLArabicios
        {
            get
            {
                return $"<iframe height=77% width=100% src='{URLArabic}?rel=0' frameborder=0 webkit-playsinline></iframe>";
                // return $"<iframe height=77% width=100% src='https://www.youtube.com/embed/WL2l_Q1AR_Q' frameborder = 0 allowfullscreen></iframe>";

            }
        }
        public string VideoURLEnglishios
        {
            get
            {
                return $"<iframe height=77% width=100% src='{URLEnglish}?rel=0' frameborder=0 webkit-playsinline></iframe>";
                //  return $"<iframe height=77% width=100% src='https://www.youtube.com/embed/IGQBtbKSVhY' frameborder=0></iframe>";

            }
        }
        public string URLArabic { get; set; }
        public string URLEnglish { get; set; }
        public string BackgroundImage { get; set; }
        public string TitleEnglish { get; set; }
        public string TitleArabic { get; set; }
        public string DescriptionEnglish { get; set; }
        public string DescriptionArabic { get; set; }
        public string ImageEnglish { get; set; }
        public string ImageArabic { get; set; }

        /// <summary>
        /// indicator to last page to show which data template to show
        /// </summary>
        public bool LastPage { get; set; }
    }
}
