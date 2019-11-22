using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }

        public IEnumerable<TaskDto> Tasks { get; set; }
    }
}
