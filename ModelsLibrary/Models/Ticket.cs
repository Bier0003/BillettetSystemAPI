using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BillettetSystemAPI.Models
{
    public class Ticket
    {
        [Key]
        public int Id_Ticket { get; set; }
        public int Ticket_Amount { get; set; }
        public int Ticket_Price { get; set; }
        public bool Ability { get; set; }
        public bool is_used { get; set; }

        [ForeignKey("Id_event")]
        public int EventId { get; set; }
        [JsonIgnore]
        public virtual Event?  Events { get; set; }
      

    }
}
