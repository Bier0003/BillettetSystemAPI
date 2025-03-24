using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelBilletterSystem.Models
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }
        [Required]  
        public string? CategoryName { get; set; }

            // Foreign key for the one-to-many relationship (Category -> Event)
            public int EventId { get; set; }

            // Navigation property back to Event
            public Event? Event { get; set; }

    


    }
}
