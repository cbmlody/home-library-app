using HomeLibraryAPI.EF.Models.Enums;

using System;
using System.Collections.Generic;

namespace HomeLibraryAPI.EF.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public short Volume { get; set; }
        public short Pages { get; set; }
        public CoverType Cover { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
