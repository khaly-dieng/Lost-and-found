using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.ItemDTO
{
    public class CreateItemDto
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public DateTimeOffset LostOrFoundDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
        public string Picture { get; set; }
    }
}
