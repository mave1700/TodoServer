using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class TaskForUpdateDto
    {
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(100, ErrorMessage = "Task name cannot be longer than 45 characters")]
        public string Name { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; }

    }
}
