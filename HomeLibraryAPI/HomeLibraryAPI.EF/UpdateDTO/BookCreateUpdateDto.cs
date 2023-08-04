using HomeLibraryAPI.EF.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.UpdateDTO
{
    public class BookCreateUpdateDto
    {
        [Required(ErrorMessage = "Title cannot be empty")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        public short Volume { get; set; }

        public short Pages { get; set; }

        [Required(ErrorMessage = "Cover Type must be specified")]
        public CoverType Cover { get; set; }

        [Required]
        public IEnumerable<Guid> Authors { get; set; }
    }
}
