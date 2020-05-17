using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhiteMvvm.Utilities;
using Signbook.Models;

namespace Signbook.Services.News
{
    public class MockNewsService
    {
        public ObservableRangeCollection<Signbook.Models.News> GetLocalNews()
        {
            ObservableRangeCollection<Signbook.Models.News> lst = new ObservableRangeCollection<Models.News>();
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            var obj2 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            var obj3 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت  مصاب بالنوموفوبيا؟ علاجه لدى   مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            lst.Add(obj2);
            lst.Add(obj3);
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            return lst;
        }
        public ObservableRangeCollection<Signbook.Models.News> GetInternationalNews()
        {
            ObservableRangeCollection<Signbook.Models.News> lst = new ObservableRangeCollection<Models.News>();
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "ه2ل",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            var obj2 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            var obj3 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت  مصاب بالنوموفوبيا؟ علاجه لدى   مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            lst.Add(obj2);
            lst.Add(obj3);
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            return lst;
        }
        public ObservableRangeCollection<Signbook.Models.News> GetThirdNews()
        {
            ObservableRangeCollection<Signbook.Models.News> lst = new ObservableRangeCollection<Models.News>();
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "هل3",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            var obj2 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            var obj3 = new Signbook.Models.News()
            {
                Id = "1",
                Title = "هل أنت  مصاب بالنوموفوبيا؟ علاجه لدى   مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركاتهل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                Date = "منذ 10 دقائق",
                Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
            };
            lst.Add(obj2);
            lst.Add(obj3);
            for (int i = 0; i < 15; i++)
            {
                var obj = new Signbook.Models.News()
                {
                    Id = "1",
                    Title = "هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات هل أنت مصاب بالنوموفوبيا؟ علاجه لدى شركات",
                    Date = "منذ 10 دقائق",
                    Url = "https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                };
                lst.Add(obj);
            }
            return lst;
        }
    }
}
