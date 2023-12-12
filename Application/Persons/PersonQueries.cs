using Application.Abstractions;
using AutoMapper;
using Domain.DTO.PersonDTO;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Persons
{
    public static class PersonQueries
    {
        #region GetPersonById
        public class GetPersonQuery : IRequest<GetPersonDto>
        {
            public GetPersonQuery(Guid personId)
            {
                PersonId = personId;
            }
            public Guid PersonId { get; set; }
        }

        public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, GetPersonDto>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetPersonQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<GetPersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
            {
                var Person = await _context.Persons
                    .Include(p => p.Items)
                    .FirstOrDefaultAsync(p => p.PersonId == request.PersonId, cancellationToken);
                return _mapper.Map<GetPersonDto>(Person);
            }
        }
        #endregion
        #region GetAllPersons
        public class GetAllPersonQuery : IRequest<List<GetPersonDto>>
        {
        }

        public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, List<GetPersonDto>>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;
            public GetAllPersonQueryHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<List<GetPersonDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
            {
                var Persons = await _context.Persons
                    .Include(p => p.Items)
                    .ToListAsync(cancellationToken);
                return _mapper.Map<List<GetPersonDto>>(Persons);
            }
        }
        #endregion

    }

}
