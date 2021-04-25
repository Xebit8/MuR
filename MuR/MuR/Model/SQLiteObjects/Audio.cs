using MuR.ViewModel;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в виде аудио файла (музыка)
    /// </summary>
    [Table("audio")]
    public class Audio
    {
        [PrimaryKey, AutoIncrement, Column("audio_id")]
        public int AudioId { get; set; }

        [Column("name_audio")]
        public string NameAudio { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("subtitle")]
        public string Subtitle { get; set; }

        [NotNull, Unique, Column("uri_file")]
        public string UriFile { get; set; }

        [NotNull, Column("uri_image")]
        public string UriImage { get; set; }

        [Column("genre_id")]
        public int? GenreId { get; set; }

        [Column("author_id")]
        public int? AuthorId { get; set; }
    }
}
