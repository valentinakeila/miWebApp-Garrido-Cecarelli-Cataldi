using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class ProductUpdateRequest
    {
        public string? Name { get; set; } = string.Empty;
        public decimal? Price { get; set; } 
        public string? Description { get; set; } = string.Empty;
        public int? CategoryId { get; set; } 
        public string? ImageUrl { get; set; } = string.Empty;
        public int? Stock { get; set; }
    }
}
