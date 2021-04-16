using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в виде аудио файла (музыка)
    /// </summary>
    [Table("audio")]
    internal class Audio
    {
        [PrimaryKey, AutoIncrement, Column("audio_id")]
        public int AudioId { get; }

        [Column("name_audio")]
        public string NameAudio { get; set; } = "Неизвестно";

        [Column("rating")]
        public int Rating { get; set; }

        [Column("subtitle")]
        public string Subtitle { get; set; }

        [NotNull, Unique, Column("uri_file")]
        public string UriFile { get; set; }

        [NotNull, Column("uri_image")]
        public string UriImage { get; set; }

        [Column("genre_id")]
        public int GenryId { get; set; }

        [Column("author_id")]
        public int AuthorId { get; set; }

    }
}
