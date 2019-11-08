using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(45, ErrorMessage = "First name cannot be longer than 45 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(45, ErrorMessage = "Last name cannot be longer than 45 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        public ICollection<Task> Tasks { get; set; }


    }
}
