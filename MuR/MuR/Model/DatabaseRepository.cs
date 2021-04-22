using System;
using Xamarin.Forms;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using MuR.Model.SQLiteObjects;
using MuR.ViewModel;

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
        /// <summary>
        /// Создать таблицу
        /// </summary>
        /// <typeparam name="T">тип на основе которого будет создаваться таблица</typeparam>
        /// <returns>была ли таблица создана</returns>
        public async Task<CreateTableResult> CreateTable<T>() where T : new()
        {
            return await DBConnection.CreateTableAsync<T>();
        }
        /// <summary>
        /// добавить запись в соответсвующую таблицу
        /// </summary>
        /// <typeparam name="T">тип на основе которого существует таблица</typeparam>
        /// <param name="insertValue">объект бд, (будет представлен в виде записи)</param>
        /// <returns>задача с > 0, если вставка оказаласть успеной, и < 0 если не удалось</returns>
        public async Task<int> InsertIntoTable<T>(T insertValue) where T : new()
        {
            return await DBConnection.InsertAsync(insertValue, typeof(T));
        }
        /// <summary>
        /// получить все записи из таблицы
        /// </summary>
        /// <typeparam name="T">тип на основе которого существует таблица</typeparam>
        /// <returns>список со всеми записи таблицы в виде объекта типа T</returns>
        public async Task<List<T>> SelectAllFromTable<T>() where T : new()
        {
            return await DBConnection.Table<T>().ToListAsync();
        }
        /// <summary>
        /// обновить запись в таблице на основе объекта типа T
        /// </summary>
        /// <typeparam name="T">тип на основе которого существует таблица</typeparam>
        /// <param name="updateValue">объект, значения которого были изменены</param>
        public async void UpdateObjectTable<T>(T updateValue) where T : new()
        {
            await DBConnection.UpdateAsync(updateValue, typeof(T));
        }
    }
}
