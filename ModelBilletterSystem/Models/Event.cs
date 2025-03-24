using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelBilletterSystem.Models
{
    public class Event
    {

        [Key]
        public int Id_event { get; set; }
        [Required]
        public string? Event_Title { get; set; }
            public string? Event_Description { get; set; }
            public DateTime Create_At { get; set; }

        // Navigation property for the one-to-many relationship (Event -> Categories)
        public ICollection<Category> Categories { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
       


    }

}
