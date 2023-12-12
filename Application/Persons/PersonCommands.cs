using Application.Abstractions;
using AutoMapper;
using Domain.DTO.PersonDTO;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Persons
{
    public static class PersonCommands
    {
        #region SAVE
        public class SavePersonCommand : IRequest<Person>
        {
            public SavePersonCommand(CreatePersonDto personDto)
            {
                PersonDto = personDto;
            }
            public CreatePersonDto PersonDto { get; set; }
        }

        public class SavePersonHandler : IRequestHandler<SavePersonCommand, Person>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public SavePersonHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }
            public async Task<Person> Handle(SavePersonCommand request, CancellationToken cancellationToken)
            {
                var person = _mapper.Map<Person>(request.PersonDto);
                await _context.Persons.AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var persisted = _context.Persons.FirstOrDefault(p => p.PersonId == person.PersonId);
                return _mapper.Map<Person>(persisted);
            }
        }
        #endregion

        #region UPDATE
        public class UpdatePersonCommand : IRequest<Person>
        {
            public UpdatePersonCommand(Guid personId, UpdatePersonDto updatePersonDto)
            {
                PersonId = personId;
                UpdatePersonDto = updatePersonDto;
            }
            public Guid PersonId { get; }
            public UpdatePersonDto UpdatePersonDto { get; set; }
        }

        public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Person>
        {
            private readonly ILostAndFoundDbContext _context;
            private readonly IMapper _mapper;

            public UpdatePersonHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
            {
                var existingPerson = await _context.Persons.FindAsync(request.PersonId);

                if (existingPerson is null)
                {
                    throw new DllNotFoundException("Person not found");
                }

                _mapper.Map(request.UpdatePersonDto, existingPerson);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<Person>(existingPerson);
            }
        }
        #endregion

        #region DELETE
        public class DeletePersonCommand : IRequest<Guid>
        {
            public DeletePersonCommand(Guid personId)
            {
                PersonId = personId;
            }
            public Guid PersonId { get; }
        }

        public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, Guid>
        {
            private readonly ILostAndFoundDbContext _context;

            public DeletePersonHandler(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<ILostAndFoundDbContext>();
            }

            public async Task<Guid> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
            {
                var existingPerson = await _context.Persons.FindAsync(request.PersonId);

                if (existingPerson is null)
                {
                    throw new DllNotFoundException("Person not found");
                }

                _context.Persons.Remove(existingPerson);
                await _context.SaveChangesAsync(cancellationToken);
                return request.PersonId;
            }
        }
        #endregion
    }

}
