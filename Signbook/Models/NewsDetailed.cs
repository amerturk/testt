using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;

namespace Signbook.Models
{
    public class NewsDetailed : BaseModel
    {
        private string _id;

        public string Id
        {
            get => _id; set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _title;

        public string Title
        {
            get => _title; set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _date;

        public string Date
        {
            get => _date; set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        private string _url;

        public string Url
        {
            get => _url; set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        private Uri _img;

        public Uri Img
        {
            get => _img; set
            {
                _img = new Uri("https://storage.googleapis.com/signboo/news/newsimages/expo.jpg");
                OnPropertyChanged();
            }
        }

        private string _NewsFile;

        public string NewsFile
        {
            get => _NewsFile; set
            {
                _NewsFile = value;
                OnPropertyChanged();
            }
        }

        private string _NewsId;

        public string NewsId
        {
            get => _NewsId; set
            {
                _NewsId = value;
                OnPropertyChanged();
            }
        }

        private string _IsReadColor = "#ffffff";

        public string IsReadColor
        {
            get => _IsReadColor; 
            set
            {
                _IsReadColor = value;
                OnPropertyChanged();
            }
        }

        
    }
}
