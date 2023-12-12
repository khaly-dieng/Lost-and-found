using Domain.DTO.PersonDTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Persons.PersonCommands;
using static Application.Persons.PersonQueries;

namespace LostAndFoundApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IMediator _mediator;

        public PersonController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<PersonController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(CreatePersonDto request)
        {
            var savePerson = new SavePersonCommand(request);
            var savedPerson = await _mediator.Send(savePerson);
            _logger.LogInformation("Person saved successfully");
            return Ok(savedPerson);
        }

        [HttpPut()]
        [Route("{PersonId}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] Guid PersonId, [FromBody] UpdatePersonDto updatePersonDto)
        {
            try
            {
                var updateCommand = new UpdatePersonCommand(PersonId, updatePersonDto);
                var updatedPerson = await _mediator.Send(updateCommand);

                _logger.LogInformation("Person Updated successfully");
                return new ObjectResult(new { Person = updatedPerson })
                {
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
                return new BadRequestObjectResult(new { Message = "Error occurred", Error = ex.Message });
            }
        }

        [HttpDelete()]
        [Route("{PersonId}")]
        public async Task DeletePersonCommand([FromRoute] Guid PersonId)
        {
            try
            {
                var deleteCommand = new DeletePersonCommand(PersonId);
                await _mediator.Send(deleteCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
            }
        }

        [HttpGet("{PersonId}")]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid PersonId)
        {
            try
            {
                var getPersonDto = new GetPersonQuery(PersonId);
                var personDto = await _mediator.Send(getPersonDto);

                if (personDto == null)
                {
                    return NotFound(); 
                }

                return Ok(personDto); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while getting the person");
            }
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllPerson()
        {
            try
            {
                var getPersons = new GetAllPersonQuery();
                var Persons = await _mediator.Send(getPersons);
                return Ok(Persons);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
                return new BadRequestObjectResult(new { Message = "Error occurred", Error = ex.Message });
            }
        }


    }

}
