using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("category")]
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string Libelle { get; set; }
        public string Code { get; set; }
        public string CategoryImage { get; set; }

        [JsonIgnore]
        public virtual List<Item> Items { get; set; }
    }
}
