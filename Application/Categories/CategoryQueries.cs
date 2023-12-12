using Application.Abstractions;
using AutoMapper;
using Domain.DTO.CategoryDTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Categories
{
    public static class CategoryQueries
    {
        #region GetCategoryById
        public class GetCategoryQuery : IRequest<GetCategoryDto>
        {
            public GetCategoryQuery(Guid categoryId)
            {
                CategoryId = categoryId;
            }
            public Guid CategoryId { get; }
        }

        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryDto>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetCategoryQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<GetCategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                var Category = await _context.Categories
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(p => p.CategoryId == request.CategoryId, cancellationToken);
                return _mapper.Map<GetCategoryDto>(Category);
            }
        }
        #endregion

        #region GetAllCategories
        public class GetAllCategoryQuery : IRequest<List<GetCategoryDto>>
        {
        }

        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<GetCategoryDto>>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetAllCategoryQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }
            public async Task<List<GetCategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories
                    .Include(c => c.Items)
                    .ToListAsync(cancellationToken);
                return _mapper.Map<List<GetCategoryDto>>(categories);
            }
        }
        #endregion

    }
}
