using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.DTO
{
    public class UserDTO
    {
        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class UserCreateDto
        {
            public string Email { get; set; }
            public string Password { get; set; }

            // default role as customer
        }


        public class UserUpdateDto
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class UserSignInDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}