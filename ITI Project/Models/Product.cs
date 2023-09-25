using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace ITI_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [MinLength(2, ErrorMessage = "Name must be more than 2 char")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Range(minimum: 2000, maximum: 200000)]
        public int Price { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }
        public Category? Category { get; set; }
    }
}
