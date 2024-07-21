using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }


        public static CategoryDto Create(Category category)
        {
            var dto = new CategoryDto();
            dto.Id = category.Id;
            dto.Name = category.Name;

            return dto;
        }
        public static List<CategoryDto?> CreateList(IEnumerable<Category> categories)
        {
            List<CategoryDto?> listDto = [];

            foreach (var c in categories)
            {
                listDto.Add(Create(c));
            }

            return listDto;
        }

        
    }
}
