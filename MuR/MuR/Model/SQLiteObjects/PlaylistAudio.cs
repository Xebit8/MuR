using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в связывающей таблицы аудио -> плейлист
    /// </summary>
    [Table("playlist_audio")]
    internal class PlaylistAudio
    {
        [PrimaryKey, AutoIncrement, Column("playlist_audio_id")]
        public int PlaylistAudioId { get; set; }

        [NotNull, Column("playlist_id")]
        public int? PlaylistId { get; set; }

        [NotNull, Column("audio_id")]
        public int? AudioId { get; set; }
    }
}
