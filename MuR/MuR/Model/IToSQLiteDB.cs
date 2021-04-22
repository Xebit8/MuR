using SQLite;

namespace MuR.Model
{
    /// <summary>
    /// Получение async соединения с базой данных в зависимости от платформы
    /// </summary>
    public interface IToSQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
