using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class User
    {
        // change this to Guid
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }

        // add role 
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