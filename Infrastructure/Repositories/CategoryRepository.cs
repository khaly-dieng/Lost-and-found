using Application.Abstractions.Repositories;
using AutoMapper;
using Domain.DTO.CategoryDTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LostAndFoundDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<LostAndFoundDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid CategoryId)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == CategoryId);
            if (existingCategory is null)
            {
                throw new InvalidOperationException("Not Found");
            }
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var Categories = await _context.Categories.Include(c => c.Items).ToListAsync();
            return Categories;
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            var existingCategory = await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(p => p.CategoryId == categoryId);
            if (existingCategory is null)
            {
                throw new InvalidOperationException("Not Found");
            }
            return existingCategory;
        }

        public async Task UpdatedCategory(Guid CategoryId, UpdateCategoryDto updatedCategoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == CategoryId);
            if (existingCategory is null)
            {
                throw new InvalidOperationException("Not Found");
            }

            _mapper.Map(updatedCategoryDto, existingCategory);

            await _context.SaveChangesAsync();
        }
    }
}
