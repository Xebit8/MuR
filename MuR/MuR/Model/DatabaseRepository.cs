using System;
using Xamarin.Forms;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using MuR.Model.SQLiteObjects;

namespace MuR.Model
{
    /// <summary>
    /// класс для работы с базой данных
    /// </summary>
    public class DatabaseRepository
    {
        /// <summary>
        /// соединение с базой данных
        /// </summary>
        private  readonly SQLiteAsyncConnection dbConnection;
        public SQLiteAsyncConnection DBConnection => dbConnection;

        public DatabaseRepository()
        {
            dbConnection = DependencyService.Get<IToSQLiteDB>().GetConnection();
        }

        public async Task<CreateTableResult> CreateTable<T>() where T : new()
        {
            return await DBConnection.CreateTableAsync<T>();
        }
        public async Task<int> InsertIntoTable<T>(T insertValue) where T : new()
        {
            return await DBConnection.InsertAsync(insertValue, typeof(T));
        }
        public async Task<List<T>> SelectAllFromTable<T>() where T : new()
        {
            return await DBConnection.Table<T>().ToListAsync();
        }
        public async void UpdateObjectTable<T>(T updateValue) where T : new()
        {
            await DBConnection.UpdateAsync(updateValue, typeof(T));
        }
    }
}
