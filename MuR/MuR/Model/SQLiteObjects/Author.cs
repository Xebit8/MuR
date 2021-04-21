using System;
using System.Collections.Generic;
using SQLite;

namespace MuR.Model.SQLiteObjects
{
    /// <summary>
    /// сущность базы данных в виде автора музыки
    /// </summary>
    [Table("author")]
    internal class Author
    {
        [PrimaryKey, AutoIncrement, Column("author_id")]
        public int AuthorId { get; set; }

        [NotNull, Unique, Column("name_author")]
        public string NameAuthor { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [NotNull, Column("uri_image")]
        public string UriImage { get; set; }

        [Column("genre_id")]
        public int? GenreId { get; set; }
    }


}
