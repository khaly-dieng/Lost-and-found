using Domain.DTO.ItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.PersonDTO
{
    public class GetPersonDto
    {
        public Guid PersonId { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public List<GetItemDto> Items { get; set; }
    }
}
