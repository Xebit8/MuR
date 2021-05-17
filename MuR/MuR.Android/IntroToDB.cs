using MuR.Model;
using Xamarin.Forms;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

[assembly: Dependency(typeof(MuR.Droid.IntroToDB))]
namespace MuR.Droid
{
       
    class IntroToDB : IToSQLiteDB
    {
        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(new MuR.Droid.FileSystemImplementation().GetExternalDirectory(typeFiles.DataBase));
            return connection;
        }
    }
}