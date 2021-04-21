using System;
using System.Collections.Generic;
using System.Text;
using MediaManager;
using Xamarin.Essentials;

namespace MuR.Model
{
    public enum typeFiles
    {
        General,
        DataBase,
        Audio
    }

    /// <summary>
    /// файловая система для конкретной файловой системы
    /// </summary>
    public interface IFileSystem
    {
        string Database { get; }
        string GetExternalDirectory(typeFiles typePath);
    }


}
