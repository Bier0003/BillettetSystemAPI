
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelBilletterSystem
{
    public class Ticket
    {
    
        [Key]
        public Guid Id_Ticket { get; set; } = Guid.NewGuid();
        public bool is_used { get; set; } = false;

        [ForeignKey("Id_event")]
        public int EventId { get; set; }
        [JsonIgnore]
        [Required]
        public virtual Event Event { get; set; }
      

    }
}
