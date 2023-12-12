using Domain.DTO.ItemDTO;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IItemRepository
    {
        public Task<IEnumerable<Item>> GetAllItems();
        public Task<Item> GetItemById(Guid ItemId);
        public Task CreateItem(Item Item);
        public Task UpdatedItem(Guid ItemId, UpdateItemDto UpdatedCategoryDto);
        public Task DeleteItem(Guid ItemId);
        public Task<IEnumerable<Item>> SearchItems(ItemSearchCriteria criteria);
        public Task<IEnumerable<Item>> GetItemsByStatus(ItemStatus status);
    }
}
