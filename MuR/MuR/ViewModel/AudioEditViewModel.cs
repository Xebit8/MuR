using System;
using System.ComponentModel;
using MuR.Model.SQLiteObjects;
using System.Windows.Input;
using Murr;

namespace MuR.ViewModel
{
    /// <summary>
    /// модель страницы изменения данных
    /// </summary>
    class AudioEditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Audio audio;
        /// <summary>
        /// команда
        /// </summary>
        public ICommand Change { get; }

        public AudioEditViewModel(Audio audio)
        {
            this.audio = audio;
            Change = new ChangeDataAudio(this);
        }
        /// <summary>
        /// обновляет объект в бд
        /// </summary>
        public void UpdateDBItem()
        {
            App.Database.UpdateObjectTable<Audio>(audio);
        }
        /// <summary>
        /// название песни
        /// </summary>
        public string Ttile 
        {
            get { return audio.NameAudio ?? "Неустановлено"; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    audio.NameAudio = value;
                    OnProperyChanged("Title");
                }
            }
        }
        /// <summary>
        /// текс песни
        /// </summary>
        public string Subtitle 
        {
            get { return audio.Subtitle ?? string.Empty; }
            set 
            {
                if(!string.IsNullOrEmpty(value))
                {
                    audio.Subtitle = value;
                    OnProperyChanged("Subtitle");
                }
            }
        }
        /// <summary>
        /// автор песни
        /// </summary>
        public int? AuthorId
        {
            get { return audio.AuthorId ?? 0; }
            set
            {
                if (value.HasValue)
                {
                    audio.AuthorId = value;
                    OnProperyChanged("AuthorId");
                }
            }
        }
        /// <summary>
        /// жанр песни
        /// </summary>
        public int? GenreId
        {
            get { return audio.GenreId ?? 0; }
            set
            {
                if (value.HasValue)
                {
                    audio.GenreId = value;
                    OnProperyChanged("GenreId");
                }
            }
        }

        protected void OnProperyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
