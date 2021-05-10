using System.ComponentModel.DataAnnotations;

namespace BackOffice.Client.ViewModels
{
    public class CategoryInputDto
    {
        [Required]
        public string Name { get; set; }
        public CategoryInputDto()
        {

        }

        public CategoryInputDto(string name)
        {
            Name = name;
        }
    }
}
