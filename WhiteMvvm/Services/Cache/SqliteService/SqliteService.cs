using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AppCenter.Crashes;
using SQLite;
using SQLiteNetExtensions.Extensions;
using WhiteMvvm.Bases;
using WhiteMvvm.Exceptions;
using WhiteMvvm.Services.DeviceUtilities;

namespace WhiteMvvm.Services.Cache.SqliteService
{
    public class SqliteService : ISqliteService
    {
        private readonly IFileSystem _fileSystem;
        private readonly SQLiteConnection _sqLiteConnection;
        private object locker = new object();
        public SqliteService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _sqLiteConnection = CreateConnection();
        }

        private SQLiteConnection CreateConnection()
        {
            try
            {

                return new SQLiteConnection(_fileSystem.CacheDirectory + "database.db");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw new SqliteException("Can't connect to database file");
            }
        }

        public bool TableExists<T>(T table)
        {
            try
            {
                lock (locker)
                {
                    var cmd = _sqLiteConnection.CreateCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=?", table.GetType().Name);
                    return cmd.ExecuteScalar<string>() != null;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException($"Unable to find {table.GetType().Name} table", exception);
            }
        }

        public bool TableExists<T>()
        {
            try
            {
                lock (locker)
                {
                    var cmd = _sqLiteConnection.CreateCommand("SELECT name FROM sqlite_master WHERE type='table' AND name=?", typeof(T).Name);
                    return cmd.ExecuteScalar<string>() != null;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException($"Unable to find {typeof(T).Name} table", exception);
            }
        }

        public bool DeleteItem<T>(T deletedObject) where T : BaseModel , new()
        {
            int result;
            try
            {
                lock (locker)
                {
                    result = _sqLiteConnection.Delete<T>(deletedObject.BaseId);
                }
                return result > 0;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to delete item", exception);
            }
        }

        public bool DeleteList<T>() where T : BaseModel , new()
        {
            int result;
            try
            {
                lock (locker)
                {
                    result = _sqLiteConnection.DeleteAll<T>();
                }
                return result > 0;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to delete items", exception);
            }
        }

        public bool DeleteList<T>(IQueryable<T> query) where T : BaseModel , new()
        {
            int result;
            try
            {
                lock (locker)
                {
                    result = _sqLiteConnection.DeleteAll<T>();
                }
                return result > 0;
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to delete items", exception);
            }
        }
        public T GetItemRecursive<T>(Expression<Func<T, bool>> query) where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    var list = _sqLiteConnection.GetAllWithChildren<T>();
                    return _sqLiteConnection.GetAllWithChildren(query).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get record", exception);
            }
        }
        public T GetItem<T>(Expression<Func<T, bool>> query) where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    return _sqLiteConnection.Table<T>().FirstOrDefault(query);
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get record", exception);
            }
        }

        public IList<T> GetList<T>() where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    return _sqLiteConnection.Table<T>().ToList();
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get list", exception);
            }
        }

        public IList<T> GetList<T>(Expression<Func<T, bool>> query) where T : BaseModel, new()
        {
            try
            {
                lock (locker)
                {
                    return _sqLiteConnection.Table<T>().Where(query).ToList();
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to get list", exception);
            }
        }

        public bool RemoveAndSaveList<T>(List<T> items) where T : BaseModel
        {
            try
            {
                if (items == null)
                    return false;
                foreach (var item in items)
                {
                    var savedItem = SaveItem<BaseModel>(item);
                    if (savedItem == null)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new SqliteException("Unable to save or update item", ex);
            }
        }

        public T SaveItem<T>(T item) where T : BaseModel , new()
        {
            try
            {
                if (item != null)
                {
                    /**/
                    lock (locker)
                    {                        
                        int result = 0;
                        var items = GetList<T>();
                        var existItem = GetItem<T>(x => x.Id == item.Id);
                        if (existItem != null)
                        {
                            item.BaseId = existItem.BaseId;
                            result = _sqLiteConnection.Update(item);
                        }
                        else
                        {
                            result = _sqLiteConnection.Insert(item);                            
                        }
                        return result > 0 ? item : null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to save or update item", exception);
            }
        }

        public bool SaveList<TBaseModel>(List<TBaseModel> items) where TBaseModel : BaseModel , new()
        {
            if (items is null)
            {
               return false;
            }

            try
            {

                foreach (var item in items)
                {
                    var savedItem = SaveItem<TBaseModel>(item);
                    if (savedItem == null)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new SqliteException("Unable to save or update item", ex);
            }
        }

        public void Update(Action action)
        {
            action.Invoke();
        }

        public void UpdateItem<T>(T item) where T : BaseModel , new()
        {
            try
            {
                lock (locker)
                {
                    if (item != null)
                    {
                        _sqLiteConnection.UpdateWithChildren(item);
                    }                  
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to save or update item", exception);
            }
        }

        public bool CreateDatabaseTables<T>(IList<T> tables) where T : BaseModel
        {
            try
            {
                lock (locker)
                {
                    foreach (var table in tables)
                    {
                        if (!TableExists(table))
                        {
                            _sqLiteConnection.CreateTable(table.GetType());
                        }
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new SqliteException("Unable to create database", exception);
            }
        }
    }
}
