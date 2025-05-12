using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using V1.Services.Services;
using Microsoft.AspNetCore.Mvc;
using V1.DyModels.VMs;
using System.Linq.Expressions;
using V1.DyModels.Dso.Requests;
using AutoGenerator.Helper.Translation;
using System;

namespace V1.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "V1")]
    [Route("api/V1/Api/[controller]")]
    [ApiController]
    public class CardModelController : ControllerBase
    {
        private readonly IUseCardModelService _cardmodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CardModelController(IUseCardModelService cardmodelService, IMapper mapper, ILoggerFactory logger)
        {
            _cardmodelService = cardmodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(CardModelController).FullName);
        }

        // Get all CardModels.
        [HttpGet(Name = "GetCardModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CardModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all CardModels...");
                var result = await _cardmodelService.GetAllAsync();
                var items = _mapper.Map<List<CardModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CardModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a CardModel by ID.
        [HttpGet("{id}", Name = "GetCardModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CardModel ID received.");
                return BadRequest("Invalid CardModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CardModel with ID: {id}", id);
                var entity = await _cardmodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CardModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CardModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CardModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CardModel by Lg.
        [HttpGet("GetCardModelByLanguage", Name = "GetCardModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardModelOutputVM>> GetCardModelByLg(CardModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CardModel ID received.");
                return BadRequest("Invalid CardModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CardModel with ID: {id}", id);
                var entity = await _cardmodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CardModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CardModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CardModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CardModels by Lg.
        [HttpGet("GetCardModelsByLanguage", Name = "GetCardModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CardModelOutputVM>>> GetCardModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid CardModel lg received.");
                return BadRequest("Invalid CardModel lg null ");
            }

            try
            {
                var cardmodels = await _cardmodelService.GetAllAsync();
                if (cardmodels == null)
                {
                    _logger.LogWarning("CardModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<CardModelOutputVM>>(cardmodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CardModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new CardModel.
        [HttpPost(Name = "CreateCardModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardModelOutputVM>> Create([FromBody] CardModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("CardModel data is null in Create.");
                return BadRequest("CardModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new CardModel with data: {@model}", model);
                var item = _mapper.Map<CardModelRequestDso>(model);
                var createdEntity = await _cardmodelService.CreateAsync(item);
                var createdItem = _mapper.Map<CardModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new CardModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple CardModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CardModelOutputVM>>> CreateRange([FromBody] IEnumerable<CardModelCreateVM> models)
        {
            if (models == null)
            {
                _logger.LogWarning("Data is null in CreateRange.");
                return BadRequest("Data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in CreateRange: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating multiple CardModels.");
                var items = _mapper.Map<List<CardModelRequestDso>>(models);
                var createdEntities = await _cardmodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<CardModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple CardModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing CardModel.
        [HttpPut(Name = "UpdateCardModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardModelOutputVM>> Update([FromBody] CardModelUpdateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid data in Update.");
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Update: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Updating CardModel with ID: {id}", model?.Id);
                var item = _mapper.Map<CardModelRequestDso>(model);
                var updatedEntity = await _cardmodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("CardModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<CardModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating CardModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a CardModel.
        [HttpDelete("{id}", Name = "DeleteCardModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CardModel ID received in Delete.");
                return BadRequest("Invalid CardModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting CardModel with ID: {id}", id);
                await _cardmodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CardModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of CardModels.
        [HttpGet("CountCardModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting CardModels...");
                var count = await _cardmodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting CardModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}