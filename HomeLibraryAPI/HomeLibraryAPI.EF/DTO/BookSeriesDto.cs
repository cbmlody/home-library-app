using System;
using System.Collections.Generic;

namespace HomeLibraryAPI.EF.DTO
{
    public class BookSeriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
