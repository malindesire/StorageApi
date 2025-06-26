using System.ComponentModel.DataAnnotations;

namespace StorageApi.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters.")]
        public string Category { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public int Count { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
