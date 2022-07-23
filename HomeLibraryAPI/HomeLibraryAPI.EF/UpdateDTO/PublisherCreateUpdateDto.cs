using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.UpdateDTO
{
    public class PublisherCreateUpdateDto
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(50, ErrorMessage = "Name cannot be longer then 50 characters")]
        public string Name { get; set; }

        public virtual ICollection<Guid> Books { get; set; }
    }
}
