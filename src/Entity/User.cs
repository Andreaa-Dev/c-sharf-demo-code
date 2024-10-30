using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using user.src.Utils;

namespace user.src.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
         public string Email { get; set; }

        [Required]
        [PasswordComplexityAttribute]
         public string Password { get; set; }
        public byte[]? Salt { get; set; }
        public string? FirstName { get; set; }
        public Role Role { get; set; } = Role.Customer;


    }
    // By default, EF Core will map the UserRole enum to an int in the database
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Customer
    }
}