using MuR.Model;
using Xamarin.Forms;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

[assembly: Dependency(typeof(Murr.Droid.IntroToDB))]
namespace Murr.Droid
{
    
    class IntroToDB : IToSQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var connection = new SQLiteAsyncConnection(new Murr.Droid.FileSystemImplementation().GetExternalDirectory(typeFiles.DataBase));
            return connection;
        }
    }
}