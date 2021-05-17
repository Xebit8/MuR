using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using MediaManager;
using MuR;
using MuR.Model.SQLiteObjects;

namespace MuR.Model
{
    /// <summary>
    /// манипулирование аудио файлами
    /// </summary>
    internal static class CrossFileManipulation
    {
        /// <summary>
        /// настройки выбора файлов в файловой системе
        /// </summary>
        internal static PickOptions optionsPicket = new PickOptions
        {
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>()
        {
            { DevicePlatform.Android, new string[]{ "audio/mpeg" } },
            { DevicePlatform.iOS, new string[] { "public.audio" } },
            { DevicePlatform.UWP, new string[] { ".mp3"} }
        })
        };
        /// <summary>
        /// Загрузить файлы во внешний кэш приложения
        /// </summary>
        /// <param name="files">файлы, которые необходимо загрузить</param>
        internal static void LoadToCache(params FileResult[] files)
        {
            FileInfo file;
            foreach (var item in files)
            {
                file = new FileInfo(item.FullPath);
                var filesPath = Path.Combine(Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalDirectory(typeFiles.Audio), file.Name);
                if (!File.Exists(filesPath))
                {
                    File.WriteAllBytes(filesPath, File.ReadAllBytes(file.FullName));
                }
                MediaManager.Library.IMediaItem mediaItem = CrossMediaManager.Current.Extractor.CreateMediaItem(file).Result;
                if (App.Database.DBConnection.FindWithQuery<MuR.Model.SQLiteObjects.Audio>("SELECT * FROM audio WHERE uri_file = ?", mediaItem.FileName) == null) // если отсутсвует в базе данных
                {
                    Audio audio = new Audio() { NameAudio = mediaItem.DisplayTitle, Subtitle = mediaItem.DisplaySubtitle, UriFile = mediaItem.FileName, UriImage = "Resources/drawable/examle2.png" }; // изменить
                    App.Database.InsertIntoTable<Audio>(audio);
                }
            }
        }
        /// <summary>
        /// получить все медиа файлы(IMediaItem) из внешнего кэша приложения
        /// </summary>
        /// <returns>список медиа объектов</returns>
        internal static IList<MediaManager.Library.IMediaItem> LoadFromExternalCache()
        {
            var filesPath = Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalDirectory(typeFiles.Audio);
            DirectoryInfo directory = new DirectoryInfo(filesPath);
            FileInfo[] files = directory.GetFiles();
            List<MediaManager.Library.IMediaItem> mediaItems = new List<MediaManager.Library.IMediaItem>(files.Length);

            MediaManager.Library.IMediaItem audio;
            foreach (var file in files)
            {
                audio = CrossMediaManager.Current.Extractor.CreateMediaItem(file).Result;
                mediaItems.Add(audio);
            }

            return mediaItems;
        }
        /// <summary>
        /// поиск по имени файла
        /// </summary>
        /// <param name="fileName"> название файла</param>
        /// <returns>преобразованный файл</returns>
        internal static MediaManager.Library.IMediaItem GetAudio(string fileName)
        {
            var filesPath = Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalDirectory(typeFiles.Audio);
            string findFilePath;
            if(File.Exists(findFilePath = Path.Combine(filesPath, fileName)))
            {
                MediaManager.Library.IMediaItem audio = CrossMediaManager.Current.Extractor.CreateMediaItem(new FileInfo(findFilePath)).Result;
                return audio;
            }
            throw new FileNotFoundException();
        }
    }
}
