using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Categories.Queries.Dtos
{
    public class CategoryDto
    {
        public CategoryDto()
        {

        }

        public CategoryDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
