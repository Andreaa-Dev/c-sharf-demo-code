using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace user.src.Entity
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        // [JsonIgnore]
        [InverseProperty("Category")]
        public required Category Category { get; set; }
    }
}