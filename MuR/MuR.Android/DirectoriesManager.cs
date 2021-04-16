using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuR.Model;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(Murr.Droid.FileSystemImplementation))]
namespace Murr.Droid
{
    class FileSystemImplementation : IFileSystem
    {
        private readonly Context context = Android.App.Application.Context;

        // <summary>
        /// имя базы данных
        /// </summary>
        public string Database => "Music.db";

        /// <summary>
        /// Получить путь к директории хранения файлов приложения
        /// </summary>
        /// <returns>путь к директории хранения файлов приложения</returns>        
        public string GetExternalDirectory(typeFiles typePath)
        {
            Java.IO.File externalDirectory = default;
            switch (typePath)
            {
                case typeFiles.Audio:
                    externalDirectory = context.GetExternalFilesDir(Android.OS.Environment.DirectoryMusic);
                    break;
                case typeFiles.DataBase:
                    externalDirectory = context.GetDatabasePath(Database);
                    break;
                case typeFiles.General:
                    externalDirectory = context.GetExternalFilesDir(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
                    break;
                default:
                    throw new MissingMemberException();
                    break;
            }
            return externalDirectory?.Path;
        }
    }
}