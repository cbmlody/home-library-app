using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class BookSeries
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}