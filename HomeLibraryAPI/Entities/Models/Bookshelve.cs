﻿using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Bookshelve
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
