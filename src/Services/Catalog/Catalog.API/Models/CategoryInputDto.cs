using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class CategoryInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}
