using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BillettetSystemAPI.Models
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }
        [Required]  
        public string? CategoryName { get; set; }


        [JsonIgnore]
       public ICollection<Event>? Events { get; set; }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public static implicit operator Category(List<Category> v)
        {
            throw new NotImplementedException();
        }
    }
}
