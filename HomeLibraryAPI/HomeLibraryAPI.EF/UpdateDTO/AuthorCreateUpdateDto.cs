using System.ComponentModel.DataAnnotations;

namespace HomeLibraryAPI.EF.UpdateDTO
{
    public class AuthorCreateUpdateDto
    {
        [Required(ErrorMessage = "First name cannot be empty")]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "First name cannot be empty")]
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters")]
        public string LastName { get; set; }
    }
}
