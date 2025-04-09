using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BillettetSystemAPI.Models
{
    public class Event
    {
        [Key]
        public int Id_event { get; set; }

        [Required]
        public string? Event_Title { get; set; }

        [Required]
        public string? Event_Description { get; set; }
        public DateTime Create_At { get; set; }

        [ForeignKey("Id_Category")]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category? Categories { get; set; }
        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }



    }

}
