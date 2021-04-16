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

        public async Task<CreateTableResult> CreateTable()
        {
            return await DBConnection.CreateTableAsync<Genre>();
        }
        public async Task<List<SQLiteConnection.ColumnInfo>> GetAuthor()
        {
            return await DBConnection.GetTableInfoAsync("genre");
        }
    }
}
