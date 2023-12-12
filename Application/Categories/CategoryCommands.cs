using Application.Abstractions;
using AutoMapper;
using Domain.DTO.CategoryDTO;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Categories
{
    public static class CategoryCommands
    {
        #region SAVE
        public class SaveCategoryCommand : IRequest<Category>
        {
            public SaveCategoryCommand(CreateCategoryDto categoryDto)
            {
                CategoryDto = categoryDto;
            }
            public CreateCategoryDto CategoryDto { get; set; }
        }

        public class SaveCategoryHandler : IRequestHandler<SaveCategoryCommand, Category>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public SaveCategoryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }
            public async Task<Category> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request.CategoryDto);
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var persisted = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                return _mapper.Map<Category>(persisted);
            }
        }
        #endregion

        #region UPDATE
        public class UpdateCategoryCommand : IRequest<Category>
        {
            public UpdateCategoryCommand(Guid categoryId, UpdateCategoryDto updateCategoryDto)
            {
                CategoryId = categoryId;
                UpdateCategoryDto = updateCategoryDto;
            }
            public Guid CategoryId { get; }
            public UpdateCategoryDto UpdateCategoryDto { get; set; }
        }

        public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public UpdateCategoryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.FindAsync(request.CategoryId);

                if (existingCategory is null)
                {
                    throw new DllNotFoundException("Category not found");
                }

                _mapper.Map(request.UpdateCategoryDto, existingCategory);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Category>(existingCategory);
            }
        }
        #endregion

        #region DELETE
        public class DeleteCategoryCommand : IRequest<Guid>
        {
            public DeleteCategoryCommand(Guid categoryId)
            {
                CategoryId = categoryId;
            }
            public Guid CategoryId { get; }
        }

        public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Guid>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public DeleteCategoryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.FindAsync(request.CategoryId);

                if (existingCategory is null)
                {
                    throw new DllNotFoundException("Category not found");
                }

                _context.Categories.Remove(existingCategory);
                await _context.SaveChangesAsync(cancellationToken);
                return request.CategoryId;
            }
        }
        #endregion
    }
}
