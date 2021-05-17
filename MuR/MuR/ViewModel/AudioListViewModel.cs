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
    public class AudioListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// коллекция аудио
        /// </summary>
        private ObservableCollection<Audio> _music;
        private Playlist playlist;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectAudioItem { get; protected set; }

        public INavigation Navigation { get; set; }
        public AudioListViewModel(IList<Audio> music)
        {
            _music = new ObservableCollection<Audio>(music);
            SelectAudioItem = new Command(SelectItem);
        }
        public ObservableCollection<Audio> Music 
        { 
            get { return _music; }
            protected set 
            {
                if (value != null)
                    _music = value;
            } 
        }
        public string NamePlaylist => playlist.NamePlayList;
        public int CountAudio => _music.Count;
        public string PathImage => playlist.UriImage;

        protected void OnPropertyChange(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // выбор песни с передачей информации о самой песни
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
                    if(CrossMediaManager.Current.PlayQueueItem(_music.IndexOf(audio)).Result)
                        await Navigation.PushAsync(new MuR.View.PlayerPage(_music));
                }
            }
        }
    }
}
