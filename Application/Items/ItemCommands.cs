using Application.Abstractions;
using AutoMapper;
using Domain.DTO.ItemDTO;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Items
{
    public static class ItemCommands
    {
        #region SAVE
        public class SaveItemCommand : IRequest<Item>
        {
            public SaveItemCommand(CreateItemDto itemDto)
            {
                ItemDto = itemDto;
            }
            public CreateItemDto ItemDto { get; set; }
        }

        public class SaveItemHandler : IRequestHandler<SaveItemCommand, Item>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public SaveItemHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }
            public async Task<Item> Handle(SaveItemCommand request, CancellationToken cancellationToken)
            {
                var Item = _mapper.Map<Item>(request.ItemDto);
                Item.UpdatedAt = DateTimeOffset.UtcNow;
                await _context.Items.AddAsync(Item, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return _context.Items.FirstOrDefault(i => i.ItemId == Item.ItemId);
              
            }
        }
        #endregion

        #region UPDATE
        public class UpdateItemCommand : IRequest<Item>
        {
            public UpdateItemCommand(Guid itemId, UpdateItemDto updateItemDto)
            {
                ItemId = itemId;
                UpdateItemDto = updateItemDto;
            }
            public Guid ItemId { get; }
            public UpdateItemDto UpdateItemDto { get; set; }
        }

        public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, Item>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public UpdateItemHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                var existingItem = await _context.Items.FindAsync(request.ItemId);

                if (existingItem is null)
                {
                    throw new DllNotFoundException("Item not found");
                }

                _mapper.Map(request.UpdateItemDto, existingItem);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Item>(existingItem);
            }
        }
        #endregion

        #region DELETE
        public class DeleteItemCommand : IRequest<Guid>
        {
            public DeleteItemCommand(Guid itemId)
            {
                ItemId = itemId;
            }
            public Guid ItemId { get; }
        }

        public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, Guid>
        {
            private readonly ILostAndFoundDbContext _context;

            public DeleteItemHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
            }

            public async Task<Guid> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
            {
                var existingItem = await _context.Items.FindAsync(request.ItemId);

                if (existingItem is null)
                {
                    throw new DllNotFoundException("Item not found");
                }

                _context.Items.Remove(existingItem);
                await _context.SaveChangesAsync(cancellationToken);
                return request.ItemId;
            }
        }
        #endregion
    }

}
