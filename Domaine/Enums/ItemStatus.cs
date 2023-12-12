using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enums
{
    [Table("ItemStatus")]
    public class ItemStatusEntity
    {
        [Key]
        public int Id { get; set; }

        public ItemStatus Status { get; set; }
    }

    public enum ItemStatus
    {
        Lost,
        Found
    }
}
