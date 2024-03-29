﻿using System;
using Xamarin.Forms;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using System.IO;

namespace MuR.Droid
{
    [Activity(Label = "MuR", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossMediaManager.Current.Init(this);

            ReWriteDataBase(new FileSystemImplementation());

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
            LoadApplication(new App());

            //LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(173, 216, 230));
            Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(173, 216, 230));
        }
            public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            
        }
        /// <summary>
        /// извлечение бд в виде двоичного файла и запись по пути
        /// </summary>
        /// <param name="toPath">путь по которому необходимо записать файл</param>
        private void ReWriteDataBase(FileSystemImplementation fileSystem)
        {
            string db = fileSystem.Database,
            dbPath = fileSystem.GetExternalDirectory(MuR.Model.typeFiles.DataBase);

            //Check DB is stay in folder(toPath)
            if (!File.Exists(dbPath))
            {
                using (BinaryReader binRead = new BinaryReader(Android.App.Application.Context.Assets.Open(db)))
                {
                    using (BinaryWriter binWrite = new BinaryWriter(new FileStream(dbPath, FileMode.CreateNew)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = binRead.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            binWrite.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }
    }
}