using System;
using System.Collections.Generic;
using System.Text;
using MediaManager;
using Xamarin.Essentials;

namespace MuR.Model
{
    /// <summary>
    /// файловая система для конкретной файловой системы
    /// </summary>
    public interface IFileSystem
    {
        string GetExternalDirectory();
        string GetExternalCacheDirectory();
    }


}
