using Entities.Models.Enums;

using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string EAN { get; set; }
        public int Volume { get; set; }
        public int Pages { get; set; }
        public CoverType CoverType { get; set; }
        public BookSeries BookSeries { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public Publisher Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}