using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MediaManager;
using MuR.Model.SQLiteObjects;
using Xamarin.Forms;

namespace MuR.ViewModel
{
    /// <summary>
    /// состояние проигрования
    /// </summary>
    enum CurrentState
    {
        Play,
        Pause
    }
    /// <summary>
    /// модель отображения медиа в плеере
    /// </summary>
    public class AudioPlayerViewModel : INotifyPropertyChanged
    {
        private Audio audio;
        private TimeSpan duration;
        private string formatTime;
        private CurrentState currentState = default;
        private static IReadOnlyDictionary<CurrentState, string> pathSource = new Dictionary<CurrentState, string>()
        {
            {CurrentState.Play, "pause.svg" },
            {CurrentState.Pause, "play.svg" }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Previous { get; protected set; }
        public ICommand Next { get; protected set; }
        public ICommand Play { get; protected set; }
        public ICommand SliderDragCompleted { get; protected set; }
        public ICommand ShuffleMode { get; protected set; }
        public ICommand RepeatMode { get; protected set; }
        /// <summary>
        /// конструкотр вида плеера
        /// </summary>
        /// <param name="audio">данные о песне</param>
        /// <param name="length">продолжительность песни</param>
        public AudioPlayerViewModel(Audio audio, TimeSpan length)
        {
            
            this.audio = audio;
            AudioLength = length;
            if (length.TotalHours >= 1)
                formatTime = @"hh\:mm\:ss";
            else
                formatTime = @"mm\:ss";
            Previous = new Command(StepBack);
            Next = new Command(StepNext);
            Play = new Command(PlayPause);
            SliderDragCompleted = new Command(DragCompleted);
            ShuffleMode = new Command(ActivOrDisActivShuffle);
            RepeatMode = new Command(ActivOrDisActivRepeatMode);
        }
        /// <summary>
        /// Название
        /// </summary>
        public string Title => audio.NameAudio ?? "Неустановлено";
        /// <summary>
        /// ссылка на image в Resources
        /// </summary>
        public string UriImage => audio.UriImage;
        /// <summary>
        /// ссылка на файл в ресурсах проекта
        /// </summary>
        public string UriFile => audio.UriFile;
        /// <summary>
        /// время проигрывания
        /// </summary>
        public TimeSpan Duration 
        {
            get { return duration; }
            set 
            {
                if (value <= AudioLength)
                {
                    duration = value;

                    OnProperyChanged("CurrentDuration");
                    OnProperyChanged("TimeDuration");
                }
            }
        }
        /// <summary>
        /// текущее время проигрывания
        /// </summary>
        public double CurrentDuration
        {
            get { return Duration.TotalSeconds; }
            set { Duration = TimeSpan.FromSeconds(value); }
        }
        /// <summary>
        /// текущее время проигрывания в сравнении с длиной аудио файла
        /// </summary>
        public string TimeDuration
        {
            get 
            { 
                return $"{Duration.ToString(formatTime)}/{AudioLength.ToString(formatTime)}"; 
            }
        }
        /// <summary>
        /// длина аудио файла
        /// </summary>
        public TimeSpan AudioLength { get; protected set; }
        /// <summary>
        /// текущее состояние кнопки (её изображение)
        /// </summary>
        public string CurrentSourceForStatus
        {
            get { return pathSource[currentState]; }
        }
        /// <summary>
        /// предыдущая песня или вернуться в начало песни
        /// </summary>
        private async void StepBack()
        {
            await CrossMediaManager.Current.PlayPrevious();
        }
        /// <summary>
        /// следующея песня
        /// </summary>
        private async void StepNext()
        {
            await CrossMediaManager.Current.PlayNext();
        }
        /// <summary>
        /// продолжить прослушивание или приостановить
        /// </summary>
        private async void PlayPause()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
                currentState = CurrentState.Pause;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                currentState = CurrentState.Play;
            }
            OnProperyChanged("CurrentSourceForStatus");
        }
        /// <summary>
        /// завершение перемещение тумблера
        /// </summary>
        private async void DragCompleted()
        {
            await CrossMediaManager.Current.SeekTo(Duration);
        }
        /// <summary>
        /// активирование или откючение перемешивания
        /// </summary>
        private void ActivOrDisActivShuffle()
        {
            CrossMediaManager.Current.ToggleShuffle();
        }
        /// <summary>
        /// активирование или откючение повторения
        /// </summary>
        private void ActivOrDisActivRepeatMode()
        {
            CrossMediaManager.Current.ToggleRepeat();
        }
        protected void OnProperyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
