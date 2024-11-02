using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using user.src.Utils;
using user.src.Entity;

namespace user.src.DTO
{
    public class UserDTO
    {
        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public Role Role { get; set; }


        }

        public class UserCreateDto
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [PasswordComplexityAttribute]
            public string Password { get; set; }

        }


        public class UserUpdateDto
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? FirstName { get; set; }


        }
        public class UserSignInDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}