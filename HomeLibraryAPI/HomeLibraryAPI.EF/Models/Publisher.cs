using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.Models
{
    public class Publisher
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}