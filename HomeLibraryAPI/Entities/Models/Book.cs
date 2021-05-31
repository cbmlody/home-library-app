using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public uint ISBN { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
