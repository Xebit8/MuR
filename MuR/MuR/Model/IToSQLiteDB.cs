using SQLite;

namespace MuR.Model
{
    public interface IToSQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
