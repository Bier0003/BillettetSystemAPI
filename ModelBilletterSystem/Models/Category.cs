
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelBilletterSystem
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }
        [Required]  
        public string? CategoryName { get; set; }
    }
}
