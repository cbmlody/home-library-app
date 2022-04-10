using HomeLibraryAPI.EF.DTO;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.UpdateDTO
{
    public class BookSeriesCreateUpdateDto
    {
        [Required(ErrorMessage = "The name of book series is requred")]
        [StringLength(100, ErrorMessage = "Book series name length cannot exceed 100 characters")]
        public string Name { get; set; }
        public IEnumerable<BookDto> Books = new List<BookDto>();
    }
}
