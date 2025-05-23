﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelBilletterSystem
{
    public class Event
    {
        [Key]
        public int Id_event { get; set; }
        [Required]
        public string? Event_Title { get; set; }
        [Required]
        public string? Event_Description { get; set; }
        public string? Image{ get; set; }    
        public DateTime Create_At { get; set; } = DateTime.UtcNow;
        public int Ticket_Amount { get; set; }
        [ForeignKey("Id_Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        [Required]
        public virtual Category Category { get; set; }
       

    }

}
