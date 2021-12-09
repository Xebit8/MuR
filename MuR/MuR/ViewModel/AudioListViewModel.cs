using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using MuR.Model.SQLiteObjects;
using Xamarin.Forms;    
using System.Collections.ObjectModel;
using MuR.Model;
using MediaManager;

namespace MuR.ViewModel
{
    /// <summary>
    /// модель плейлиста
    /// </summary>
    public class AudioListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// коллекция аудио
        /// </summary>
        private ObservableCollection<Audio> _music;
        /// <summary>
        /// плейлист, к которому относятся данные аудиа
        /// </summary>
        private Playlist playlist;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectAudioItem { get; protected set; }

        public INavigation Navigation { get; set; }
        public AudioListViewModel(IList<Audio> music)
        {
            _music = new ObservableCollection<Audio>(music);
            SelectAudioItem = new Command(SelectItem);
        }
        /// <summary>
        /// треки плейлиста
        /// </summary>
        public ObservableCollection<Audio> Music 
        { 
            get { return _music; }
            private set 
            {
                if (value != null)
                    _music = value;
            } 
        }
        /// <summary>
        /// Имя плейлиста
        /// </summary>
        public string NamePlaylist => playlist?.NamePlayList ?? "Без названия";
        /// <summary>
        /// Количество треков в плейлисте
        /// </summary>
        public int CountAudio => _music?.Count ?? 0;
        /// <summary>
        /// ссылка на обложку плейлиста
        /// </summary>
        public string PathImage => playlist?.UriImage ?? "Resources/drawable/examle.png";

        protected void OnPropertyChange(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// выбор песни с передачей информации о самой песни
        /// </summary>
        /// <param name="sender">объект аудио</param>        
        private async void SelectItem(object sender)
        {
            // какой-то быдло код
            if (sender is Audio audio)
            {
                if(App.Player != null)
                { 
                    if(CrossMediaManager.Current.PlayQueueItem(_music.IndexOf(audio)).Result)
                        await Navigation.PushAsync(App.Player);
                }
                else
                {
                    await Navigation.PushAsync(new MuR.View.PlayerPage(_music));
                    await CrossMediaManager.Current.PlayQueueItem(_music.IndexOf(audio));
                }
            }
        }
    }
}
