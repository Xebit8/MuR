using System;
using System.Windows.Input;
using System.ComponentModel;
using MuR.Model.SQLiteObjects;
using Xamarin.Forms;    
using System.Collections.ObjectModel;

namespace MuR.ViewModel
{
    public class AudioListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// коллекция аудио
        /// </summary>
        public ObservableCollection<Audio> music;
        private Audio SelectedAudio;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectAudioItem { get; protected set; }

        public INavigation Navigation { get; set; }
        public AudioListViewModel()
        {
            music = new ObservableCollection<Audio>(Murr.App.Database.SelectAllFromTable<Audio>().Result);
            SelectAudioItem = new Command(SelectItem);
        }
        public Audio SelectAudio
        {
            get { return SelectAudio; }
            set
            {
                SelectedAudio = value;
            }
        }

        private void SelectItem(object AudioObject)
        {
            if(AudioObject is Audio audioItem)
            {

            }
        }

    }
}
