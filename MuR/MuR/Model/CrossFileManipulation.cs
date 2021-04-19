using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using MediaManager;

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
                var filesPath = Path.Combine(Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalCacheDirectory(), file.Name);
                if (!File.Exists(filesPath))
                    File.WriteAllBytes(filesPath, File.ReadAllBytes(file.FullName));
            }
        }

        /// <summary>
        /// получить все медиа файлы(IMediaItem) из внешнего кэша приложения
        /// </summary>
        /// <returns>список медиа объектов</returns>
        internal static IList<MediaManager.Library.IMediaItem> LoadFromExternalCache()
        {
            var filesPath = Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalCacheDirectory();
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
            var filesPath = Xamarin.Forms.DependencyService.Get<IFileSystem>().GetExternalCacheDirectory();
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
