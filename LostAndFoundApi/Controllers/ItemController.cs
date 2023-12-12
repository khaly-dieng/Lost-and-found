using Domain.DTO.ItemDTO;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Items.ItemCommands;
using static Application.Items.ItemQueries;

namespace LostAndFoundApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IMediator _mediator;

        public ItemController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<ItemController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpPost]
        public async Task<IActionResult> SaveItem(CreateItemDto request)
        {
            var saveItem = new SaveItemCommand(request);
            var savedItem = await _mediator.Send(saveItem);
            _logger.LogInformation("Item saved successfully");
            return Ok(savedItem);
        }

        [HttpPut()]
        [Route("{ItemId}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid ItemId, [FromBody] UpdateItemDto updateItemDto)
        {
            try
            {
                var updateCommand = new UpdateItemCommand(ItemId, updateItemDto);
                var updatedItem = await _mediator.Send(updateCommand);

                _logger.LogInformation("Item Updated successfully");
                return new ObjectResult(new { Item = updatedItem })
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
        [Route("{ItemId}")]
        public async Task DeleteItemCommand([FromRoute] Guid ItemId)
        {
            try
            {
                var deleteCommand = new DeleteItemCommand(ItemId);
                await _mediator.Send(deleteCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
            }
        }

        [HttpGet("{ItemId}")]
        public async Task<IActionResult> GetItemById([FromRoute] Guid ItemId)
        {
            try
            {
                var getItemDto = new GetItemQuery(ItemId);
                var ItemDto = await _mediator.Send(getItemDto);

                if (ItemDto == null)
                {
                    return NotFound();
                }

                return Ok(ItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while getting the Item");
            }
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllItem()
        {
            try
            {
                var getItems = new GetAllItemQuery();
                var Items = await _mediator.Send(getItems);
                return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
                return new BadRequestObjectResult(new { Message = "Error occurred", Error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchItems([FromQuery] ItemSearchCriteria Criteria)
        {
            try
            {
                var searchItems = new SearchItemsQuery(Criteria);
                var Items = await _mediator.Send(searchItems);
                return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
                return new BadRequestObjectResult(new { Message = "Error occurred", Error = ex.Message });
            }
        }

    }

}
