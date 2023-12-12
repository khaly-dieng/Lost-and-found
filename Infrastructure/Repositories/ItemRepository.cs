using Application.Abstractions.Repositories;
using Domain.DTO.ItemDTO;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public Task CreateItem(Item Item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(Guid ItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemById(Guid ItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetItemsByStatus(ItemStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> SearchItems(ItemSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Task UpdatedItem(Guid ItemId, UpdateItemDto UpdatedCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
