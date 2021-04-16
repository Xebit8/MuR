using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в виде плейлиста
    /// </summary>
    [Table("playlist")]
    internal class Playlist
    {
        [PrimaryKey, AutoIncrement, Column("playlist_id")]
        public int PlayListId { get; }

        [NotNull, Unique, Column("name_playlist")]
        public string NamePlayList { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [NotNull, Column("uri_image")]
        public string UriImage { get; set; }

        [Column("genre_id")]
        public int GenreId { get; set; }
    }
}
