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
        // путь к директории хранения файлов приложения
        public string GetExternalDirectory()
        {
            var externalDirectory = context.GetExternalFilesDir(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
            return externalDirectory.Path;
        }
        // путь к музыке во внешней директории
        public string GetExternalCacheDirectory()
        {
            var filesPath = context.GetExternalFilesDir(Android.OS.Environment.DirectoryMusic);
            return filesPath.AbsolutePath;
        }
    }
}