using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;

namespace Signbook.Models
{
    public class News : BaseModel
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

        private string _img;

        public string Img
        {
            get => _img; set
            {
                _img = value;
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
    }
}
