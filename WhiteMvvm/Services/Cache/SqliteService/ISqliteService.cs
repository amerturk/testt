using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WhiteMvvm.Bases;

namespace WhiteMvvm.Services.Cache.SqliteService
{
    public interface ISqliteService
    {

        /// <summary>
        /// method to create tables after check if it exists
        /// </summary>
        /// <returns></returns>
        bool CreateDatabaseTables<T>(IList<T> tables) where T : BaseModel;
        /// <summary>
        /// Checks the database to see if the table exists
        /// </summary>
        bool TableExists<T>();
        /// <summary>
        /// Checks the database to see if the table exists
        /// </summary>
         bool TableExists<T>(T table);
        /// <summary>
        /// Method to save data in cache return true whenever data are saved
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        T SaveItem<T>(T item) where T : BaseModel, new();

        /// <summary>
        /// Method to save list in cache return true whenever list are saved
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TBaseModel"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        bool SaveList<TBaseModel>(List<TBaseModel> items) where TBaseModel : BaseModel, new();
        /// <summary>
        /// Method to get all table data to remoce and then save list in cache return true whenever list are saved
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TBaseModel"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        bool RemoveAndSaveList<TBaseModel>(List<TBaseModel> items) where TBaseModel : BaseModel;

        T GetItemRecursive<T>(Expression<Func<T, bool>> query) where T : BaseModel, new();
        /// <summary>
        /// Method to get item from cache return object inherited from base model depend on linq query 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetItem<T>(Expression<Func<T, bool>> query) where T : BaseModel , new();
        /// <summary>
        /// Method get all list from object inherited from base model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetList<T>() where T : BaseModel, new();
        /// <summary> 
        /// Method to get some data from list of object depend on linq query 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<T> GetList<T>(Expression<Func<T, bool>> query) where T : BaseModel, new();
        /// <summary>
        /// Method to delete one object from cache return true whenever object deleted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool DeleteItem<T>(T deletedObject) where T : BaseModel, new();
        /// <summary>
        /// Method to delete all data of object from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool DeleteList<T>() where T : BaseModel, new();
        /// <summary>
        /// Method to delete all data of object from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool DeleteList<T>(IQueryable<T> query) where T : BaseModel, new();
        void Update(Action action);
        void UpdateItem<T>(T item) where T : BaseModel, new();
    }
}
