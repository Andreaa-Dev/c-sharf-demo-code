using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class Test
    {
        public int Id { get; set; }

        [Required]  // Name is required
        [StringLength(100)]  // Maximum length of the Name is 100 characters
        // add require
        public required string Name { get; set; }

        [EmailAddress]  // Email should be in a valid format
        public required string Email { get; set; }

        [Range(18, 99)]  // Age must be between 18 and 99
        public int Age { get; set; }

        public string Password { get; set; }


        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username can only contain letters.")]
        public string Username { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}