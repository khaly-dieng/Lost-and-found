using Application.Abstractions.Repositories;
using Domain.DTO.PersonDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task DeletePerson(Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonById(Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePerson(Guid PersonId, UpdatePersonDto updatedPersonDto)
        {
            throw new NotImplementedException();
        }
    }
}
