using HomeLibraryAPI.EF.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(13)]
        public string EAN { get; set; }

        public short Volume { get; set; }

        public short Pages { get; set; }

        public CoverType CoverType { get; set; }

        public virtual BookSeries BookSeries { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Bookshelve> Bookshelves { get; set; }
    }
}