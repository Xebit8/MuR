using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в виде жанра музыки
    /// </summary>
    [Table("genre")]
    internal class Genre
    {
        [PrimaryKey, AutoIncrement, Column("genre_id")]
        public int GenreId { get; set; }

        [NotNull, Unique, Column("name_genre")]
        public string NameGenre { get; set; }
    }
}
