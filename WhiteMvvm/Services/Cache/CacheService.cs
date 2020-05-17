using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.Storage;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Dialog;

namespace WhiteMvvm.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly Realm _realm = Realm.GetInstance();

        public bool IsInTransaction => _realm.IsInTransaction;

        //private static Realm realm;
        //public static Realm _realm
        //{
        //    get
        //    {
        //        if (realm == null)
        //            realm = Realm.GetInstance();
        //        return realm;
        //    }
        //}

        /// <inheritdoc />
        public T SaveItem<T>(T item) where T : RealmObject
        {
            if (item == null)
                return null;
            var newItem = new object();
            using (var trans = _realm.BeginWrite())
            {
                newItem = _realm.Add(item, true);

                    trans.Commit();
            }
            return (T)newItem;
        }
        public void UpdateItem<T>(T item) where T : RealmObject
        {
            _realm.Write(() => _realm.Add(item, update: true));
        }

        /// <inheritdoc />
        public bool SaveList<TRealmObject>(List<TRealmObject> items) where TRealmObject : RealmObject
        {
            using (var trans = _realm.BeginWrite())
            {
                foreach (var item in items)
                {
                    var newItem = _realm.Add(item, true);
                }
                trans.Commit();
            }
            return true;
        }
       
        public T GetItem<T>() where T : RealmObject
        {
            var item = _realm.All<T>().FirstOrDefault();
            return item;
        }

        /// <inheritdoc />
        public T GetItem<T>(Expression<Func<T, bool>> query) where T : RealmObject
        {
            var item = _realm.All<T>().FirstOrDefault(query);
            return item;
        }

        public IList<T> GetList<T>() where T : RealmObject
        {
            var items = _realm.All<T>();
            return items.ToList();
        }

        public IList<T> GetList<T>(Expression<Func<T, bool>> query) where T : RealmObject
        {
            var items = _realm.All<T>().Where(query);
            return items.ToList();
        }

        /// <inheritdoc />
        public bool DeleteItem<T>(T deletedObject) where T : RealmObject
        {
            _realm.Write((() =>
            {
                _realm.Remove(deletedObject);
            }));
            return true;
        }

        /// <inheritdoc />
        public bool DeleteList<T>() where T : RealmObject
        {
            _realm.Write((() =>
            {
                _realm.RemoveAll<T>();
            }));
            return true;
        }
        /// <inheritdoc />
        public bool DeleteList<T>(IQueryable<T> query) where T : RealmObject
        {
            _realm.Write((() =>
            {
                _realm.RemoveRange(query);
            }));
            return true;
        }
        public void Update(Action action)
        {
            _realm.Write(action);
        }
        /// <inheritdoc />
        public bool RemoveAndSaveList<TRealmObject>(List<TRealmObject> items) where TRealmObject : RealmObject
        {
            using (var trans = _realm.BeginWrite())
            {
                var savedData = _realm.All<TRealmObject>();
                _realm.RemoveRange(savedData);
                foreach (var item in items)
                {
                    var newItem = _realm.Add(item,true);
                }
                trans.Commit();
            }
            return true;
        }

        public void Refresh()
        {
            _realm.Refresh();
        }
    }
}
