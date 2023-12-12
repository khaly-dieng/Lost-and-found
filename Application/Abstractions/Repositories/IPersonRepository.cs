using Domain.DTO.PersonDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IPersonRepository
    {
        public Task<IEnumerable<Person>> GetAllPersons();
        public Task<Person> GetPersonById(Guid personId);
        public Task CreatePerson(Person person);
        public Task UpdatePerson(Guid PersonId, UpdatePersonDto updatedPersonDto);
        public Task DeletePerson(Guid personId);
    }
}
