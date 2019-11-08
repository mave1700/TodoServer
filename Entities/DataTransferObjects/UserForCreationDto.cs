using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(45, ErrorMessage = "First name cannot be longer than 45 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(45, ErrorMessage = "Last name cannot be longer than 45 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
    }
}
