using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("task")]
    public class Task
    {
        [Key]
        [Column("TaskId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        [StringLength(100, ErrorMessage = "Task name cannot be longer than 45 characters")]
        public string Name { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage ="User Id is required")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        

    }
}
