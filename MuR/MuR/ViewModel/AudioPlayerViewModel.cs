using System;
using MuR.Model.SQLiteObjects;
using Murr;


namespace MuR.ViewModel
{
    /// <summary>
    /// модель отображения медиа в плеере
    /// </summary>
    class AudioPlayerViewModel
    {
        private Audio audio;
        public AudioPlayerViewModel(Audio audio)
        {
            this.audio = audio;
        }

        public string Ttile => audio.NameAudio ?? "Неустановлено";
        public string UriImage => audio.UriImage;
        public string UriFile => audio.UriFile;
    }
}
