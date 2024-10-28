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
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required  decimal Price { get; set; }
        public required string  ImageUrl { get; set; }
        public required string Description  { get; set; }
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
    }
}