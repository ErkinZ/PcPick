using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PcPick.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}