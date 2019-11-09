using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class TaskForCreationDto
    {
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(100, ErrorMessage = "Task name cannot be longer than 45 characters")]
        public string Name { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public Guid UserId { get; set; }
    }
}
