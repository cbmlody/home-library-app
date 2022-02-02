using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Bookshelve
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
