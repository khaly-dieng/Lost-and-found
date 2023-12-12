using Domain.Enums;

namespace Domain.DTO.ItemDTO
{
    public class GetItemDto
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public DateTimeOffset LostOrFoundDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
        public string Picture { get; set; }
    }
}
