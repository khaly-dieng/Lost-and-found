using Domain.DTO.CategoryDTO;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Categories.CategoryCommands;
using static Application.Categories.CategoryQueries;

namespace LostAndFoundApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;

        public CategoryController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<CategoryController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(CreateCategoryDto request)
        {
            var saveCategory = new SaveCategoryCommand(request);
            var savedCategory = await _mediator.Send(saveCategory);
            _logger.LogInformation("Category saved successfully");
            return Ok(savedCategory);
        }

        [HttpPut()]
        [Route("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var updateCommand = new UpdateCategoryCommand(categoryId, updateCategoryDto);
                var updatedCategory = await _mediator.Send(updateCommand);

                _logger.LogInformation("Category Updated successfully");
                return new ObjectResult(new { Category = updatedCategory })
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
        [Route("{categoryId}")]
        public async Task DeleteCategoryCommand([FromRoute] Guid categoryId)
        {
            try
            {
                var deleteCommand = new DeleteCategoryCommand(categoryId);
                await _mediator.Send(deleteCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            try
            {
                var getCategoryDto = new GetCategoryQuery(categoryId);
                var categoryDto = await _mediator.Send(getCategoryDto);

                if (categoryDto == null)
                {
                    return NotFound();
                }

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while getting the Category");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var getCategories = new GetAllCategoryQuery();
                var categories = await _mediator.Send(getCategories);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred");
                return new BadRequestObjectResult(new { Message = "Error occurred", Error = ex.Message });
            }
        }


    }

}
