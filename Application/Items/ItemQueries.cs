using Application.Abstractions;
using AutoMapper;
using Domain.DTO.ItemDTO;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
    public static class ItemQueries
    {
        #region GetItemById
        public class GetItemQuery : IRequest<GetItemDto>
        {
            public GetItemQuery(Guid itemId)
            {
                ItemId = itemId;
            }
            public Guid ItemId { get; set; }
        }

        public class GetItemQueryHandler : IRequestHandler<GetItemQuery, GetItemDto>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetItemQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<GetItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
            {
                var Item = await _context.Items.FirstOrDefaultAsync(i=> i.ItemId == request.ItemId, cancellationToken);
                return _mapper.Map<GetItemDto>(Item);
            }
        }
        #endregion
        #region GetAllItems
        public class GetAllItemQuery : IRequest<List<GetItemDto>>
        {
        }

        public class GetAllItemQueryHandler : IRequestHandler<GetAllItemQuery, List<GetItemDto>>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetAllItemQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }


            public async Task<List<GetItemDto>> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
            {
                var Items = await _context.Items.ToListAsync(cancellationToken);
                return _mapper.Map<List<GetItemDto>>(Items);
            }
        }
        #endregion

        #region Search Items
        public class SearchItemsQuery : IRequest<List<GetItemDto>>
        {
            public SearchItemsQuery(ItemSearchCriteria itemSearchCriteria)
            {
                ItemSearchCriteria = itemSearchCriteria;
            }
            public ItemSearchCriteria ItemSearchCriteria { get; set; }
        }

        public class SearchItemsQueryHandler : IRequestHandler<SearchItemsQuery, List<GetItemDto>>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public SearchItemsQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<List<GetItemDto>> Handle(SearchItemsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Items.AsQueryable();

                if (!string.IsNullOrEmpty(request.ItemSearchCriteria.ItemName))
                {
                    query = query.Where(item => item.ItemName.Contains(request.ItemSearchCriteria.ItemName));
                }

                if (!string.IsNullOrEmpty(request.ItemSearchCriteria.Description))
                {
                    query = query.Where(item => item.Description.Contains(request.ItemSearchCriteria.Description));
                }
                if (!string.IsNullOrEmpty(request.ItemSearchCriteria.Description))
                {
                    query = query.Where(item => item.Color.Contains(request.ItemSearchCriteria.Color));
                }

                var result = await query.ToListAsync(cancellationToken);
                return _mapper.Map<List<GetItemDto>>(result);
            }
        }
        #endregion


    }

}
