using Domain.DTO.CategoryDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(Guid categoryId);
        public Task CreateCategory(Category category);
        public Task UpdatedCategory(Guid CategoryId, UpdateCategoryDto updatedCategoryDto);
        public Task DeleteCategory(Guid CategoryId);
    }
}
