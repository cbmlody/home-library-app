using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
